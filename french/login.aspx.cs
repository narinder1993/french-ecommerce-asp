using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    string mac;
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
        var cust = Convert.ToInt16(custId.Value);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("select count(email) from customer where email like '"+email.Text.ToString()+"' and password like '"+password.Text.ToString()+"'", con);
        int count=Convert.ToInt16(cmd.ExecuteScalar());
        if (count == 1)
        {
            SqlCommand cmd1 = new SqlCommand("delete from customer where mac_address like '" + mac.ToString() + "' and fname like 'Guest User'", con);
            
            SqlCommand cmd3 = new SqlCommand("update customer set mac_address=null where mac_address like '"+mac+"'", con);
            SqlCommand cmd2 = new SqlCommand("update customer set mac_address='"+mac+"', last_login=CURRENT_TIMESTAMP where email like '"+email.Text.ToString()+"' and password like '"+password.Text.ToString()+"'", con);
            cmd1.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd2.ExecuteScalar();
            SqlCommand cmd10 = new SqlCommand("update cart set email='"+email.Text+"' where mac  like '"+mac+"'", con);
            cmd10.ExecuteScalar();
            SqlCommand cmd4 = new SqlCommand("update cart set mac='"+mac+"' where email like '"+email.ToString()+"'", con);
            cmd4.ExecuteScalar();
            Response.Write("<script>alert('Login Successful.'); window.location.href=\"index.aspx\"</script>");

        }
        else { Response.Write("<script>alert('Invalid email/password')</script>"); }
        con.Close();
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
    public static void loginFb(string name, string email)
    {
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();

        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string mac = nics[0].GetPhysicalAddress().ToString();

        SqlCommand cmd = new SqlCommand("select count(email) from customer where email like '" + email + "' and fname like '" + name + "'", con);
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        if (count>0)
        {
            SqlCommand cmd1 = new SqlCommand("delete from customer where mac_address like '" + mac.ToString() + "' and fname like 'Guest User'", con);
            SqlCommand cmd3 = new SqlCommand("update customer set mac_address=null where mac_address like '" + mac + "'", con);
            SqlCommand cmd2 = new SqlCommand("update customer set mac_address='" + mac + "', last_login=CURRENT_TIMESTAMP where email like '" + email + "' and fname like '" + name + "'", con);
            cmd1.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd2.ExecuteScalar();
            SqlCommand cmd10 = new SqlCommand("update cart set email='" + email + "' where mac  like '" + mac + "'", con);
            cmd10.ExecuteScalar();
            SqlCommand cmd4 = new SqlCommand("update cart set mac='" + mac + "' where email like '" + email.ToString() + "'", con);
            cmd4.ExecuteScalar();
        }
        else
        {
            SqlCommand cmd1 = new SqlCommand("delete from customer where mac_address like '" + mac.ToString() + "' and fname like 'Guest User'", con);
            SqlCommand cmd3 = new SqlCommand("update customer set mac_address=null where mac_address like '" + mac + "'", con);
            SqlCommand cmd2 = new SqlCommand("insert into customer (email, fname, last_login, mac_address) values ('" + email + "','" + name + "', CURRENT_TIMESTAMP, '"+mac+"')", con);
            SqlCommand cmd4 = new SqlCommand("select Id from customer where mac_address like '" + mac + "'",con);
            cmd1.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd2.ExecuteScalar();
            SqlCommand cmd10 = new SqlCommand("update cart set email='" + email + "' where mac  like '" + mac + "'", con);
            cmd10.ExecuteScalar();
            string custid=cmd4.ExecuteScalar().ToString();
            SqlCommand cmd5 = new SqlCommand("insert into cart(email, mac, custId, prodQty, price) values ('"+email+"', '"+mac+"', '"+custid+"',0,0)", con);
            cmd5.ExecuteScalar();
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