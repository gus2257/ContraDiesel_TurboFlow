(function () {
    //Project Controller.
    app.controller('AccesoController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var acceso = this;
        acceso.filter = {};

        acceso.ObtieneDatos = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    acceso.MostrarMenu = false;
                    var info = Ex.GroupArray(response.d.Pantallas, 'PadreId', false);
                    acceso.Configuration = info[1]== undefined ? [] : info[1];
                    acceso.Operation = info[10]== undefined ? []:info[10];
                    acceso.Reports = info[17] == undefined ? [] : info[17];
                    acceso.Catalogos = info[22] == undefined ? [] : info[22];
                    acceso.MostrarMenu = true;
                }
                Ex.executepath(domainURL + "Acceso.aspx", "ObtieneDatosAcceso", acceso.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };
    

    }]);


})();


