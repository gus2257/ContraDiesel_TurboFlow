(function () {
    //Project Controller.
    app.controller('ActividadController', ['$scope', '$http', function ($scope, $http) {
        $Ex.Http = $http;

        $scope.itemModelo = 0;

        var actividad = this;

        actividad.filter = {};
        actividad.filter.isValid = true;
        actividad.esConsulta = true;
        actividad.ActividadMarcas = [];

        actividad.ObtieneDatos = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    //actividad.Marcas = response.d.Marcas;
                    //actividad.Modelos = response.d.Modelos;
                    actividad.esSoloLectura = accesoPantalla[0].SoloLectura;
                }
                actividad.filter = {};
                $Ex.Execute("ObtieneDatos", actividad.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.ObtieneActividades = function () {
            try {
                Ex.load(true);
                var callback = function (response) {
                    Ex.load(false);
                    actividad.Actividades = response.d.Actividades;
                    actividad.ActividadesAux = response.d.Actividades;
                }
                $Ex.Execute("ObtieneActividades", actividad.filter, callback);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.buscar = function () {
            actividad.ObtieneActividades();
        }

        actividad.nuevo = function () {
            actividad.Form.isValid = true;
            actividad.form = {};
            actividad.form.ActividadID = 0;
            actividad.form.Actividad = '';
            actividad.form.HorasHombre = '';
            actividad.form.EsActivo = true;

            actividad.ActividadMarcas = [];
            actividad.MarcaRepuesto = [];
            actividad.MarcaModelos = [];
            actividad.ActividadMarcas.push({ ActividadMarcaID: 0, ActividadID: 0, MarcaID: '', EsEditar: false });

            $('#modal-long').modal('show');
        };

        actividad.guardar = function () {
            try {
                Ex.load(true);

                /*
                if (!actividad.Form.$invalid) {
                    for (var index = 0; index < actividad.MarcaRepuesto.length; index++) {
                        actividad.MarcaRepuesto[index].RepuestoID = actividad.MarcaRepuesto[index].RepuestoSel.RepuestoID;
                        actividad.MarcaRepuesto[index].RepuestoSel = '';
                    }
                }

                if (!actividad.Form.$invalid) {

                    var Actmarca = [];

                    actividad.form.MarcaRepuesto = actividad.MarcaRepuesto;
                    actividad.form.MarcaModelos = angular.copy(actividad.MarcaModelos);
                    var modelosSelec = [];

                    angular.forEach(actividad.ActividadMarcas, function (actMarca, key) {

                        angular.forEach(actMarca.MarcaSelectedID, function (marcaSel, key) {

                            this.push(
                                {
                                    ActividadID: actMarca.ActividadID,
                                    ActividadMarcaID: actMarca.ActividadMarcaID,
                                    MarcaID: marcaSel.MarcaID,
                                    EsEditar:actMarca.EsEditar
                                }
                            )
                           
                        }, Actmarca);
                    });

                    for (var index = 0; index < actividad.form.MarcaModelos.length; index++) {

                        actividad.form.MarcaModelos[index].AnioInicio = actividad.form.MarcaModelos[index].AnioModelos.StartDate;
                        actividad.form.MarcaModelos[index].AnioFin = actividad.form.MarcaModelos[index].AnioModelos.EndDate;
                        actividad.form.MarcaModelos[index].AnioModelos = null;

                        for (var index2 = 0; index2 < actividad.form.MarcaModelos[index].ModSelectedID.length; index2++) {

                            var modeloID = actividad.form.MarcaModelos[index].ModSelectedID[index2].ModeloID;
                            var marcaID = actividad.form.MarcaModelos[index].ModSelectedID[index2].MarcaID;

                            modelosSelec.push(
                                {
                                    ActividadMarcaID: actividad.form.MarcaModelos[index].ActividadMarcaID,
                                    ActividadMarcaModeloID: actividad.form.MarcaModelos[index].ActividadMarcaModeloID,
                                    AnioInicio: actividad.form.MarcaModelos[index].AnioInicio,
                                    AnioFin: actividad.form.MarcaModelos[index].AnioFin,
                                    EsActivo: actividad.form.MarcaModelos[index].EsActivo,
                                    ModeloID: modeloID,
                                    MarcaID: marcaID
                                })
                        }

                        actividad.form.MarcaModelos[index].ModSelectedID = null;
                    }

                    actividad.form.ActividadMarcas = Actmarca;
                    actividad.form.MarcaModelos = modelosSelec;
                    
                }
                */

                $Ex.Execute("GuardarActividad", actividad.form, function (response, isInvalid) {

                    if (isInvalid) {
                        actividad.Form.isValid = false;
                        Ex.load(false);
                        return;
                    }

                    if (response.d == "OK") {
                        $('#modal-long').modal('hide');
                        actividad.ObtieneActividades();
                        Ex.mensajes(Ex.GetResourceValue("MsgConGuardarActividad"),4);
                    }
                    else {
                        Ex.mensajes(response.d);
                        Ex.load(false);
                    }
                },
                actividad.Form);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.editar = function (item) {
            try {
                Ex.load(true);
                actividad.Form.isValid = true;
                actividad.form = {};
                var params = {};
                params.ActividadID = item.ActividadID;
                $Ex.Execute("ObtieneActividadDetalle", params, function (response) {
                    actividad.form = response.d.Actividad[0];
                    actividad.ActividadMarcas = response.d.ActividadMarcas;
                    actividad.MarcaRepuesto = response.d.MarcaRepuesto;
                    actividad.MarcaModelos = response.d.MarcaModelos;

                    $('#modal-long').modal('show');
                });

                Ex.load(false);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.eliminar = function () {
            try {
                Ex.load(true);
                var params = {};
                params.ActividadID = actividad.form.ActividadID;
                $Ex.Execute("EliminarActividad", params, function (response) {

                    if (response.d == "OK") {
                        actividad.ObtieneActividades();
                        $('#modal-long').modal('hide');
                        Ex.mensajes(Ex.GetResourceValue("MsgConEliminarActividad"),4);
                    }
                    else {
                        Ex.mensajes(response.d);
                        Ex.load(false);
                    }

                });

                Ex.load(false);
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.confirmaEliminar = function () {
            try {
                Ex.mensajes(Ex.GetResourceValue("MsgEliminar"), 5, null, null, null, actividad.eliminar, function () { });
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.confirmaEliminarModelo = function (item) {
            try {
                $scope.itemModelo = item;
                Ex.mensajes(Ex.GetResourceValue("MsgEliminar"), 5, null, null, null, actividad.eliminarModelo, function () { });
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.agregarActividadMarca = function () {
            try {

                actividad.ActividadMarcas.push({ ActividadMarcaID: 0, ActividadID: 0, MarcaID: '', EsEditar: false });

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.eliminarActividadMarca = function (index) {
            try {

                if (actividad.ActividadMarcas.length > 1) {

                    Ex.load(true);
                    var params = {};
                    params.ActividadMarcaID = actividad.ActividadMarcas[index].ActividadMarcaID;

                    var callback = function (response) {
                        Ex.load(false);
                        actividad.ActividadMarcas.remove(index);
                    }
                    $Ex.Execute("EliminarActividadMarca", params, callback);
                }
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.guardarActividadMarca = function (item, index) {
            try {

                if (actividad.Form.$invalid) {
                    actividad.Form.isValid = false;
                    Ex.load(false);
                    return;
                }
        
                if (item.ActividadMarcaID == 0) {
                    var max = 0;
                    for (var i = 0; i < actividad.ActividadMarcas.length; i++) {
                        if (actividad.ActividadMarcas[i].ActividadMarcaID > (max || 0))
                            max = actividad.ActividadMarcas[i].ActividadMarcaID;
                    }
                    item.ActividadMarcaID = max + 1;
                }

                item.EsEditar = true;

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }

        actividad.editarActividadMarca = function (item) {
            try {
                item.EsEditar = false;
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }

        actividad.configurarRepuestos = function (item) {

            try {

                actividad.FormRepuestos.isValid = true;
                actividad.formrepuestos = {};
                actividad.formrepuestos.ActividadMarcaID = item.ActividadMarcaID;

                if (item.ActividadMarcaID == 0) {
                    if (actividad.MarcaRepuesto.length == 0)
                        actividad.MarcaRepuesto.push({ ActividadMarcaRepuestoID: 0, ActividadMarcaID: item.ActividadMarcaID, RepuestoID: '', Codigo: '', Cantidad: '', EsEditar: false });
                }

                $('#modal-long-repuestos').modal('show');
            }
            catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.agregarRepuesto = function (ActividadMarcaID) {
            actividad.MarcaRepuesto.push({ ActividadMarcaRepuestoID: 0, ActividadMarcaID: ActividadMarcaID, RepuestoID: '', Codigo: '', Cantidad: '', EsEditar: false });
        };

        actividad.guardarRepuesto = function (item, EsEditar) {
            try {

                if (actividad.FormRepuestos.$invalid) {
                    actividad.FormRepuestos.isValid = false;
                    Ex.load(false);
                    return;
                }

                item.EsEditar = EsEditar;
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }

        actividad.eliminarRepuesto = function (index) {
            try {

                Ex.load(true);
                var params = {};
                params.ActividadMarcaRepuestoID = actividad.MarcaRepuesto[index].ActividadMarcaRepuestoID;

                if (params.ActividadMarcaRepuestoID == 0) {
                    actividad.MarcaRepuesto.remove(index);
                    Ex.load(false);
                }
                else {
                    var callback = function (response) {
                        Ex.load(false);
                        actividad.MarcaRepuesto.remove(index);
                    }
                    $Ex.Execute("EliminarMarcaRepuesto", params, callback);
                }

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.configurarModelos = function (item) {

            actividad.FormModelos.isValid = true;
            actividad.formmodelos = {};
            actividad.formmodelos.ActividadMarcaID = item.ActividadMarcaID;
                        
            var modelosSelec = [];

            angular.forEach(item.MarcaSelectedID, function (marcaSel, keyMarca) {
                
                angular.forEach(actividad.Modelos, function (modelo, keyModelo) {

                    if (marcaSel.MarcaID == modelo.MarcaID) {
                        this.push(
                            {
                                MarcaID:modelo.MarcaID,
                                ModeloID: modelo.ModeloID,
                                Modelo: modelo.Modelo
                            }
                        )
                    }

                }, modelosSelec);
            });
            
            actividad.formmodelos.modelosMarca = modelosSelec;

            $('#modal-long-modelos').modal('show');
        };

        actividad.agregarModelo = function (ActividadMarcaID) {
            try {
                actividad.MarcaModelos.push({
                    ActividadMarcaModeloID: 0, ActividadMarcaID: ActividadMarcaID,
                    ModeloID: '', EsActivo: true, EsEditar: false
                });
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }

        actividad.guardarModelo = function (item, editar) {
            try {

                if (actividad.FormModelos.$invalid) {
                    actividad.FormModelos.isValid = false;
                    Ex.load(false);
                    return;
                }
                item.EsEditar = editar;

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }

        actividad.eliminarModelo = function () {

            try {
                Ex.load(true);
                var index = actividad.MarcaModelos.indexOf($scope.itemModelo);

                var params = {};
                params.ActividadMarcaModeloID = actividad.MarcaModelos[index].ActividadMarcaModeloID;

                var callback = function (response) {
                    Ex.load(false);
                    actividad.MarcaModelos.remove(index);
                }
                $Ex.Execute("EliminarMarcaModelo", params, callback);

            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        };

        actividad.buscarRepuestos = function (description) {

            var filter = {};
            filter.CodDescr = description;
            filter.ProveedorID = 0;

            $Ex.Execute("BuscarRepuestos", filter, function (response) {
                actividad.RepuestosInfo = response.d.Repuestos;
                Ex.load(false);
            }, 'undefined', false);
        }

        actividad.SetClassSummitValid = function () {
            if (!actividad.Form.isValid)
                return true;
            else
                return false;
        };

        actividad.SetClassSummitRepuestoValid = function () {
            if (!actividad.FormRepuestos.isValid)
                return true;
            else
                return false;
        };

        actividad.SetClassSummitModeloValid = function () {
            if (!actividad.FormModelos.isValid)
                return true;
            else
                return false;
        };

        actividad.FormatoCalendario = {
            format: " yyyy", // Notice the Extra space at the beginning
            viewMode: "years",
            minViewMode: "years",
            startDate: '-30y',
            endDate: '+3y'
        };

        actividad.ConfirmarCerrarModal = function () {
            try {
                Ex.mensajes(Ex.GetResourceValue("MsgCerrarModal"), 5, null, null, null, actividad.cerrarModal, function () {
                  
                });
            } catch (ex) {
                Ex.mensajes(ex.message);
                Ex.load(false);
            }
        }


        actividad.cerrarModal = function ()
        {
            $('#modal-long').modal('hide');

        }


        actividad.ValidarRepuesto = function (repuestoSel, item, ActividadMarcaID) {

            item.RepuestoID = repuestoSel.RepuestoID;

            for (var i = 0; i < actividad.MarcaRepuesto.length; i++) {
                if (actividad.MarcaRepuesto[i].RepuestoID === repuestoSel.RepuestoID
                    && actividad.MarcaRepuesto[i].RepuestoSel !== repuestoSel
                    && actividad.MarcaRepuesto[i].ActividadMarcaID == ActividadMarcaID) {
                    item.RepuestoSel = null;
                    Ex.mensajes(Ex.GetGlobalResourceValue('lblMsgDuplicado'));
                }
            }
        }

   
        actividad.ObtieneDatos();
        actividad.ObtieneActividades();
        actividad.esSoloLectura = true;

    }]);

})();


