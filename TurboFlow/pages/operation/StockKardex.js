(function () {
    //Project Controller.
    app.controller('StockController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var stock = this;

        stock.filter = {};
        stock.form = {};
        $scope.emailPattern = /^([A-Za-z0-9._%+-])+@([A-Za-z0-9-])+\.(([A-Za-z]{2,4})+((\.([A-Za-z]{2,4}))?))$/;
        stock.esSoloLectura = accesoPantalla[0].SoloLectura;
        stock.ContactNotFound = false;
        stock.ContactNew = false;


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
                    stock.Repairs = response.d.Repairs;

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

            stock.ContactNotFound = false;
            stock.ContactNew = false;
        
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


        stock.ShowActivities = function (item) {

            try {
              

                stock.filter = {
                    "ContactID": item.ContactID,
                    "StockID": stock.form.StockID,
                }


                var callback = function (response) {
                    // Ex.load(false);

                    stock.ContactKardex = response.d.ContactKardex;

                    $('#modal-long2').modal('show');
                }
                $Ex.Execute("ContactKardex", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                // Ex.load(false);
            }

           
        };

        stock.searchContact = function (description) {

            var filter = {};
            filter.ContactName = description;

            $Ex.Execute("ContactsAutocomplete", filter, function (response) {
                stock.ContactsFilter = response.d.Contacts;

                if (stock.ContactsFilter.length == 0) {
                    stock.ContactNotFound = true;
                } else {
                    stock.ContactNotFound = false;
                }

                Ex.load(false);
            }, 'undefined', false);
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


                if (stock.ContactSel != undefined) {
                    stock.form.ContactID = stock.ContactSel.ContactID;
                    stock.form.CustomerName = stock.ContactSel.ContactName;

                } else {
                    stock.form.ContactID = 0;
                    stock.form.CustomerName = stock.CustomerName;
                }
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


