Exertus.demos = {}

Exertus.demos.Build = function () {
    var type = Exertus.demos = function () { };
    Exertus.BuildServer();
    Exertus.BuildBinding();
    // Pagina demos
    var demos = new Exertus.demos();
    demos.demos = new Exertus.Binding('Principal');
    Exertus.Binding = demos;

    $('#btnOcultar').on('click', function (e) {
        Ex.hide('divPrueba');
    });

    $('#btnMostrar').on('click', function (e) {
        Ex.display('divPrueba');
    });

    $('#btnObtenerValor').on('click', function (e) {
        var val = "";
        val = Ex.getValue('sDrop');
        alert(val);
        val = Ex.getValue('txtNombre')
        alert(val);
        val = Ex.getValue('lblEtiqueta')
        alert(val);
        val = Ex.getValue('chkPrueba');
        alert(val);
        val = Ex.getValue('rdB');
        alert(val);
        //  Ex.mensajes(Ex.value('lblEtiqueta'), 1);
    });

    $('#btnAlerta').on('click', function (e) {
        Ex.mensajes('Este es un mensaje de prueba', 1);
    });

    $('#btnNotificacion').on('click', function (e) {
        Ex.mensajes("¿Desea aceptar este mensaje de confirmación?", 2, null, null, null, Exertus.demos.mensajeConfirma, Exertus.demos.mensajeRegresa, null);
    });

    type["mensajeRegresa"] = function () {
        Ex.mensajes('No confirmó', 1);
    }

    type["mensajeConfirma"] = function (tipo) {
        Ex.mensajes('Si confirmó', 1);
    }





}

jQuery(document).ready(setTimeout(Exertus.demos.Build, 250));




