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

        [Authorize(Roles = "Admin")]
        public ActionResult Pacientes()
        {
            ViewBag.Title = "Pacientes";

            return View();
        }

        /// <summary>
        /// Consultar la información paciente
        /// </summary>
        /// <param name="IdPacient">Paciente</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainPacientInfo(string IdPacient)
        {
            var InfoPacient = _proxy.GetForMessage<string>($"{ConstantesApi.getPacientInfoUri}?IdPacient={Server.UrlEncode(IdPacient)}");

            return Content(InfoPacient.Body, "application/json");
        }

        /// <summary>
        /// Consultar la información citas
        /// </summary>
        /// <param name="IdPacient">Paciente</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObtainAppointmentsByPacient(string IdPacient)
        {
            var InfoAppByPacient = _proxy.GetForMessage<string>($"{ConstantesApi.getAppointmentsUri}?IdPacient={Server.UrlEncode(IdPacient)}");

            return Content(InfoAppByPacient.Body, "application/json");
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
