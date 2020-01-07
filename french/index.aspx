<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html>
<head>
<title>Maye Store</title>
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
             document.getElementById("loginButton").innerHTML += '<p style="color:white">Bienvenue</p>';
         }
         else if (document.getElementById("name").value != "Guest User") {
             document.getElementById("loginButton").innerHTML = '<p style="margin-left:-11%; color:white">Bienvenue ' + document.getElementById("name").value + "<br><a onclick='logout()' onmouseover='' style='cursor: pointer; color:white; text-decoration:none; text-align:right'>Se déconnecter</a></p>";
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
             success: function () { alert("Logout Successfull"); window.location.href = "login.aspx" },
             failure: function (response) { alert(response.d); }
         });
     }
     
    </script>
</head>
<body onload="loader()">
<!--header-->
<div class="container">
<div class="header" style="    position: fixed;
    z-index: 1;
    background-color: white;     ">
	<div class="header-top"  >
		<div >
             
			<div class="search" style="background-color:black">
					<form id="Form1" runat="server">
                                 <asp:hiddenfield id="name" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="custId" runat="server"></asp:hiddenfield>
                        <asp:DropDownList style="display:none" AutoPostBack="true" class="btn btn-warning dropdown-toggle" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                      <asp:ListItem>LANGUE</asp:ListItem>
                        <asp:ListItem>ENGLISH</asp:ListItem>
                    <asp:ListItem>FRENCH</asp:ListItem>
                                 </asp:DropDownList>
                </form>
			</div>
			<div class="header-left">		
					<ul id="loginButton">
						<li ><a href="login.aspx"  >S'identifier</a><a href="register.aspx"  >Registre</a></li>

					</ul>
					<div class="cart box_1">
						<a href="checkout.aspx">
						<h3> <div class="total">
							<span >&euro;<asp:Label ID="amt" runat="server" ></asp:Label> </span> (
                            <span >
                                <asp:Label ID="itemCount" runat="server"></asp:Label></span> articles)</div>
						
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
					  <li class="active grid"><a class="color2"  href="index.aspx">Home</a></li>	
				      
				   <li class="grid"><a class="color2" href="#">	Catégories</a>
					  	<div class="mepanel" style="width:200px; margin-left:20%"><center>
						<div class="row">
							<div class="col1">
								<div class="h_nav">
									<ul style="font-size:large; margin-left:20px"><center>
										<li><a href="category.aspx?category=vest&page=1" >Gilets</a></li>
										<li><a href="category.aspx?category=jean&page=1">Jeans</a></li>
										<li><a href="category.aspx?category=robes&page=1">Peignoir</a></li>
										<li><a href="category.aspx?category=Top&page=1">Tops</a></li>
										
								</ul>	
								</div>						
							</div>
							
						  </div></center>
						</div>
			    </li><li class="active grid"><a class="color2"  href="products.aspx">	Produits</a></li>	
				 <li><a class="color2"  href="contact.aspx">Contact</a></li>
			  </ul> 
			</div>
				
				<div class="clearfix"> </div>
		</div>
		</div>
    </div>
	</div>

	<style>
        @keyframes slidy {
0% { left: 0%; }
20% { left: 0%; }
25% { left: -100%; }
45% { left: -100%; }
50% { left: -200%; }
70% { left: -200%; }
75% { left: -300%; }
95% { left: -300%; }
100% { left: -400%; }
}

body { margin: 0; } 
div#slider { overflow: hidden; }
div#slider figure img { width: 20%; float: left; }
div#slider figure { 
  position: relative;
  width: 500%;
  margin: 0;
  left: 0;
  text-align: left;
  font-size: 0;
  animation: 30s slidy infinite; 
}

    </style>
	<div class="banner" style="height:600px; overflow:hidden; margin-top:157px">
		 <div id="slider">
<figure>
    <img src="/images/1.jpg"  alt="">
    <img src="/images/2.jpg"  alt="">
    <img src="/images/3.jpg"  alt="">
    <img src="/images/4.jpg" alt="">
    <img src="/images/2.jpg"  alt="">
</figure>
</div>
	</div>

<!--content-->
<div class="content">
	<div class="container">
	<div class="content-top">
		<h3> MISE À JOUR SUR <asp:Label style="text-transform:capitalize" ID="date" runat="server"></asp:Label></h3>
		<div class="grid-in" id="robeStrip">
			</div>
		<div class="grid-in" id="vestStrip">		 
		</div>
        <div class="grid-in" id="jeanStrip">		 
		</div>
        <div class="grid-in" id="topStrip">		 
		</div>
	</div>
	<!----->
	
	<div class="content-top-bottom">
		 
		<div class="col-md-6 men">
			<a href="category.aspx?category=jeans&page=1" class="b-link-stripe b-animate-go  thickbox"><img class="img-responsive" src="images/t1.jpg" alt="">
				<div class="b-wrapper">
									<h3 class="b-animate b-from-top top-in   b-delay03 ">
										<span>JEANS</span>	
									</h3>
								</div>
			</a>
			
			
		</div>
		<div class="col-md-6">
			<div class="col-md1 ">
				<a href="category.aspx?category=top&page=1" class="b-link-stripe b-animate-go  thickbox"><img class="img-responsive" src="images/t2.jpg" alt="">
					<div class="b-wrapper">
									<h3 class="b-animate b-from-top top-in1   b-delay03 ">
										<span>TOPS</span>	
									</h3>
								</div>
				</a>
				
			</div>
			<div class="col-md2">
				<div class="col-md-6 men1">
					<a href="category.aspx?category=vest&page=1" class="b-link-stripe b-animate-go  thickbox"><img class="img-responsive" src="images/t3.jpg" alt="">
							<div class="b-wrapper">
									<h3 class="b-animate b-from-top top-in2   b-delay03 " style="height:205px">
										<span>Gilets</span>	
									</h3>
								</div>
					</a>
					
				</div>
				<div class="col-md-6 men2">
					<a href="category.aspx?category=robes&page=1" class="b-link-stripe b-animate-go  thickbox"><img class="img-responsive" src="images/t4.jpg" alt="">
							<div class="b-wrapper">
									<h3 class="b-animate b-from-top top-in2   b-delay03 " style="height:205px">
										<span>Peignoir</span>	
									</h3>
								</div>
					</a>
					
				</div>
				<div class="clearfix"> </div>
			</div>
		</div>
		<div class="clearfix"> </div>
	</div>
	</div>
	<!---->
	
</div><br /><br /><br />
<div class="footer" >
				<div class="container">
			<div class="footer-top-at">
			
				<div class="col-md-4 amet-sed">
				
				
			<h4>CONTACTEZ NOUS</h4>
				
					<p>
maye.collection@hotmail.com</p>
					<p>Service client </p>
					<p>du mercredi au dimanche  de <br />9h00-13h00 14h00-20h00.</p>
					<ul class="social">
						<li><a href="#"><i> </i></a></li>						
						<li><a href="#"><i class="twitter"> </i></a></li>
						<li><a href="#"><i class="rss"> </i></a></li>
						<li><a href="#"><i class="gmail"> </i></a></li>
						
					</ul>
				</div>
				<div id="counter" class="col-md-4 amet-sed">
					<h4>Compteur de visite</h4>
					<p>Nombre de personnes qui ont visité ce site</p>
					<a href="http://www.reliablecounter.com" target="_blank"><img src="http://www.reliablecounter.com/count.php?page=localhost:58949/&digit=style/plain/6/&reloads=1" alt="www.reliablecounter.com" title="www.reliablecounter.com" border="0"></a><br /><a href="http://www.clemensschleiwies.com" target="_blank" style="font-family: Geneva, Arial; font-size: 9px; color: #330010; text-decoration: none;">Clemens Schleiwies</a></form>
				</div>
				<div class="clearfix"> </div>
			</div>
		</div>
		
		</div>
</body>
</html>
