<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="stockKardex.aspx.cs" Inherits="WorkShop.pages.operation.stockKardex
    " %>
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

                 <a id="lnkBuscar" href="stock.aspx">
                     <button class="btn btn-primary btn-xs m-l-sm" id="btnBuscar" type="button"  skip-disable>
                         <i class="fa fa-arrow-left"></i>&nbsp;  <%= this.GetMessage("lblBack") %></button>
                 </a>
            </li>
         </ol>
    
        <span style="visibility:hidden">
        <input type="text" id="valStockID"  />
            </span>
        <script>

            document.getElementById("valStockID").value = "<%=StockID %>";

        </script>
       
        <div class="page-content" disable-all="stock.esSoloLectura">
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                   
                        <div class="col-lg-12">

                               
                                <div class="row">
                                        <div class="col-lg-1">
                                         </div>
                                        <div class="col-lg-5" style="border-style:solid; border-width:2px;">
                                             <div class="col-lg-12" style="text-align:center; font-weight:bold; font-size:12pt;">
                                                 Main information
                                             </div>   
                                           <div class="col-lg-12">
                                                <div class="col-lg-3">
                                                    Category:
                                                </div>
                                                <div class="col-lg-3">
                                                        {{ stock.Category }}
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                    <div class="col-lg-6">
                                                            <div class="col-lg-6">
                                                            Brand:
                                                            </div>
                                                            <div class="col-lg-6">
                                                                 {{ stock.Brand }}
                                                            </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                            <div class="col-lg-6">
                                                            Model:
                                                            </div>
                                                            <div class="col-lg-6">
                                                                 {{ stock.Model }}
                                                            </div>
                                                    </div>
                                            </div>
                                         </div>
                                        <div class="col-lg-1">
                                         </div>
                                        <div class="col-lg-4" style="border-style:solid; border-width:2px;">
                                            <div class="col-lg-12" style="text-align:right">
                                               <span class="cursor" ng-click="stock.PrintCode()" skip-disable>
                                                    <i class="fa fa-print" style="padding-left: 5px" skip-disable></i>&nbsp; 
                                                </span>
                                            </div>   
                                            <div class="row">
                                                 <div class="col-lg-12"  style="text-align:center; font-weight:bold; font-size:25pt;">
                                                    {{ stock.StockNum }}
                                                </div>

                                            </div>
                                            <div class="row" style="text-align:center; font-weight:bold; font-size:25pt;">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-lg-11" style="text-align:center;">
                                                     <div class="col-lg-6">
                                                        {{ stock.StockStatus }}
                                                    </div>
                                                     <div class="col-lg-6" style="text-align:right;  font-size:8pt;">
                                                        {{ stock.LastUpdate }}
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                         <div class="col-lg-1">
                                         </div>
                                </div>
                           
                                <br /><br />
                                </div>
                           
                             <div class="row">
                                <div class="col-lg-1">
                                    </div>
                            <div class="col-lg-10" style="text-align:center;">
                                <div class="row" style="text-align:center; font-weight:bold; font-size:14pt;">
                                        History
                                </div>                               
                                <div class="row">
                                        <div class="col-lg-12"  style="text-align:center">
                                  
                                        <table style="width: 100%; align-content:left" class="table table-condensed table-striped table-hover table-fixed" 
                                            st-table="stock.Grouped"  >
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%; text-align:center">Date</th>
                                                    <th style="width: 10%; text-align:center">Activity</th>
                                                    <th style="width: 20%; text-align:center">Customer / Vendor</th>
                                                    <th style="width: 10%; text-align:center">Unit Ref</th>
                                                    <th style="width: 10%; text-align:center">Replace (from/to)</th>
                                                    <th style="width: 20%; text-align:center">Notes</th>
                                                    <th style="width: 15%; text-align:center">Changed by</th>  
                                                    <th style="width: 5%; text-align:center">
                                                        <a  id="lnkAgregar" href="#">
                                                             <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="stock.NewHistory(0)" ng-hide="stock.esSoloLectura">
                                                                 <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAdd") %></button>
                                                         </a> 

                                                    </th> 
                                                </tr>
                                            </thead>
                                            <tbody style="max-height: 500px">
                                                <tr ng-repeat="item in stock.History">
                                                    <td style="width: 10%">{{item.AuditDateShort}}</td>
                                                    <td style="width: 10%">{{item.StockActivity}}</td>
                                                    <td style="width: 20%">
                                                        <a href="#" ng-click="stock.ShowActivities(item)" title="Show activities">{{item.Customer}}</a>
                                                     </td>
                                                    <td style="width: 10%">{{item.UnitRef}}</td>
                                                    <td style="width: 10%">
                                                        <a href="StockKardex.aspx?StockID={{item.StockIDReplace}}" title="Show activities">{{item.StockReplace}}</a></td>
                                                    <td style="width: 20%">{{item.NotesShort}}</td>
                                                    <td style="width: 15%">{{item.UserName}}</td>
                                                    <td style="width: 5%">
                                                    <span class="cursor" ng-click="stock.NewHistory(item)" skip-disable>
                                                        <i class="fa fa-eye" style="padding-left: 5px" skip-disable></i>&nbsp; 
                                                    </span>
                                                        <a ng-show="item.StockActivityID == 10" href="../CreatePDF.aspx?TypePDF=1&HistoryID={{item.StockHistoryID}}" target"_blank"> <i class="fa fa-file-pdf-o" style="padding-left: 5px" skip-disable></i></a>
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
                                <br />
                                 <div class="row" style="text-align:center; font-weight:bold; font-size:14pt;">
                                        Repairs<br />
                                </div>

                                <div class="row">
                                    
                                    <table style="width: 100%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                        st-table="stock.Repairs" >
                                        <thead>
                                            <tr>
                                                <th style="width: 15%; text-align:center">Technician</th>
                                                <th style="width: 15%; text-align:center">Activity</th>
                                                <th style="width: 15%; text-align:center">Status</th>
                                                <th style="width: 35%; text-align:center">Notes</th>
                                                <th class="hidden-xs" style="width: 15%; text-align:center">Last Update</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody style="max-height: 500px">
                                            <tr ng-repeat="item in stock.Repairs">
                                                <td style="width: 15%">{{item.Technician}}</td>
                                                <td style="width: 15%">{{item.RepairActivity}}</td>
                                                <td style="width: 15%">{{item.RepairStatus}}</td>
                                                <td style="width: 35%">{{item.Notes}}</td>
                                                <td class="hidden-xs" style="width: 15%">{{item.LastUpdate}}</td>
                                                <td style="width: 5%">
                                                <span class="cursor" ng-click="stock.RepairView(item)" ng-show="false" skip-disable>
                                                    <i class="fa fa-eye" style="padding-left: 5px" skip-disable></i>&nbsp; 
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
                            </div>
                               <div class="col-lg-1">
                                    </div>
                            </div>
                            </div>
 

                 </div>
               
        <%-- Moda de Historia --%>
        <div id="modal-long" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="stock.Form" ng-class="{'submitted': stock.SetClassSummitValid()}">
            <div class="modal-dialog modal-wide-med" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>New activity</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Activity</div>
                                         <select class="form-control" ng-model="stock.form.StockActivityID"
                                            ng-options="StockActivity.StockActivityID as StockActivity.StockActivity for StockActivity in stock.StockActivity"  required>
                                          </select>     
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="col-lg-10" ng-show="stock.form.StockActivityID == 10">
                                        <div class="col-lg-6">
                                            <div class="col-lg-12">Applies warranty:</div>
                                             <input type="checkbox" ng-model="stock.form.Warranty" numeric-type="integer" numeric="true">
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="col-lg-12">Replacement:</div>
                                             <input type="checkbox" ng-model="stock.form.Replacement" numeric-type="integer" numeric="true">
                                        </div>
                                      </div>
                                </div>
                                <div class="col-lg-4" ng-hide="!stock.form.Replacement">
                                     <div class="col-lg-12">Replace with Stock ID:</div>
                                       <input type="text" ng-model="stock.form.StockReplace" numeric-type="integer" key-enter="stock.StockFind()" ng-readonly="stock.form.StockIDReplace != 0" numeric="true" required allow-pattern="\d+" />
                                        <button class="btn btn-primary btn-sm m-l-sm" type="button" ng-click="stock.StockFind()" ng-hide="stock.form.StockIDReplace != 0">
                                         <i class="fa fa-search"></i></button>
                                        <button class="green btn btn-success btn-radius btn-sm m-l-sm" type="button" ng-click="stock.form.StockIDReplace = 0; stock.form.StockReplace = '';" ng-hide="stock.form.StockIDReplace == 0">
                                            <i class="fa fa-check"></i></button><br />
                                         <span style="color:red;">{{ stock.form.Message }}</span>
                                </div>
                            </div>
                            <div class="row" ng-show="stock.form.StockActivityID != 30">
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Customer / vendor:</div>
                                                <ui-select ng-model="stock.ContactSel" theme="bootstrap" style="width: 100%" ng-show="!stock.ContactNew">
                                                <ui-select-match >
                                                    {{ $select.selected.ContactName || $select.selected}}
                                                </ui-select-match>
                                                <ui-select-choices repeat="contact in stock.ContactsFilter | filter: $select.search"  refresh="stock.searchContact($select.search)" refresh-delay="400" minimum-Input-Length="4" >
                                                    <b><div ng-bind-html="contact.ContactName | highlight: $select.search"></div> </b>
                                                    <small >                                                                                                                                           
                                                        {{contact.AddressComplete }}&nbsp;, {{contact.City }}&nbsp;, {{contact.State }}
                                                    </small>     
                                                </ui-select-choices>
                                            </ui-select>
                                                <a href="#" ng-show="stock.ContactNotFound" ng-click="stock.ContactNew = true; stock.ContactNotFound = false;">Create new</a>
                                                 <input type="text" class="form-control" ng-model="stock.CustomerName" ng-show="stock.ContactNew"  placeholder="Type new customer or vendor"   ng-required="stock.ContactNew"/>

<%--                                         <input  type="text" class="form-control"  ng-model="stock.form.Customer" maxlength="50" ng-required="stock.form.StockActivityID != 30" />--%>
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Contact name:</div>
                                         <input  type="text" class="form-control"  ng-model="stock.form.ContactName" maxlength="50" ng-required="stock.form.StockActivityID != 30" />
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Unit ref:</div>
                                         <input  type="text" class="form-control"  ng-model="stock.form.UnitRef" maxlength="50"  />
                                      </div>
                                </div>
                            </div>
                            <div class="row" ng-show="stock.form.StockActivityID != 30">
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Contact phone:</div>
                                         <input  type="text" class="form-control"  ng-model="stock.form.ContactPhone" maxlength="50" ng-required="stock.form.StockActivityID != 30"  />
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Contact email:</div>
                                         <input  type="text" class="form-control"  ng-model="stock.form.ContactEmail" maxlength="50" ng-pattern="emailPattern"  />
                                      </div>
                                </div>
                            </div>
                             <div class="row">
                             <div class="col-lg-12">
                                     <div class="col-lg-12">Notes:</div>
                                       <textarea ng-model="stock.form.Notes" rows="4" cols="100" ng-required="stock.form.StockActivityID == 30" ></textarea>
                                   </div>
                             </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                       <div class="row">
                        <div class="col-lg-8" style="font-size:8pt; text-align:left">
        
                                  By: {{stock.historyBy}}
                                  Date: {{stock.historyDate}}
                        </div>
                       <div class="col-lg-4">
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="stock.Save()" ng-hide ="stock.esSoloLectura">
                            Save
                        </button>
                           </div>
                          </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <%-- Moda de activities --%>
        <div id="modal-long2" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="stock.Form">
            <div class="modal-dialog modal-wide-med" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>Customer/vendor activity</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12" style="text-align:center">
                                <table style="width: 90%; align-content:left" class="table table-condensed table-striped table-hover table-fixed" 
                                    st-table="stock.ContactKardex"  >
                                    <thead>
                                        <tr>
                                            <th style="width: 10%; text-align:center">Stock</th>
                                            <th style="width: 10%; text-align:center">Model</th>
                                            <th style="width: 20%; text-align:center">Activity</th>
                                            <th style="width: 10%; text-align:center">Warranty</th>
                                            <th style="width: 5%; text-align:center">Unit Ref</th>
                                            <th style="width: 20%; text-align:center">Notes</th>
                                            <th style="width: 10%; text-align:center">Date</th>  
                                        </tr>
                                    </thead>
                                    <tbody style="max-height: 500px">
                                        <tr ng-repeat="item in stock.ContactKardex">
                                            <td style="width: 10%">{{item.StockNum}}</td>
                                            <td style="width: 10%">{{item.ModelName}}</td>
                                            <td style="width: 20%">{{item.StockActivity}}</td>
                                            <td style="width: 10%"><input type="checkbox" ng-checked="{{item.Warranty}}" value="{{item.Warranty}}" disabled /></td>
                                            <td style="width: 5%">{{item.UnitRef}}</td>
                                            <td style="width: 20%">{{item.Notes}}</td>
                                            <td style="width: 10%">{{item.LastUpdate}}</td>
                                        </tr>
                                    </tbody>
                                </table>.
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



    <script type="text/javascript" language="javascript" src="StockKardex.js?V00039"></script>

   
</asp:Content>
