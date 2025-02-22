<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="WorkShop.pages.catalogos.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="UsuarioController as usuario">
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
             <%-- <a id="lnkExportar" href="#">
                         <button class="btn btn-primary btn-xs m-l-sm" id="btnExportar" type="button"><%= this.GetMessage("lblExportar") %></button>
                     </a>--%>
        </li>
    </ol>
    
       
        <div class="page-content" disable-all="usuario.esSoloLectura">
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                    <div id="Filtros">
                        <div class="col-lg-12">

                            <div class="ibox float-e-margins">
                                    
                                <div class="row">
                                    <div class="col-xs-4">
                                        <div class="input">
                                            <input placeholder="<%= this.GetMessage("lblUsuario") %>" type="text" class="form-control"
                                                ng-model="usuario.filter.Filtro" maxlength="50" allow-pattern="[a-zA-Z0-9-_.\s@]+" key-enter="usuario.buscar()" skip-disable autofocus/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="ibox-content">
                                        <div id="Filters" class="padding-form" ng-form="usuario.FilterForm" ng-class="{'submitted': !usuario.filter.isValid}">
                                        </div>
                                        <br />
                                        <div class="padding-top-7" style="overflow-y: auto" ng-show="usuario.esConsulta">
                                            <table style="width: 95%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                                st-table="usuario.Usuarios" st-safe-src="usuario.UsuariosAux">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 20%"><%= this.GetMessage("lblNombre")%>  </th>
                                                        <th style="width: 20%"><%= this.GetMessage("lblApellido")%> </th>
                                                        <th style="width: 20%"><%= this.GetMessage("lblCorreo")%>   </th>
                                                        <th style="width: 10%"><%= this.GetMessage("lblUltimaVez")%>   </th>
                                                        <th style="width: 10%"><%= this.GetMessage("lblEstatus")%>   </th>
                                                        <th  style="width: 5%"class="center">&nbsp;</th>
                                                    </tr>
                                                </thead>
                                                <tbody style="max-height: 500px">
                                                    <tr ng-repeat="item in usuario.Usuarios">
                                                        <td style="width: 20%">{{item.Nombre}}</td>
                                                        <td style="width: 20%">{{item.Apellido}}</td>
                                                        <td style="width: 20%">{{item.Correo}}</td>
                                                        <td style="width: 10%">{{item.UltimoAcceso}}</td>
                                                        <td style="width: 10%">{{item.Estatus}}</td>
                                                        <td style="width: 5%" class="center">
                                                            <span class="cursor" ng-click="usuario.editar(item)" skip-disable>
                                                                <i class="fa fa-pencil-square-o" style="padding-left: 5px" skip-disable></i>&nbsp;                                                                      
                                                            </span>
                                                        </td>
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
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div id="modal-long" tabindex="-1" data-replace="true"  class="modal fade"  data-backdrop="static" data-keyboard="false"  >
                    <div ng-form="usuario.Form" ng-class="{'submitted': usuario.SetClassSummitValid()}">
                        <div class="modal-dialog modal-wide-med" style="min-width: 780px !important" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" data-dismiss="modal" aria-hidden="true"
                                        class="close" skip-disable>
                                        &times;</button>
                                    <h4><%= this.GetMessage("lblTabUsuario") %></h4>

                                </div>
                                <div class="modal-body">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="">
                                                    <div class="col-lg-10">
                                                        <div class="col-lg-12"><%= this.GetMessage("lblNombre") %></div>
                                                        <input type="text" class="form-control" ng-model="usuario.form.Nombre" maxlength="40" required />
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <div class="col-lg-12"><%= this.GetMessage("lblApellido") %></div>
                                                        <input type="text" class="form-control" ng-model="usuario.form.Apellido" maxlength="40" required />
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <div class="col-lg-12"><%= this.GetMessage("lblCorreo") %></div>
                                                        <input type="email" name="Correo" class="form-control" ng-model="usuario.form.Correo" maxlength="50"  ng-pattern="emailPattern"/>
                                                         <small class="msg-error" ng-show="usuario.Form.Correo.$error.pattern"><%= this.GetMessage("lblMailInvalido") %>
                                                         </small>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <div class="col-lg-12"><%= this.GetMessage("lblLoginName") %></div>
                                                        <input type="text" class="form-control" ng-model="usuario.form.LoginName" allow-pattern="(\d|[a-z]|.)"  maxlength="20" required />

                                                    </div>                                                    
                                                    <div class="col-lg-10">
                                                        <div class="col-lg-12"><%= this.GetMessage("lblContasenia") %></div>
                                                        <input type="text" class="form-control" name="Contrasenia" ng-model="usuario.form.Contrasenia" maxlength="20" ng-pattern="/^(?=.*\d).{8,20}$/" ng-minlength="8" required />
                                                        <span class="msg-error" ng-show="usuario.Form.Contrasenia.$error.minlength"><%= this.GetMessage("lblLongitudContrasenia") %></span><br />
                                                        <span class="msg-error" ng-show="usuario.Form.Contrasenia.$error.pattern"><%= this.GetMessage("lblContraseniaInvalida") %></span>
                                                    </div>
                                                     <div class="col-lg-10">
                                                        <div class="col-lg-12">Behavior</div>
                                                        <div selected-model="usuario.behaviorSelected " options="usuario.Behavior" ng-dropdown-multiselect=""
                                                            extra-settings="usuario.behaviorMultiSelectedConfiguration" required>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-10">
                                                           <div class="col-lg-12"><%= this.GetMessage("lblEsActivo") %>
                                                        <input type="checkbox" id="checkbox1" ng-model="usuario.form.EsActivo" numeric-type="integer" numeric="true"></div>
                                                        </div>
                                                    </div>

                                            </div>
                                            <div class="col-lg-9">
                                                <div class="col-lg-12">
                                                    <div class="row">
                                                        <table style="border: 1px solid" st-table="usuario.Permisos" st-safe-src="usuario.PermisosAux">
                                                            <thead style="background-color: #EDEDED">
                                                                <tr>
                                                                    <th width="2%"></th>
                                                                    <th width="25%"></th>
                                                                    <th width="15%"></th>
                                                                    <th width="10%" class="bold" style="text-align:center"><%= this.GetMessage("lblSoloLectura") %></th>
                                                                    <th width="10%" class="bold" style="text-align:center"><%= this.GetMessage("lblEditar") %></th>
                                                                    <th width="10%" class="bold" style="text-align:center"><%= this.GetMessage("lblPagina") %></th>
                                                                </tr>
                                                                <tr>
                                                                    <th width="2%"></th>
                                                                    <th width="25%"></th>
                                                                    <th width="15%"></th>
                                                                    <th width="10%" class="bold" style="text-align:center">                                                                        
                                                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id="chkSoloLectura" ng-model="usuario.opcionSoloLectura" ng-click="usuario.SeleccionarSoloLectura(false)" numeric-type="integer" numeric="true" />
                                                                         <label><%= this.GetMessage("lblSeleccionar") %></label>
                                                                    </th>
                                                                    <th width="10%" class="bold" style="text-align:center">                                                                        
                                                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id="chkEditar" ng-model="usuario.opcionEditar" ng-click="usuario.SeleccionarEditar(true)" numeric-type="integer" numeric="true" />
                                                                         <label><%= this.GetMessage("lblSeleccionar") %></label>
                                                                    </th>
                                                                     <th width="10%">
                                                                         &nbsp;
                                                                     </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr ng-repeat="item in usuario.Permisos">
                                                                    <td width="2%"></td>
                                                                    <td width="25%">

                                                                        <div ng-show="item.PadreId == 0">
                                                                            <b><h4>{{item.Nombre}}</h4></b>
                                                                        </div>
                                                                        <div ng-show="item.PadreId != 0">
                                                                            <span>{{item.Nombre}}</span>
                                                                        </div>
                                                                    </td>
                                                                    <td width="15%">
                                                                        <b>  <span  class="blod" ng-show="item.EsAutorizacion">  <%= this.GetMessage("lblAutorizar") %></span>  <input type="checkbox"  name="checkbox" ng-model="item.Autorizar"  ng-show="item.EsAutorizacion"></b>
                                                                    </td>
                                                                    <td width="10%" style="text-align:center">
                                                                        <input type="checkbox" ng-show="item.PadreId != 0" name="checkbox" ng-click="usuario.ValidaPermiso(item,false)" ng-model="item.SoloLectura">
                                                                    </td> 
                                                                    <td width="10%" style="text-align:center">
                                                                        <input type="checkbox" ng-show="item.PadreId != 0" name="checkbox" ng-click="usuario.ValidaPermiso(item,true)" ng-model="item.Editar">
                                                                    </td>
                                                                     <td width="10%" style="text-align:center">
                                                                        <input type="radio" ng-show="item.PadreId != 0" name="radioGroup" ng-model="item.EsPredeterminado" ng-checked="(item.EsPredeterminado)" ng-change="usuario.ValidaPage(item)" />
                                                                                 
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                    <br />

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger btn-radius big-input" ng-disabled="usuario.form.UsuarioID==0" ng-click="usuario.confirmaEliminar()" ng-hide ="usuario.esSoloLectura">
                                        <%= this.GetMessage("lblEliminar") %>
                                    </button>
                                    <button type="button" class="green btn btn-success btn-radius big-input" ng-click="usuario.guardar(usuario.Permisos)" ng-hide ="usuario.esSoloLectura">
                                        <%= this.GetMessage("lblGuardar") %>
                                    </button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/catalogos/Usuario.js?V00039"></script>

</asp:Content>
