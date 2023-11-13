namespace Fast_Report_API.Models
{
    public class ApplicationForm_model
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? age { get; set; }
        public string? sex { get; set; }
        public string? civil_status { get; set; }
        public string? maiden_name { get; set; }
        public string? spouse_name { get; set; }
        public string? birthdate { get; set; }
        public string? birth_place { get; set; }
        public string? citizenship { get; set; }
        public string? country_if_not_filipino { get; set; }
        public string? address_city { get; set; }
        public string? address_province { get; set; }
        public string? telephone_no { get; set; }
        public string? cellphone_no { get; set; }
        public string? father_name { get; set; }
        public string? mother_name { get; set; }
        public string? sub_specialty { get; set; }
        public string? specialty { get; set; }
        public string? position_applied { get; set; }
        public string? email { get; set; }
        public List<Educational_background>? educational_background { get; set; }
        public List<Work_experience>? work_experiences { get; set; }
        public List<Recognitions>? recognitions { get; set; }
        public List<References>? references { get; set; }

    }

    public class Educational_background 
    {
        public string? level { get; set; }
        public string? degree { get; set; }
        public string? school_name { get; set; }
        public string? year_graduated { get; set; }
    }

    public class Work_experience
    {
        public string? type { get; set; }
        public string? organization { get; set; }
        public string? year_completed { get; set; }
    }

    public class Recognitions
    {
        public string? recognition_details { get; set; }
    }

    public class References
    {
        public string? name { get; set; }
        public string? addres { get; set; }
        public string? contact_no { get; set; }
    }
}
