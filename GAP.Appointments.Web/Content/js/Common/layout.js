function cerrarSesion(evento) {
    var $control = $("#btnSalir");
    var URLLogOut = $control.data("logout")
    $.ajax({
        url: URLLogOut,
        data: "{id:'" + '' + "'}",
        type: 'POST',
        username: '',
        password: '',
        success: function () {
            alert('logged off');
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
