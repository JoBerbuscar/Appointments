using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class UnitTesIntegrationAppointmentController
    {       

        [TestMethod]
        public void GetInfoPacientByPacient()
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
            Assert.IsNotNull(Pacient.Name);
        }

        [TestMethod]
        public void GetInfoAppointmentsByPacient()
        {
            //token
            RestClient client;
            string appointmentServiceUrl = ConfigurationManager.AppSettings["APIRoot"];
            client = new RestClient(appointmentServiceUrl);

            var request = new RestRequest("GetAppointments", Method.GET);
            request.AddQueryParameter("IdPacient", "Berbuscar"); // aplica para cada estacion, etiqueta, parametro

            // execute the request
            IRestResponse response = client.ExecuteAsGet(request, "GET");

            List<AppointmenTO> AppointmentsByPacient = JsonConvert.DeserializeObject<List<AppointmenTO>>(response.Content);

            Assert.IsNotNull(AppointmentsByPacient);
            Assert.IsTrue(AppointmentsByPacient.Count > 0);
        }

        [TestMethod]
        public void CreateInfoAppointmentsByPacient()
        {
            //token
            RestClient client;
            string appointmentServiceUrl = ConfigurationManager.AppSettings["APIRoot"];
            client = new RestClient(appointmentServiceUrl);

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

            var request = new RestRequest("CreateAppointment", Method.POST);
            // easily add HTTP Headers
            request.AddJsonBody(AppointmenTO);

            // execute the request
            IRestResponse response = client.ExecuteAsPost(request, "POST");

            bool AddAppointmentsByPacient = JsonConvert.DeserializeObject<bool>(response.Content);

            Assert.IsNotNull(AddAppointmentsByPacient);
            Assert.IsTrue(AddAppointmentsByPacient);
        }

        [TestMethod]
        public void UpdateInfoAppointmentsByPacient()
        {
            //token
            RestClient client;
            string appointmentServiceUrl = ConfigurationManager.AppSettings["APIRoot"];
            client = new RestClient(appointmentServiceUrl);

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
            AppointmenTO.IdKey = 6; //id appointment
            AppointmenTO.IdKeyPacient = 1; // id pacient
            AppointmenTO.IdType = 1;//type
            AppointmenTO.IdState = 2;//state cancel

            var request = new RestRequest("UpdateAppointment", Method.POST);
            // easily add HTTP Headers
            request.AddJsonBody(AppointmenTO);

            // execute the request
            IRestResponse response = client.ExecuteAsPost(request, "POST");

            bool AddAppointmentsByPacient = JsonConvert.DeserializeObject<bool>(response.Content);

            Assert.IsNotNull(AddAppointmentsByPacient);
            Assert.IsTrue(AddAppointmentsByPacient);
        }
    }
}
