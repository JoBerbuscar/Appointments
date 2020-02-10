
function obtenerInformacionPaciente() {
    var patient = userName;
    var url = $("#PatienteInfo").data("url");   

    $("#PatienteInfo").removeClass("hide");
    $.ajax({
        url: url,
        data: {
            IdPatient: patient
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