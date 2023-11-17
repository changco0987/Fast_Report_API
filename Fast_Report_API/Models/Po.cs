using FastReport.Web;
namespace Fast_Report_API.Models
{
    public class Po
    {
        // Noa columns
        public string? title { get; set; }
        public string? noa_contract_ID { get; set; }
        public string? noa_title { get; set; }
        public string? grand_total { get; set; }
        public string? grand_total_amount_in_words { get; set; }
        public string? date_needed { get; set; }
        public string? supplier_name { get; set; }
        public string? contact_person { get; set; }
        public string? delivery_address { get; set; }
     
        public string? position { get; set; }
        public string? position_name { get; set; }
        public string? pur_tbl { get; set; }
        public string? fax_number { get; set; }
        public string? attention_title { get; set; }
        public string? department_name { get; set; }
        public string? mode_name { get; set; }
        public string? mode_description { get; set; }
        public string? perf_sec_30 { get; set; }
        public string? perf_sec_5 { get; set; }

        // Purchase Order columns
        public int? purchase_order_ID { get; set; }
        public string? purchase_order_number { get; set; }
        public string? TIN { get; set; }
        public string? contact { get; set; }
        public string? place_of_delivery { get; set; }
        public string? payment_term { get; set; }
        public int? commodities_ID { get; set; }
        public string? commodities { get; set; }
        public string? prefix { get; set; }
        public string? delivery_term { get; set; }
        public string? supplier_date { get; set; }
        public string? fund_cluster { get; set; }
        public string? funds_available { get; set; }
        public string? ors_burs_number { get; set; }
        public string? ors_burs_date { get; set; }
        public string? amount { get; set; }
        public string? auth_first_name { get; set; }
        public string? auth_last_name { get; set; }
        public string? chief_first_name { get; set; }
        public string? chief_last_name { get; set; }

        //new update po 
        public string? date_needed_po { get; set; }
        public string? auth_user_position { get; set; }
        public string? chief_accountant_user_position { get; set; }

        //terms
        public List<Noa_details>? noa_details { get; set; }
        public List<Noa_terms>? terms_and_condition { get; set; }
    }
}
