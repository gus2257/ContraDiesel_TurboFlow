<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="operations.aspx.cs" Inherits="TurboFlow.pages.menus.operations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        img.hover {          
            border: 1px dashed transparent;
            border-color: #b4b4b4;
            
        }
        img.hover:hover {          
            border: 3px solid transparent;
            border-color: #E76122;
    
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    
<div ng-controller="MenuController as menu">

    <div class="page-content">
        <div id="Principal">
            <div class="row wrapper border-bottom white-bg">
                <div id="Filtros">
                    <div class="row">

                        <div class="col-lg-12; center" >
                            <br />
                            <br />
                            <img src="../../images/logo.webp" style="width:300px;" />

                            <br /> <br /> <br /> <br />
                         </div>

                    </div>
                    <div class="row; center">
                           <div class="col-lg-3">&nbsp;
                           </div>   
                           <div class="col-lg-6">                             
                                <div class="col-lg-4 col-md-6">
                                       <a href="../operation/stock.aspx">
                                     <img class="hover" src="../../images/menuStock.png" style="width:150px; height:150px"/></a><br />
                                     <div style="font-size:15pt; color:black; padding:10px;">Stock</div>
                                    
                                 </div>
                                <div class="col-lg-4 col-md-6">
                                    <a href="../operation/repairs.aspx">
                                    <img class="hover" src="../../images/menuWorkshop.png" style="width:150px; height:150px;" />
                                     </a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Workshop</div>
                                </div>
                                <div class="col-lg-4 col-md-6">
                                    <a href="menuReports.aspx">
                                    <img class="hover" src="../../images/menuPurchase.png" style="width:150px; height:150px;" /></a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Purchases</div>
                                </div>
                                <div class="col-lg-4 col-md-6">
                                    <a href="menuConfig.aspx">
                                    <img class="hover" src="../../images/menuInventory.png" style="width:150px; height:150px;" /></a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Inventory</div>
                                </div>
                                <div class="col-lg-4 col-md-6">
                                    <a href="main.aspx">
                                    <img class="hover" src="../../images/menuBack.png" style="width:150px; height:150px;" /></a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Menu</div>
                                </div>                           </div>
                           <div class="col-lg-3">&nbsp;
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
