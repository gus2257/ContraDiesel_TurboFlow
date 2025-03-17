<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WorkShop.pages.catalogos.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="ContactController as control">

        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-tag fa-fw"></i></li>
            <li><a href="../menus/config.aspx">Configuration</a>&nbsp;</li>
            <li class="active">Customers & Vendors</li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="control.Edit(0)" ng-hide="control.esSoloLectura">
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
                                                    <div class="col-lg-12">Vendors & Customers</div>
                                                    <input placeholder="Type name to search" type="text" class="form-control"
                                                        ng-model="control.filter.ContactName"  key-enter="control.Search()" />
                                                </div>
                                            </div>
                                           
                                        </div>
                                <br />
                                <div class="row">
                                  
                                    <table style="width: 1000px;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                        st-table="control.Contacts" >
                                        <thead>
                                            <tr>
                                                <th style="width: 25%">Vendor / Customer</th>
                                                <th style="width: 25%">Address</th>
                                                <th style="width: 20%">Contact name</th>
                                                <th style="width: 10%">Phone</th>
                                                <th style="width: 10%">Activiy</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody style="max-height: 1000px">
                                            <tr ng-repeat="item in control.Contacts">
                                                <td style="width: 25%">{{item.ContactName}}</td>
                                                <td style="width: 25%">{{item.AddressComplete}}</td>
                                                <td style="width: 20%">{{item.PersonName}}</td>
                                                <td style="width: 10%">{{item.Phone}}</td>
                                                <td style="width: 10%">
                                                     <input type="checkbox" ng-model="item.Active" ng-checked="{{item.Active}}" value="{{item.Active}}" disabled /> 
                                                </td>
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
                        <h4>Vendor / Customer</h4>

                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                             <div class="row">
                                <div class="col-lg-6">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Company</div>
                                          <input type="text" class="form-control" ng-model="control.form.ContactName" required/> 
                                    </div>
                                </div>
                                   <div class="col-lg-3">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">AKA</div>
                                          <input type="text" class="form-control" ng-model="control.form.Aka"/> 
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Active</div>
                                         <input  type="checkbox" ng-model="control.form.Active" /> 

                                      </div>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-2" style="font-size:12pt;">
                                        <i class="fa fa-home" class="form-control" style="display:inline"></i>&nbsp;Address
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-lg-6">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Address 1</div>
                                          <input type="text" class="form-control" ng-model="control.form.Address01" required/> 
                                    </div>
                                </div>
                                   <div class="col-lg-6">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Address 2</div>
                                          <input type="text" class="form-control" ng-model="control.form.Address02"/> 
                                    </div>
                                </div>
                               
                            </div>
                             <div class="row">
                                <div class="col-lg-6">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">City</div>
                                          <input type="text" class="form-control" ng-model="control.form.City" required/> 
                                    </div>
                                </div>
                                   <div class="col-lg-3">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">State</div>
                                          <input type="text" class="form-control" ng-model="control.form.State" required/> 
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                  <div class="col-lg-10">
                                        <div class="col-lg-12">Country</div>
                                         <input type="text" class="form-control" ng-model="control.form.Country" required/> 

                                      </div>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                <div class="col-lg-10">
                                    <div class="col-lg-12">Office Phone</div>
                                      <input type="text" class="form-control" ng-model="control.form.Phone"/> 
                                </div>
                                </div>
                                 <div class="col-lg-3">
                                 <div class="col-lg-10">
                                     <div class="col-lg-12">Postal Code</div>
                                       <input type="text" class="form-control" ng-model="control.form.PostalCode"/> 
                                 </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-2" style="font-size:12pt;">
                                        <i class="fa fa-user" class="form-control" style="display:inline"></i>&nbsp;Contact
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Person</div>
                                          <input type="text" class="form-control" ng-model="control.form.PersonName"/> 
                                    </div>
                                </div>
                                   <div class="col-lg-3">
                                    <div class="col-lg-10">
                                        <div class="col-lg-12">Phone</div>
                                          <input type="text" class="form-control" ng-model="control.form.PersonPhone"/> 
                                    </div>
                                </div>                                
                            </div>
                            <div class="row">
                            <div class="col-lg-6">
                                <div class="col-lg-10">
                                    <div class="col-lg-12">Email</div>
                                      <input type="text" class="form-control" ng-model="control.form.PersonEmail"/> 
                                </div>
                                </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                           <div class="row">
                                <div class="col-lg-6" style="text-align:left; font-size:8pt;">
                                    Updated by: {{control.info.AuditName}} @ {{control.info.LastUpdate}}
                                </div>
                                   <div class="col-lg-6">
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
        </div>

               
     </div>

    <script type="text/javascript" language="javascript" src="Contact.js?V00039"></script>

</asp:Content>
