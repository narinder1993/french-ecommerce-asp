using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    string mac;
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
        
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
        con.ConnectionString = ConfigurationManager.ConnectionStrings["mayeDb"].ConnectionString;
    
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        mac = nics[0].GetPhysicalAddress().ToString();
        if (con.State == System.Data.ConnectionState.Closed)
        con.Open();
        SqlCommand cmd = new SqlCommand("select count(id) from customer where mac_address like '"+mac+"'", con);
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
       int  cust = Convert.ToInt16(custId.Value);
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
        if (con.State == System.Data.ConnectionState.Closed)
        con.Open();
        SqlCommand cmd2 = new SqlCommand("select count(email) from customer where email like '"+TextBox1.Text.ToString()+"'",con);
        int count = Convert.ToInt16(cmd2.ExecuteScalar());
        if (count == 0)
        {
            SqlCommand cmd1 = new SqlCommand("delete from customer where mac_address like '" + mac.ToString() + "' and fname like 'Guest User'", con);
            SqlCommand cmd = new SqlCommand("Insert into customer (fname, lname, email, password, mac_address, last_login) values ('" + TextBox3.Text.ToString() + "', '" + TextBox2.Text.ToString() + "','" + TextBox1.Text.ToString() + "', '" + TextBox4.Text.ToString() + "', '"+mac.ToString()+"', CURRENT_TIMESTAMP) ", con);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd4 = new SqlCommand("update cart set email='"+TextBox1.Text.ToString()+"',  custId=(select top 1 Id from customer where mac like '"+mac.ToString()+"' order by last_login desc) where mac like '"+mac+"'",con);
            cmd4.ExecuteScalar();
            cmd.ExecuteNonQuery();
            Response.Redirect("Index.aspx");
        }
        else
        {
            Response.Write("<script language=javascript>alert('This email is already registered with Mayes.com.')</script>");
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