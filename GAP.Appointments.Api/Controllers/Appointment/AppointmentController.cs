using GAP.Appointments.Common.Constantes;
using GAP.Appointments.Core.Repository;
using GAP.Appointments.TO.Appointments;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GAP.Appointments.Api.Controllers
{
    [AllowAnonymous]
    public class AppointmentController : ApiController
    {
        private IInfoAppointmentRepository _repository;

        //public IInfoAppointmentRepository InitInfoAppointmentRepository()
        //{
        //    InfoAppointmentRepository u = new InfoAppointmentRepository();
        //    return u;
        //}

        public AppointmentController()
        {
           // _repository = InitInfoAppointmentRepository();
        }
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="GAP.Appointments.Api.Controllers.AppointmentController"/>
        /// </summary>
        /// <param name="repository">Instancia del repositorio inyectada a través de spring.</param>    
        public AppointmentController(IInfoAppointmentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all info about employees
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(PatientTO))]
        [Route(ConstantesApi.getPatientInfoUri)]
        public async Task<IHttpActionResult> GetPatientInfo(string IdPatient)
        {
            PatientTO PatientInfo = await _repository.GetPatientInfo(IdPatient);
            return Ok(PatientInfo);
        }

        /// <summary>
        /// Get all info about employees
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(ICollection<AppointmenTO>))]
        [Route(ConstantesApi.getAppointmentsUri)]
        public async Task<IHttpActionResult> GetAppointments(string IdPatient)
        {
            ICollection<AppointmenTO> AppointmenInfo = await _repository.GetAppointments(IdPatient); 
            return Ok(AppointmenInfo);
        }

        /// <summary>
        /// Get all types
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(ICollection<TypeTO>))]
        [Route(ConstantesApi.getTypesUri)]
        public async Task<IHttpActionResult> GetTypes()
        {
            ICollection<TypeTO> Types = await _repository.GetTypes();
            return Ok(Types);
        }

        /// <summary>
        /// Get all states
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(ICollection<StateTO>))]
        [Route(ConstantesApi.getStatesUri)]
        public async Task<IHttpActionResult> GetStates()
        {
            ICollection<StateTO> States = await _repository.GetStates();
            return Ok(States);
        }

        /// <summary>
        /// Get all info about employees
        /// </summary>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route(ConstantesApi.getCreateAppointmentUri)]
        public async Task<IHttpActionResult> CreateAppointment(AppointmenTO filtro)
        {
            bool AppointmenState = await _repository.CreateAppointment(filtro);
            return Ok(AppointmenState);
        }
    }

    
}
