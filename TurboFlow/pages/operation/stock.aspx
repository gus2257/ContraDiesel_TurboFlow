<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="stock.aspx.cs" Inherits="WorkShop.pages.operation.stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #SearchParameters.in,
        #SearchParameters.collapsing {
            display: block!important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="StockController as stock">
        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-tag fa-fw"></i></li>
            <li><%= this.GetMessage("MainModule") %> &nbsp;</li>
            <li class="active"><%= this.GetMessage("Module") %></li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="usuario.nuevo()" ng-hide="usuario.esSoloLectura">
                         <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAgregar") %></button>
                 </a>
                 <a id="lnkBuscar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="btnBuscar" type="button" ng-click="usuario.buscar()" skip-disable>
                         <i class="fa fa-search"></i>&nbsp;  <%= this.GetMessage("lblBuscar") %></button>
                 </a>
            </li>
         </ol>
    
       
        <div class="page-content" disable-all="stock.esSoloLectura">
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                   
                        <div class="col-lg-12">

                               
                                <div class="row">
                                        <div class="col-sm-10" style="border-style:solid; border-width:1px;">

                                             <span class="btn visible-xs" data-toggle="collapse" data-target="#SearchParameters" style="width:100%; text-align:right;">
                                                Filters <span class="fa fa-filter"></span>
                                            </span>
                                        <div class="hidden-xs SearchParameters" id="SearchParameters">
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Category</div>
                                                    <select class="form-control" ng-model="stock.filter.CategoryID"
                                                            ng-options="Category.CategoryID as TipoUsuario.NombreTipoUsuario for TipoUsuario in usuario.TipoUsuario" ng-change="usuario.CambiarTipoUsuario()" required>
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Marca</div>
                                                    <select class="form-control" ng-model="stock.filter.CategoryID"
                                                            ng-options="Category.CategoryID as TipoUsuario.NombreTipoUsuario for TipoUsuario in usuario.TipoUsuario" ng-change="usuario.CambiarTipoUsuario()" required>
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                 <div class="input">
                                                    <div class="col-lg-12">Modelo</div>
                                                    <select class="form-control" ng-model="stock.filter.CategoryID"
                                                            ng-options="Category.CategoryID as TipoUsuario.NombreTipoUsuario for TipoUsuario in usuario.TipoUsuario" ng-change="usuario.CambiarTipoUsuario()" required>
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="input">
                                                    <div class="col-lg-12">Stock ID</div>
                                                    <input placeholder="00000" type="text" class="form-control"
                                                        ng-model="stock.filter.StockID" maxlength="50" allow-pattern="\d+" key-enter="stock.Seach()" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                 <div class="input">
                                                    <div class="col-lg-12">Estatus</div>
                                                    <select class="form-control" ng-model="stock.filter.CategoryID"
                                                            ng-options="Category.CategoryID as TipoUsuario.NombreTipoUsuario for TipoUsuario in usuario.TipoUsuario" ng-change="usuario.CambiarTipoUsuario()" required>
                                                    </select>                                                            
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    
                                        <div class="col-sm-2 hidden-xs" style="border-style:solid; border-width:1px; height:100%; border-spacing:5px">
                                             <div class="input">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>Inventario:</td><td>0</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Hold:</td><td>0</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Cliente:</td><td>0</td>
                                                        </tr>
                                                    </table>                                                        
                                            </div>
                                        </div>
                                </div>

                                <div class="row">
                                  
                                <div class="modal-body">
                                    <div class="ibox-content">
                                        <ul id="myTab" class="nav nav-tabs ul-edit">
                                            <li class="active" skip-disable><a href="#tabGrouped" data-toggle="tab" skip-disable>Group</a></li>
                                            <li skip-disable><a href="#tabDetailed" data-toggle="tab" skip-disable>Detail</a></li>
                                        </ul>
                                        <div id="myTabContent" class="tab-content">
                                            <div id="tabGrouped" class="tab-pane fade in active">
                                                <div class="row">
                                                     <table style="width: 95%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                                         st-table="usuario.Usuarios" st-safe-src="usuario.UsuariosAux">
                                                         <thead>
                                                             <tr>
                                                                 <th style="width: 20%">Category</th>
                                                                 <th style="width: 20%">Brande</th>
                                                                 <th style="width: 20%">Sku</th>
                                                                 <th style="width: 20%">Model</th>
                                                                 <th style="width: 10%">Available</th>
                                                                 <th style="width: 10%">Hold</th>
                                                                 
                                                             </tr>
                                                         </thead>
                                                         <tbody style="max-height: 500px">
                                                             <tr ng-repeat="item in usuario.Usuarios">
                                                                 <td style="width: 20%">{{item.Nombre}}</td>
                                                                 <td style="width: 20%">{{item.Apellido}}</td>
                                                                 <td style="width: 20%">{{item.Correo}}</td>
                                                                 <td style="width: 10%">{{item.UltimoAcceso}}</td>
                                                                 <td style="width: 10%">{{item.TipoUsuario}}</td>
                                                                 <td style="width: 10%">{{item.Estatus}}</td>
                                                             </tr>
                                                         </tbody>
                                                         <tfoot>
                                                             <tr>
                                                                 <td colspan="9" class="text-right" style="padding-bottom: 0">
                                                                     <div st-pagination="5" st-items-by-page="30" st-template="../../Templates/pagination.html"></div>
                                                                 </td>
                                                             </tr>
                                                         </tfoot>
                                                     </table>                                                
                                                </div>
                                            </div>
                                            <div id="tabDetailed" class="tab-pane fade in active">
                                                <div class="row">
                                                        tab2
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>

                 </div>
               
            </div>
        </div>
     </div>

    <script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/operation/Stock.js?V00039"></script>

</asp:Content>
