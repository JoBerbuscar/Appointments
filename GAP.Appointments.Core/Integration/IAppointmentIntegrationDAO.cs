using GAP.Appointments.TO.Appointments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAP.Appointments.Core.Integration
{
    /// <summary>
    /// Interface with data access methods for the Employees Service
    /// </summary>
    public interface IAppointmentIntegrationDAO
    {
        /// <summary>
        /// Get appointments by IdPatient
        /// </summary>
        /// <returns>List of <see cref="AppointmenTO"/></returns>
        Task<ICollection<AppointmenTO>> GetAppointments(string IdPatient);

        /// <summary>
        /// Get patient info by IdPatient
        /// </summary>
        /// <returns>List of <see cref="PatientTO"/></returns>
        Task<PatientTO> GetPatientInfo(string IdPatient);

        /// <summary>
        /// Get types
        /// </summary>
        /// <returns>List of <see cref="TypeTO"/></returns>
        Task<ICollection<TypeTO>> GetTypes();

        /// <summary>
        /// Get states
        /// </summary>
        /// <returns>List of <see cref="StateTO"/></returns>
        Task<ICollection<StateTO>> GetStates();

        /// create appointments by idPacient
        /// </summary>
        /// <param name="filtro">appointmente</param>
        /// <returns>Instance of <see cref="AppointmenTO"/> with Employee information</returns>
        Task<bool> CreateAppointment(AppointmenTO filtro);
    }
}
