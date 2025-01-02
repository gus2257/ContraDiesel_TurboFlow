(function () {
    //Project Controller.
    app.controller('Dashboard', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        $scope.EsActivoFilter = function (item) {
            if (item.EsActivo === true) {
                return item.EsActivo;
            } else {
                return;
            }
         
        };

        var dashboard = this;

        dashboard.form = {};

        var intervalPromise = setInterval(function () {
            if (dashboard != undefined) {
                dashboard.ObtieneDatos();
            }
        }, 300000)

        //obtener lista
        dashboard.ObtieneDatos = function () {
            try {
                Ex.load(true);

                var callback = function (response) {
                    Ex.load(false);
                    dashboard.MecanicosRep = response.d.MecanicosRep;
                    dashboard.UnidadesRep = response.d.UnidadesRep;
                    dashboard.InspeccionesRep = response.d.InspeccionesRep;

                    
                    dashboard.MecanicosQty = response.d.MecanicosQty;
                    dashboard.UnidadesQty = response.d.UnidadesQty;
                    dashboard.InspeccionesQty = response.d.InspeccionesQty;

                    const today = new Date();
                    const yyyy = today.getFullYear();
                    let mm = today.getMonth() + 1; // Months start at 0!
                    let dd = today.getDate();
                    let hh = today.getHours();
                    let mi = today.getMinutes();

                    if (dd < 10) dd = '0' + dd;
                    if (mm < 10) mm = '0' + mm;
                    if (mi < 10) mi = '0' + mi;

                    const formattedToday = mm + '/' + dd + '/' + yyyy + ' ' + hh + ':' + mi + 'hrs.';

                    dashboard.LastUpdate = formattedToday;
                 
                }
                $Ex.Execute("ObtieneDatos", dashboard.form, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);                                                                                                                                         
            }
        };


        dashboard.ObtieneDatos();

    }]);


})();


