(function () {
    //Project Controller.
    app.controller('UsuarioController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        var usuario = this;

        usuario.filter = {};
        usuario.filter.Filtro = '';
        usuario.filter.isValid = true;
        usuario.esConsulta = true;
        usuario.permiso = {};
        usuario.TipoUsuario = [];
        usuario.Datos = [];
        $scope.emailPattern = /^([A-Za-z0-9._%+-])+@([A-Za-z0-9-])+\.(([A-Za-z]{2,4})+((\.([A-Za-z]{2,4}))?))$/;

        // UEN
        usuario.ubicacion = [];
        usuario.ubicacionSelected = [];
        usuario.ubicacionMultiSelectedConfiguration = {
            displayProp: 'NombreUbicacion', idProp: 'UbicacionID', enableSearch: false,
            scrollableHeight: '220px', scrollable: true, buttonClasses: 'btn btn-multiselect',
            showCheckAll: true, showUncheckAll: true
        };

        //obtener lista
        usuario.ObtieneUsuarios = function () {
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
                $Ex.Execute("ObtieneUsuarios", usuario.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        usuario.buscar = function () {
            usuario.ObtieneUsuarios();
        }

        usuario.exportar = function () {
            Ex.load(true);
            $Ex.Execute("Exportar", {}, function (response, isInvalid) {
                Ex.load(false);
                window.location = domainURL + "DownLoadPage.aspx";
            },
            usuario.From);
        }

        //agregar un nuevo atributo
        usuario.nuevo = function () {
            usuario.ubicacionSelected = [];
            usuario.Form.isValid = true;
            usuario.form = {};
            usuario.form.UsuarioID = 0;
            usuario.form.EsBaja = false;
            usuario.form.EsActivo = true;
            usuario.form.TipoUsuarioID = 1;
            usuario.Permisos = angular.copy($scope.PermisosIniciales);
            usuario.PermisosAux = angular.copy($scope.PermisosIniciales);
            usuario.opcionSoloLectura = false;
            usuario.opcionEditar = false;
            $('#modal-long').modal('show');
        };

        //establece si el formulario de usuario es valido
        usuario.SetClassSummitValid = function () {
            if (!usuario.Form.isValid)
                return true;
            else
                return false;
        };

        //carga la informacion para editar
        usuario.editar = function (item) {
            try {
                Ex.load(true);
                usuario.Form.isValid = true;
                usuario.form = {};
                var params = {};
                params.UsuarioID = item.UsuarioID;
                $Ex.Execute("ObtieneUsuarios", params, function (response) {
                    Ex.load(false);
                    usuario.form = response.d.Usuarios[0];
                    usuario.Permisos = response.d.Permisos;
                    usuario.PermisosAux = response.d.Permisos;
                    usuario.ubicacion = response.d.Ubicacion;
                    usuario.opcionSoloLectura = false;
                    usuario.opcionEditar = false;
                    // Selected
                    usuario.ubicacionSelected = response.d.ubicacionSelected;

                    $('#modal-long').modal('show');
                });

                Ex.load(false);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }
        };

        usuario.confirmaEliminar = function (item) {
            try {
                Ex.mensajes("Are you sure you want to delete the information?", 5, null, null, null, usuario.eliminar, function () { });
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }
        };

        usuario.eliminar = function () {
            try {
                Ex.load(true);
                var params = {};
                params.UsuarioID = usuario.form.UsuarioID;
                $Ex.Execute("EliminarUsuarios", params, function (response) {
                    if (response.d == "OK") {
                        usuario.ObtieneUsuarios();
                        Ex.mensajes(Ex.GetResourceValue("MsgEliminarExitoUsuario"),4);
                        $('#modal-long').modal('hide');
                        
                    }
                    else
                       Ex.mensajes(response.d);
                });
                Ex.load(false);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }
        };

        usuario.regresar = function () {
            $('#totop').click();
            usuario.esConsulta = true;
        };

        usuario.guardar = function (listaPermisos) {
            try {
                Ex.load(true);

                usuario.Datos = listaPermisos;
                usuario.form.listaPermisos = usuario.Datos;
                usuario.form.ubicacionList = usuario.ubicacionSelected;
                $Ex.Execute("GuardarUsuarios", usuario.form, function (response, isInvalid) {

                    if (isInvalid) {
                        usuario.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d == "OK") {
                       
                        usuario.regresar();
                        usuario.ObtieneUsuarios();
                        Ex.mensajes(Ex.GetResourceValue("MsgGuardarUsuario"),4);
                        $('#modal-long').modal('hide');
                        //window.location.href = "Usuario.aspx";
                    }
                    else {
                        Ex.mensajes(response.d);
                        Ex.load(false);
                    }
                },
                usuario.Form);
            } catch (ex) {
                Ex.mensajes(ex.Message);
                Ex.load(false);
            }
        };

        usuario.ValidaPermiso = function (item, esEditar) {

     
            if (esEditar && item.Editar) {
                item.SoloLectura = false;
            }

            if (esEditar == false && item.SoloLectura) {
                item.Editar = false;
            }
            
            if (item.Editar == false && item.SoloLectura == false) {
                item.EsPredeterminado = false;
            }
        };

        usuario.ValidaPage = function (item) {
            if (item.Editar == false && item.SoloLectura == false) {
                angular.forEach(usuario.Permisos, function (permiso) {
                    permiso.EsPredeterminado = false;
                });
                Ex.mensajes(Ex.GetResourceValue("MsjPaginaDefault"));
                return;
            }
            angular.forEach(usuario.Permisos, function (permiso) {
                permiso.EsPredeterminado = (permiso.EsPredeterminado == undefined ? true : false);
            });
        };


        usuario.SeleccionarSoloLectura = function (esEditar) {
           
                angular.forEach(usuario.Permisos, function (permiso, esEditar) {
                    permiso.SoloLectura = usuario.opcionSoloLectura;
                    if (usuario.opcionSoloLectura) {
                        permiso.Editar = false;
                        usuario.opcionEditar = false;
                    }
                });
            
        };

        usuario.SeleccionarEditar = function () {
                 angular.forEach(usuario.Permisos, function (permiso) {
                    permiso.Editar = usuario.opcionEditar;
                    if (usuario.opcionEditar) {
                        permiso.SoloLectura = false;
                        usuario.opcionSoloLectura = false;
                    }
                });
            
        };

       

        usuario.ObtieneUsuarios();
        usuario.esSoloLectura = true;

    }]);


})();


