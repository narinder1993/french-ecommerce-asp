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

public partial class products : System.Web.UI.Page
{
    int page = 1;
     
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
                 con.Close();
                 var cat = "";
                 var page = "";
                 try
                 {
                    

                     con = new System.Data.SqlClient.SqlConnection();
                     con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
                     if (con.State == System.Data.ConnectionState.Closed)
                         con.Open();
                     StringBuilder cstext2 = new StringBuilder();
                     cstext2.Clear();
                     cstext2.Append("<script type='text/javascript'> var newInput; window.onload=function OnLoad() {");
                     cstext2.Append("document.getElementById('products').innerHTML ='';");
                     System.Data.SqlClient.SqlCommand cmd8 = new System.Data.SqlClient.SqlCommand(";with A aS(SELECT  id, nameFrn, ctgEng, descEng, price, image1 isAvlbl, sizes,ROW_NUMBER() OVER (ORDER BY Id) AS 'RN' from products where isFeatured =1 )	select * from A order by newid()", con);
                     System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
                     da.SelectCommand = cmd8;
                     System.Data.DataSet ds = new System.Data.DataSet();
                     da.Fill(ds);

                     

                     int x = 0;
                     cstext2.Append("newInput=\"");
                     foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                     {
                         //product

                         if (x % 3 == 0) { cstext2.Append("<div class=' bottom-product'>"); }
                         cstext2.Append("<div class='col-md-4 bottom-cd simpleCart_shelfItem'>");
                         cstext2.Append("<div class='product-at '>");
                         cstext2.Append("<a href='single.aspx?id=" + dr[0].ToString() + "'><img class='img-responsive' style='width:254.984px; height:403.453px' src='" + dr[5].ToString() + "'>");
                         cstext2.Append("<div class='pro-grid' style='margin-top:70px'>");
                         cstext2.Append("<span class='buy-in'  href='single.aspx?id=" + dr[0].ToString() + "'>Acheter Maintenant</span>");
                         cstext2.Append("</div>");
                         cstext2.Append("</a>	");
                         cstext2.Append("</div>");
                         cstext2.Append("<p style='height:71px' class='tun'>" + dr[1].ToString() + "</p>");
                         cstext2.Append("<a href='single.aspx?id=" + dr[0].ToString() + "' class='item_add'><p class='number item_price'><i> </i>&euro;" + dr[4].ToString() + "</p></a>						");
                         cstext2.Append("<br><br></div>");
                         if ((x + 1) % 3 == 0) { cstext2.Append("<div>"); }

                         x++;
                         //productEnd
                     } cstext2.Append("\";");
                     cstext2.Append(" document.getElementById('products').innerHTML += newInput;");

                     cstext2.Append("var newInput1=\"<div class='of-left-in'><h3 class='best'>Best Sellers</h3></div>");
                     System.Data.SqlClient.SqlCommand cmd9 = new System.Data.SqlClient.SqlCommand("select top 2 id, nameFrn, price, image1 from products order by newid() ", con);
                     System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter();
                     da1.SelectCommand = cmd9;
                     System.Data.DataSet ds1 = new System.Data.DataSet();
                     da1.Fill(ds1);
                     foreach (System.Data.DataRow dr in ds1.Tables[0].Rows)
                     {
                         cstext2.Append("<div class='product-go'><div class=' fashion-grid'><a href='single.aspx?id="+dr[0]+"'><img class='img-responsive' src='"+dr[3].ToString()+"' ></a></div>");
                         cstext2.Append("<div class='fashion-grid1'><h6 class='best2'><a href='single.aspx?id=" + dr[0] + "' >"+dr[1].ToString()+"</a></h6>");
                         cstext2.Append("<span class=' price-in1'> &euro;"+dr[2]+"</span></div><div class='clearfix'> </div></div>");
                     }
                     cstext2.Append("\";");
                     cstext2.Append(" document.getElementById('ticker').innerHTML = newInput1;");
                     

                     cstext2.Append("}<");
                     cstext2.Append("/script>");
                     Response.Write(cstext2.ToString());
                 }
                 catch (Exception a) { }
             }
             catch (Exception a)
             { Response.Redirect("index.aspx"); }
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
        public static string cartAdd(string id1)
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

            SqlCommand cmd = new SqlCommand("insert into cart (email, productId, mac,   custId, price) values ('" + email + "'," + id + ", '" + mac + "',   '" + custId + "', (select price from products where Id =" + id + "))", con);
            cmd.ExecuteNonQuery();
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
  