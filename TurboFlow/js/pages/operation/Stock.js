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
        stock.ObtieneUsuarios = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    usuario.Usuarios = response.d.Usuarios;
                    usuario.UsuariosAux = response.d.Usuarios;
                    usuario.Permisos = response.d.Permisos;
                    usuario.PermisosAux = response.d.Permisos;
                    $scope.PermisosIniciales = response.d.Permisos;
                    usuario.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("ObtieneUsuarios", stock.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

       

        stock.ObtieneUsuarios();
       

    }]);


})();


