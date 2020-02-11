using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GAP.Appointments.Core.Integration;
using GAP.Appointments.Domain;
using GAP.Appointments.TO.Appointments;
using System.Linq;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Data.Entity.SqlServer;

namespace GAP.Appointments.DAO.Appointments
{
    public class AppointmentDAO : IAppointmentIntegrationDAO
    {
        /// <see cref="MasGlobal.Employees.Domain.Integration.EmployeeIntegrationDAO.GetEmployees"/>
        public async Task<ICollection<AppointmenTO>> GetAppointments(string IdPacient)
        {
            List<AppointmenTO> appointmentsByPacient = new List<AppointmenTO>();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                appointmentsByPacient = await (from app in dbContext.Appointments
                                               join pat in dbContext.Pacients on app.IdKeyPacient equals pat.IdKey
                                               where pat.Id == IdPacient
                                               select app).ProjectTo<AppointmenTO>().ToListAsync();
            }
            return appointmentsByPacient;
        }

        public async Task<PacientTO> GetPacientInfo(string IdPacient)
        {
            PacientTO PacienteInfoByPacient = new PacientTO();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                PacienteInfoByPacient = await dbContext.Pacients.Where(x=> x.Id == IdPacient).ProjectTo<PacientTO>().FirstOrDefaultAsync();
            }
            return PacienteInfoByPacient;
        }

        public async Task<ICollection<TypeTO>> GetTypes()
        {
            List< TypeTO> types = new List<TypeTO>();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                types = await dbContext.Types.ProjectTo<TypeTO>().ToListAsync();
            }
            return types;
        }

        public async Task<ICollection<StateTO>> GetStates()
        {
            List<StateTO> states = new List<StateTO>();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                states = await dbContext.States.ProjectTo<StateTO>().ToListAsync();
            }
            return states;
        }

        public async Task<bool> CreateAppointment(AppointmenTO filtro)
        {
            List<AppointmenTO> appointmentsByPacient = new List<AppointmenTO>();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                appointmentsByPacient = await (from app in dbContext.Appointments
                                               join pat in dbContext.Pacients on app.IdKeyPacient equals pat.IdKey    
                                               join sta in dbContext.Types on app.Type.IdKey equals sta.IdKey
                                               where app.IdKeyPacient == filtro.IdKeyPacient
                                                 && DbFunctions.TruncateTime(app.Date) == filtro.Date.Date
                                                 && sta.Description == "Programado"
                                               select app).ProjectTo<AppointmenTO>().ToListAsync();

                if (appointmentsByPacient.Count <= 0)
                {
                    Appointment AppointmentCreate = Mapper.Map<Appointment>(filtro);
                    dbContext.Appointments.Add(AppointmentCreate);
                    dbContext.SaveChanges();
                }
                else
                {
                    return false;
                }
                
            }
            return true;
        }

        public async Task<bool> UpdateAppointment(AppointmenTO filtro)
        {
            List<AppointmenTO> appointmentsByPacient = new List<AppointmenTO>();
            using (AppointmentsEntities dbContext = new AppointmentsEntities())
            {
                appointmentsByPacient = await (from app in dbContext.Appointments
                                               join pat in dbContext.Pacients on app.IdKeyPacient equals pat.IdKey
                                               where app.IdKeyPacient == filtro.IdKeyPacient
                                                 && app.IdKey == filtro.IdKey
                                                 &&  DbFunctions.DiffHours(app.Date, SqlFunctions.GetDate().Value) > 24
                                               select app).ProjectTo<AppointmenTO>().ToListAsync();

                if (appointmentsByPacient.Count <= 0)
                {
                    Appointment AppointmentUpdate = Mapper.Map<Appointment>(filtro);
                    dbContext.Appointments.Attach(AppointmentUpdate);
                    var entry = dbContext.Entry(AppointmentUpdate);
                    entry.Property(e => e.IdState).IsModified = true;
                    // other changed properties
                    dbContext.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            return true;
        }
    }
}
