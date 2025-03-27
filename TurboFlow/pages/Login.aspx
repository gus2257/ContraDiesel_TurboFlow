<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WorkShop.pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Turbo Flow</title>
    <link rel="stylesheet" href="../css/font-icon-awesome.min.css" />
    <link rel="stylesheet" media="screen" href="../css/bootstrap.min.css" />
    <link rel="stylesheet" media="screen" href="../css/Login.css" />
    <link rel="icon" href="../images/logo.ico" type="image/x-icon" />
</head>
<body class="body">
    <script type="text/javascript" src="<%=ruta %>js/jquery/jquery.js"></script>
      <div id="containerHeader">
          
      </div>
    <form id="Form1" action=""  runat="server">
        
        <div class="sign-up ">
        
        <div class="sign-up-title center"> 
        <img alt="" src="<%=ruta%>images/logo.webp" style="width:200px; object-fit:contain;" />
    </div>   
        <div class="row">
        <input id="txtUsuario" runat="server" type="text" class="sign-up-input" placeholder="User" autofocus  style="background-color:white"/>
        <input id="txtPassword" runat="server" type="password" class="sign-up-input" placeholder="Password" style="background-color:white"/>

             <br /> 
            <div class="error">
                <asp:Label ID="lblError"  runat="server"></asp:Label>
            </div>
       </div>
           
        <div class="panel-icon-og cursor">
             <span class="fa fa-1x center position" onclick="byRef();">   
                  LOG IN
             </span></div></div><div style="text-align:center; display: none"> 
            <div style="text-align:center; display: none">  
                <asp:Button  ID="btnLogin" CssClass="circle" runat="server" Text="LOG IN" OnClick="btnLogin_Click" />
                <a href="#" target="_parent"> Olvide mi contraseña </a></div>
             <input id="txtTimeOffset" runat="server" type="text" style="display:none"/>
     </div>
       
    </form>
     
   
  <script type="text/javascript">

      txtOffset = document.getElementById('txtTimeOffset')
      txtOffset.value = new Date().getTimezoneOffset();

      function byRef() {
          document.getElementById('btnLogin').click();
      }
      var click;
      $('form').keypress(function (e) {
          var code = e.keyCode || e.which;
          if (code === 13) {
              e.preventDefault();
              if (click) {
                  clearTimeout(click);
                  setTimeout(Deshabilita, 5000);
              }
              else {
                  click = new Object();
                  setTimeout(Entrar, 1);
              }
              return false;
          }
      })

      function Deshabilita() {
          click = null;
      }
      function Entrar() {
          document.getElementById('btnLogin').click();
      }

  </script>
</body>

</html>
