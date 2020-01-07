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

public partial class checkout : System.Web.UI.Page
{
    public string mac;
    int cust;
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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

            //checkout prods//
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Clear();
            SqlCommand cmd10 = new SqlCommand("select CONCAT('''',a.productId,''''), a.size, a.prodQty, a.price, b.nameFrn, b.image1 from cart a join products b  on a.productId =b.Id where a.custId like '" + custId.Value + "' ", con);
            cmd10.ExecuteNonQuery();
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            da.SelectCommand = cmd10;
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            int x = 0;
            cstext2.Append("<script type='text/javascript'> var newInput; window.onload=function OnLoad() {");
            cstext2.Append("document.getElementById('products').innerHTML ='';");
            cstext2.Append("newInput=\"");
            double price = 0;
            foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                x++;
                price += (Convert.ToDouble(dr[3]) * Convert.ToDouble(dr[2]));
                cstext2.Append("<div class='cart-header'>");
                cstext2.Append("<div class='close1' onclick= delet(" + dr[0].ToString() + "," + dr[2] + ")> </div>");
                cstext2.Append("<div class='cart-sec simpleCart_shelfItem'>");
                cstext2.Append("<div class='cart-item cyc'>");
                cstext2.Append("	 <img src='" + dr[5].ToString() + "' class='img-responsive' />");
                cstext2.Append("</div>");
                cstext2.Append(" <div class='cart-item-info'>");
                cstext2.Append("<h3><a href='#'>" + dr[4].ToString() + "</a></h3>");
                cstext2.Append("<ul class='qty'>");
                cstext2.Append("	<li><p>Size : " + dr[1].ToString() + "</p></li>");
                cstext2.Append("	<li><p>Qty : " + dr[2].ToString() + "</p></li>");
                cstext2.Append("</ul>");
                cstext2.Append(" <div class='delivery'>");
                cstext2.Append(" <p>Prix : &euro;" + dr[3].ToString() + "</p>");
                //cstext2.Append(" <span>Livré en 2-3 jours ouvrables</span>");
                cstext2.Append(" <div class='clearfix'></div>");
                cstext2.Append("  </div>	");
                cstext2.Append(" </div>");
                cstext2.Append("  <div class='clearfix'></div>");
                cstext2.Append(" </div>");
                cstext2.Append("</div>");
            }
            cstext2.Append("\";");
            //details
            SqlCommand cmd11 = new SqlCommand("select a.prodQty, a.price, b.nameFrn from cart a join products b  on a.productId =b.Id where a.custId like '" + custId.Value + "' ", con);
            cmd10.ExecuteNonQuery();
            System.Data.SqlClient.SqlDataAdapter da4 = new System.Data.SqlClient.SqlDataAdapter();
            da4.SelectCommand = cmd11;
            System.Data.DataSet ds4 = new System.Data.DataSet();
            da4.Fill(ds4);
           // int x = 0;
             cstext2.Append("document.getElementById('details').innerHTML =\"");
            //cstext2.Append("newInput=\"");
            //double price = 0;
            foreach (System.Data.DataRow dr in ds4.Tables[0].Rows)
            {
                cstext2.Append("<b>" + dr[2].ToString() + "</b><br>Quantité: <b>" + dr[0].ToString() + "</b><br>Prix: <b>" + dr[1].ToString() + "</b><br><br>");
            }
            cstext2.Append("\";");
            //details

            cstext2.Append(" document.getElementById('products').innerHTML += newInput;");
            cstext2.Append(" document.getElementById('count').innerHTML = 'Votre Panier Provisoire (" + x + ")';");
            cstext2.Append(" document.getElementById('total1').innerHTML = '&euro; " + price + "';");
            cstext2.Append(" document.getElementById('total2').innerHTML = '&euro; " + price + "';");
            if (name.Value.Equals("Guest User "))
                cstext2.Append(" document.getElementById('orderButton').innerHTML = \"<a class='order' href='orderI.aspx'>Passer la commande</a>\";");
            else
                cstext2.Append(" document.getElementById('orderButton').innerHTML = \"<a class='order' href='orderII.aspx'>Passer la commande</a>\";");
            cstext2.Append("}<");
            cstext2.Append("/script>");
            Response.Write(cstext2.ToString());
            //checkout prods//
            con.Close();

        }
        catch (Exception a)
        { }
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
    public static void cartDel(string id, string qty)
    {
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string mac = nics[0].GetPhysicalAddress().ToString();
        SqlCommand cmd = new SqlCommand("delete top (1) from cart where productId like '" + id + "' and prodQty like '" + qty + "' and mac like '" + mac + "'", con);
        cmd.ExecuteNonQuery();
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