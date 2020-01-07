<%@ Page Language="C#" AutoEventWireup="true" CodeFile="single.aspx.cs" Inherits="single" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
<title>Product Description- Maye Store</title>
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
<script src="js/main.js"></script>
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

               if (document.getElementById("name1").value === "Guest User ") {
                   document.getElementById("loginButton").innerHTML += '<p style="color:white">Bienvenue </p>';
               }
               else if (document.getElementById("name1").value != "Guest User ") {
                   document.getElementById("loginButton").innerHTML = '<p style="margin-left:-11%; color:white">Bienvenue ' + document.getElementById("name").value + "<br><a onclick='logout()' onmouseover='' style='cursor: pointer; color:white; text-decoration:none; text-align:right'>Se déconnecter</a></p>";
               }
               if (document.getElementById("name1").value != "Admin Maye") {
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
           var id;
           function cart( id1,price, qty) {
               qty=document.getElementById("qty").value.toString() ;
               id = { "id1": id1, "qty": qty, "size": document.getElementById('size').value.toString() };
               document.getElementById("amt").innerHTML = parseInt(document.getElementById("amt").innerHTML) + (price * parseInt(document.getElementById("qty").value));
               document.getElementById("itemCount").innerHTML = parseInt(document.getElementById("itemCount").innerHTML) + parseInt(document.getElementById("qty").value);

               $.ajax
                ({
                    type: "POST",
                    url: "single.aspx/cartAdd",
                    data: JSON.stringify(id),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
           }


    </script>
 
</head>
<body  onload="loader()">
<!--header-->
<div class="container">
<div class="header" style="    position: fixed;
    z-index: 1;
    background-color: white; ">
	<div class="header-top"  >
		<div >
             
			<div class="search" style="background-color:black"><form id="Form1" runat="server">
					<asp:hiddenfield id="name1" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
				 	<asp:hiddenfield id="custId" runat="server"></asp:hiddenfield>
              <asp:DropDownList style="display:none"  AutoPostBack="true" class="btn btn-warning dropdown-toggle" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                     <asp:ListItem>LANGUE</asp:ListItem>      <asp:ListItem>ENGLISH</asp:ListItem>
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
<!--content-->
<!---->

				<!---->
				<br />
				</div>
				<div class="col-md-9 product-price1" style="  margin-top:168px; margin-left:10%">
				<div class="col-md-5 single-top">	
			<div class="flexslider">
  <ul class="slides" id="slideshow">
   
  </ul>
</div>
<!-- FlexSlider -->
  <script defer src="js/jquery.flexslider.js"></script>
<link rel="stylesheet" href="css/flexslider.css" type="text/css" media="screen" />

<script>
    // Can also be used with $(document).ready()
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            controlNav: "thumbnails"
        });
    });
</script>
					</div>	
					<div class="col-md-7 single-top-in simpleCart_shelfItem">
						<div class="single-para " style="margin-left:8%">
						<h4 style="text-transform:capitalize" id="name"></h4>
							<div class="star-on">
								
							<div class="clearfix"> </div>
							</div>
							
							<h5 id='price' class="item_price">&euro; </h5>
							<p id="description">  </p>
							<div class="available">
                                <p style=" font-size:1em;   font-family: 'Lato', serif;" class="size-in">Quantité  
                                <asp:TextBox ID="qty" style="font-size:1em;   font-family: 'Lato', serif; text-align:right; margin-left:9.3%; width:50px" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                                </p><ul>
									
								<li style="font-size:1em;    font-family: 'Lato', serif;" class="size-in">Taille<select style="
    margin-top: -34px;
    margin-left: 69px;
" id="size">
									
								</select></li>
                                    <li class="size-in" style="font-size:1em;   font-family: 'Lato', serif;" id="colorDrop">
                                        Couleur<select style=" font-size:1em;   font-family: 'Lato', serif;   margin-top: -34px;margin-left: 54px;" onChange="window.location.href=this.value" id="color">
                                            <option>Sélectionner</option>
									
								</select></li>
                                    <div id="fb-root"></div>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.5&appId=806639039448463";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
                                    <div class="fb-like" data-href="http://localhost:58949/index.aspx" data-layout="standard" data-action="like" data-show-faces="true" data-share="true"></div>
								<div class="clearfix"> </div>
							</ul>
						</div>
							<center><br />
								<div id="button"></div>
							</center>
						</div>
					</div>
				<div class="clearfix"> </div>
			<!---->
					<div class="cd-tabs">
			<nav>
				<ul class="cd-tabs-navigation">
					  <li><a data-content="television" href="#0" >Avis </a></li>
					
				</ul> 
			</nav>
	<ul class="cd-tabs-content">
		<li data-content="fashion" >
		<div class="facts">
									  <p id="description2"> There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined </p>
							        </div>

</li>
 
<li data-content="television" class="selected">
	<div class="comments-top-top" id="reviewpanel">
				 
       </div> 
       <br /> <asp:TextBox style="width:70%" ID="review" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
       <br /> <asp:Button class="number item_price" ID="Button1" runat="server" Text="Donnéz Votre Avis" OnClick="Button1_Click" />
		
				 
			

</li>
<div class="clearfix"></div>
	</ul> 
</div> </form>
		<div class=" bottom-product" id="suggestions">
					 
				</div>
</div>

		<div class="clearfix"> </div>
		</div>
		</div>
<!--//content--><br />
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
			