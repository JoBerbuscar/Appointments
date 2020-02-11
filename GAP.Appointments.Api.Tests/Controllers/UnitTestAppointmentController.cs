using System;
using System.Configuration;
using System.Threading.Tasks;
using GAP.Appointments.Core.Integration;
using GAP.Appointments.Core.Repository;
using GAP.Appointments.DAO.Appointments;
using GAP.Appointments.DAO.AutoMapper;
using GAP.Appointments.TO.Appointments;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace GAP.Appointments.Api.Tests.Controllers
{
    [TestClass]
    public class UnitTestAppointmentController
    {
        public static ServiceContainer Container { get; set; }
        public UnitTestAppointmentController()
        {
            AutoMapperConfig.RegistrarMapeosGlobales();
            //Se registra el encargado de resolver las instancias y dependencias
            //Service Container
            Container = new ServiceContainer();
            Container.Register<IAppointmentIntegrationDAO, AppointmentDAO>();
            Container.Register<IInfoAppointmentRepository, InfoAppointmentRepository>();
        }

        [TestMethod]
        public async Task AddAppointmentDiferentDayByPacient()
        {
            var _appointmentRepository = Container.GetInstance<IInfoAppointmentRepository>();
            AppointmenTO AppointmenTO = new AppointmenTO();

            //types
            //1 Medicina General
            //2 Odontología
            //3 Pediatría
            //4 Neurología

            //states
            //1 Programado
            //2 Cancelado
            //3 Verificado

            AppointmenTO.Date = DateTime.Now;
            AppointmenTO.IdKeyPacient = 1; //
            AppointmenTO.IdType = 1;
            AppointmenTO.IdState = 1;

            bool CanAddAppointment = await  _appointmentRepository.CreateAppointment(AppointmenTO);

            Assert.IsNotNull(CanAddAppointment);
            Assert.IsTrue(CanAddAppointment);
        }

        [TestMethod]
        public async Task Success_CancelAppointmentLess_24_Hours_ByPacient()
        {
            var _appointmentRepository = Container.GetInstance<IInfoAppointmentRepository>();
            AppointmenTO AppointmenTO = new AppointmenTO();

            //types
            //1 Medicina General
            //2 Odontología
            //3 Pediatría
            //4 Neurología

            //states
            //1 Programado
            //2 Cancelado
            //3 Verificado           

            //prueba exito
            AppointmenTO.Date = DateTime.Now;
            AppointmenTO.IdKey = 1; //id appointment
            AppointmenTO.IdKeyPacient = 1; // id pacient
            AppointmenTO.IdType = 1;//type
            AppointmenTO.IdState = 2;//state cancel

            bool CanAddAppointment = await _appointmentRepository.UpdateAppointment(AppointmenTO);

            Assert.IsNotNull(CanAddAppointment);
            Assert.IsTrue(CanAddAppointment);
        }

        [TestMethod]
        public async Task Error_CancelAppointmentLess_24_Hours_ByPacient()
        {
            var _appointmentRepository = Container.GetInstance<IInfoAppointmentRepository>();
            AppointmenTO AppointmenTO = new AppointmenTO();

            //types
            //1 Medicina General
            //2 Odontología
            //3 Pediatría
            //4 Neurología

            //states
            //1 Programado
            //2 Cancelado
            //3 Verificado

            //prueba error
            AppointmenTO.Date = DateTime.Now;
            AppointmenTO.IdKey = 6; //id appointment
            AppointmenTO.IdKeyPacient = 1; // id pacient
            AppointmenTO.IdType = 1;//type
            AppointmenTO.IdState = 2;//state cancel

            bool CanAddAppointment = await _appointmentRepository.UpdateAppointment(AppointmenTO);

            Assert.IsNotNull(CanAddAppointment);
            Assert.IsTrue(CanAddAppointment);
        }
    }
}
