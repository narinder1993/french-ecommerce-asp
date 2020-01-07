<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE html>
<html>
<head>
<title>Login- Maye Store</title>
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
    <script>
        window.fbAsyncInit = function () {
            FB.init({
                appId: '806639039448463',
                xfbml: true,
                version: 'v2.5'
            });
        };
        function logt() {
            alert("here")
            FB.logout(function (response) {
                alert("logged out")
            });
        }
        function checkLoginState()
        {
            var email;
            var name;
            FB.api('/me ', function (data)
            {
                name = data.name;
                email = name + "@facebook.com";
                var pack = { "name": name, "email": email };
                alert(JSON.stringify(name))
                $.ajax
                ({
                    type: "POST",
                    url: "login.aspx/loginFb",
                    data: JSON.stringify(pack),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function ( )
                    {
                        window.location.href = "/index.aspx";
                    }
                })
            })
            
        } 
        

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
</script>
<script src="js/simpleCart.min.js"> </script>
    <script>
        function statusChangeCallback(response) {
            console.log('statusChangeCallback');
            console.log(response);
            // The response object is returned with a status field that lets the
            // app know the current login status of the person.
            // Full docs on the response object can be found in the documentation
            // for FB.getLoginStatus().
            if (response.status === 'connected') {
                // Logged into your app and Facebook.
                 
            } else if (response.status === 'not_authorized') {
                // The person is logged into Facebook, but not your app.
                document.getElementById('status').innerHTML = 'Please log ' +
                  'into this app.';
            } else {
                // The person is not logged into Facebook, so we're not sure if
                // they are logged into this app or not.
                document.getElementById('status').innerHTML = 'Please log ' +
                  'into Facebook.';
            }
        }

        // This function is called when someone finishes with the Login
        // Button.  See the onlogin handler attached to it in the sample
        // code below.
         

       

            // Now that we've initialized the JavaScript SDK, we call 
            // FB.getLoginStatus().  This function gets the state of the
            // person visiting this page and can return one of three states to
            // the callback you provide.  They can be:
            //
            // 1. Logged into your app ('connected')
            // 2. Logged into Facebook, but not your app ('not_authorized')
            // 3. Not logged into Facebook and can't tell if they are logged into
            //    your app or not.
            //
            // These three cases are handled in the callback function.
       
        // Load the SDK asynchronously
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        // Here we run a very simple test of the Graph API after login is
        // successful.  See statusChangeCallback() for when this call is made.
        function testAPI() {
            console.log('Welcome!  Fetching your information.... ');
            FB.api('/me', function (response) {
                console.log('Successful login for: ' + response.email);
                document.getElementById('status').innerHTML =
                  'Thanks for logging in, ' + response.name + '!';
                
            });
        }
        
        
        
    </script>
    
    <script type="text/javascript">
        function loader()
        {
            if (document.getElementById("name").value === "Guest User ") {
                document.getElementById("loginButton").innerHTML += '<p style="color:white">Bienvenue</p>';
            }
            else if (document.getElementById("name").value != "Guest User") {
                document.getElementById("loginButton").innerHTML = '<p style="margin-left:-11%; color:white">Bienvenue ' + document.getElementById("name").value + "<br><a onclick='logout()' onmouseover='' style='cursor: pointer; color:white; text-decoration:none; text-align:right'>Se déconnecter</a></p>";
            }
            if (document.getElementById("name").value != "Admin Maye") {
                document.getElementById("counter").style.display = "none";
            }
        }
        function logout()
        {
            logt();
            $.ajax
            ({
                type: "POST",
                url: "login.aspx/logout",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function() {alert("Logout Successfull"); window.location.href="index.aspx"},
                failure: function(response) {alert(response.d);}
            });
        }

        
    </script>
</head>
<body onload="loader()">
    <div id="fb-root"></div>
<script>(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.5&appId=806639039448463";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
<!--header-->
<div class="container">
<div class="header" style="    position: fixed;
    z-index: 1;
    background-color: white; ">
	<div class="header-top"  >
		<div >
             
			<div class="search" style="background-color:black">
					<form runat="server">
                                 <asp:hiddenfield id="name" runat="server"></asp:hiddenfield>
                                 <asp:hiddenfield id="macadd" runat="server"></asp:hiddenfield>
				 		<asp:hiddenfield id="custId" runat="server"></asp:hiddenfield>
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
		</div></div>

	</div>	
<!--content-->
<div class="container" style="margin-top:124px">
		<div class="account">
		<h1>S'identifier</h1>
		<div class="account-pass">
		<div class="col-md-8 account-top" style="width: 56.66666667%;">
			     
			<div> 	
                 
				<span>Email</span>
				 <asp:TextBox style="width:400px" ID="email" runat="server"></asp:TextBox>
			</div>
			<div> 
				<span >Password</span>
				<asp:TextBox style="width:400px" ID="password" runat="server" TextMode="Password"></asp:TextBox>
			</div>				
                <asp:Button  ID="Button1" runat="server" Text="S'IDENTIFIER" OnClick="Button1_Click" />
                <p> <br /> <br />Si vous ne disposez pas d'un compte, <a href="register.aspx">Inscrivez-vous ici</a></p>
			</form>
		</div >
      <div id="facebook"><br /><br />
            <h4>Vous pouvez également vous connecter en utilisant Facebook</h4>
				<div  style="margin-top:4%" onlogout="logt()"  onlogin="checkLoginState();" class="fb-login-button" data-max-rows="1" data-size="xlarge" data-show-faces="true" data-auto-logout-link="true"></div>
              	<div class="clearfix"> </div>
         
	</div>
	</div>
    <br /><br />
</div>
    </div>
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
</body>
</html>
			