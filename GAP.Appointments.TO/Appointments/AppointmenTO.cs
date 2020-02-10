
namespace GAP.Appointments.TO.Appointments
{
    public class AppointmenTO
    {
        public int IdKey { get; set; }
        public int IdKeyPacient { get; set; }
        public System.DateTime Date { get; set; }
        public int IdType { get; set; }
        public int IdState { get; set; }
        public string StateDescription { get; set; }
        public string TypeDescription  { get; set; }
    }
}
