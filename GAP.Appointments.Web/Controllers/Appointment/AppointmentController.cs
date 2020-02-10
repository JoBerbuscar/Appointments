using GAP.Appointments.Commo.ProveedoresDependencias;
using GAP.Appointments.Common.Constantes;
using GAP.Appointments.TO.Appointments;
using GAP.Appointments.Web.Models;
using Spring.Rest.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace GAP.Appointments.Web.Controllers.Appointment
{
    [CustomAuthorize(Roles = "User")]
    public class AppointmentController : Controller
    {
        private RestTemplate _proxy;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        public AppointmentController()
        {
            _proxy = LocalizadorServicio<RestTemplate>.GetService();
        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        /// <param name="proxy">proxy rest inyectado a través del contenedor de instancias de spring</param>
        public AppointmentController(RestTemplate proxy)
        {
            _proxy = proxy;
        }

        public ActionResult Citas()
        {
            ViewBag.Title = "Citas";

            return View();
        }

        /// <summary>
        /// Consultar la información paciente
        /// </summary>
        /// <param name="IdPatient">Paciente</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainPatientInfo(string IdPatient)
        {
            var InfoPatient = _proxy.GetForMessage<string>($"{ConstantesApi.getPatientInfoUri}?IdPatient={Server.UrlEncode(IdPatient)}");

            return Content(InfoPatient.Body, "application/json");
        }

        /// <summary>
        /// Consultar la información citas
        /// </summary>
        /// <param name="IdPatient">Paciente</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainAppointmentsByPatient(string IdPatient)
        {
            var InfoAppByPatient = _proxy.GetForMessage<string>($"{ConstantesApi.getAppointmentsUri}?IdPatient={Server.UrlEncode(IdPatient)}");

            return Content(InfoAppByPatient.Body, "application/json");
        }

        /// <summary>
        /// Consultar tipos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainTypes()
        {
            var InfoTypes = _proxy.GetForMessage<string>($"{ConstantesApi.getTypesUri}");

            return Content(InfoTypes.Body, "application/json");
        }

        /// <summary>
        /// Consultar estados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainStates()
        {
            var InfoStates = _proxy.GetForMessage<string>($"{ConstantesApi.getStatesUri}");

            return Content(InfoStates.Body, "application/json");
        }

        /// <summary>
        /// Consultar estados
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAppointment(AppointmenTO filtro)
        { 
            var Datos =  _proxy.PostForMessage<string>(ConstantesApi.getCreateAppointmentUri, filtro);

            return Content(Datos.Body, "application/json");
        }
    }
}
