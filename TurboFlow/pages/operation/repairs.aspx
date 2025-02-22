<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="repairs.aspx.cs" Inherits="WorkShop.pages.operation.repairs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="RepairController as control">

        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-tag fa-fw"></i></li>
            <li><%= this.GetMessage("MainModule") %> &nbsp;</li>
            <li class="active"><%= this.GetMessage("Module") %></li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="control.New()" ng-hide="control.esSoloLectura">
                         <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAgregar") %></button>
                 </a>
                 <a id="lnkBuscar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="btnBuscar" type="button" ng-click="control.RepairLoad()" skip-disable>
                         <i class="fa fa-search"></i>&nbsp;  <%= this.GetMessage("lblBuscar") %></button>
                 </a>
            </li>
         </ol>
    
       
        <div class="page-content" disable-all="control.esSoloLectura">
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
                                                    <select class="form-control" ng-model="control.filter.CategoryID"
                                                            ng-options="Category.CategoryID as Category.Category for Category in control.Category" ng-change="control.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Brand</div>
                                                    <select class="form-control" ng-model="control.filter.BrandID"
                                                            ng-options="Brand.BrandID as Brand.Brand for Brand in control.Brand" ng-change="control.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                 <div class="input">
                                                    <div class="col-lg-12">Model</div>
                                                    <select class="form-control" ng-model="control.filter.ModelID"
                                                            ng-options="Model.ModelID as Model.ModelName for Model in control.Model" ng-change="control.ReloadFilters()">
                                                    </select>                                                            
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="input">
                                                    <div class="col-lg-12">Stock ID</div>
                                                    <input placeholder="00000" type="text" class="form-control"
                                                        ng-model="control.filter.StockNum" maxlength="6" allow-pattern="\d+" key-enter="control.Search()" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                 <div class="input">
                                                    <div class="col-lg-12">Status</div>
                                                     <select class="form-control" ng-model="control.filter.RepairStatusID">
                                                            <option value="0">Show all</option>
                                                            <option value="1">Show open</option>
                                                            <option value="10">Pending</option>
                                                            <option value="20">In process</option>
                                                            <option value="30">Completed</option>
                                                            <option value="40">Hold</option>
                                                            <option value="50">Unable to fix</option>
                                                      </select>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                </div>
                                <br />
                                <div class="row">
                                  
                                    <table style="width: 95%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                        st-table="control.Repairs" >
                                        <thead>
                                            <tr>
                                                <th style="width: 15%">Stock ID</th>
                                                <th style="width: 15%">Type</th>
                                                <th style="width: 15%">Technician</th>
                                                <th style="width: 15%">Activity</th>
                                                <th style="width: 15%">Status</th>
                                                <th class="hidden-xs" style="width: 15%">Last Update</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody style="max-height: 500px">
                                            <tr ng-repeat="item in control.Repairs">
                                                <td style="width: 15%">{{item.StockNum}}</td>
                                                <td style="width: 15%">{{item.StockType}}</td>
                                                <td style="width: 15%">{{item.Technician}}</td>
                                                <td style="width: 15%">{{item.RepairActivity}}</td>
                                                <td style="width: 15%">{{item.RepairStatus}}</td>
                                                <td class="hidden-xs" style="width: 15%">{{item.LastUpdate}}</td>
                                                <td style="width: 5%">
                                                <span class="cursor" ng-click="control.RepairEdit(item)" skip-disable>
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
               

        <div id="modal-long" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="control.Form" ng-class="{'submitted': control.SetClassSummitValid()}">
            <div class="modal-dialog modal-wide-med" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>New Repair</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                             <div class="row">
                                     <div class="col-lg-6">
                                     <div class="col-lg-12">Stock ID:</div>
                                       <input type="text" ng-model="control.form.StockNum" numeric-type="integer" key-enter="control.StockFind()" ng-disabled="!control.form.IsNew" numeric="true" required allow-pattern="\d+" />
                                        <button class="btn btn-primary btn-sm m-l-sm" type="button" ng-click="control.StockFind()" ng-visible="control.form.IsNew" >
                                       <i class="fa fa-search"></i></button>
                                         <span style="color:red;">{{ control.form.Message }}</span>
                                   </div>
                             </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Category</div>
                                          <input type="text" ng-model="control.form.Category" ng-disabled="true"/> 
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Brand:</div>
                                         <input type="text" ng-model="control.form.Brand" ng-disabled="true"/> 
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Model:</div>
                                         <input type="text" ng-model="control.form.Model" ng-disabled="true"/> 
                                      </div>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="control.RepairCreate()" ng-hide="control.esSoloLectura || control.form.StockID == 0">
                            Create
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <div id="modal-long2" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="control.Form" ng-class="{'submitted': control.SetClassSummitValid()}">
            <div class="modal-dialog modal-wide-med" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>New Repair</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                             <div class="row">
                                     <div class="col-lg-4">
                                     <div class="col-lg-12">Stock ID:</div>
                                          {{ control.form.StockNum }} 
                                       <%--<input type="text" ng-model="control.form.StockNum" numeric-type="integer" ng-disabled="true" />--%>
                                   </div>
                                    <div class="col-lg-8">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Status:</div>
                                          {{ control.form.RepairStatus }} 
                                    </div>
                                    </div>
                                
                             </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Category</div>
                                             {{ control.form.Category }} 
                                         <%-- <input type="text" ng-model="control.form.Category" ng-disabled="true"/> --%>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Brand:</div>
                                         {{ control.form.Brand }} 
                                         <%--<input type="text" ng-model="control.form.Brand" ng-disabled="true"/> --%>
                                      </div>
                                </div>
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Model:</div>
                                       {{ control.form.Model }} 
                                        <%-- <input type="text" ng-model="control.form. {{ control.form.Model }} " ng-disabled="true"/> --%>
                                      </div>
                                </div>
                           
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Technician</div>
                                            <select class="form-control" ng-model="control.form.TechnicianID" ng-required="control.form.RepairID > 0"
                                                ng-options="Technicians.UsuarioID as Technicians.UsuarioNombre for Technicians in control.Technicians" >
                                            </select> 
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Activity</div>
                                         <select class="form-control" ng-model="control.form.RepairActivityID"  ng-required="control.form.RepairStatusID == 20"
                                            ng-options="Activities.RepairActivityID as Activities.RepairActivity for Activities in control.Activities">
                                        </select>
                                    </div>
                                </div>


                            </div>
                             <div class="row">
                             <div class="col-lg-12">
                                     <div class="col-lg-12">Parts Lot:</div>
                                       <textarea ng-model="control.form.Lot" rows="2" cols="100" ng-required="control.form.RepairStatusID == 20"> rows="4" cols="100"></textarea>
                                   </div>
                             </div>
                            <div class="row">
                             <div class="col-lg-12">
                                     <div class="col-lg-12">Notes:</div>
                                       <textarea ng-model="control.form.Notes" rows="4" cols="100"></textarea>
                                   </div>
                             </div>
                             <div class="row">
                             <div class="col-lg-12">
                                      Updated by:{{ control.form.UpdatedBy }} @ {{ control.form.LastUpdate }}
                             </div>
                             </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                          <button type="button" class="red btn btn-success btn-radius big-input" ng-click="control.RepairSave(50)" ng-show="control.form.RepairStatusID == 20">
                            Unable Repair
                        </button>
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="control.RepairSave(20)" ng-show="control.form.RepairStatusID == 10">
                            Start Repair
                        </button>
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="control.RepairSave(30)" ng-show="control.form.RepairStatusID == 20">
                            Close Repair
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

            </div>
        </div>

               
     </div>

    <script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/operation/Repairs.js?V00039"></script>

</asp:Content>
