<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WorkShop.pages.reportes.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div ng-controller="Dashboard as dashboard">
        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-bar-chart fa-fw"></i></li>
            <li><%= this.GetMessage("lblReportes") %> &nbsp;</li>
            <li class="active"><%= this.GetMessage("lblAlias") %></li>
            
        </ol>
        <div class="page-content">
            <div id="Principal" class="margin-right50">
                <div class="row wrapper border-bottom white-bg">
                    <div id="Filtros">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-tools pull-right">
                                    <div class="row">
                                        <div class="col-lg-12" style="text-align:right; font-size:12pt; font-weight:bold">
                                                Last Update: {{ dashboard.LastUpdate }}
                                            
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-5">
                                             <div class="col-lg-12" style="text-align:center; font-size:20pt; font-weight:bold">
                                                 <%= this.GetMessage("lblWO") %><br />
                                                 <%= this.GetMessage("lblCompleted") %>
                                              </div>
                                              <div class="col-lg-12">
                                                 Closed orders (this month): {{ dashboard.MecanicosQty }}
                                              </div>
                                            <div class="col-lg-12">
                                            <div class="padding-top-7" style="overflow-y: auto">
                                                <table style="width: 95%" class="col-lg-12 table table-condensed table-striped table-hover table-fixed"
                                                    st-table="dashboard.MecanicosRep">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 38%; vertical-align:middle"><%= this.GetMessage("lblMecanico") %>
                                                            </th>
                                                            <th style="width: 12%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblMes") %>
                                                            </th>
                                                           <%-- <th style="width: 8%"><%= d6 %>
                                                            </th>
                                                            <th style="width: 8%"><%= d5 %>
                                                            </th>--%>
                                                            <th style="width: 10%; text-align:center; vertical-align:middle"><%= d4 %>
                                                            </th>
                                                            <th style="width: 10%; text-align:center; vertical-align:middle"><%= d3 %>
                                                            </th>
                                                            <th style="width: 10%; text-align:center; vertical-align:middle"><%= d2 %>
                                                            </th>
                                                            <th style="width: 10%; text-align:center; vertical-align:middle"><%= d1 %>
                                                            </th>
                                                            <th style="width: 10%; text-align:center; vertical-align:middle"><%= d0 %>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-repeat="item in dashboard.MecanicosRep">
                                                            <td style="width: 38%">{{item.Mecanico}}</td>
                                                            <td style="width: 12%; text-align:center">{{item.ThisMonth}}</td>
<%--                                                            <td style="width: 8%; text-align:center">{{item.D6}}</td>
                                                            <td style="width: 8%; text-align:center%">{{item.D5}}</td>--%>
                                                            <td style="width: 10%; text-align:center">{{item.D4}}</td>
                                                            <td style="width: 10%; text-align:center">{{item.D3}}</td>
                                                            <td style="width: 10%; text-align:center">{{item.D2}}</td>
                                                            <td style="width: 10%; text-align:center">{{item.D1}}</td>
                                                            <td style="width: 10%; text-align:center">{{item.D0}}</td>
                                                        </tr>
                                                    </tbody>
                                                    
                                                </table>
                                                <br />

                                            </div>
                                            </div>
                                        </div>
                                         <div class="col-lg-4">
                                             <div class="col-lg-12" style="text-align:center; font-size:20pt; font-weight:bold">
                                                 <%= this.GetMessage("lblWO") %><br />
                                                 <%= this.GetMessage("lblInProcess") %><br />
                                              </div>
                                             <div class="col-lg-12">
                                               Open orders: {{ dashboard.UnidadesQty }}
                                            </div>
                                            <div class="col-lg-12">
                                            <div class="padding-top-7" style="overflow-y: auto">
                                                <table style="width: 95%" class="col-lg-12 table table-condensed table-striped table-hover table-fixed"
                                                    st-table="dashboard.MecanicosRep">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 20%; vertical-align:middle"><%= this.GetMessage("lblUnidad") %>
                                                            </th>
                                                            <th style="width: 25%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblOrden") %>
                                                            </th>
                                                          <th style="width: 30%; vertical-align:middle"><%= this.GetMessage("lblMecanico") %>
                                                            </th>
                                                            <%--<th style="width: 15%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblSpear") %>
                                                                 </th>--%>
                                                            <th style="width: 15%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblLifeTime") %>
                                                            </th>                                                         
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-repeat="item in dashboard.UnidadesRep">
                                                            <td style="width: 20%">{{item.Type}}:{{item.NumUnidad}}</td>
                                                            <td style="width: 25%; text-align:center">{{item.NumOrdenServicio}}</td>
                                                            <td style="width: 30%">{{item.Mecanico}}</td>
                                                            <%--<td style="width: 15%; text-align:center">{{item.Costo | currency }}</td>--%>
                                                            <td style="width: 15%; text-align:center">{{item.LifeTime}}</td>
                                                        </tr>
                                                    </tbody>
                                                    
                                                </table>
                                                <br />

                                            </div>

                                                </div>
                                        </div>
                                        <div class="col-lg-3">
                                             <div class="col-lg-12" style="text-align:center; font-size:20pt; font-weight:bold">
                                                
                                                 <%= this.GetMessage("lblInspection") %><br />
                                                 (Today)
                                              </div>
                                             <div class="col-lg-12">
                                              Inspections: {{ dashboard.InspeccionesQty }}
                                            </div>
                                            <div class="col-lg-12">
                                            <div class="padding-top-7" style="overflow-y: auto">
                                                <table style="width: 95%" class="col-lg-12 table table-condensed table-striped table-hover table-fixed"
                                                    st-table="dashboard.InspeccionesRep">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 30%; vertical-align:middle"><%= this.GetMessage("lblHora") %>
                                                            </th>
                                                            <th style="width: 20%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblEntrada") %>
                                                            </th>
                                                            <th style="width: 30%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblSalida") %>
                                                            </th>
                                                            <th style="width: 20%; text-align:center; vertical-align:middle"><%= this.GetMessage("lblTotal") %>
                                                            </th>                                                         
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-repeat="item in dashboard.InspeccionesRep">
                                                            <td style="width: 30%">{{item.Hora}}</td>
                                                            <td style="width: 20%; text-align:center">{{item.Inbound}}</td>
                                                            <td style="width: 30%; text-align:center">{{item.Outbound}}</td>
                                                            <td style="width: 20%; text-align:center">{{item.Total}}</td>
                                                        </tr>
                                                    </tbody>
                                                    
                                                </table>
                                                <br />

                                            </div>
                                             </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/reportes/dashboard.js?V00039"></script>

</asp:Content>
