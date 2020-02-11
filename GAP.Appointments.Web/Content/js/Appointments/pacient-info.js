
function obtenerInformacionPaciente() {
    var Pacient = userName;
    var url = $("#PacienteInfo").data("url");   

    $("#PacienteInfo").removeClass("hide");
    $.ajax({
        url: url,
        data: {
            IdPacient: Pacient
        },
        success: function (data) {
            if (data) {
                $("#pId").text(data.IdKey);
                $("#pIdPaciente").text(data.Id);
                $("#pNombre").text(data.Name);
            }
        },
        error: function (data) {
            Alert(data);
            limpiarInformacionEstacion();
        }
    });
}


/**
 * Evento que se genera cuando se carga la página.
 */
$(function () {
    obtenerInformacionPaciente();
});