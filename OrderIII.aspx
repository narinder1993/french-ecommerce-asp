<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderIII.aspx.cs" Inherits="OrderIII" %>


<!DOCTYPE html>
<html>
<head>
<title>Order- Maye Store</title>
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
     <link href='http://fonts.googleapis.com/css?family=PT+Sans+Caption:400,700' rel='stylesheet' type='text/css'>


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
<script>(function (d, s, id) {
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
         var custId = document.getElementById("custId").valueOf;
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
             success: function () { alert("Logout Successfull"); window.location.href = "/login.aspx" },
             failure: function (response) { alert(response.d); }
         });
     }

    </script>
<style>
    @-webkit-keyframes myanimation {
  from {
    left: 0%;
  }
  to {
    left: 50%;
  }
}
h1 {
  text-align: center;
  font-family: 'PT Sans Caption', sans-serif;
  font-weight: 400;
  font-size: 20px;
  padding: 20px 0;
  color: #777;
}

.checkout-wrap {
  color: #444;
  font-family: 'PT Sans Caption', sans-serif;
  margin: 40px auto;
  max-width: 1200px;
  position: relative;
}

ul.checkout-bar li {
  color: #ccc;
  display: block;
  font-size: 16px;
  font-weight: 600;
  padding: 14px 20px 14px 80px;
  position: relative;
}
ul.checkout-bar li:before {
  -webkit-box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
  box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
  background: #ddd;
  border: 2px solid #FFF;
  border-radius: 50%;
  color: #fff;
  font-size: 16px;
  font-weight: 700;
  left: 20px;
  line-height: 37px;
  height: 35px;
  position: absolute;
  text-align: center;
  text-shadow: 1px 1px rgba(0, 0, 0, 0.2);
  top: 4px;
  width: 35px;
  z-index: 999;
}
ul.checkout-bar li.active {
  color: #8bc53f;
  font-weight: bold;
}
ul.checkout-bar li.active:before {
  background: #8bc53f;
  z-index: 99999;
}
ul.checkout-bar li.visited {
  background: #ECECEC;
  color: #57aed1;
  z-index: 99999;
}
ul.checkout-bar li.visited:before {
  background: #57aed1;
  z-index: 99999;
}
ul.checkout-bar li:nth-child(1):before {
  content: "1";
}
ul.checkout-bar li:nth-child(2):before {
  content: "2";
}
ul.checkout-bar li:nth-child(3):before {
  content: "3";
}
ul.checkout-bar li:nth-child(4):before {
  content: "4";
}
ul.checkout-bar li:nth-child(5):before {
  content: "5";
}
ul.checkout-bar li:nth-child(6):before {
  content: "6";
}
ul.checkout-bar a {
  color: #57aed1;
  font-size: 16px;
  font-weight: 600;
  text-decoration: none;
}

@media all and (min-width: 800px) {
  .checkout-bar li.active:after {
    -webkit-animation: myanimation 3s 0;
    background-size: 35px 35px;
    background-color: #8bc53f;
    background-image: -webkit-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    background-image: -moz-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    -webkit-box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    content: "";
    height: 15px;
    width: 100%;
    left: 50%;
    position: absolute;
    top: -50px;
    z-index: 0;
  }

  .checkout-wrap {
    margin: 80px auto;
  }

  ul.checkout-bar {
    -webkit-box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    background-size: 35px 35px;
    background-color: #EcEcEc;
    background-image: -webkit-linear-gradient(-45deg, rgba(255, 255, 255, 0.4) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.4) 50%, rgba(255, 255, 255, 0.4) 75%, transparent 75%, transparent);
    background-image: -moz-linear-gradient(-45deg, rgba(255, 255, 255, 0.4) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.4) 50%, rgba(255, 255, 255, 0.4) 75%, transparent 75%, transparent);
    border-radius: 15px;
    height: 15px;
    margin: 0 auto;
    padding: 0;
    position: absolute;
    width: 100%;
  }
  ul.checkout-bar:before {
    background-size: 35px 35px;
    background-color: #57aed1;
    background-image: -webkit-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    background-image: -moz-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    -webkit-box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    border-radius: 15px;
    content: " ";
    height: 15px;
    left: 0;
    position: absolute;
    width: 10%;
  }
  ul.checkout-bar li {
    display: inline-block;
    margin: 50px 0 0;
    padding: 0;
    text-align: center;
    width: 19%;
  }
  ul.checkout-bar li:before {
    height: 45px;
    left: 40%;
    line-height: 45px;
    position: absolute;
    top: -65px;
    width: 45px;
    z-index: 99;
  }
  ul.checkout-bar li.visited {
    background: none;
  }
  ul.checkout-bar li.visited:after {
    background-size: 35px 35px;
    background-color: #57aed1;
    background-image: -webkit-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    background-image: -moz-linear-gradient(-45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
    -webkit-box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    box-shadow: inset 2px 2px 2px 0px rgba(0, 0, 0, 0.2);
    content: "";
    height: 15px;
    left: 50%;
    position: absolute;
    top: -50px;
    width: 100%;
    z-index: 99;
  }
}

</style>
</head>
<body onload="loader()">
<!--header-->
<div class="header">
	<div class="header-top">
		<div class="container">
			<div class="search">
					<form id="Form1" runat="server">
                                 <asp:hiddenfield id="name" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="custId" runat="server"></asp:hiddenfield>
				 	
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
  

<div class="checkout-wrap">  
<ul class="checkout-bar">    
<li class="visited first">      
<a href="#">Login</a>    
</li>    
<li class="previous visited">Shipping Information</li>
<li class="active">Shipping Options</li>    
<li class="next">Review & Payment</li>   
<li class="">Complete</li>  
</ul>
</div> <br /><br /><br /><br /><br /><br />
 <div>
     <center><h3>Please select your shipping zone</h3>
         <h5>(Applicable per item)</h5>
      </center>
     <br /><br /> 
     <fieldset style="font-size:large; margin-left:7%">
         <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged1">
             <asp:ListItem Value="1">Zone 1(&euro;8.93): Within France</asp:ListItem>
             <asp:ListItem Value="2">Zone 2(&euro;22.98): Guadeloupe (including Saint Barthélémy and St Martin), Martinique, Réunion,
Guyana, Mayotte and St Pierre and Miquelon</asp:ListItem>
             <asp:ListItem Value="3">Zone 3(&euro;41.39): New Caledonia and Dependencies, French Polynesia, Wallis and Islands
Futuna, French Southern and Antarctic Lands</asp:ListItem>
             <asp:ListItem Value="4">Zone 4(&euro;13.22): Germany, Belgium, Netherlands,
Luxembourg.</asp:ListItem>
             <asp:ListItem Value="5">Zone 5(&euro;15.93): UK, Ireland, Italy,
Spain, Portugal, Switzerland, Austria</asp:ListItem>
             <asp:ListItem Value="6">Zone 6(&euro;16.52): Denmark, Hungary, Poland,
Czech Republic, Slovakia, Slovenia,
Estonia, Latvia, Lithuania</asp:ListItem>
             <asp:ListItem Value="7">Zone 7(&euro;19.37): Greece, Iceland, Finland, Norway,
Sweden, Turkey, the Maghreb, specific territories
Portugal and Spain, Eastern Europe.</asp:ListItem>
             <asp:ListItem Value="8">Zone 8(&euro;40.65): USA, Canada, Australia,
China, Japan, Hong Kong, Singapore,
South Korea, Thailand, Taiwan, Viet
Nam, India, Russia, Israel</asp:ListItem>
             <asp:ListItem Value="9">Zone 9(&euro;48.42): Africa (excluding North Africa), the Middle East,
Americas, other countries of Asia, Oceania.</asp:ListItem>
              
         </asp:RadioButtonList>
     <br /><br /><br />
     <p>Your shipping charges are:&euro;<asp:Label ID="ship" runat="server" Text="0"></asp:Label></p>
       <center>  <asp:Button ID="Button1" CssClass="btn" Font-Size="Large" BackColor="White" ForeColor="Black" BorderStyle="Groove" BorderColor="Black" runat="server" Text="Proceed" OnClick="Button1_Click" />
         </center>
     </fieldset> 
 </div>   
	<!---->
 <br /><br /><br /><br /><br /><br />
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