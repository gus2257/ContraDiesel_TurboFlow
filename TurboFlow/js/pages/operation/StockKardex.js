(function () {
    //Project Controller.
    app.controller('StockController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var stock = this;

        stock.filter = {};
        stock.form = {};

        stock.esSoloLectura = accesoPantalla[0].SoloLectura;


        //obtener lista
        stock.StockLoad = function () {
            try {
                // Ex.load(true);
                let stID = angular.element(document.getElementById('valStockID'))[0].value;

                stock.form.StockID = stID;

                stock.filter = {
                    "StockID": stID,
                }
                

                var callback = function (response) {
                    // Ex.load(false);
                   
                    stock.Category = response.d.Category;
                    stock.Brand = response.d.Brand;
                    stock.Model = response.d.Model;
                    stock.StockNum = response.d.StockNum;
                    stock.StockStatus = response.d.StockStatus;
                    stock.LastUpdate = response.d.LastUpdate;

                    stock.History = response.d.History;

                    $scope.PermisosIniciales = response.d.Permisos;
                    stock.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("StockLoad", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
               // Ex.load(false);
            }
        };


        stock.LoadDrops = function () {
            try {
                
                var callback = function (response) {


                    stock.StockActivity = response.d.StockActivity;

                }
                $Ex.Execute("LoadDropdowns", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                // Ex.load(false);
            }
        };

     
        stock.NewHistory = function (item) {

            stock.Form.isValid = true;

            if (item == 0) {
                stock.form.StockHistoryID = 0;
                stock.form.Customer = '';
                stock.form.StockActivityID = undefined;
                stock.form.Warranty = false;
                stock.form.Customer = '';
                stock.form.ContactName = '';
                stock.form.ContactPhone = '';
                stock.form.ContactEmail = ''
                stock.form.UnitRef = ''
                stock.form.Notes = '';
                stock.historyBy = '';
                stock.historyDate = '';
            } else {
                stock.form.StockHistoryID = item.StockHistoryID;
                
                stock.form.Customer = item.Customer;
                stock.form.StockActivityID = item.StockActivityID;
                stock.form.Warranty = item.Warranty;
                stock.form.Customer = item.Customer;
                stock.form.ContactName = item.ContactName;
                stock.form.ContactPhone = item.ContactPhone;
                stock.form.ContactEmail = item.ContactEmail;
                stock.form.UnitRef = item.UnitRef;
                stock.form.Notes = item.Notes;
                stock.historyBy = item.UserName;
                stock.historyDate = item.AuditDateComplete;

            }
           
            $('#modal-long').modal('show');
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
            
                $Ex.Execute("StockHistorySave", stock.form, function (response, isInvalid) {

                    if (isInvalid) {
                        stock.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d == "OK") {

                        stock.StockLoad();
                        Ex.mensajes("Stock history updated", 4);
                        $('#modal-long').modal('hide');
                        //window.location.href = "Usuario.aspx";
                    }
                    else {
                        Ex.mensajes(response.d);
                        Ex.load(false);
                    }
                },
                    stock.Form);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }

        };


        stock.LoadDrops();
        stock.StockLoad();
       

       

    }]);


})();


