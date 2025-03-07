(function () {
    //Project Controller.
    app.controller('ActivityController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var control = this;

        control.filter = {};
        control.filter.ShowAll = true;
        control.form = {};

        control.esSoloLectura = accesoPantalla[0].SoloLectura;


        //obtener lista
        control.LoadInit = function () {
            try {
               // Ex.load(true);
                var callback = function (response) {
                    // Ex.load(false);
                    control.Activities = response.d.Activities;

                    $scope.PermisosIniciales = response.d.Permisos;
                    control.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("InitLoad", control.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
               // Ex.load(false);
            }
        };


        control.Search = function () {
            control.LoadInit();
        }

        control.Edit = function (item) {

            control.Form.isValid = true;
            control.form.RepairActivityID = item.RepairActivityID;
            control.form.RepairActivity = item.RepairActivity;
            control.form.Active = item.Active;

            $('#modal-long').modal('show');
        };


        control.New = function () {

            control.Form.isValid = true;
            control.form.RepairActivityID = 0;
            control.form.RepairActivity = "";
            control.form.Active = true;

            $('#modal-long').modal('show');
        };



        control.SetClassSummitValid = function () {
            if (!control.Form.isValid)
                return true;
            else
                return false;
        };

        control.Save = function (action) {

            try {
                Ex.load(true);


                $Ex.Execute("Save", control.form, function (response, isInvalid) {

                    if (isInvalid) {
                        control.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d.Result == "OK") {
                        

                        control.LoadInit();
                        Ex.mensajes("Saved succesfully", 4);
                        $('#modal-long').modal('hide');
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


        control.LoadInit();
       

    }]);


})();


