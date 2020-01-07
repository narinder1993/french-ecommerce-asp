using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderIII : System.Web.UI.Page
{
    public string mac;
    int cust;
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
       // try{
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
        con.Close();
        //}
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

     
    protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (con.State == System.Data.ConnectionState.Closed)
            con.Open();
        SqlCommand cmd6 = new SqlCommand("select sum(prodQty) from cart where custId like '" + custId.Value + "'", con);
        int prodQty = Convert.ToInt16(cmd6.ExecuteScalar());
        
        if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 1)
        {
            double amt=prodQty * 8.93;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 2)
        {
             double amt = prodQty * 22.98;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 3)
        {
            double amt = prodQty * 41.93;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 4)
        {
             double amt = prodQty * 13.22;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 5)
        {
           double amt = prodQty * 15.93;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 6)
        {
             double amt = prodQty * 16.52;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 7)
        {
             double amt = prodQty * 19.37;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 8)
        {
            double amt = prodQty * 40.65;
            ship.Text = amt.ToString();
        }
        else if (Convert.ToInt16(RadioButtonList1.SelectedValue) == 9)
        {
             double amt = prodQty * 48.42;
            ship.Text = amt.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ship.Text.Equals("0"))
        { ship.Text = "Select your zone first."; }
        else
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("update cart set shipping="+ship.Text+" where custId like '"+custId.Value.ToString()+"'", con);
            cmd.ExecuteNonQuery();
            Response.Redirect("OrderIV.aspx");
        }
    }
}