function cerrarSesion(evento) {
    var $control = $("#btnSalir");
    var URLLogOut = $control.data("logout")
    $.ajax({
        url: URLLogOut,
        //data: "{id:'" + '' + "'}",
        type: 'POST',
        //username: '',
        //password: '',
        success: function () {
            window.location.href = '/Account/Login'
        }
    });
}

// EVENTOS:
// --------------------------------------------------------------------------------

/**
 * Evento que se genera cuando se carga la página.
 */
$(function () {
    $('#btnSalir').on("click", cerrarSesion);
});
