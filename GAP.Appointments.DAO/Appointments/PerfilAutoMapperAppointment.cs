#region Derechos Reservados
// ===================================================
// Desarrollado Por             : julian.rojas
// Fecha de Creación            : 2017/08/03
// Modificado Por               : julian.rojas
// Fecha Modificación           : 2017/08/03
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion
#region Referencias
using AutoMapper;
using GAP.Appointments.Domain;
using GAP.Appointments.TO.Appointments;
#endregion

namespace GAP.Appointments.DAO.Appointments
{
    /// <summary>
    /// Perfil de AutoMapper que contiene todos los mapeos del repositorio de captura.
    /// </summary>
    public class PerfilAutoMapperAppointment : Profile
    {
        /// <summary>
        /// Construye una nueva instancia de la clase
        /// </summary>
        public PerfilAutoMapperAppointment()
        {          

            CreateMap<Appointment, AppointmenTO>()
               .ForMember(dest => dest.Date, options => options.MapFrom(source => source.Date))
                .ForMember(dest => dest.IdKey, options => options.MapFrom(source => source.IdKey))
                .ForMember(dest => dest.IdKeyPacient, options => options.MapFrom(source => source.IdKeyPacient))
                .ForMember(dest => dest.IdState, options => options.MapFrom(source => source.IdState))
                .ForMember(dest => dest.IdType, options => options.MapFrom(source => source.IdType))
                .ForMember(dest => dest.StateDescription, options => options.MapFrom(source => source.State.Description))
                .ForMember(dest => dest.TypeDescription, options => options.MapFrom(source => source.Type.Description)
                );

            CreateMap<Pacient, PatientTO>();

            CreateMap<AppointmenTO, Appointment>();

            CreateMap<Type, TypeTO>();

            CreateMap<State, StateTO>();
        }
    }
}
