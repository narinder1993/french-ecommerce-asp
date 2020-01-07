<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="category" %>

<!DOCTYPE html>

<head>
<title>Products- Maye Store</title>
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
<link href='http://localhost:58949/fonts.googleapis.com/css?family=Lato:100,300,400,700,900' rel='stylesheet' type='text/css'>
<link href='http://localhost:58949/fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900' rel='stylesheet' type='text/css'><!--//fonts-->
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

     function load()
        {
            var newInput="";
            var cat = location.search.split('category=')[1] ? location.search.split('category=')[1] : '0';
            var array = cat.split('&');
            var myParam = array[0];
            if (myParam == 'vest') {
			    //newInput = "<li class=\"disabled\"><a href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">«</span></a></li>";

			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=1\">1 <span class=\"sr-only\">(current)</span></a></li>";
			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=2\">2 <span class=\"sr-only\"></span></a></li>";
			    //newInput += "<li> <a href=\"#\" aria-label=\"Next\"><span aria-hidden=\"true\">»</span> </a> </li>";
                //
                document.getElementById("vest").style.color = "Gray";

			    document.getElementById("pages").innerHTML = newInput;
			}
			if (myParam == 'Top') {
			    //newInput = "<li class=\"disabled\"><a href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">«</span></a></li>";

			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=1\">1 <span class=\"sr-only\">(current)</span></a></li>";
			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=2\">2 <span class=\"sr-only\"></span></a></li>";
			    //newInput += "<li> <a href=\"#\" aria-label=\"Next\"><span aria-hidden=\"true\">»</span> </a> </li>";
			    document.getElementById("top").style.color = "Gray";
			    document.getElementById("pages").innerHTML += newInput;
			}
			if (myParam == 'jean') {
			    document.getElementById("jean").style.color = "Gray";
			}
			if (myParam == 'robes') {
			    //newInput = "<li class=\"disabled\"><a href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">«</span></a></li>";

			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=1\">1 <span class=\"sr-only\">(current)</span></a></li>";
			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=2\">2 <span class=\"sr-only\"></span></a></li>";
			    //newInput += "<li><a href=\"category.aspx?category=" + myParam + "&page=3\">3 <span class=\"sr-only\"></span></a></li>";
			    //newInput += "<li> <a href=\"#\" aria-label=\"Next\"><span aria-hidden=\"true\">»</span> </a> </li>";
			    document.getElementById("robe").style.color = "Gray";
			    document.getElementById("pages").innerHTML += newInput;
			}
			loader();
        }
        var id;
        function cart(price, id1)
        {
            id = { "id1": id1 };
            document.getElementById("amt").innerHTML = parseInt(document.getElementById("amt").innerHTML) + price;
            document.getElementById("itemCount").innerHTML = parseInt(document.getElementById("itemCount").innerHTML) + 1;
             
            $.ajax
             ({
                 type: "POST",
                 url: "category.aspx/cartAdd",
                 data:JSON.stringify(id),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json" 
             });
        }
    </script>
    <script type="text/javascript">
         function loader() {

             if (document.getElementById("name").value === "Guest User ") {
                 document.getElementById("loginButton").innerHTML += '<p style="color:white">Bienvenue</p>';
             }
             else if (document.getElementById("name").value != "Guest User") {
                 document.getElementById("loginButton").innerHTML = '<p style="margin-left:-11%; color:white">Bienvenue ' + document.getElementById("name").value + "<br><a onclick='logout()' onmouseover='' style='cursor: pointer; color:white; text-decoration:none; text-align:right'>Se déconnecter</a></p>";
             } if (document.getElementById("name").value != "Admin Maye") {
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
                 success: function () { alert("Logout Successfull"); window.location.href = "index.aspx" },
                 failure: function (response) { alert(response.d); }
             });
         }
    </script>
    </head><form id="Form1" runat="server">
<body  onload="load()">
    
<!--header-->
<div class="container">
<div class="header" style="position: fixed;
    z-index: 1;
    background-color: white;" >   
	<div class="header-top"  >
		<div >
             
			<div class="search" style="background-color:black">
					<asp:hiddenfield id="name" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
				 	<asp:hiddenfield id="custId" runat="server">

				 	</asp:hiddenfield>
                    <asp:DropDownList style="display:none"  AutoPostBack="true" class="btn btn-warning dropdown-toggle" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>LANGUE</asp:ListItem>
                        <asp:ListItem>ENGLISH</asp:ListItem>
                    <asp:ListItem>FRENCH</asp:ListItem>
                                 </asp:DropDownList>
                
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
<!--content-->
<!---->
		<div class="product">
			<div class="container" style="margin-top:124px">
				<div class="col-md-3 product-price">
					  
				<div class=" rsidebar span_1_of_left">
					<div class="of-left">
						<h3 class="cate">Catégories</h3>
					</div>
		<ul class="menu">
		    <li  class="item1"><a id="vest" href="category.aspx?category=vest&page=1" >Gilets</a></li>
            <li class="item1"><a id="jean" href="category.aspx?category=jean&page=1">Jeans</a></li>
            <li class="item1"><a id="robe" href="category.aspx?category=robes&page=1">Peignoir</a></li>
            <li class="item1"><a id="top" href="category.aspx?category=Top&page=1">Tops</a></li>
		</ul>
					</div>
				<!--initiate accordion-->
		
<!---->
	
				<!---->
				<div class="product-bottom" id="ticker">
					 
				</div>
 
				</div>
				<div class="col-md-9 product1" id="products">
			
				</div>
		<div class="clearfix"> </div>
		<nav class="in" >

            	  <ul class="pagination" id="pages">
					 </ul>
				</nav>
		</div>
		
		</div>
			
				<!---->

<!--//content-->
<div class="footer">
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
					<form>
					<a href="http://www.reliablecounter.com" target="_blank"><img src="http://www.reliablecounter.com/count.php?page=localhost:58949/&digit=style/plain/6/&reloads=1" alt="www.reliablecounter.com" title="www.reliablecounter.com" border="0"></a><br /><a href="http://www.clemensschleiwies.com" target="_blank" style="font-family: Geneva, Arial; font-size: 9px; color: #330010; text-decoration: none;">Clemens Schleiwies</a></form>
				</div>
				<div class="clearfix"> </div>
			</div>
		</div>
		<div class="footer-class">
		</div>
		</div>
</body></form>
</html>
			