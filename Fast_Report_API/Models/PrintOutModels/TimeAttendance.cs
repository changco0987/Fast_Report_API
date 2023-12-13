namespace Fast_Report_API.Models.PrintOutModels
{
    public class TimeAttendance
    {
    
        public DateTime date { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string name { get; set; }
        public string department { get; set; }
    }

    public class Record
    {
        public DateTime date { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string name { get; set; }
        public string department { get; set; }
    }
}
