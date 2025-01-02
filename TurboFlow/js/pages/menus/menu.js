(function () {
    //Project Controller.
    app.controller('MenuController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var menu = this;

        menu.filter = {};
       

        //obtener lista
        menu.loadAccess = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);

                    //usuario.Usuarios = response.d.Usuarios;
                    //usuario.UsuariosAux = response.d.Usuarios;
                    //usuario.Permisos = response.d.Permisos;
                    //usuario.PermisosAux = response.d.Permisos;
                    //$scope.PermisosIniciales = response.d.Permisos;
                    //usuario.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                $Ex.Execute("LoadAccess", menu.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };


        menu.loadAccess();
       
    }]);


})();


