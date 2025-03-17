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
            <li><a href="../menus/operations.aspx">Operation</a>&nbsp;</li>
            <li class="active"><%= this.GetMessage("Module") %></li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="stock.New()" ng-hide="stock.esSoloLectura">
                         <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAgregar") %></button>
                 </a>
                 <a id="lnkBuscar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="btnBuscar" type="button" ng-click="stock.Search()" skip-disable>
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
                                            <div class="col-sm-2">
                                                 <div class="input">
                                                    <div class="col-lg-12">Category</div>
                                                    <select class="form-control" ng-model="stock.filter.CategoryID"
                                                            ng-options="Category.CategoryID as Category.Category for Category in stock.Category" ng-change="stock.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Brand</div>
                                                    <select class="form-control" ng-model="stock.filter.BrandID"
                                                            ng-options="Brand.BrandID as Brand.Brand for Brand in stock.Brand" ng-change="stock.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Model</div>
                                                    <select class="form-control" ng-model="stock.filter.ModelID"
                                                            ng-options="Model.ModelID as Model.ModelName for Model in stock.Model" ng-change="stock.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="input">
                                                    <div class="col-lg-12">Stock ID</div>
                                                    <input placeholder="00000" type="text" class="form-control"
                                                        ng-model="stock.filter.StockNum" maxlength="6" allow-pattern="\d+" key-enter="stock.Search()" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                 <div class="input">
                                                    <div class="col-lg-12">Status</div>
                                                    <select class="form-control" ng-model="stock.filter.StockStatusID"
                                                            ng-options="StockStatus.StockStatusID as StockStatus.StockStatus for StockStatus in stock.StockStatus">
                                                    </select>                                                            
                                                </div>
                                            </div>
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
                                                         st-table="stock.Grouped" st-safe-src="stock.Grouped">
                                                         <thead>
                                                             <tr>
                                                                 <th class="hidden-xs" style="width: 20%">Category</th>
                                                                 <th style="width: 20%">Brand</th>
                                                                 <th style="width: 20%">Sku</th>
                                                                 <th style="width: 20%">Model</th>
                                                                 <th style="width: 20%">Qty</th>
                                                                 
                                                             </tr>
                                                         </thead>
                                                         <tbody style="max-height: 500px">
                                                             <tr ng-repeat="item in stock.Grouped">
                                                                 <td class="hidden-xs" style="width: 20%">{{item.Category}}</td>
                                                                 <td style="width: 20%">{{item.Brand}}</td>
                                                                 <td style="width: 20%">{{item.sku}}</td>
                                                                 <td style="width: 20%">{{item.Model}}</td>
                                                                 <td style="width: 20%">{{item.Qty}}</td>

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
                                            <div id="tabDetailed" class="tab-pane fade">
                                                <div class="row">
                                                     <table style="width: 95%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                                         st-table="stock.List" st-safe-src="stock.List">
                                                         <thead>
                                                             <tr>
                                                                 <th class="hidden-xs" style="width: 15%">Category</th>
                                                                 <th style="width: 15%">Brand</th>
                                                                 
                                                                 <th style="width: 20%">Model</th>
                                                                 <th style="width: 15%">Stock ID</th>
                                                                 <th style="width: 10%">Status</th>
                                                                 <th class="hidden-xs" style="width: 10%">Last Update</th>
                                                                 <th style="width: 5%"></th>
                                                             </tr>
                                                         </thead>
                                                         <tbody style="max-height: 500px">
                                                             <tr ng-repeat="item in stock.List">
                                                                 <td class="hidden-xs" style="width: 15%">{{item.Category}}</td>
                                                                 <td style="width: 15%">{{item.Brand}}</td>
                                                                
                                                                 <td style="width: 20%">{{item.ModelName}}</td>
                                                                 <td style="width: 15%">{{item.StockNum}}</td>
                                                                 <td style="width: 10%">{{item.StockStatus}}</td>
                                                                 <td class="hidden-xs" style="width: 10%">{{item.LastUpdate}}</td>
                                                                 <td style="width: 5%">
                                                                    <span class="cursor" ng-click="stock.Kardex(item)" skip-disable>
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
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>

                 </div>
               

        <div id="modal-long" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="stock.Form" ng-class="{'submitted': stock.SetClassSummitValid()}">
            <div class="modal-dialog modal-wide-med" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>New Core</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                             <div class="row">
                                     <div class="col-lg-12">
                                     <div class="col-lg-12">Stock ID:</div>
                                       <input type="text" ng-model="stock.form.StockNum" class="form-control" ng-value="0" numeric-type="integer" numeric="true" required allow-pattern="\d+" />
                                         <span style="text-decoration-color:gray;">Type "0" for new stock number</span>
                                   </div>
                             </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Category</div>
                                         <select class="form-control" ng-model="stock.form.CategoryID"
                                            ng-options="Category.CategoryID as Category.Category for Category in stock.CategoryFrm" ng-change="stock.LoadDrops(1)" required>
                                          </select>     
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Brand:</div>
                                         <select class="form-control" ng-model="stock.form.BrandID"
                                            ng-options="Brand.BrandID as Brand.Brand for Brand in stock.BrandFrm"  ng-change="stock.LoadDrops(2)" required>
                                          </select> 
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Model:</div>
                                         <select class="form-control" ng-model="stock.form.ModelID"
                                            ng-options="Model.ModelID as Model.ModelName for Model in stock.ModelFrm" required>
                                          </select>
                                      </div>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="col-lg-12">Cutomer / Vendor</div>


                                    <ui-select ng-model="stock.ContactSel" theme="bootstrap" style="width: 100%">
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
                                    <a href="#" ng-show="stock.ContactNotFound" ng-click="stock.NewContact">Create new</a>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Activity</div>
                                         <select class="form-control" ng-model="stock.form.StockActivityID"  required>
                                              <option value="10">Receive (for repair)</option>
                                               <option value="40">Receive (for stock)</option>
                                          </select>     
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="col-lg-10" ng-show="stock.form.StockActivityID == 10">
                                        <div class="col-lg-12">Applies warranty:</div>
                                         <input type="checkbox" ng-model="stock.form.Warranty" numeric-type="integer" numeric="true">
                                      </div>
                                </div>
                            </div>
                             <div class="row">
                             <div class="col-lg-12">
                                     <div class="col-lg-12">Notes:</div>
                                       <textarea ng-model="stock.form.Notes" rows="4" cols="100"></textarea>
                                   </div>
                             </div>
                        </div>
                    </div>
                    <div class="modal-footer">
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

               
     </div>

    <script type="text/javascript" language="javascript" src="Stock.js?V00039"></script>

</asp:Content>
