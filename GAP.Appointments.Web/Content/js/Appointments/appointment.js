var datatable;
var _modelo = null;

function ConsularCitas() {
    var Pacient = userName;
    var urlConsulta = $('#tableListaCitas').data("url");
    //$('#loadingmessage').show();
    datatable = $('#tableListaCitas').DataTable();
    //sleep(3000);

    $.ajax({
        async: false,
        cache: false,
        type: "GET",
        dataType: "json",
        url: urlConsulta,
        data: {
            IdPacient: Pacient
        },
        success: function (response) {
            datatable.clear();
            datatable.rows.add(response);
            datatable.draw();

        },
        error: function (error) {
            var errorMessage = error.statusText;
            alert(errorMessage);
        },
        complete: function (data) {
            $('#loadingmessage').hide();
            //filters();
        }
    });
}

function filters() {
    // Setup - add a text input to each footer cell
    $('#tableListaCitas thead tr').clone(true).appendTo('#tableListaCitas thead');
    $('#tableListaCitas thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');

        $('input', this).on('keyup change', function () {
            if (datatable.column(i).search() !== this.value) {
                datatable
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });
}

function sleep(delay) {
    var start = new Date().getTime();
    while (new Date().getTime() < start + delay);
}

function inicializar() {
    configurarTabla();
}

function configurarTabla() {
    // Register date formats to allow DataTables sorting of the dates
    $.fn.dataTable.moment('YYYY-MM-DD HH:mm');

    $('#tableListaCitas').DataTable({
        "orderCellsTop": true,
        "fixedHeader": true,
        "bFilter": false,
        //"searching": true,
        //"paging": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "title": "Id", "targets": 0 },
            { "title": "Fecha", "targets": 1 },
            { "title": "Estado", "targets": 2 },
            { "title": "Tipo", "targets": 3 },
            //{
            //    "targets": 4,
            //    "data": null,
            //    "defaultContent": "<button>Editar</button>"
            //},
            {
                "targets": 4,
                "data": null,
                "defaultContent": "<button>Cancelar</button>"
            }
        ],
        columns: [
            { "data": "IdKey" },
            {
                "data": "Date",
                render: function (data, type, row) {
                    if (type === "sort" || type === "type") {
                        return data;
                    }
                    return moment(data).format("YYYY-MM-DD HH:mm");
                }
            },
            { "data": "StateDescription" },
            { "data": "TypeDescription" }
        ]
    });

    var table = $('#tableListaCitas').DataTable()

    $('#tableListaCitas tbody').on('click', 'button', function () {
        var data = table.row($(this).parents('tr')).data();
        alert(data[0] + "'s salary is: " + data[5]);
    });

    $('#addbtn').on('click', function () {
        abrirPopupEditarVariableValor();
    });
}

function abrirPopupEditarVariableValor() {
    crearControles();
    showModal('EditarCitaPopup', '900px');
};

function crearControles() {
    ControlTipos();
    ControlEstado();
    InicializarFecha();
}

function ControlTipos() {
    var urlObtenerTipos = $('#tableListaCitas').data("type");
    $.ajax({
        type: "GET",
        url: urlObtenerTipos,
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Seleccionar un tipo</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].IdKey + '"selected>' + data[i].Description + '</option>';
            }
            $("#ddlTipo").html(s);
        }
    });
}

function ControlEstado() {
    var urlObtenerEstados = $('#tableListaCitas').data("state");

    $.ajax({
        type: "GET",
        url: urlObtenerEstados,
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Seleccionar un estado</option>';
            for (var i = 0; i < data.length; i++) {
                if (data[i].Description == 'Programado') {
                    s += '<option value="' + data[i].IdKey + '"selected>' + data[i].Description + '</option>';
                }
                else {
                    s += '<option value="' + data[i].IdKey + '">' + data[i].Description + '</option>';
                }
            }
            $("#ddlEstado").html(s);
            $('#ddlEstado').prop('disabled', 'disabled');
        }
    });
}

function InicializarFecha() {
    //var fecha = $('#datFecha')
    var fecha = document.getElementById("datFecha");
    fecha.defaultValue = new Date().toISOString();
    //fecha.val(new Date().toDateInputValue());
}

Date.prototype.toISOString = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());

    return local.getUTCFullYear() +
        '-' + pad(local.getUTCMonth() + 1) +
        '-' + pad(local.getUTCDate()) +
        'T' + pad(local.getUTCHours()) +
        ':' + pad(local.getUTCMinutes()) +
        ':' + pad(0) +
        '.' + (0).toFixed(3).slice(2, 5);
});

//Date.prototype.toDateInputValue = (function () {
//    var local = new Date(this);
//    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
//    return local.toLocaleString();
//    //return local.toJSON().toString();
//    //return local.toJSON().slice(0, 10);
//});

//Date.prototype.toISOString = (function () {
//    var local = new Date(this);
//    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());

//    return local.getUTCFullYear() +
//        '-' + pad(local.getUTCMonth() + 1) +
//        '-' + pad(local.getUTCDate()) +
//        'T' + pad(local.getUTCHours()) +
//        ':' + pad(local.getUTCMinutes()) +
//        ':' + pad(local.getUTCSeconds()) +
//        '.' + (local.getUTCMilliseconds() / 1000).toFixed(3).slice(2, 5);
//});

function pad(number) {
    if (number < 10) {
        return '0' + number;
    }
    return number;
}

//function to check validation (Required field)
function checkReqFields() {
   
}

function actualizarCita() {
    var urlCreate = $('#tableListaCitas').data("create");
    _modelo = {};
    tipo = "POST";

    _modelo.IdKeyPacient = $("#pId").text();
    _modelo.IdType = $('#ddlTipo').val();
    _modelo.IdState = $('#ddlEstado').val();
    _modelo.Date = $("#datFecha").val();

    var jsonData = JSON.stringify(_modelo);

    $.ajax({
        url: urlCreate,
        contentType: "application/json; charset=UTF-8",
        data: jsonData,
        dataType: "json",
        type: tipo,
        success: function (respuesta) {
            if (respuesta) {
                hideModal('EditarCitaPopup');
                ConsularCitas();
                showMessage('Exito', 'alert-success.png','Registro Se almacena con éxito');
            }
            else {
                showMessage('Validación', 'alert-information.png','No se puede adicionar la cita, ya que existe una cita programada para el mismo día')
            }
        },
        error: function (error) {
            showMessage('Error', 'alert-error.png',error.status + '-' + error.statusText);
        }
    });
}

function showMessage(tittle, img, message) {  
    showMessage(
        tittle, img, message);
};

$(".close").click(function () {
    $("#myAlert").alert('close');
});

function showMessage(title, image, message) {
    image = 'img/' + image;

    $('#alertaPopup .modal-title').html(title);
    $('#imgIconoAlerta').attr('src', image);
    $('#labMensajeAlerta').html(message);
    $('#btnAceptarAlerta').on('click', CerrarVentana);
    showModal('alertaPopup', '600px');
    $('#btnAceptarAlerta').focus();
}

function CerrarVentana() {
    hideModal('alertaPopup');
}

function showModal(id, width) {
    if (typeof width === 'string') {
        $('#' + id + ' .modal-content').css('width', width);
    }
    $('#' + id).modal('show');
};

function hideModal(id) {
    $('#' + id).modal('hide');
};

function registrarEventos() {
    $('#btnGuardarEdicion').on('click', actualizarCita);
}

$(function () {
    inicializar();
    ConsularCitas();
    registrarEventos();
});