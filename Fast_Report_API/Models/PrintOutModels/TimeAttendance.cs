namespace Fast_Report_API.Models.PrintOutModels
{
    public class TimeAttendance
    {
    
        public DateTime date { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string year { get; set; }
        public string monthName { get; set; }
        public string tardiness { get; set; }
        public string undertime { get; set; }
        public string schedule { get; set; }
    }

    public class Record
    {
        public DateTime date { get; set; }
        public string schedule { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string year { get; set; }
        public string monthName { get; set; }
        public string tardiness { get; set; }
        public string undertime { get; set; }
    }
}
