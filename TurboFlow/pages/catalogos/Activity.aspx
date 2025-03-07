<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="WorkShop.pages.catalogos.activity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="ActivityController as control">

        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-tag fa-fw"></i></li>
            <li><a href="../menus/config.aspx">Configuration</a>&nbsp;</li>
            <li class="active">Activities</li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="control.New()" ng-hide="control.esSoloLectura">
                         <i class="fa fa-plus"></i>&nbsp; Add</button>
                 </a>
                 <a id="lnkBuscar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="btnBuscar" type="button" ng-click="control.Search()" skip-disable>
                         <i class="fa fa-search"></i>&nbsp; Search</button>
                 </a>
            </li>
         </ol>
    
       
        <div class="page-content" >
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                   
                        <div class="col-lg-12">
                                <div class="row">
                                        
                                            <div class="col-sm-2">
                                                <div class="input">
                                                    <div class="col-lg-12">Activity</div>
                                                    <input placeholder="Type activity name" type="text" class="form-control"
                                                        ng-model="control.filter.Activity"  key-enter="control.Search()" />
                                                </div>
                                            </div>
                                           
                                        </div>
                                <br />
                                <div class="row">
                                  
                                    <table style="width: 50%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                        st-table="control.Repairs" >
                                        <thead>
                                            <tr>
                                                <th style="width: 60%">Activiy</th>
                                                <th style="width: 20%">Active</th>
                                                <th class="hidden-xs" style="width: 15%">Last Update</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody style="max-height: 500px">
                                            <tr ng-repeat="item in control.Activities">
                                                <td style="width: 60%">{{item.RepairActivity}}</td>

                                                <td style="width: 20%">
                                                     <input type="checkbox" ng-model="item.Active" ng-checked="{{item.Active}}" value="{{item.Active}}" disabled /> 
                                                </td>
                                                <td class="hidden-xs" style="width: 15%">{{item.LastUpdate}}</td>
                                                <td style="width: 5%">
                                                <span class="cursor" ng-click="control.Edit(item)" skip-disable>
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
                <div class="modal-content" disable-all="control.esSoloLectura">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-hidden="true"
                            class="close" skip-disable>
                            &times;</button>
                        <h4>Repair activity</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                             <div class="row">
                                <div class="col-lg-8">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Activity</div>
                                          <input type="text" ng-model="control.form.RepairActivity" required/> 
                                    </div>
                                </div>
                               
                                 <div class="col-lg-4">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Active</div>
                                         <input  type="checkbox" ng-model="control.form.Active" /> 

                                      </div>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="control.Save()" ng-hide="control.esSoloLectura">
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

    <script type="text/javascript" language="javascript" src="Activity.js?V00039"></script>

</asp:Content>
