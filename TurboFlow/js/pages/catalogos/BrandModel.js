(function () {
    //Project Controller.
    app.controller('BrandModelController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var brandmodel = this;

        brandmodel.filter = {};
        brandmodel.form = {};

        brandmodel.esSoloLectura = accesoPantalla[0].SoloLectura;


        brandmodel.LoadInit = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);

                    brandmodel.Categories = response.d.Categories;

                }
                $Ex.Execute("LoadInit", brandmodel.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };
        brandmodel.LoadBrands = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);

                    brandmodel.Brands = response.d.Brands;

                }
                $Ex.Execute("BrandLoad", brandmodel.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        brandmodel.BrandEdit = function (item) {

            brandmodel.Form.isValid = true;
            
            brandmodel.form.BrandID = item.BrandID;
            brandmodel.form.Brand = item.Brand;
            brandmodel.form.Active = item.Active;

            brandmodel.filter = {};
            brandmodel.filter.BrandID = item.BrandID;

            brandmodel.LoadModels();

            $('#modal-long').modal('show');
        };

        brandmodel.BrandNew = function () {

            brandmodel.Form.isValid = true;
            // stock.form = {};
            brandmodel.form.BrandID = 0;
            brandmodel.form.Brand = '';
            brandmodel.form.Active = true;

            brandmodel.Models = undefined;
           
            $('#modal-long').modal('show');
        };



        brandmodel.SetClassSummitValid = function () {
            if (!stock.Form.isValid)
                return true;
            else
                return false;
        };
        brandmodel.BrandSave = function () {
            try {
                Ex.load(true);

                brandmodel.form.models = brandmodel.Models;
                brandmodel.modelsqty = brandmodel.Models.length;
            
                $Ex.Execute("BrandSave", brandmodel.form, function (response, isInvalid) {

                    if (isInvalid) {
                        brandmodel.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d.Result == "OK") {
                        Ex.mensajes("Brand & Models saved successfully", 4);

                        brandmodel.LoadBrands();

                        //stock.LoadStock();
                        //Ex.mensajes(Ex.GetResourceValue("MsgGuardarUsuario"), 4);
                        $('#modal-long').modal('hide');
                    }
                    else {
                        Ex.mensajes(response.d.Message);
                        Ex.load(false);
                    }
                },
                    brandmodel.Form);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }

        };




        brandmodel.LoadModels = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);

                    brandmodel.Models = response.d.Models;

                }
                $Ex.Execute("ModelLoad", brandmodel.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };


        brandmodel.ModelNew = function () {
           
            var newRow = {
                EsEditar: true,
                IsNew: true,
                BrandID: brandmodel.form.BrandID,
                Model: '',
                Active: true,
                ModelID: 0
            };
            brandmodel.Models.push(newRow);
        };


        brandmodel.ModelSave = function (item) {


          
            for (var i = 0; i < brandmodel.Categories.length; i++) {
                if (brandmodel.Categories[i].CategoryID == item.CategoryID) {
                    item.Category = brandmodel.Categories[i].Category;
                }
            }
          

            item.EsEditar = false;
        };

        brandmodel.ModelEdit = function (obj) {
            obj.ModelBack = obj.Model;
            obj.ActiveBack = obj.Active;
            obj.CategoryBack = obj.Category;
            obj.CategoryIDBack = obj.CategoryID
            obj.SKUBack = obj.SKU;
            obj.EsEditar = obj.EsEditar ? false : true;

            
        }

        brandmodel.ModelCancel = function (obj) {
            obj.Model = (obj.ModelBack == undefined ? '' : obj.ModelBack);
            obj.SKU = (obj.SKUBack == undefined ? '' : obj.SKUBack);
            obj.Category = (obj.CategoryBack == undefined ? '' : obj.CategoryBack);
            obj.CategoryID = (obj.CategoryIDBack == undefined ? '' : obj.CategoryIDBack);
            obj.Active = (obj.ActiveBack == undefined ? false : obj.ActiveBack);
            obj.EsEditar = false;
        }

        brandmodel.ModelConfirmDelete = function (item) {
            try {
                $scope.itemModelo = item;
                Ex.mensajes("Delete model?", 2, null, null, null, brandmodel.ModelDelete, function () { });
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        brandmodel.ModelDelete = function (index, item) {

            try {

                Ex.load(false);
                var params = {};
                params.BrandID = $scope.itemModelo.BrandID;
                params.ModelID = $scope.itemModelo.ModelID;

                var index = brandmodel.Models.indexOf($scope.itemModelo);

                var callback = function (response) {
                    if (response.d.Result == 'OK') {
                        Ex.load(false);
                        brandmodel.LoadModels();
                    }
                    else
                        Ex.mensajes(response.d.Message);
                }

                $Ex.Execute("ModelDelete", params, callback);

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }

        };

        brandmodel.LoadInit();
        brandmodel.LoadBrands();
       

    }]);


})();


