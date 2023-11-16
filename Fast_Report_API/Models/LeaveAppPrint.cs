using System.ComponentModel;

namespace Fast_Report_API.Models
{
    public class LeaveAppPrint
    {
        public string first_name { get; set; }

        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string department_name { get; set; }
        public string date_of_filing { get; set; }
        public string position_name { get; set; }
        public string leave_type_name { get; set; }
        public string leaveDetails { get; set; }

        public string other_remarks { get; set; }
        public string from_needed_date { get; set; }
        public string to_needed_date { get; set; }
        public string no_days { get; set; }
        public string imageUrl { get; set; }
        public string imageUrlDept { get; set; }
        public string UPimageUrlPNG { get; set; }
        public string PGHimageUrl { get; set; }
    }
}
