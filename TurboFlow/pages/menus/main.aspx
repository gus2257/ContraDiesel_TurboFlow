<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="TurboFlow.pages.menus.main" %>
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
                           <div class="col-lg-3 hidden-xs">&nbsp;
                           </div>   
                           <div class="col-lg-6 col-12">                             
                                <div class="col-lg-4 col-3">
                                       <a href="../reports/monitor.aspx">
                                     <img class="hover" src="../../images/menuMonitor.png" style="width:150px; height:150px; object-fit:contain;"/></a><br />
                                     <div style="font-size:15pt; color:black; padding:10px;">Monitor</div>
                                    
                                 </div>
                                <div class="col-lg-4 col-3">
                                    <a href="operations.aspx">
                                    <img class="hover" src="../../images/menuOperations.png" style="width:150px; height:150px; object-fit:contain;" />
                                     </a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Operation</div>
                                </div>
                                <div class="col-lg-4 col-3">
                                    <a href="menuReports.aspx">
                                    <img class="hover" src="../../images/menuReports.png" style="width:150px; height:150px; object-fit:contain;" /></a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Reports</div>
                                </div>
                                <div class="col-lg-4 col-3">
                                    <a href="config.aspx">
                                    <img class="hover" src="../../images/menuConfig.png" style="width:150px; height:150px; object-fit:contain;" /></a><br />
                                    <div style="font-size:15pt; color:black; padding:10px;">Configuration</div>
                                </div>
                           </div>
                           <div class="col-lg-3 hidden-xs">&nbsp;
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
