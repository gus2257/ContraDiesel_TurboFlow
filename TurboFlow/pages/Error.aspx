<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WorkShop.pages.Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <link rel="stylesheet" media="screen" href="../css/main.css" />
</head>
<body id="error-page">
    <form id="form1" runat="server">
        <div id="error-page-content">
            <h1>Error!</h1>
            <div class="row">
                 <div class="col-xs-9">
                <p>Tenemos un problema.</p>
            </div>
            <div class="col-xs-3"><a class="a-danger pointer" onclick="regresa();">Regresar</a></div>
            </div>
            <br />
            [<span id="sError" runat="server"></span>]
            <br />
        </div>
    </form>
    <script type="text/javascript">
        function regresa() {
            history.back();
        }
    </script>
</body>
</html>
