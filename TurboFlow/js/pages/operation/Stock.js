(function () {
    //Project Controller.
    app.controller('StockController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var stock = this;

        stock.filter = {};
        stock.form = {};

        stock.esSoloLectura = accesoPantalla[0].SoloLectura;


        //obtener lista
        stock.LoadFilters = function () {
            try {
               // Ex.load(true);
                var callback = function (response) {
                   // Ex.load(false);
                    stock.Category = response.d.Category;
                    stock.Brand = response.d.Brand;
                    stock.Model = response.d.Model;
                    stock.StockStatus = response.d.StockStatus;

                    if (stock.filter.CategoryID == null) {
                        stock.filter.CategoryID = 0;
                    }
                    if (stock.filter.BrandID == null) {
                        stock.filter.BrandID = 0;
                    }
                    if (stock.filter.ModelID == null) {
                        stock.filter.ModelID = 0;
                    }
                    if (stock.filter.StockStatusID == null) {
                        stock.filter.StockStatusID = 10;
                    }

                    $scope.PermisosIniciales = response.d.Permisos;
                    stock.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("InitLoad", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
               // Ex.load(false);
            }
        };

        //obtener lista
        stock.LoadStock = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    stock.Grouped = response.d.StockGrouped;
                    stock.List = response.d.StockList;

                }
                $Ex.Execute("StockLoad", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };


        stock.LoadDrops = function (who) {
            try {
                // Ex.load(true);
                var callback = function (response) {
                    // Ex.load(false);
                    stock.CategoryFrm = response.d.Category;
                    stock.BrandFrm = response.d.Brand;
                    stock.ModelFrm = response.d.Model;


                }
                $Ex.Execute("LoadDrops", stock.form, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                // Ex.load(false);
            }
        };

        stock.New = function () {

            stock.Form.isValid = true;
            // stock.form = {};
            stock.form.StockID = 0;
            stock.form.CategoryID = undefined;
            stock.form.BrandID = undefined;
            stock.form.ModelID = undefined;
            stock.form.StockActivityID = undefined;
            stock.form.Warranty = false;
            stock.form.StockNum = '';
            stock.form.Notes = '';
           
            $('#modal-long').modal('show');
        };


        stock.Kardex = function (item) {

            location.replace("StockKardex.aspx?StockID=" + item.StockID);
           
        };

        stock.SetClassSummitValid = function () {
            if (!stock.Form.isValid)
                return true;
            else
                return false;
        };
        stock.Save = function () {
            try {
                Ex.load(true);


            
                $Ex.Execute("StockSave", stock.form, function (response, isInvalid) {

                    if (isInvalid) {
                        stock.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d.Result == "OK") {
                        alert(Ex.GetResourceValue("MsgGuardarUsuario"));
                        location.replace("StockKardex.aspx?StockID=" + response.d.StockID);

                        //stock.LoadStock();
                        //Ex.mensajes(Ex.GetResourceValue("MsgGuardarUsuario"), 4);
                        //$('#modal-long').modal('hide');
                    }
                    else {
                        Ex.mensajes(response.d.Message);
                        Ex.load(false);
                    }
                },
                    stock.Form);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }

        };

        stock.ReloadFilters = function () {
            stock.LoadFilters();
        }

        stock.Search = function () {
            stock.LoadStock();
        }

        stock.LoadFilters();
        stock.LoadStock();
        stock.LoadDrops(0);
       

    }]);


})();


