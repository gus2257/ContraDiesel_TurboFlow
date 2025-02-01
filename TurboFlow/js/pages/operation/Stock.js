(function () {
    //Project Controller.
    app.controller('StockController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var stock = this;

        stock.filter = {};
        stock.filter.Filtro = '';
        stock.filter.show = true;
        stock.filter.isValid = true;

        stock.esSoloLectura = accesoPantalla[0].SoloLectura;

        //obtener lista
        stock.LoadStock = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    stock.Grouped = response.d.StockGrouped;
                    stock.List = response.d.StockList;
                   
                    $scope.PermisosIniciales = response.d.Permisos;
                    stock.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("InitLoad", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

       

        stock.LoadStock();
       

    }]);


})();


