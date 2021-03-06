﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>
<html>
<head>
<title>Register- Maye Store</title>
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.min.js"></script>
<!-- Custom Theme files -->
<!--theme-style-->
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />	
<!--//theme-style-->
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="New Store Responsive web template, Bootstrap Web Templates, Flat Web Templates, Andriod Compatible web template, 
Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!--fonts-->
<link href='//fonts.googleapis.com/css?family=Lato:100,300,400,700,900' rel='stylesheet' type='text/css'>
<link href='//fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900' rel='stylesheet' type='text/css'><!--//fonts-->
<!-- start menu -->
<link href="css/memenu.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="js/memenu.js"></script>
<script>$(document).ready(function () { $(".memenu").memenu(); });</script>
<script src="js/simpleCart.min.js"> </script>
</head><script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.5&appId=806639039448463";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
 <script type="text/javascript">
     window.fbAsyncInit = function () {
         FB.init({
             appId: '806639039448463',
             xfbml: true,
             version: 'v2.5'
         });

     }
     function logt() {
         FB.logout(function (response) {
             alert("logged out")
         });
     }
        function loader() {

            if (document.getElementById("name").value === "Guest User ") {
                document.getElementById("loginButton").innerHTML += '<p style="color:white">Welcome Guest User</p>';
            }
            else if (document.getElementById("name").value != "Guest User") {
                document.getElementById("loginButton").innerHTML = '<p style="margin-left:-11%; color:white">Welcome ' + document.getElementById("name").value + "<br><a onclick='logout()' onmouseover='' style='cursor: pointer; color:white; text-decoration:none; text-align:right'>Logout</a></p>";
            }
            if (document.getElementById("name").value != "Admin Maye") {
                document.getElementById("counter").style.display = "none";
            }
        }
        function logout() {
            logt();
            $.ajax
            ({
                type: "POST",
                url: "login.aspx/logout",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () { alert("Logout Successfull"); window.location.href = "/index.aspx" },
                failure: function (response) { alert(response.d); }
            });
        }
    </script>
<body onload="loader()">
<!--header-->
<div class="header">
	<div class="header-top">
		<div class="container">
			<div class="search" style="background-color:black"><form id="Form1" runat="server">
					<asp:hiddenfield id="Hiddenfield1" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="Hiddenfield2" runat="server"></asp:hiddenfield>
				 	<asp:hiddenfield id="custId" runat="server"></asp:hiddenfield>
                <asp:DropDownList AutoPostBack="true" class="btn btn-warning dropdown-toggle" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                     <asp:ListItem>LANGUAGE</asp:ListItem>    <asp:ListItem>ENGLISH</asp:ListItem>
                    <asp:ListItem>FRENCH</asp:ListItem>
                                 </asp:DropDownList>
                
			</div>
			<div class="header-left">		
					<ul id="loginButton">
						<li ><a href="login.aspx"  >Login</a></li>
						<li><a  href="register.aspx"  >Register</a></li>

					</ul>
					<div class="cart box_1">
						<a href="checkout.aspx">
						<h3> <div class="total">
							<span >&euro;<asp:Label ID="amt" runat="server" ></asp:Label> </span> (
                            <span >
                                <asp:Label ID="itemCount" runat="server"></asp:Label></span> items)</div>
							<img src="images/cart.png" alt=""/></h3>
						</a>
						
					</div>
					<div class="clearfix"> </div>
			</div>
				<div class="clearfix"> </div>
		</div>
		</div>
		<div class="container">
			<div class="head-top">
				<div class="logo">
					<a href="index.aspx"><img src="images/logo.png" alt=""></a>	
				</div>
		  <div class=" h_menu4">
				<ul class="memenu skyblue">
					  <li class="active grid"><a class="color8" href="index.aspx">Home</a></li>	
				      
				    <li class="grid"><a class="color2" href="#">	Categories</a>
					  	<div class="mepanel" style="width:200px; margin-left:20%"><center>
						<div class="row">
							<div class="col1">
								<div class="h_nav">
									<ul style="font-size:large; margin-left:20px"><center>
										<li><a href="category.aspx?category=vest&page=1" >Vests</a></li>
										<li><a href="category.aspx?category=jean&page=1">Jeans</a></li>
										<li><a href="category.aspx?category=robes&page=1">Robes</a></li>
										<li><a href="category.aspx?category=Top&page=1">Tops</a></li>
										
								</ul>	
								</div>						
							</div>
							
						  </div></center>
						</div>
			    </li><li class="active grid"><a class="color8" href="products.aspx">	Products</a></li>
				 <li><a class="color6" href="contact.aspx">Contact</a></li>
			  </ul> 
			</div>
				
				<div class="clearfix"> </div>
		</div>
		</div>

	</div>	
<!--content-->
<div class=" container">
<div class=" register">
    
	<h1>Register</h1>
		  	   
                    <asp:hiddenfield id="name" runat="server"></asp:hiddenfield>
                    <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
				 <div class="col-md-6 register-top-grid">
					<h3>Personal information</h3>
					 <div>
						<span>First Name <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox3" ErrorMessage="Please fill First Name" ForeColor="Red"></asp:RequiredFieldValidator>
                         </span>
						
						<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
					 </div>
					 <div>
						<span>
                         Last Name <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please fill Last Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
						<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox> 
					 </div>
					 <div>
						 <span>
                         Email Address<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please fill Email Address" ForeColor="Red"></asp:RequiredFieldValidator>
					 </span>
						 <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
					     <br />
                         </div>
					  
					 </div>
				     <div class="col-md-6 register-bottom-grid">
						    <h3>Login information</h3>
							 <div>
								<span>Password<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox4" ErrorMessage="Please set a password" ForeColor="Red"></asp:RequiredFieldValidator>
							 </span>
								<asp:TextBox ID="TextBox4" style="width:540px; height:50px" runat="server" TextMode="Password"></asp:TextBox>
							     <br />
                                 </div>
							 <div>
								<span>Confirm Password</span><br />
								<asp:TextBox ID="TextBox5" style="width:540px; height:50px"  runat="server" TextMode="Password"></asp:TextBox>
							 </div>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox4" ControlToValidate="TextBox5" ErrorMessage="Passwords do not match" ForeColor="Red"></asp:CompareValidator>
                            <br />
							 <asp:Button ID="Button1" runat="server" Text="SUBMIT" OnClick="Button1_Click" />
							
					 </div>
					 <div class="clearfix"> </div>
				</form>
			</div>
</div>
<!--//content-->
<div class="footer">
				<div class="container">
			<div class="footer-top-at">
			
				<div class="col-md-4 amet-sed">
				 
				</div>
				<div class="col-md-4 amet-sed ">
				<h4>CONTACT US</h4>
				
					<p>
maye.collection@hotmail.com</p>
					<p>Customer service</p>
					<p>Wednesday to Sunday <br />9h00-13h00 14h00-20h00</p>
					<ul class="social">
						<li><a href="#"><i> </i></a></li>						
						<li><a href="#"><i class="twitter"> </i></a></li>
						<li><a href="#"><i class="rss"> </i></a></li>
						<li><a href="#"><i class="gmail"> </i></a></li>
						
					</ul>
				</div>
				<div id="counter" class="col-md-4 amet-sed">
					<h4>Visit Counter</h4>
					<p>Number of people who visited this website</p>
					<form>
						<a href="http://www.reliablecounter.com" target="_blank"><img src="http://www.reliablecounter.com/count.php?page=localhost:58949/&digit=style/plain/6/&reloads=1" alt="www.reliablecounter.com" title="www.reliablecounter.com" border="0"></a><br /><a href="http://www.clemensschleiwies.com" target="_blank" style="font-family: Geneva, Arial; font-size: 9px; color: #330010; text-decoration: none;">Clemens Schleiwies</a>	</form>
				</div>
				<div class="clearfix"> </div>
			</div>
		</div>
		<div class="footer-class">
		</div>
		</div>
</body>
</html>
			