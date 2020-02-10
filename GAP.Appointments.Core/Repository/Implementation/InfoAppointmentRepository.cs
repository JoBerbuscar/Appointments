using GAP.Appointments.Core.Integration;
using GAP.Appointments.TO.Appointments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAP.Appointments.Core.Repository
{
    /// <summary>
    /// Implement operations for the Employees Service
    /// </summary>
    public class InfoAppointmentRepository : IInfoAppointmentRepository
    {
        /// <summary>
        /// Instance DAO for appointments
        /// </summary>
        public IAppointmentIntegrationDAO AppointmentDAO { get; set; }

        public InfoAppointmentRepository()
        {
        }

        /// <see cref="GAP.Appointments.Core.Repository.IEmployeeRepository.GetAppointments(string)"
        public async Task<ICollection<AppointmenTO>> GetAppointments(string IdPatient)
        {
            //Get employees list from data source
            ICollection<AppointmenTO> appointments = await AppointmentDAO.GetAppointments(IdPatient);
            return appointments;
        }

        /// <see cref="GAP.Appointments.Core.Repository.IEmployeeRepository.GetPatientInfo(string)"
        public async Task<PatientTO> GetPatientInfo(string IdPatient)
        {
            //Get employees list from data source
            PatientTO patient = await AppointmentDAO.GetPatientInfo(IdPatient);
            return patient;
        }

        /// <summary>
        /// Get types
        /// </summary>
        /// <returns>List of <see cref="TypeTO"/></returns>
        public async Task<ICollection<TypeTO>> GetTypes()
        {
            //Get employees list from data source
            ICollection<TypeTO> types = await AppointmentDAO.GetTypes();
            return types;
        }

        /// <summary>
        /// Get states
        /// </summary>
        /// <returns>List of <see cref="StateTO"/></returns>
        public async Task<ICollection<StateTO>> GetStates()
        {
            //Get employees list from data source
            ICollection<StateTO> states = await AppointmentDAO.GetStates();
            return states;
        }

        /// <see cref="GAP.Appointments.Core.Repository.IEmployeeRepository.GetAppointments(string)"
        public async Task<bool> CreateAppointment(AppointmenTO filtro)
        {
            //Get employees list from data source
            bool appointmentState = await AppointmentDAO.CreateAppointment(filtro);
            return appointmentState;
        }
    }
}
