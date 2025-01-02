<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="TurboFlow.pages.menus.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    
     <div ng-controller="MenuController as menu">

    <div class="page-content">
        <div id="Principal">
            <div class="row wrapper border-bottom white-bg">
                <div id="Filtros">
                     <div class="row">

                        <div class="col-lg-12; center" >
                            <a href="../../images/logo.webp">../../images/logo.webp</a>
                         </div>

                    </div>
                    <div class="row; center">

                         <div class="col-lg-4">
                             <img src="../../images/menuMonitor.png" style="width:200px; height:200px" />
                         </div>
                        <div class="col-lg-4">>
                            <img src="../../images/menuOperations.png" style="width:200px; height:200px" />
                        </div>
                        <div class="col-lg-4">>
                            <img src="../../images/menuReports.png" style="width:200px; height:200px" />
                        </div>
                        <div class="col-lg-4">>
                            <img src="../../images/menuConfig.png" style="width:200px; height:200px" />
                        </div>

                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
           
        </div>
    </div>
</div>

<script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/menus/menu.js?V00039"></script>

</asp:Content>
