(function () {
    //Project Controller.
    app.controller('FiltersController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        $scope.itemModelo = 0;

        var filtersC = this;

        filtersC.filter = {};
       
        filtersC.loadAll = function () {
        };

        filtersC.StockCategory = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);

                    filtersC.Category = response.d.StockList;
                }
                filtersC.filter = {};
                $Ex.Execute("StockCategory_Sel", filtersC.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        filtersC.loadAll();


    }]);

})();


