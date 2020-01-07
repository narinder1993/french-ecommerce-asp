using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class single : System.Web.UI.Page
{
    String id;
    protected void Page_Load(object sender, EventArgs e)
    {
        // try{
        string mac;
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();

        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        mac = nics[0].GetPhysicalAddress().ToString();
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("select count(id) from customer where mac_address like '" + mac + "'", con);
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        macadd.Value = mac.ToString();

        if (count > 0)
        {
            SqlCommand cmd1 = new SqlCommand("select top 1 concat(fname,' ',lname) from customer where mac_address like '" + mac + "' order by last_login desc", con);
            SqlCommand cmd2 = new SqlCommand("select top 1 id from customer where mac_address like '" + mac + "' order by last_login desc", con);

            String nameLog = cmd1.ExecuteScalar().ToString();
            custId.Value = cmd2.ExecuteScalar().ToString();
            name1.Value = nameLog.ToString();
            SqlCommand cmd5 = new SqlCommand("select count(custId) from cart where custId like '" + custId.Value + "'", con);
            int countId = Convert.ToInt16(cmd5.ExecuteScalar());
            if (countId == 0)
            {
                SqlCommand cmd4 = new SqlCommand("insert into cart (custId, mac , prodQty,price) values ('" + custId.Value + "', '" + mac + "', 0,0)", con);
                cmd4.ExecuteScalar();
            }
        }
        else
        {
           name1.Value = "Guest User";
            SqlCommand cmd3 = new SqlCommand("insert into customer (fname, mac_address, last_login) values ('Guest User', '" + mac + "', CURRENT_TIMESTAMP)", con);
            SqlCommand cmd2 = new SqlCommand("select top 1 id from customer where mac_address like '" + mac + "' order by last_login desc", con);
            cmd3.ExecuteNonQuery();

            custId.Value = cmd2.ExecuteScalar().ToString();
            SqlCommand cmd5 = new SqlCommand("select count(Id) from customer where Id like '" + custId.Value + "'", con);
            int countId = Convert.ToInt16(cmd5.ExecuteScalar());
            if (countId == 1)
            {
                SqlCommand cmd4 = new SqlCommand("insert into cart (custId, mac, prodQty,price) values ('" + custId.Value + "', '" + mac + "', 0,0)", con);
                cmd4.ExecuteScalar();
            }
        }
        int cust = Convert.ToInt16(custId.Value);
        SqlCommand cmd6 = new SqlCommand("select sum(prodQty) from cart where custId like '" + custId.Value + "'", con);
        int prodQty = Convert.ToInt16(cmd6.ExecuteScalar());
        SqlCommand cmd7 = new SqlCommand("select sum(prodQty*price) from cart where custId like '" + custId.Value + "'", con);
        double amtDue = Convert.ToDouble(cmd7.ExecuteScalar());
        itemCount.Text = prodQty.ToString();
        amt.Text = amtDue.ToString();

        con.Close();

        var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
        var arrQueryStrings = queryStrings.Split('=');
        id = Convert.ToString(arrQueryStrings[1]);
        con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        SqlCommand cmd8 = new SqlCommand("select nameFrn, price, descFrn, sizes from products where Id like '" + id + "'", con);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
        da.SelectCommand = cmd8;
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        StringBuilder cstext2 = new StringBuilder();
        cstext2.Clear();

        cstext2.Append("<script type='text/javascript'> var newInput; window.onload=function OnLoad() {");
        foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
        {
            cstext2.Append("document.getElementById('name').innerHTML='" + dr[0].ToString() + "';");
            cstext2.Append("document.getElementById('price').innerHTML+='" + dr[1].ToString() + "';");
            cstext2.Append("document.getElementById('description').innerHTML='" + dr[2].ToString() + "';");
            cstext2.Append("document.getElementById('button').innerHTML='<a  onclick=\\'cart(\"" + id + "\", " + dr[1].ToString() + ", " + qty.Text + ")\\' style=\\'height:53px; font-weight:bolder; border-style:groove; width:502px; text-align:center\\' class=\\'add-cart item_add\\'><br />AJOUTER AU PANIER</a>';");
            string size = dr[3].ToString();
            var sizeString = size.Split(',');
            for (int i = 0; i < sizeString.Length; i++)
            {
                cstext2.Append("document.getElementById('size').innerHTML+='<option>" + sizeString[i].ToString() + "</option>';");
            }
            cstext2.Append("document.getElementById('description2').innerHTML='" + dr[2].ToString() + "';");

            SqlCommand cmd1 = new SqlCommand("select image1 from products where Id like '" + id + "'", con);
            SqlCommand cmd2 = new SqlCommand("select image2 from products where Id like '" + id + "'", con);
            SqlCommand cmd3 = new SqlCommand("select image3 from products where Id like '" + id + "'", con);
            SqlCommand cmd4 = new SqlCommand("select image4 from products where Id like '" + id + "'", con);
            string image1 = cmd1.ExecuteScalar().ToString();
            string image2 = cmd2.ExecuteScalar().ToString();
            string image3 = cmd3.ExecuteScalar().ToString();
            string image4 = cmd4.ExecuteScalar().ToString();
            if (!image1.Equals(""))
                cstext2.Append("document.getElementById('slideshow').innerHTML=\"<li  style='height:600px' data-thumb='" + image1 + "'><img style='height:600px' src='" + image1 + "' ></li>\";");
            if (!image2.Equals(""))
                cstext2.Append("document.getElementById('slideshow').innerHTML+=\"<li  style='height:600px' data-thumb='" + image2 + "'><img style='height:600px' src='" + image2 + "' ></li>\";");
            if (!image3.Equals(""))
                cstext2.Append("document.getElementById('slideshow').innerHTML+=\"<li  style='height:600px' data-thumb='" + image3 + "'><img style='height:600px' src='" + image3 + "' ></li>\";");
            if (!image4.Equals(""))
                cstext2.Append("document.getElementById('slideshow').innerHTML+=\"<li  style='height:600px' data-thumb='" + image4 + "'><img style='height:600px' src='" + image4 + "' ></li>\";");
        }

        SqlCommand cmd12 = new SqlCommand("select top 3 id, nameFrn, ctgEng, price, image1 from  products  where id not like " + id + " order by newid()", con);
        System.Data.SqlClient.SqlDataAdapter da5 = new System.Data.SqlClient.SqlDataAdapter();
        da5.SelectCommand = cmd12;
        System.Data.DataSet ds3 = new System.Data.DataSet();
        da5.Fill(ds3);
        cstext2.Append("document.getElementById('suggestions').innerHTML='");

        foreach (System.Data.DataRow dr in ds3.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"col-md-4 bottom-cd simpleCart_shelfItem\">");
            cstext2.Append("<div class=\"product-at \"><center>");
            cstext2.Append("<a href=\"single.aspx?id=" + dr[0] + "\"><img class=\"img-responsive\" style=\"width:254.984px; height:403.453px\" src=\"" + dr[4].ToString() + "\" alt=\"\">");
            cstext2.Append("<div class=\"pro-grid\" style=\"margin-top:70px\">");
            cstext2.Append("<span href=\"single.aspx?id=" + dr[0] + "\" class=\"buy-in\">Buy Now</span>");
            cstext2.Append("</div>");
            cstext2.Append("</a></center>");
            cstext2.Append("</div>");
            cstext2.Append("<p style=\"height:71px\" class=\"tun\">" + dr[1].ToString() + "</p>");
            cstext2.Append("<a  href=\"single.aspx?id=" + dr[0] + "\" class=\"item_add\"><p class=\"number item_price\"><i> </i>&euro;" + dr[3].ToString() + "</p></a>	");
            cstext2.Append("</div>");
        }
        cstext2.Append("';");
        //color
        SqlCommand cmd120 = new SqlCommand("select color from products where id like '" + id + "'", con);
        string color = cmd120.ExecuteScalar().ToString();
        var id1 = id.Split('-');
        if (!color.Equals("0"))
        {
            // cstext2.Append("document.getElementById('color').innerHTML+=\"<li class='size-in' id='colorDrop'>Color<select id='color'>\";");
            var colorString = color.Split(',');

            for (int z = 0; z < colorString.Length; z++)
            {
                cstext2.Append("document.getElementById('color').innerHTML+='<option value=\\'single.aspx?id=" + id1[0] + "-" + (z + 1) + "\\'>" + colorString[z].ToString() + "</option>';");
            }
            cstext2.Append("document.getElementById('color').innerHTML+=\"</select></li>\";");
        }
        else
        {
            cstext2.Append("document.getElementById('colorDrop').style.display = 'none';");
        }
        //color

        SqlCommand cmd15 = new SqlCommand("select custName, review, date from review where productId like '" + id + "'", con);
        System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter();
        da1.SelectCommand = cmd15;
        System.Data.DataSet ds1 = new System.Data.DataSet();
        da1.Fill(ds1);
        cstext2.Append("document.getElementById('reviewpanel').innerHTML='");
        foreach (System.Data.DataRow dr in ds1.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"top-comment-left\">");
            cstext2.Append("<img class=\"img-responsive\" src=\"images/co.png\" >");
            cstext2.Append("</div>");
            cstext2.Append("<div class=\"top-comment-right\">");
            cstext2.Append("<h6><a >" + dr[0].ToString() + "</a> - " + dr[2].ToString() + "</h6>");
            cstext2.Append("<p>" + dr[1].ToString() + "</p>");
            cstext2.Append("</div>");
            cstext2.Append("<div class=\"clearfix\"> </div>");
        }
        cstext2.Append("';}</script>");
        Response.Write(cstext2.ToString());
        con.Close();
        // }
        //catch (Exception a) { Response.Redirect("index.aspx"); }
    }
    [System.Web.Services.WebMethod]
    public static string logout()
    {
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();

        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string mac = nics[0].GetPhysicalAddress().ToString();

        SqlCommand cmd1 = new SqlCommand("update customer set mac_address=null where mac_address like '" + mac + "'", con);
        cmd1.ExecuteNonQuery();
        SqlCommand cmd2 = new SqlCommand("update cart set mac=null where mac like '" + mac + "'", con);
        cmd2.ExecuteNonQuery();

        return "out";
    }
    [System.Web.Services.WebMethod]
    public static string cartAdd(string id1, string qty, string size)
    {
        var id = id1;
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string mac = nics[0].GetPhysicalAddress().ToString();
        SqlCommand cmd2 = new SqlCommand("select top 1 id from customer where mac_address like '" + mac + "' order by last_login desc", con);
        string custId = cmd2.ExecuteScalar().ToString();
        SqlCommand cmd3 = new SqlCommand("select top 1 email from customer where mac_address like '" + mac + "' order by last_login desc", con);
        string email = cmd3.ExecuteScalar().ToString();

        SqlCommand cmd = new SqlCommand("insert into cart (email, productId, mac, prodQty, custId, price, size) values ('" + email + "','" + id + "', '" + mac + "', " + qty + ", '" + custId + "', (select price from products where Id like '" + id + "'), '" + size + "')", con);
        cmd.ExecuteNonQuery();
        return "out";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        if (name1.Value.Equals("Guest User "))
        { Response.Write("<script>alert('Please login to enter a review')</script>"); }
        else
        {
            SqlCommand cmd = new SqlCommand("insert into review(productId, custName, review, date) values('" + id + "','" + name1.Value + "', '" + review.Text + "',CURRENT_TIMESTAMP)", con);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "ENGLISH")
        { Response.Redirect("/index.aspx"); }
        else
        { Response.Redirect("/french/index.aspx"); }
    }
}