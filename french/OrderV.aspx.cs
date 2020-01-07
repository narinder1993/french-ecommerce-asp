using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderV : System.Web.UI.Page
{
    public string mac;
    int cust;
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try{
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
            SqlCommand cmd5 = new SqlCommand("select count(custId) from customer where Id like '" + custId.Value + "'", con);
            int countId = Convert.ToInt16(cmd5.ExecuteScalar());
            if (countId == 0)
            {
                SqlCommand cmd4 = new SqlCommand("insert into cart (custId, mac_address, prodQty,price) values ('" + custId.Value + "', '" + mac + "', 0,0", con);
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
        if (name.Value.ToString().Equals("Guest User ")) { Response.Redirect("orderI.aspx"); }
        if (prodQty == 0)
        {
            Response.Redirect("index.aspx");
        }
        SqlCommand cmd50 = new SqlCommand("select count(shipping) from cart where custId like '" + custId.ToString() + "' and productId is not null and shipping=0", con);
        int a = Convert.ToInt16(cmd50.ExecuteScalar());
        if (a > 0)
        {
            Response.Redirect("orderIII.aspx");
        }
        //Orders
        SqlCommand cmd14 = new SqlCommand("insert into orders (orderDate, deliveryDate, custId, prodId, prodQty, size, shipping, prodPrice) select CONVERT(date, GETDATE()), (GETDATE()+1), custId, productId, prodQty, size, shipping, price from cart where custId like '" + custId.Value.ToString() + "'", con);
        cmd14.ExecuteNonQuery();
        mail();
        SqlCommand cmd15 = new SqlCommand("delete from cart where custId like '" + custId.Value.ToString() + "'", con);
        cmd15.ExecuteScalar();

        //Orders
        con.Close();
        //}
        //catch (Exception a) { Response.Redirect("index.aspx"); }
    }
    public void mail()
    {
        try
        {


            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("npsingh1993@gmail.com");

            mailMessage.From = new MailAddress("mayesfashion@gmail.com", "Mayes Fashion Mailer");
            mailMessage.Subject = "New Order au Mayes.fr";
            SqlCommand cmd10 = new SqlCommand("select a.productId, a.size, a.prodQty, a.price, a.shipping, b.nameFrn, GETDATE(), b.image1 from cart a join products b  on a.productId =b.Id where a.custId like '" + custId.Value + "' ", con);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            da.SelectCommand = cmd10;
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append("Félicitations à vous!! Vous avez réussi à placer votre commande à Mayes.fr \nFollowing sont les détails de la commande:");
            float price = 0;
            foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                price += (Convert.ToInt16(dr[3]) * Convert.ToInt16(dr[2]));
                cstext2.Append("\n\nID de produit: " + dr[0].ToString() + "\nNom du produit: " + dr[5].ToString() + "\nTaille du produit: " + dr[1].ToString() + "\nLa quantité de produit: " + dr[2].ToString() + "\nPrix du produit: " + dr[3].ToString() + "\nFrais d'expédition: " + dr[4].ToString() + "\nDate de commande: " + dr[6].ToString() + "");
            }
            SqlCommand cmd = new SqlCommand("select concat(fname,' ',lname), email, mobile, address, city, state, zipcode from customer where id like '" + custId.Value + "'", con);
            System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter();
            da1.SelectCommand = cmd;
            System.Data.DataSet ds1 = new System.Data.DataSet();
            da1.Fill(ds1);
            foreach (System.Data.DataRow dr in ds1.Tables[0].Rows)
            {

                mailMessage.To.Add(dr[1].ToString());
                cstext2.Append("\n\n\n INFORMATIONS DE LIVRAISON\n\nNom: " + dr[0].ToString() + "\nEmail: " + dr[1].ToString() + "\nMobile: " + dr[2].ToString() + "\nAdresse: " + dr[3].ToString() + "\nVille: " + dr[4].ToString() + "\nState: " + dr[5].ToString() + "\nZip Code: " + dr[6].ToString() + "\n\n\nMerci de magasiner avec nous.\nSi vous avez des questions, s'il vous plaît contactez-nous au maye.collection@hotmail.com\n\nWith regards\n\nMaye Collections");
            }
            mailMessage.Body = cstext2.ToString();
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential("mayesfashion@gmail.com", "websitelogin");
            client.EnableSsl = true;
            client.Send(mailMessage);


        }
        catch (Exception ex)
        {
            Response.Write("Could not send the e-mail - error: " + ex.Message);
        }
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

}