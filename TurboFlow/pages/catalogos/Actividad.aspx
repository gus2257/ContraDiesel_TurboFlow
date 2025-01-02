<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="Actividad.aspx.cs" Inherits="WorkShop.pages.catalogos.Actividad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <div ng-controller="ActividadController as actividad">
    <ol class="breadcrumb page-breadcrumb">
        <li><i class="fa fa-cog fa-fw"></i></li>
        <li><%= this.GetMessage("lblCatalogo") %>  &nbsp;</li>
        <li class="active"><%= this.GetMessage("lblAlias") %></li>
        <li class="notSlide pull-right margin10"> 
            <a id="A1" href="#">
                <button class="btn btn-primary btn-xs m-l-sm" id="Button1" type="button" ng-click="actividad.nuevo()" tabindex="2" ng-hide="actividad.esSoloLectura">
                    <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAgregar") %></button>
            </a>
            <a id="A2" href="#">
                <button class="btn btn-primary btn-xs m-l-sm" id="Button2" type="button" ng-click="actividad.buscar()" tabindex="3" skip-disable>
                    <i class="fa fa-search"></i>&nbsp;  <%= this.GetMessage("lblBuscar") %>
                </button>
            </a>

        </li>
    </ol>
    
      
        <div class="page-content" disable-all="actividad.esSoloLectura">
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                    <div id="Filtros">
                        <div class="col-lg-12">

                            <div class="ibox float-e-margins">
                                <div class="row">
                                    <div class="col-xs-4">
                                        <div class="input">
                                            <input placeholder="<%= this.GetMessage("lblFiltro") %>" type="text" class="form-control"
                                                ng-model="actividad.filter.Actividad" maxlength="40" tabindex="1"  key-enter="actividad.buscar()" skip-disable autofocus/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="ibox-content">
                                        <div class="padding-top-7" style="overflow-y: auto">
                                            <table style="width: 95%" class=" col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                                st-table="actividad.Actividades" st-safe-src="actividad.ActividadesAux">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 45%"><%= this.GetMessage("lblActividad") %>
                                                        </th>
                                                        <th style="width: 25%; text-align: center;"><%= this.GetMessage("lblHoras") %>
                                                          </th>
                                                        <th style="width: 25%; text-align: center;"><%= this.GetMessage("lbEstatus") %>
                                                        </th>
                                                        <th style="width: 5%" class="right">&nbsp;</th>
                                                    </tr>
                                                </thead>
                                                <tbody style="max-height: 500px">
                                                    <tr ng-repeat="item in actividad.Actividades">
                                                        <td style="width: 45%">{{item.Actividad}}</td>
                                                        <td style="width: 25%; text-align: center;">{{item.HorasHombre}}</td>
                                                        <td style="width: 25%; text-align: center;">{{item.Estatus}}</td>
                                                        <td style="width: 5%" class="right">
                                                            <span class="cursor" ng-click="actividad.editar(item)" skip-disable>
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
                <div id="modal-long" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
                    <div ng-form="actividad.Form" ng-class="{'submitted': actividad.SetClassSummitValid()}">
                        <div class="modal-dialog modal-wide-med" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" aria-hidden="true" class="close" skip-disable ng-click="actividad.ConfirmarCerrarModal()">&times;</button>
                                    <h4><%= this.GetMessage("lblAlias") %></h4>

                                </div>
                                <div class="modal-body">
                                    <div id="truck" class="tab-pane fade in active">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="portlet box portlet-pink">
                                                    <div class="portlet-body">
                                                        <div role="form">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <label><%= this.GetMessage("lblActividad") %></label>
                                                                        <input type="hidden" class="form-control" ng-model="actividad.form.ActividadID" disabled="disabled" />
                                                                        <input type="text" class="form-control" ng-model="actividad.form.Actividad" maxlength="40" <%--allow-pattern="[a-zA-Z0-9\s]+"--%> required />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <label><%= this.GetMessage("lblHoras") %></label>
                                                                        <input type="text" class="form-control" ng-model="actividad.form.HorasHombre" maxlength="5" decimal required />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label><%= this.GetMessage("lblEsActivo") %></label>
                                                                        <div class="input-icon">
                                                                            <input type="checkbox" id="checkbox1" ng-model="actividad.form.EsActivo" numeric-type="integer" numeric="true" />
                                                                        </div>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h5><%= this.GetMessage("lblKit") %></h5>
                                                                    </div>
                                                                </div>
                                                                <%--<div class="row">
                                                                    <div class="col-md-12">
                                                                        <div id="marcamodelo">
                                                                            <div class="padding-top-7" style="overflow-y: auto">
                                                                                <table class="table table-condensed table-striped table-hover table-fixed" style="min-width: 500px"
                                                                                    st-table="actividad.ActividadMarcas">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th style="width: 50%"><%= this.GetMessage("lblMarca") %>
                                                                                            </th>
                                                                                            <th style="width: 20%"><%= this.GetMessage("lblModelos") %>
                                                                                            </th>
                                                                                            <th style="width: 20%"><%= this.GetMessage("lblRepuesto") %>
                                                                                            </th>
                                                                                            <th style="width: 5%" class="right"></th>
                                                                                            <th style="width: 5%" class="right">
                                                                                                <span class="cursor" ng-click="actividad.agregarActividadMarca()">
                                                                                                    <i class="fa fa-plus" style="padding-left: 5px"></i>&nbsp;
                                                                                                </span>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody style="max-height: 100%; min-height: 100%;">
                                                                                        <tr ng-repeat="item in actividad.ActividadMarcas">
                                                                                            <td style="width: 50%">
                                                                                                <div class="form-group">
                                                                                                      <div style="width: 400px;position:absolute;"> 
                                                                                                            <multiselect
                                                                                                                ng-model="item.MarcaSelectedID"
                                                                                                                options="item.MarcaID as item.Marca for item in actividad.Marcas"
                                                                                                                data-multiple="true"
                                                                                                                scroll-after-rows="8"
                                                                                                                ng-disabled="item.EsEditar"
                                                                                                                max-width="500"
                                                                                                                required
                                                                                                                tooltip="item.Descripcion"
                                                                                                                tabindex="-1">                        
                                                                                                         </multiselect>    
                                                                                                           </div>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                                <a class="cursor extrabtn btn-link" ng-show="item.EsEditar" ng-click="actividad.configurarModelos(item)" skip-disable><%= this.GetMessage("lblConfiguracion") %></a>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                                <a class="cursor extrabtn btn-link" ng-show="item.EsEditar" title="Configuration Spare Parts" ng-click="actividad.configurarRepuestos(item)" skip-disable><%= this.GetMessage("lblConfiguracion") %></a>
                                                                                            </td>
                                                                                            <td style="width: 5%" class="right">
                                                                                                <span class="cursor" ng-show="!item.EsEditar" ng-click="actividad.guardarActividadMarca(item,$index)"><i class="fa fa-floppy-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                                                <span class="cursor" ng-show="item.EsEditar" ng-click="actividad.editarActividadMarca(item,$index)"><i class="fa fa-pencil-square-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                                            </td>
                                                                                            <td style="width: 5%" class="right">
                                                                                                <span class="cursor" ng-click="actividad.eliminarActividadMarca($index)"><i class="fa fa-trash-o" style="padding-left: 5px"></i>&nbsp;</span>
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
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger btn-radius big-input" ng-disabled="actividad.form.ActividadID == 0" ng-click="actividad.confirmaEliminar()" ng-hide ="actividad.esSoloLectura">
                                        <%= this.GetMessage("lblEliminar") %>
                                    </button>
                                    <button type="button" class="green btn btn-success btn-radius big-input" ng-click="actividad.guardar()" ng-hide ="actividad.esSoloLectura">
                                        <%= this.GetMessage("lblGuardar") %>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="modal-long-repuestos" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
                    <div ng-form="actividad.FormRepuestos" ng-class="{'submitted': actividad.SetClassSummitRepuestoValid()}">
                        <div class="modal-dialog modal-wide-med" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" data-dismiss="modal" aria-hidden="true" class="close" skip-disable>&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="portlet box portlet-pink">
                                        <div class="portlet-body">
                                            <div role="form">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <input type="hidden" class="form-control" ng-model="actividad.formrepuestos.ActividadMarcaID" disabled="disabled" />
                                                            <div class="padding-top-7" style="overflow-y: auto">
                                                                <table class="table table-condensed table-striped table-hover table-fixed" style="min-width: 500px"
                                                                    st-table="actividad.MarcaRepuesto">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 20%"><%= this.GetMessage("lblCodigo") %>
                                                                            </th>
                                                                            <th style="width: 40%"><%= this.GetMessage("lblDescripcion") %>
                                                                            </th>
                                                                            <th style="width: 20%"><%= this.GetMessage("lblCantidad") %>
                                                                            </th>
                                                                            <th style="width: 5%" class="right"></th>
                                                                            <th style="width: 5%" class="right">
                                                                                <span class="cursor" ng-click="actividad.agregarRepuesto(actividad.formrepuestos.ActividadMarcaID)">
                                                                                    <i class="fa fa-plus" style="padding-left: 5px"></i>&nbsp;
                                                                                </span>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody style="max-height: 100%; min-height: 100%;">
                                                                        <tr ng-repeat="item in actividad.MarcaRepuesto | filter:{ActividadMarcaID:actividad.formrepuestos.ActividadMarcaID}">
                                                                              <td style="width: 20%" ng-hide="!item.EsEditar">
                                                                                  <input type="hidden" class="form-control" ng-model="item.RepuestoID" disabled="disabled" />
                                                                                  <input type="text" ng-disabled="item.EsEditar" value="{{item.RepuestoSel.Codigo}}" class="form-control" ng-model="item.Codigo" />
                                                                              </td>
                                                                              <td style="width: 40%" ng-hide="!item.EsEditar">
                                                                                <input type="text" ng-disabled="item.EsEditar" class="form-control" ng-model="item.Descripcion" />
                                                                              </td>
                                                                            <td style="width: 40%" ng-show="!item.EsEditar">
                                                                                <div style="width: 500px;position:absolute;">
                                                                                    <div class="form-group">
                                                                                        <ui-select
                                                                                            ng-model="item.RepuestoSel"
                                                                                            theme="bootstrap" style="width: 40%"
                                                                                            ng-disabled="item.EsEditar"
                                                                                            reset-search-input="false"
                                                                                            on-select="actividad.ValidarRepuesto($select.selected,item,actividad.formrepuestos.ActividadMarcaID)"
                                                                                            required>
                                                                                                    <ui-select-match placeholder="<%= this.GetMessage("lblBuscarAutocomplete") %>" >
                                                                                                        {{ $select.selected.Codigo || $select.selected}}
                                                                                                    </ui-select-match>
                                                                                                    <ui-select-choices repeat="item in actividad.RepuestosInfo | filter: $select.search"  refresh="actividad.buscarRepuestos($select.search)" refresh-delay="400" minimum-Input-Length="1" >
                                                                                                        <div ng-bind-html="item.Codigo | highlight: $select.search"></div>                                                                                                         
                                                                                                    </ui-select-choices>
                                                                                     </ui-select>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                          
                                                                            <td style="width: 20%">
                                                                                <input type="text" ng-disabled="item.EsEditar" class="form-control" ng-model="item.Cantidad" ng-paste="$event.preventDefault()" allow-pattern="\d" maxlength="2" required />
                                                                            </td>
                                                                            <td style="width: 5%" class="right">
                                                                                <span class="cursor" ng-show="!item.EsEditar" ng-click="actividad.guardarRepuesto(item,true)"><i class="fa fa-floppy-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                                <span class="cursor" ng-show="item.EsEditar" ng-click="actividad.guardarRepuesto(item,false)"><i class="fa fa-pencil-square-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                            </td>
                                                                            <td style="width: 5%" class="right">
                                                                                <span class="cursor" ng-click="actividad.eliminarRepuesto($index)"><i class="fa fa-trash-o" style="padding-left: 5px"></i>&nbsp;</span>
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
                <div id="modal-long-modelos" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
                    <div ng-form="actividad.FormModelos" ng-class="{'submitted': actividad.SetClassSummitModeloValid()}">
                        <div class="modal-dialog modal-wide-med" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" data-dismiss="modal" aria-hidden="true" class="close" skip-disable>&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="portlet box portlet-pink">
                                        <div class="portlet-body">
                                            <div role="form">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <input type="hidden" class="form-control" ng-model="actividad.formmodelos.ActividadMarcaID" disabled="disabled" />
                                                            <div class="padding-top-7" style="overflow-y: auto">
                                                                <table class="table table-condensed table-striped table-hover table-fixed" style="min-width: 500px"
                                                                    st-table="actividad.MarcaModelos">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 55%"><%= this.GetMessage("lblModelo") %>
                                                                            </th>
                                                                            <th style="width: 25%; text-align: center"><%= this.GetMessage("lblAnio") %>
                                                                            </th>
                                                                            <th style="width: 10%"><%= this.GetMessage("lblEsActivo") %>
                                                                            </th>
                                                                            <th style="width: 5%" class="right"></th>
                                                                            <th style="width: 5%" class="right">
                                                                                <span class="cursor" ng-click="actividad.agregarModelo(actividad.formmodelos.ActividadMarcaID)">
                                                                                    <i class="fa fa-plus" style="padding-left: 5px"></i>&nbsp;
                                                                                </span>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody style="max-height: 100%; min-height: 100%;">
                                                                        <tr ng-repeat="item in actividad.MarcaModelos | filter:{ActividadMarcaID:actividad.formmodelos.ActividadMarcaID}">
                                                                            <td style="width: 55%">
                                                                                <div style="width: 300px; position:absolute;">
                                                                                 <div class="form-group">
                                                                                      <multiselect
                                                                                            ng-model="item.ModSelectedID"
                                                                                            options="modelo.ModeloID as modelo.Modelo for modelo in actividad.formmodelos.modelosMarca"
                                                                                            data-multiple="true"
                                                                                            scroll-after-rows="5"
                                                                                            filter-after-rows="5"
                                                                                            ng-disabled="item.EsEditar"
                                                                                            max-width="500"
                                                                                            required
                                                                                            tooltip="item.Descripcion"
                                                                                            tabindex="-1">                        
                                                                                        </multiselect>
                                                                                   </div>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 25%">
                                                                                <datepicker-range datepicker-options="actividad.FormatoCalendario" ng-model="item.AnioModelos" label-separator="<%= this.GetMessage("lblSeparator") %>" is-disabled="{{ item.EsEditar }}" is-required="true" disable-all="actividad.esSoloLectura"></datepicker-range>
                                                                            </td>
                                                                            <td style="width: 10%">
                                                                                <input type="checkbox" ng-disabled="item.EsEditar" id="chkActivoModelo" ng-model="item.EsActivo" numeric-type="integer" numeric="true" />
                                                                            </td>
                                                                            <td style="width: 5%" class="right">
                                                                                <span class="cursor" ng-show="!item.EsEditar" ng-click="actividad.guardarModelo(item,true)"><i class="fa fa-floppy-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                                <span class="cursor" ng-show="item.EsEditar" ng-click="actividad.guardarModelo(item,false)"><i class="fa fa-pencil-square-o" style="padding-left: 5px"></i>&nbsp;</span>
                                                                            </td>
                                                                            <td style="width: 5%" class="right">
                                                                                <span class="cursor" ng-click="actividad.confirmaEliminarModelo(item)"><i class="fa fa-times" style="padding-left: 5px"></i>&nbsp;</span>
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
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=ruta%>js/pages/catalogos/Actividad.js?V00039"></script>


</asp:Content>
