/**
 * Created by:      Robert Medina
 * Created date:    2017/03/06
 * Modified by:     -
 * Modified date:   -
 * Company:         MVM Ingeniería de Software S.A.S
 *
 * Ventana para la adición y edición de variables.
 */
(function (exports, lac, $, Promise) {
    'use strict';

    var _modelo = null;
    var _urlAdicionar = lac.getUrlServicioVariables("AdicionarVariableValor");
    var _urlActualizar = lac.getUrlServicioVariables("ActualizarVariableValor");
    var _urlVariableAnalista = lac.getUrlServicioVariables("ObtenerVariables");
    var _urlVariable = lac.getUrlServicioVariables("ObtenerVariablesPorTipo?tipoRol={0}");
    //var _urlAgentes = lac.getUrlServicioVariables("ObtenerAgentes");
    var _urlAgentes = lac.getUrlServicioVariables("ObtenerAgentes?rolLogueado={0}&usuario={1}");
    var _urlSTRs = lac.getUrlServicioVariables("ObtenerSTRs");
    var _urlConsultarVariable = lac.getUrlServicioVariables("EsSTR?codigo={0}");

    // CONSTANTES:
    // --------------------------------------------------------------------------------

    exports.abrirPopupEditarVariableValor = function (model) {
        _modelo = model;

        crearControles();
        registrarValidador();
        setearValores();

        lac.showModal('EditarVariableValorPopup', '900px');
    };

    // MÉTODOS PRIVADOS:
    // --------------------------------------------------------------------------------

    /**
     * Deja la popup en su estado inicial.
     */
    function setearValores() {
        $("#ddlVariable").data('kendoDropDownList').value("");
        $("#ddlVariable").data("kendoDropDownList").enable(true);
        $("#ddlAgente").data('kendoDropDownList').value("");
        $("#ddlAgente").data("kendoDropDownList").enable(true);
        $("#ddlSTR").data('kendoDropDownList').value("");
        $("#ddlSTR").data("kendoDropDownList").enable(false);
        $("#txtValorEdicion").val("");
        $("#datFechaInicioEdicion").data('kendoDatePicker').value("");
        $("#datFechaInicioEdicion").data("kendoDatePicker").enable(true);
        $("#datFechaFinEdicion").data('kendoDatePicker').value("");  
        $("#txtDescripcionEdicion").val("");
        if (_modelo) {
            $("#ddlVariable").data('kendoDropDownList').value(_modelo.Codigo);
            $("#ddlVariable").data("kendoDropDownList").enable(false);
            $("#ddlAgente").data('kendoDropDownList').value(_modelo.CodigoUnidadNegocio);
            $("#ddlAgente").data("kendoDropDownList").enable(false);
            $("#ddlSTR").data('kendoDropDownList').value(_modelo.CodigoSTR);
            $("#ddlSTR").data("kendoDropDownList").enable(false);
            $("#txtValorEdicion").val(_modelo.Valor);
            $("#datFechaInicioEdicion").data('kendoDatePicker').value(_modelo.FechaInicio);
            $("#datFechaInicioEdicion").data("kendoDatePicker").enable(false);
            $("#datFechaFinEdicion").data('kendoDatePicker').value(_modelo.FechaFin);
            $("#txtDescripcionEdicion").val(_modelo.Descripcion);
        }
        cambioFechaInicio();
    }

    /**
     * Permite configurar los diferentes controles del formulario.
     */
    function crearControles() {
        var urlObtenerVariables = _urlVariable; 
        var urlObtenerAgentes = _urlAgentes;
        if (tipoRol == 0) {
            urlObtenerVariables = _urlVariableAnalista
        }
        else {
            urlObtenerVariables = lac.format(urlObtenerVariables, tipoRol);
        }
        
        urlObtenerAgentes = lac.format(urlObtenerAgentes, rolLogueado, usuarioLogueado);
        $("#datFechaInicioEdicion").kendoDatePicker({
            change: cambioFechaInicio,
            start: "month",
            depth: "month"
        });

        $("#datFechaFinEdicion").kendoDatePicker({
            start: "month",
            depth: "month"
        });

        $("#ddlVariable").kendoDropDownList({
            optionLabel: "Seleccione Variable...",
            dataTextField: "Nombre",
            dataValueField: "Codigo",     
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: urlObtenerVariables,
                    }
                }
            },
            change: onChange
        });

        $("#ddlAgente").kendoDropDownList({
            optionLabel: "Seleccione Agente...",
            dataTextField: "NombreCorto",
            dataValueField: "ObjID",
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: urlObtenerAgentes,
                    }
                }
            },

        });

        $("#ddlSTR").kendoDropDownList({
            optionLabel: "Seleccione STR...",
            dataTextField: "Nombre",
            dataValueField: "ObjID",
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _urlSTRs,
                    }
                }
            },            

        });     

        // create NumericTextBox from input HTML element using custom format
        $("#txtValorEdicion").kendoNumericTextBox({
            format: "{0:#,##0.#########}",
            decimals: 9
        });
    }

    /**
     * Crea el validador de kendo sino esta creado ya.
     */
    function registrarValidador() {
        if (!$('#contenedorFiltrosEditarVariableValor').data('kendoValidator')) {
            $('#contenedorFiltrosEditarVariableValor').kendoValidator();
        }
        $('#contenedorFiltrosEditarVariableValor').data('kendoValidator').hideMessages();
    }

    /**
     * Guarda la edición o la adición del variable.
     */
    function actualizarVariableValor() {
        if ($('#contenedorFiltrosEditarVariableValor').data('kendoValidator').validate()) {
            var url = _urlActualizar;
            var tipo = "PUT";

            if (!_modelo) {
                _modelo = {};
                url = _urlAdicionar;
                tipo = "POST";
            }

            _modelo.Codigo = $('#ddlVariable').val();
            _modelo.UnidadNegocio = $('#ddlAgente').val();
            _modelo.STR = $('#ddlSTR').val();
            _modelo.Valor = $('#txtValorEdicion').val();
            _modelo.FechaInicio = $("#datFechaInicioEdicion").val();
            _modelo.FechaFin = $("#datFechaFinEdicion").val();
            _modelo.Descripcion = $("#txtDescripcionEdicion").val();

            $.ajax({
                url: url,
                contentType: "application/json; charset=UTF-8",
                data: kendo.stringify(_modelo),
                dataType: "json",
                type: tipo,
                success: function (respuesta) {
                    lac.hideModal('EditarVariableValorPopup');
                    $('#gridValores').data('kendoGrid').dataSource.read();
                    lac.showSuccess();
                },
                error: function (error) {
                    lac.showError(error);
                }
            });
        }
    }

   
    /**
     * Evento que se dispara cuando se cambia la fecha de inicio.
     */
    function cambioFechaInicio() {

        var fechaFin = $("#datFechaFinEdicion").data("kendoDatePicker");

        var valorFechaInicio = $("#datFechaInicioEdicion").val();        
        var valorFechaFin = $("#datFechaFinEdicion").val();

        if (valorFechaInicio) {
            if (valorFechaFin) {
                if (valorFechaFin < valorFechaInicio) {
                    fechaFin.value("");
                }
            }
            fechaFin.min(valorFechaInicio);
        }
    }     

    function onChange() {
        var variableId = $("#ddlVariable").data("kendoDropDownList").value();    
        mostrarSTR(variableId);
    };

    function mostrarSTR(variableId) {
        var url = _urlConsultarVariable;
        url = lac.format(url, variableId)
        $.ajax({
            url: url,                  
            success: function (respuesta) {
                var dropDown = $("#ddlSTR").data("kendoDropDownList");                
                if (respuesta == true) {
                    dropDown.enable(true);
                }
                else {
                    dropDown.value("");
                    dropDown.enable(false);
                }
            },
            error: function (error) {
                lac.showError(error);
            }
        });
    };

    $(function () {
        lac.click('btnGuardarEdicion', actualizarVariableValor);
    });
})(window.lac.variables, window.lac, window.$, window.Promise);