using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact : System.Web.UI.Page
{
    string mac;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
            int cust = Convert.ToInt16(custId.Value);
            SqlCommand cmd6 = new SqlCommand("select sum(prodQty) from cart where custId like '" + custId.Value + "'", con);
            int prodQty = Convert.ToInt16(cmd6.ExecuteScalar());
            SqlCommand cmd7 = new SqlCommand("select sum(prodQty*price) from cart where custId like '" + custId.Value + "'", con);
            double amtDue = Convert.ToDouble(cmd7.ExecuteScalar());
            itemCount.Text = prodQty.ToString();
            amt.Text = amtDue.ToString();

            con.Close();
        }
        catch (Exception a) { Response.Redirect("index.aspx"); }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {


            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("mayesfashion@gmail.com");

            mailMessage.From = new MailAddress("mayesfashion@gmail.com", "Mayes Fashion Mailer");
            mailMessage.Subject = "New Message at Mayes.fr";
            mailMessage.Body = "New Message at Mayes.fr. Following are the details of the person:\n\nName: " + names.Text + "\nEmail: " + email.Text + "\nSubject: " + subject.Text + "\nMessage: " + msg.Text + "\n";
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "ENGLISH")
        { Response.Redirect("/index.aspx"); }
        else
        { Response.Redirect("/french/index.aspx"); }
    }
}