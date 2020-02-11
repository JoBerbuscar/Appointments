using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using GAP.Appointments.Core.Integration;
using GAP.Appointments.Core.Repository;
using GAP.Appointments.DAO.Appointments;
using GAP.Appointments.TO.Appointments;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace GAP.Appointments.Api.Tests.Controllers
{
    [TestClass]
    public class AppointmentController
    {
        public static ServiceContainer Container { get; set; }
        public AppointmentController()
        {
            //Se registra el encargado de resolver las instancias y dependencias
            //Service Container
            Container = new ServiceContainer();
            Container.Register<IAppointmentIntegrationDAO, AppointmentDAO>();
            Container.Register<IInfoAppointmentRepository, InfoAppointmentRepository>();
        }

        [TestMethod]
        public void GetInfoEmployeesByURL()
        {
            //token
            RestClient client;
            string appointmentServiceUrl = ConfigurationManager.AppSettings["APIRoot"];
            client = new RestClient(appointmentServiceUrl);

            var request = new RestRequest("GetPacientInfo", Method.GET);
            request.AddQueryParameter("IdPacient", "Berbuscar"); // aplica para cada estacion, etiqueta, parametro

            // execute the request
            IRestResponse response = client.ExecuteAsGet(request, "GET");

            PacientTO Pacient = JsonConvert.DeserializeObject<PacientTO>(response.Content);

            Assert.IsNotNull(Pacient);
        }
    }
}
