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
        /// Get appointments by IdPacient
        /// </summary>
        /// <returns>List of <see cref="AppointmenTO"/></returns>
        Task<ICollection<AppointmenTO>> GetAppointments(string IdPacient);

        /// <summary>
        /// Get Pacient info by IdPacient
        /// </summary>
        /// <returns>List of <see cref="PacientTO"/></returns>
        Task<PacientTO> GetPacientInfo(string IdPacient);

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

        /// update appointments by idPacient
        /// </summary>
        /// <param name="filtro">appointmente</param>
        /// <returns>Instance of <see cref="AppointmenTO"/> with Employee information</returns>
        Task<bool> UpdateAppointment(AppointmenTO filtro);
    }
}
