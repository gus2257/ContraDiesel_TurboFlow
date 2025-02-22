(function () {
    //Project Controller.
    app.controller('RepairController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var control = this;

        control.filter = {};
        control.form = {};

        control.esSoloLectura = accesoPantalla[0].SoloLectura;


        //obtener lista
        control.LoadFilters = function () {
            try {
               // Ex.load(true);
                var callback = function (response) {
                   // Ex.load(false);
                    control.Category = response.d.Category;
                    control.Brand = response.d.Brand;
                    control.Model = response.d.Model;
                   // control.RepairStatus = response.d.RepairStatus;
                    control.Technicians = response.d.Technicians;
                    control.Activities = response.d.Activities;

                    if (control.filter.CategoryID == null) {
                        control.filter.CategoryID = 0;
                    }
                    if (control.filter.BrandID == null) {
                        control.filter.BrandID = 0;
                    }
                    if (control.filter.ModelID == null) {
                        control.filter.ModelID = 0;
                    }
                    if (control.filter.RepairStatusID == null) {
                        control.filter.RepairStatusID = 1;
                    }

                    $scope.PermisosIniciales = response.d.Permisos;
                    control.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("InitLoad", control.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
               // Ex.load(false);
            }
        };

        //obtener lista
        control.RepairLoad = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    control.Repairs = response.d.Repairs;

                }
                $Ex.Execute("RepairLoad", control.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        control.RepairEdit = function (item) {

            control.Form.isValid = true;
            // stock.form = {};
            control.form.RepairID = item.RepairID;

            control.form.StockID = item.StockID;
            control.form.StockNum = item.StockNum;
            control.form.RepairStatusID = item.RepairStatusID;
            control.form.RepairStatus = item.RepairStatus;
            control.form.Category = item.Category;
            control.form.Brand = item.Brand;
            control.form.Model = item.Model;
            control.form.TechnicianID = item.TechnicianID
            control.form.Notes = item.Notes
            control.form.Lot = item.Lot
            control.form.UpdatedBy = item.UpdatedBy
            control.form.LastUpdate = item.LastUpdate;

            $('#modal-long2').modal('show');
        };

        control.StockFind = function () {

            control.stockFind = {};
            control.stockFind.StockID = 0; 
            control.stockFind.StockNum = control.form.StockNum; 

            try {
                // Ex.load(true);
                var callback = function (response) {
                    // Ex.load(false);
                    if (response.d.Result == "OK") {
                        control.form.StockID = response.d.StockID;
                        control.form.Category = response.d.Category;
                        control.form.Brand = response.d.Brand;
                        control.form.Model = response.d.Model;

                        control.form.Message = "";
                    } else {
                        control.form.Message = response.d.Message;
                    }


                }
                $Ex.Execute("StockSearch", control.stockFind, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                // Ex.load(false);
            }
        };

        control.New = function () {

            control.Form.isValid = true;
            // control.form = {};
            control.form.RepairID = 0;
            control.form.StockID = 0;
            control.form.IsNew = true;
            control.form.Category = '';
            control.form.Brand = '';
            control.form.Model = '';
            control.form.StockNum = '';
            control.form.RepairStatusID = 10;
           
            $('#modal-long').modal('show');
        };



        control.SetClassSummitValid = function () {
            if (!control.Form.isValid)
                return true;
            else
                return false;
        };

        control.RepairSave = function (action) {

            try {
                Ex.load(true);


                control.form.RepairStatusID2 = action;
                $Ex.Execute("RepairSave", control.form, function (response, isInvalid) {

                    if (isInvalid) {
                        control.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d.Result == "OK") {
                        

                        control.RepairLoad();
                        Ex.mensajes("Saved succesfully", 4);
                        $('#modal-long2').modal('hide');
                    }
                    else {
                        Ex.mensajes(response.d.Message);
                        Ex.load(false);
                    }
                },
                    control.Form);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }


        };

        control.RepairCreate = function () {

            if (control.form.StockID == 0) {
                Ex.mensajes("Stock ID is no valid");

            } else {

                try {
                    Ex.load(true);



                    $Ex.Execute("RepairSave", control.form, function (response, isInvalid) {

                        if (isInvalid) {
                            control.Form.isValid = false;
                            Ex.load(false);
                            return;
                        }

                        if (response.d.Result == "OK") {
                           
                            control.RepairLoad();
                            Ex.mensajes("Repair created", 4);
                            $('#modal-long').modal('hide');
                           // $('#modal-long2').modal('show');
                        }
                        else {
                            Ex.mensajes(response.d.Message);
                            Ex.load(false);
                        }
                    },
                        control.Form);
                } catch (ex) {
                    Ex.mensajes(ex.Message);
                    Ex.load(false);
                }
            }

        };

        control.ReloadFilters = function () {
            control.LoadFilters();
        }

        control.Search = function () {
            control.RepairLoad();
        }

        control.LoadFilters();
        control.RepairLoad();
       

    }]);


})();


