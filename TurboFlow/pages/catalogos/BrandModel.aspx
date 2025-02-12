<%@ Page Title="" Language="C#" MasterPageFile="~/include/master.Master" AutoEventWireup="true" CodeBehind="BrandModel.aspx.cs" Inherits="WorkShop.pages.catalogos.brandmodel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

        <div ng-controller="BrandModelController as brandmodel">

        <ol class="breadcrumb page-breadcrumb">
            <li><i class="fa fa-tag fa-fw"></i></li>
            <li><%= this.GetMessage("MainModule") %> &nbsp;</li>
            <li class="active"><%= this.GetMessage("Module") %></li>
            <li class="notSlide pull-right margin10"> 
                <a  id="lnkAgregar" href="#">
                     <button class="btn btn-primary btn-xs m-l-sm" id="edit" type="button" ng-click="brandmodel.BrandNew()" ng-hide="stock.esSoloLectura">
                         <i class="fa fa-plus"></i>&nbsp; <%= this.GetMessage("lblAgregar") %></button>
                 </a>
            </li>
         </ol>
    
        <div class="row">&nbsp;</div>
       
        <div class="page-content" disable-all="stock.esSoloLectura">
            <div id="Principal">
                <div class="row wrapper border-bottom white-bg">
                        <div class="col-lg-12">
                            <div class="row">
                                   
                                    <table style="width: 50%;" class="col-lg-12 table table-condensed table-striped table-hover table-fixed" 
                                        st-table="brandmodel.Brands" st-safe-src="brandmodel.Brands">
                                        <thead>
                                            <tr>
                                                <th style="width: 50%">Brand</th>
                                                <th style="width: 40%">Active</th>
                                                <th style="width: 10%"></th>
                                            </tr>
                                        </thead>
                                        <tbody style="max-height: 500px">
                                            <tr ng-repeat="item in brandmodel.Brands">
                                                <td style="width: 50%">{{item.Brand}}</td>
                                                
                                                <td style="width: 40%">
                                                    <input type="checkbox" ng-model="item.Active" ng-checked="{{item.Active}}" value="{{item.Active}}" disabled /> 
                                                </td>
                                                <td style="width: 10%">
                                                    <span class="cursor" ng-click="brandmodel.BrandEdit(item)" title="Edit" skip-disable> 
                                                        <i class="fa fa-pencil-square-o" style="padding-left: 5px"  skip-disable></i> 
                                                            
                                                    </span>
                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>                                                
                            </div>
                    </div>
                </div>
                                

        <div id="modal-long" tabindex="-1" data-replace="true" class="modal fade" data-backdrop="static" data-keyboard="false">
        <div ng-form="brandmodel.Form" ng-class="{'submitted': stock.SetClassSummitValid()}">
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
                                     <div class="col-lg-7">
                                     <div class="col-lg-12">Brand:</div>
                                       <input type="text" ng-model="brandmodel.form.Brand" numeric="true" required  />
                                   </div>
                                    <div class="col-lg-5">
                                        <div class="col-lg-10">
                                            <div class="col-lg-12">Active</div>
                                                <div class="col-lg-12"><input type="checkbox" id="checkbox1" ng-model="brandmodel.form.Active" numeric-type="integer" numeric="true" ></div>
                                        </div>
                                    </div>
                             </div>
                            <div class="row">
                                            <br />
                                            <div class="row" ng-show="!marca.form.EsTrailer">
                                                <div style="padding-left: 15px" class="col-lg-10 bold">
                                                    Models
                                                </div>
                                           </div>
                                            <div class="row">
                                                <table class="table table-condensed table-striped table-hover table-fixed" 
                                        st-table="brandmodel.Models" st-safe-src="brandmodel.Models" style="width:96%; margin-left:2%; margin-right:2%;">
                                        <thead>
                                            <tr>
                                                <th width="30%">Category</th>
                                                <th width="20%">SKU</th>
                                                <th width="30%">Model</th>
                                                <th width="10%">Active</th>                                              
                                                <th width="10%" align="right">
                                                      <div class="pull-right paddingR">
                                                       <div class="cursor btn-border btn-default" ng-click="brandmodel.ModelNew()">
                                                           <i class="fa fa-plus">&nbsp;Add</i> 
                                                       </div> 
                                                    </div>
                                                </th>  
                                            </tr>
                                        </thead>
                                        <tbody style="overflow:scroll; overflow-x:hidden; max-height:300px">
                                            <tr ng-repeat="item in brandmodel.Models">
                                                <td width="30%">
                                                     <span ng-show="!item.EsEditar">{{item.Category}}</span>
                                                     <select class="form-control" ng-model="item.CategoryID" ng-show="!!item.EsEditar"
                                                            ng-options="Category.CategoryID as Category.Category for Category in brandmodel.Categories">
                                                    </select>  
                                                </td>
                                                <td width="20%">
                                                  <span ng-show="!item.EsEditar">{{item.SKU}}</span>
                                                    <input ng-show="!!item.EsEditar" type="text" ng-model="item.SKU" class="form-control" />
                                                </td>
                                                 <td width="30%">
                                                  <span ng-show="!item.EsEditar">{{item.Model}}</span>
                                                    <input ng-show="!!item.EsEditar" type="text" ng-model="item.Model" class="form-control" />
                                                </td>
                                                <td width="10%">
                                                    <input  type="checkbox" ng-show="!item.EsEditar" ng-model="item.Active" disabled="disabled"/> 
                                                    <input  type="checkbox" ng-show="!!item.EsEditar" ng-model="item.Active" />                    
                                                   </td>
                                                 <td width="10%">
                                                   <div class="pull-right">
                                                        <button id="Tbtn" ng-show="!item.EsEditar" ng-click="brandmodel.ModelEdit(item, $index)" class="blue btn btn-white btn-radius big-input" type="button">
                                                            <i class="fa fa-pencil-square-o" title="Edit"></i>  
                                                        </button>
                                                         <button ng-show="item.EsEditar" ng-click="brandmodel.ModelSave(item, $index)"  class="btn btn-success btn-radius big-input" type="button">
                                                            <i class="fa fa-floppy-o" title="Save"></i>  
                                                        </button>
                                                         <button ng-show="item.EsEditar" ng-click="brandmodel.ModelCancel(item)"  class="orange btn btn-white btn-radius big-input" type="button">
                                                            <i class="fa fa-ban" title="Cancel"></i> 
                                                        </button>
                                                     </div>
                                                     <input type="hidden" ng-show="item.ModeloID" />
                                                      <button id="Button1" ng-show="!item.EsEditar" ng-click="brandmodel.ModelConfirmDelete(item)" class="btn btn-white btn-radius" type="button">
                                                            <i class="fa fa-trash-o" title="Delete"></i> 
                                                        </button>
                                                      </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="9" class="text-right" style="padding-bottom: 0">
                                                  <div st-pagination="5" st-items-by-page="200" st-template="../../Templates/pagination.html">



                                                  </div>
                                                   
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                  </div>
                             </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="green btn btn-success btn-radius big-input" ng-click="brandmodel.BrandSave()" ng-hide ="brandmodel.esSoloLectura">
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

    <script type="text/javascript" language="javascript" src="<%=ruta %>js/pages/catalogos/BrandModel.js?V00039"></script>

</asp:Content>
