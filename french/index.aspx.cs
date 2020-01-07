using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    public string mac;
    int cust;
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        // try{
        DateTime thisDay = DateTime.Today;
        CultureInfo ci = new CultureInfo("fr-FR");
        date.Text = thisDay.ToString("D",ci);
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
            name.Value = nameLog.ToString();
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
            name.Value = "Guest User";
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
        cust = Convert.ToInt16(custId.Value);
        SqlCommand cmd6 = new SqlCommand("select sum(prodQty) from cart where custId like '" + custId.Value + "'", con);
        int prodQty = Convert.ToInt16(cmd6.ExecuteScalar());
        SqlCommand cmd7 = new SqlCommand("select sum(prodQty*price) from cart where custId like '" + custId.Value + "'", con);
        double amtDue = Convert.ToDouble(cmd7.ExecuteScalar());
        itemCount.Text = prodQty.ToString();
        amt.Text = amtDue.ToString();
        SqlCommand cmd10 = new SqlCommand("select top 3 id, nameFrn, image1, price from products where ctgEng like 'robes' and image1 not like 'images/img/no.jpg' and isFeatured like '1' order by newid() ", con);
        System.Data.SqlClient.SqlDataAdapter da5 = new System.Data.SqlClient.SqlDataAdapter();
        da5.SelectCommand = cmd10;
        System.Data.DataSet ds3 = new System.Data.DataSet();
        da5.Fill(ds3);
        StringBuilder cstext2 = new StringBuilder();
        cstext2.Clear();
        cstext2.Append("<script type='text/javascript'> var newInput; window.onload=function OnLoad() {");
        cstext2.Append("document.getElementById('robeStrip').innerHTML='");
        foreach (System.Data.DataRow dr in ds3.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"col-md-4 grid-top\">");
            cstext2.Append("<a href=\"single.aspx?id=" + dr[0] + "\" class=\"b-link-stripe b-animate-go  thickbox\"><img class=\"img-responsive\" src=\"" + dr[2].ToString() + "\" alt=\"\">");
            cstext2.Append("<div class=\"b-wrapper\">");
            cstext2.Append("<h3 class=\"b-animate b-from-left    b-delay03 \" style=\"min-height:475px\">");
            cstext2.Append("<span><br /><br />ROBES SPECIAL: <br>ACHETER MAINTENANT</span>	");
            cstext2.Append("</h3>");
            cstext2.Append("</div>");
            cstext2.Append("</a>");
            cstext2.Append("<p><a href=\"single.aspx?id=" + dr[0] + "\">" + dr[1].ToString() + " &euro;" + dr[3].ToString() + "</a></p>");
            cstext2.Append("</div>");
        }
        cstext2.Append("<div class=\"clearfix\"> </div>';");

        SqlCommand cmd11 = new SqlCommand("select top 3 id, nameFrn, image1, price from products where ctgEng like 'vest' and image1 not like 'images/img/no.jpg'  and isFeatured like '1' order by newid()", con);
        System.Data.SqlClient.SqlDataAdapter da6 = new System.Data.SqlClient.SqlDataAdapter();
        da6.SelectCommand = cmd11;
        System.Data.DataSet ds4 = new System.Data.DataSet();
        da6.Fill(ds4);
        cstext2.Append("document.getElementById('vestStrip').innerHTML='");

        foreach (System.Data.DataRow dr in ds4.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"col-md-4 grid-top\">");
            cstext2.Append("<a href=\"single.aspx?id=" + dr[0] + "\" class=\"b-link-stripe b-animate-go  thickbox\"><img class=\"img-responsive\" src=\"" + dr[2].ToString() + "\" alt=\"\">");
            cstext2.Append("<div class=\"b-wrapper\">");
            cstext2.Append("<h3 class=\"b-animate b-from-left    b-delay03 \" style=\"min-height:475px\">");
            cstext2.Append("<span><br /><br />VESTS SPECIAL: <br>ACHETER MAINTENANT</span>	");
            cstext2.Append("</h3>");
            cstext2.Append("</div>");
            cstext2.Append("</a>");
            cstext2.Append("<p><a href=\"single.aspx?id=" + dr[0] + "\">" + dr[1].ToString() + " &euro;" + dr[3].ToString() + "</a></p>");
            cstext2.Append("</div>");
        }
        cstext2.Append("<div class=\"clearfix\"> </div>';");

        SqlCommand cmd12 = new SqlCommand("select top 3 id, nameFrn, image1, price from products where ctgEng like 'jean' and image1 not like 'images/img/no.jpg'  and isFeatured like '1' order by newid()", con);
        System.Data.SqlClient.SqlDataAdapter da7 = new System.Data.SqlClient.SqlDataAdapter();
        da7.SelectCommand = cmd12;
        System.Data.DataSet ds5 = new System.Data.DataSet();
        da7.Fill(ds5);
        cstext2.Append("document.getElementById('jeanStrip').innerHTML='");

        foreach (System.Data.DataRow dr in ds5.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"col-md-4 grid-top\">");
            cstext2.Append("<a href=\"single.aspx?id=" + dr[0] + "\" class=\"b-link-stripe b-animate-go  thickbox\"><img class=\"img-responsive\" src=\"" + dr[2].ToString() + "\" alt=\"\">");
            cstext2.Append("<div class=\"b-wrapper\">");
            cstext2.Append("<h3 class=\"b-animate b-from-left    b-delay03 \" style=\"min-height:475px\">");
            cstext2.Append("<span><br /><br />JEANS SPECIAL: <br>ACHETER MAINTENANT</span>	");
            cstext2.Append("</h3>");
            cstext2.Append("</div>");
            cstext2.Append("</a>");
            cstext2.Append("<p><a href=\"single.aspx?id=" + dr[0] + "\">" + dr[1].ToString() + " &euro;" + dr[3].ToString() + "</a></p>");
            cstext2.Append("</div>");
        }
        cstext2.Append("<div class=\"clearfix\"> </div>';");

        SqlCommand cmd13 = new SqlCommand("select top 3 id, nameFrn, image1, price from products where ctgEng like 'top' and image1 not like 'images/img/no.jpg' and isFeatured like '1'  order by newid()", con);
        System.Data.SqlClient.SqlDataAdapter da8 = new System.Data.SqlClient.SqlDataAdapter();
        da8.SelectCommand = cmd13;
        System.Data.DataSet ds6 = new System.Data.DataSet();
        da8.Fill(ds6);
        cstext2.Append("document.getElementById('topStrip').innerHTML='");

        foreach (System.Data.DataRow dr in ds6.Tables[0].Rows)
        {
            cstext2.Append("<div class=\"col-md-4 grid-top\">");
            cstext2.Append("<a href=\"single.aspx?id=" + dr[0] + "\" class=\"b-link-stripe b-animate-go  thickbox\"><img class=\"img-responsive\" src=\"" + dr[2].ToString() + "\" alt=\"\">");
            cstext2.Append("<div class=\"b-wrapper\">");
            cstext2.Append("<h3 class=\"b-animate b-from-left    b-delay03 \" style=\"min-height:475px\">");
            cstext2.Append("<span><br /><br />TOPS SPECIAL: <br>ACHETER MAINTENANT</span>	");
            cstext2.Append("</h3>");
            cstext2.Append("</div>");
            cstext2.Append("</a>");
            cstext2.Append("<p><a href=\"single.aspx?id=" + dr[0] + "\">" + dr[1].ToString() + " &euro;" + dr[3].ToString() + "</a></p>");
            cstext2.Append("</div>");
        }
        cstext2.Append("<div class=\"clearfix\"> </div>';");
        cstext2.Append(" }</script>");
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "ENGLISH")
        { Response.Redirect("/index.aspx"); }
        else
        { Response.Redirect("/french/index.aspx"); }
    }
}