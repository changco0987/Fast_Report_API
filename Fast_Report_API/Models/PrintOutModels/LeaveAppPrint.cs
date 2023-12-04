using System.ComponentModel;

namespace Fast_Report_API.Models.PrintOutModels
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
        public string from_needed_date { get; set; }
        public string to_needed_date { get; set; }
        public string no_days { get; set; }
        public string leaveDetails { get; set; }
        



        public string other_remarks { get; set; }
        public string imageUrl { get; set; }
        public string imageUrlDept { get; set; }
        public string PGHimageUrl { get; set; }
        public string UPimageUrlPNG { get; set; }
        public string leave_balance { get; set; }
        public string imageUrlHR { get; set; }
        public string remarks { get; set; }
        public string approved_date { get; set; }
        public List<leave_names> leave_list { get; set; }





    }

    public class leave_names
    {
        public string? name { get; set; }
        public string? value { get; set; }
    }
    public class leave_names_desc
    {
        public string? name { get; set; }
        public string? value { get; set; }
    }
}
