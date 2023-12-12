using FastReport.Web;

namespace Fast_Report_API.Models.PrintOutModels
{
    public class Noa
    {
        public string? title { get; set; }
        //Data from noa_tbl
        public string? noa_contract_ID { get; set; }
        public string? noa_title { get; set; }
        public string? grand_total { get; set; }
        public string? grand_total_amount_in_words { get; set; }
        public string? date_needed { get; set; }
        public string? date_bid { get; set; }
        public string? date_awarded { get; set; }
        public string? type { get; set; }
        public string? position_name { get; set; }

        //Company info from suppliers_tbl
        public string? supplier_name { get; set; }
        public string? contact_person { get; set; }
        public string? delivery_address { get; set; }
        public string? contact { get; set; }
        public string? position { get; set; }
        public string? fax_number { get; set; }                    
        public string? pur_tbl { get; set; }
        public string? attention_title { get; set; }
        //Approving authority
        public string? first_name { get; set; }
        public string? last_name { get; set; }

        //Data from departments_tbl
        public string? department_name { get; set; }

        //Data from mode_off_procurement
        public string? mode_name { get; set; }
        public string? mode_description { get; set; }

        //Data for performance bond

        public string? perf_sec_30 { get; set; }
        public string? perf_sec_5 { get; set; }


        //Items info from noa_details_tbl
        //public string? description { get; set; }
        //public string? unit_cost { get; set; }
        //public string? total_cost { get; set; }

        public List<Noa_details>? noa_details { get; set; }
        public List<Noa_terms>? terms_and_condition { get; set; }

    }



    public class ReportModel
    {
        public WebReport webReport { get; set; }
    }

}