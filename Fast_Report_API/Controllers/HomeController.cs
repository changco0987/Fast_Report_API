using Fast_Report_API.Models;
using FastReport;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using Fast_Report_API.Controllers;
using FastReport.Web;
using System.Drawing;
using System.Text.Json;
using System.Globalization;
using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using FastReport.DataVisualization.Charting;
using Microsoft.EntityFrameworkCore;
using Fast_Report_API.Models.PrintOutModels;

namespace Fast_Report_API.Controllers
{
    //[Route("api/[controller]")]
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;
        private PghContext _context;
        private IWebHostEnvironment _env;
        private List<Noa> noa_list;
        private List<Po> po_list;
        private List<Noa_terms> noa_terms_list;
        private List<Noa_details> noa_detail_list;


        private List<LeaveAppPrint> leave_list;
        private List<TimeAttendance> Time_Record;
        private List<leave_names> leave_name_list;
        //private List<leave_names_desc> leave_name_desc_list;


        private List<ApplicationForm_model> application_form_list = new List<ApplicationForm_model>();
        private List<Educational_background> educational_Backgrounds_list = new List<Educational_background>();
        private List<Work_experience> work_experience_list = new List<Work_experience>();
        private List<Recognitions> recognition_list = new List<Recognitions>();
        private List<References> references_list = new List<References>();

        //application_form_list = new List<ApplicationForm_model>();
        //educational_Backgrounds_list = new List<Educational_background>();
        //work_experience_list = new List<Work_experience>();
        //recognition_list = new List<Recognitions>();
        //references_list = new List<References>();

        private string fileName;


        //public WebReport UserWebReport { get; set; }

        Report Report { get; set; }
        DataSet DataSet { get; }
        ToolbarSettings Toolbar { get; }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, PghContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        public static string Base64Decode(string base64EncodedData)
        {
            string replaced = base64EncodedData.Replace(" ", "+");
            var base64EncodedBytes = System.Convert.FromBase64String(replaced);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Generate(string title, string response, string response2, string response3)
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();
            //MapPath mapPath = new MapPath(_env);

            //rep.Report.SetParameterValue("param1", noa_list[0].noa_title);
            //rep.Report.SetParameterValue("param2","2nd parameter");
            //rep.Report.SetParameterValue("param3","3rd parameter");
            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;


            //rep.Toolba




            noa_list = new List<Noa>();
            leave_list = new List<LeaveAppPrint>();
            string decode = Base64Decode(response);//base64 to string
            //string decode = "";
            //Add another condition statement if there's new file/report to print
            if (title.ToLower().Equals("noa"))
            {

                var noa_details = Base64Decode(response2);//base64 to string

                var noa_terms = Base64Decode(response3);//base64 to string

                var noa_data = System.Text.Json.JsonSerializer.Deserialize<Noa>(decode);
                fileName = "/" + noa_data.title + "_report.frx";
                string path = Path.Combine(_env.WebRootPath + fileName);
                //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
                UserWebReport.Report.Load(path);



                noa_list.Add(new Noa()
                {
                    noa_contract_ID = noa_data.noa_contract_ID,
                    noa_title = noa_data.noa_title,
                    grand_total = noa_data.grand_total,
                    grand_total_amount_in_words = noa_data.grand_total_amount_in_words,
                    date_needed = DateTime.ParseExact(noa_data.date_needed, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                    supplier_name = noa_data.supplier_name,
                    delivery_address = noa_data.delivery_address,
                    contact = noa_data.contact,
                    position = noa_data.position,
                    fax_number = noa_data.fax_number,
                    pur_tbl = noa_data.pur_tbl,
                    attention_title = noa_data.attention_title,
                    contact_person = noa_data.contact_person,
                    first_name = noa_data.first_name,
                    last_name = noa_data.last_name,
                    department_name = noa_data.department_name,
                    mode_name = noa_data.mode_name,
                    mode_description = noa_data.mode_description,
                    perf_sec_30 = noa_data.perf_sec_30,
                    perf_sec_5 = noa_data.perf_sec_5,
                });

                //This will assign list of noa_details separately
                noa_detail_list = new List<Noa_details>();
                if (noa_details != null)
                {

                    var noa_details_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_details>>(noa_details);
                    foreach (var data in noa_details_converted)
                    {
                        noa_detail_list.Add(new Noa_details
                        {
                            quantity = data.quantity,
                            item_number = data.item_number,
                            uom_name = data.uom_name,
                            unit_cost = data.unit_cost,
                            total_cost = data.total_cost,
                            description = data.description,
                            stock_property_number = data.stock_property_number,
                        });
                    }
                }

                noa_terms_list = new List<Noa_terms>();
                if (noa_terms != null)
                {
                    var noa_terms_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_terms>>(noa_terms);
                    foreach (var datas in noa_terms_converted)
                    {
                        noa_terms_list.Add(new Noa_terms
                        {
                            number = datas.number,
                            description = datas.description,
                        });
                    }

                }

                UserWebReport.Report.RegisterData(noa_list, "NoaRef");//pass the data to fast report
                UserWebReport.Report.RegisterData(noa_terms_list, "NoaRefTerm");//pass the data to fast report
                UserWebReport.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report
            }
            else if (title.ToLower().Equals("leaveapplication"))
            {

                var leave = System.Text.Json.JsonSerializer.Deserialize<LeaveAppPrint>(decode);
                fileName = "/" + title + "_report.frx";
                string path = Path.Combine(_env.WebRootPath + fileName);
                //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
                UserWebReport.Report.Load(path);

                leave_list.Add(new LeaveAppPrint()
                {
                    first_name = leave.first_name,
                    middle_name = leave.middle_name,
                    last_name = leave.last_name,
                    department_name = leave.department_name,
                    date_of_filing = leave.date_of_filing,
                    position_name = leave.position_name,
                    leave_type_name = leave.leave_type_name,
                    other_remarks = leave.other_remarks,
                    imageUrl = leave.imageUrl,
                    imageUrlDept = leave.imageUrlDept,
                    leave_balance = leave.leave_balance,

                });

                //This will assign list of noa_details separately
                //noa_detail_list = new List<Noa_details>();

                //foreach (var data in leave.noa_details)
                //{
                //    noa_detail_list.Add(data);
                //}
                UserWebReport.Report.RegisterData(leave_list, "leaveRequest");

                ViewData["ReportName"] = "leaveapplication";
                ViewData["Resp"] = response;
                ViewData["Resp2"] = response2;
                ViewData["Resp3"] = response3;
            }
            else if (title.ToLower().Equals("appform"))
            {

                ApplicationForm_model application_details = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationForm_model>(decode);
                fileName = "/" + title + "_report.frx";
                string path = Path.Combine(_env.WebRootPath + fileName);
                //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
                UserWebReport.Report.Load(path);

                application_form_list = new List<ApplicationForm_model>();
                educational_Backgrounds_list = new List<Educational_background>();
                work_experience_list = new List<Work_experience>();
                recognition_list = new List<Recognitions>();
                references_list = new List<References>();

                application_form_list.Add(application_details);
                educational_Backgrounds_list = application_details.educational_background;
                work_experience_list = application_details.work_experiences;
                recognition_list = application_details.recognitions;
                references_list = application_details.references;

                UserWebReport.Report.RegisterData(application_form_list, "appForm_ref");
                UserWebReport.Report.RegisterData(educational_Backgrounds_list, "education_ref");
                UserWebReport.Report.RegisterData(work_experience_list, "work_exp_ref");
                UserWebReport.Report.RegisterData(recognition_list, "recognitions_ref");
                UserWebReport.Report.RegisterData(references_list, "references_ref");
            }


            else if (title.ToLower().Equals("po"))
            {
                var noa_details = Base64Decode(response2);//base64 to string

                var noa_terms = Base64Decode(response3);//base64 to string

                var po_data = System.Text.Json.JsonSerializer.Deserialize<Po>(decode);
                fileName = "/" + po_data.title + "_report.frx";
                string path = Path.Combine(_env.WebRootPath + fileName);
                //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
                UserWebReport.Report.Load(path);

                po_list = new List<Po>();

                po_list.Add(new Po()
                {
                    title ="" ,
                    noa_contract_ID = po_data.noa_contract_ID ?? "",
                    noa_title = po_data.noa_title ?? "",
                    grand_total = po_data.grand_total ?? "",
                    grand_total_amount_in_words = po_data.grand_total_amount_in_words ?? "",
                    date_needed = DateTime.ParseExact(po_data.date_needed, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                    supplier_name = po_data.supplier_name ?? "",
                    delivery_address = po_data.delivery_address ?? "",
                    position = po_data.position ?? "",
                    position_name = "sample" ?? ""/*po_data.position_name*/,//
                    fax_number = po_data.fax_number ?? "",
                    pur_tbl = po_data.pur_tbl ?? "",
                    attention_title = "sample"/*po_data.attention_title*/,//
                    contact_person = po_data.contact_person ?? "",
                    department_name = po_data.department_name ?? "",
                    mode_name = po_data.mode_name ?? "",
                    mode_description = po_data.mode_description ?? "",
                    perf_sec_30 = po_data.perf_sec_30 ?? "",
                    perf_sec_5 = po_data.perf_sec_5 ?? "",
                    //po
                    purchase_order_ID = po_data.purchase_order_ID ,
                    purchase_order_number = po_data.purchase_order_number ?? "",
                    TIN = po_data.TIN ?? "",
                    contact = po_data.contact ?? "",
                    place_of_delivery = po_data.place_of_delivery ?? "",
                    payment_term = po_data.payment_term ?? "",
                    commodities_ID = po_data.commodities_ID ,
                    commodities = po_data.commodities ?? "",
                    prefix = po_data.prefix ?? "",
                    delivery_term = po_data.delivery_term ?? "",
                    supplier_date = po_data.supplier_date ?? "",
                    fund_cluster = po_data.fund_cluster ?? "",
                    funds_available = po_data.funds_available ?? "",
                    ors_burs_number = po_data.ors_burs_number ?? "",
                    ors_burs_date = po_data.ors_burs_date ?? "",
                    amount = po_data.amount ?? "",
                    auth_first_name = po_data.auth_first_name ?? "",
                    auth_last_name = po_data.auth_last_name ?? "",
                    chief_first_name = po_data.chief_first_name ?? "",
                    chief_last_name = po_data.chief_last_name ?? "",
                    //new
                    date_needed_po = po_data.date_needed_po ?? "",
                    auth_user_position = po_data.auth_user_position ?? "",
                    chief_accountant_user_position = po_data.chief_accountant_user_position ?? "",
                });

                noa_detail_list = new List<Noa_details>();
                if (noa_details != null)
                {

                    var noa_details_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_details>>(noa_details);
                    foreach (var data in noa_details_converted)
                    {
                        noa_detail_list.Add(new Noa_details
                        {
                            quantity = data.quantity,
                            item_number = data.item_number,
                            uom_name = data.uom_name,
                            unit_cost = data.unit_cost,
                            total_cost = data.total_cost,
                            description = data.description,
                            stock_property_number = data.stock_property_number,
                        });
                    }
                }

                noa_terms_list = new List<Noa_terms>();
                if (noa_terms != null)
                {
                    var noa_terms_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_terms>>(noa_terms);
                    foreach (var datas in noa_terms_converted)
                    {
                        noa_terms_list.Add(new Noa_terms
                        {
                            number = datas.number,
                            description = datas.description,
                        });
                    }

                }

                UserWebReport.Report.RegisterData(po_list, "PoRef");//pass the data to fast report
                UserWebReport.Report.RegisterData(noa_terms_list, "NoaRefTerm");//pass the data to fast report
                UserWebReport.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report
            }

            ViewBag.WebReport = UserWebReport;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (UserWebReport.Report.Prepare())
            {


                //FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                //pdfExport.ShowProgress = false;
                //pdfExport.Subject = "Subject Report";
                //pdfExport.Title = "Report Report";
                //MemoryStream ms = new MemoryStream();
                //rep.Report.Export(pdfExport, ms);
                //rep.Report.Dispose();
                //pdfExport.Dispose();
                //ms.Position = 0;

                //ReportModel reportModel = new ReportModel();
                //reportModel.webReport = rep;
                //ViewBag.reportModel = reportModel;
                //return View("Views/Home/ReportView.cshtml", reportModel);
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> OldPurchasing(string title, string response, string response2, string response3) 
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();
            //MapPath mapPath = new MapPath(_env);

            //rep.Report.SetParameterValue("param1", noa_list[0].noa_title);
            //rep.Report.SetParameterValue("param2","2nd parameter");
            //rep.Report.SetParameterValue("param3","3rd parameter");
            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;


            //rep.Toolba




            noa_list = new List<Noa>();
            leave_list = new List<LeaveAppPrint>();
            string decode = Base64Decode(response);//base64 to string
            //string decode = "";
            //Add another condition statement if there's new file/report to print
         

            var noa_details = Base64Decode(response2);//base64 to string

            var noa_terms = Base64Decode(response3);//base64 to string

            var noa_data = System.Text.Json.JsonSerializer.Deserialize<Noa>(decode);
            fileName = "/" + noa_data.title + "_report.frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);



            noa_list.Add(new Noa()
            {
                noa_contract_ID = noa_data.noa_contract_ID,
                noa_title = noa_data.noa_title,
                grand_total = noa_data.grand_total,
                grand_total_amount_in_words = noa_data.grand_total_amount_in_words,
                date_needed = DateTime.ParseExact(noa_data.date_needed, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                supplier_name = noa_data.supplier_name,
                delivery_address = noa_data.delivery_address,
                contact = noa_data.contact,
                position = noa_data.position,
                fax_number = noa_data.fax_number,
                pur_tbl = noa_data.pur_tbl,
                attention_title = noa_data.attention_title,
                contact_person = noa_data.contact_person,
                first_name = noa_data.first_name,
                last_name = noa_data.last_name,
                department_name = noa_data.department_name,
                mode_name = noa_data.mode_name,
                mode_description = noa_data.mode_description,
                perf_sec_30 = noa_data.perf_sec_30,
                perf_sec_5 = noa_data.perf_sec_5,
            });

            //This will assign list of noa_details separately
            noa_detail_list = new List<Noa_details>();
            if (noa_details != null)
            {

                var noa_details_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_details>>(noa_details);
                foreach (var data in noa_details_converted)
                {
                    noa_detail_list.Add(new Noa_details
                    {
                        quantity = data.quantity,
                        item_number = data.item_number,
                        uom_name = data.uom_name,
                        unit_cost = data.unit_cost,
                        total_cost = data.total_cost,
                        description = data.description,
                        stock_property_number = data.stock_property_number,
                    });
                }
            }

            noa_terms_list = new List<Noa_terms>();
            if (noa_terms != null)
            {
                var noa_terms_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_terms>>(noa_terms);
                foreach (var datas in noa_terms_converted)
                {
                    noa_terms_list.Add(new Noa_terms
                    {
                        number = datas.number,
                        description = datas.description,
                    });
                }

            }

            UserWebReport.Report.RegisterData(noa_list, "NoaRef");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_terms_list, "NoaRefTerm");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report

            ViewBag.WebReport = UserWebReport;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (UserWebReport.Report.Prepare())
            {


                //FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                //pdfExport.ShowProgress = false;
                //pdfExport.Subject = "Subject Report";
                //pdfExport.Title = "Report Report";
                //MemoryStream ms = new MemoryStream();
                //rep.Report.Export(pdfExport, ms);
                //rep.Report.Dispose();
                //pdfExport.Dispose();
                //ms.Position = 0;

                //ReportModel reportModel = new ReportModel();
                //reportModel.webReport = rep;
                //ViewBag.reportModel = reportModel;
                //return View("Views/Home/ReportView.cshtml", reportModel);
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Noa(string title, int Id)
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();
            //MapPath mapPath = new MapPath(_env);

            //rep.Report.SetParameterValue("param1", noa_list[0].noa_title);
            //rep.Report.SetParameterValue("param2","2nd parameter");
            //rep.Report.SetParameterValue("param3","3rd parameter");
            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;


            //rep.Toolba




            noa_list = new List<Noa>();
            leave_list = new List<LeaveAppPrint>();


            var noa_data = await _context.NoaTbls.Where(x => x.NoaId == (ulong)Id)      
             .Include(x => x.NoaDetailsTbls)
             .Include(x => x.TermsAndConditionTbls)
             .Include(x => x.Supplier)
             .Include(x => x.ModeOfPrecurement)
             .Include(x => x.AppAuthUser)
             .Include(x => x.AppAuthUser.Department).FirstOrDefaultAsync();





            fileName = "/" + title + "_report.frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);



            noa_list.Add(new Noa()
            {
                noa_contract_ID = noa_data.NoaContractId,
                noa_title = noa_data.NoaTitle,
                grand_total = noa_data.GrandTotal.ToString(),
                grand_total_amount_in_words = noa_data.GrandTotalAmountInWords,
                date_needed = ((DateTime)noa_data.DateNeeded).ToString("yyyy-MM-dd"),
                supplier_name = noa_data.Supplier.SupplierName,
                delivery_address = noa_data.Supplier.DeliveryAddress,
                contact = noa_data.Supplier.ContactPerson,
                position = noa_data.Supplier.Position,
                fax_number = noa_data.Supplier.FaxNumber,
                pur_tbl = noa_data.PurTbl,
                attention_title = noa_data.Supplier.AttentionTitle,
                contact_person = noa_data.Supplier.ContactPerson,
                first_name = noa_data.AppAuthUser.FirstName,
                last_name = noa_data.AppAuthUser.LastName,
                department_name = noa_data.AppAuthUser.Department.DepartmentName,
                mode_name = noa_data.ModeOfPrecurement.ModeName,
                mode_description = noa_data.ModeOfPrecurement.ModeDescription,
                perf_sec_30 = noa_data.PerfSec30.ToString(),
                perf_sec_5 = noa_data.PerfSec5.ToString(),
            });

            //This will assign list of noa_details separately
            noa_detail_list = new List<Noa_details>();
            if (noa_data.NoaDetailsTbls != null)
            {
                foreach (var data in noa_data.NoaDetailsTbls)
                {
                    var uomName = await _context.UomTbls.Where(x => x.UomId == data.UomId).Select(x => x.UomName).FirstOrDefaultAsync();

                    noa_detail_list.Add(new Noa_details
                    {
                        quantity = data.Quantity.ToString(),
                        item_number = data.ItemNumber,
                        uom_name = uomName,
                        unit_cost = data.UnitCost.ToString(),
                        total_cost = data.TotalCost.ToString(),
                        description = data.Description,
                        stock_property_number = data.StockPropertyNumber,
                    });
                }
            }

            noa_terms_list = new List<Noa_terms>();
            if (noa_data.TermsAndConditionTbls != null)
            {
                foreach (var datas in noa_data.TermsAndConditionTbls)
                {
                    noa_terms_list.Add(new Noa_terms
                    {
                        number = (int)datas.Number,
                        description = datas.Description,
                    });
                }
            }

            UserWebReport.Report.RegisterData(noa_list, "NoaRef");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_terms_list, "NoaRefTerm");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report

            ViewBag.WebReport = UserWebReport;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (UserWebReport.Report.Prepare())
            {


                //FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                //pdfExport.ShowProgress = false;
                //pdfExport.Subject = "Subject Report";
                //pdfExport.Title = "Report Report";
                //MemoryStream ms = new MemoryStream();
                //rep.Report.Export(pdfExport, ms);
                //rep.Report.Dispose();
                //pdfExport.Dispose();
                //ms.Position = 0;

                //ReportModel reportModel = new ReportModel();
                //reportModel.webReport = rep;
                //ViewBag.reportModel = reportModel;
                //return View("Views/Home/ReportView.cshtml", reportModel);
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Purchasing(string title,int Id)
        {
            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();
            //MapPath mapPath = new MapPath(_env);

            //rep.Report.SetParameterValue("param1", noa_list[0].noa_title);
            //rep.Report.SetParameterValue("param2","2nd parameter");
            //rep.Report.SetParameterValue("param3","3rd parameter");
            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;

            //rep.Toolba

            po_list = new List<Po>();
            
            fileName = "/" + title + "_report.frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);

            var po = await _context.PurchaseOrdersTbls.Where(x => x.PurchaseOrderId == (ulong)Id)
                .Include(x => x.Commodities)
                .Include(x => x.SignatureAuthorizedOfficial)
                .Include(x => x.SignatureChiefAccountant)
                .Include(x => x.Noa)
                .Include(x => x.Noa.NoaDetailsTbls)
                .Include(x => x.Noa.TermsAndConditionTbls)
                .Include(x => x.Noa.Supplier)
                .Include(x => x.Noa.ModeOfPrecurement)
                .Include(x => x.Noa.AppAuthUser)
                .Include(x => x.Noa.AppAuthUser.Department).FirstOrDefaultAsync();


            var authUserPosition = await _context.PositionTbls.Where(x => x.PositionId == po.SignatureAuthorizedOfficialId).Select(x => x.PositionName).FirstOrDefaultAsync();
            var chiefAccountantUserPosition = await _context.PositionTbls.Where(x => x.PositionId == po.SignatureChiefAccountantId).Select(x => x.PositionName).FirstOrDefaultAsync();

            po_list.Add(new Po()
            {
                noa_contract_ID = po.Noa.NoaContractId,
                noa_title = po.Noa.NoaTitle,
                grand_total = po.Noa.GrandTotal.ToString(),
                grand_total_amount_in_words = po.Noa.GrandTotalAmountInWords,
                //date_needed = DateTime.ParseExact(po.Noa.DateNeeded, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                date_needed = ((DateTime)po.Noa.DateNeeded).ToString("yyyy-MM-dd"),
                supplier_name = po.Noa.Supplier.SupplierName,
                delivery_address = po.Noa.Supplier.DeliveryAddress,
                position = po.Noa.Supplier.Position,
                position_name = po.Noa.Supplier.Position,
                fax_number = po.Noa.Supplier.FaxNumber,
                pur_tbl = po.Noa.PurTbl,
                attention_title = po.Noa.Supplier.AttentionTitle,
                contact_person = po.Noa.Supplier.ContactPerson,
                department_name = po.Noa.AppAuthUser.Department.DepartmentName,
                mode_name = po.Noa.ModeOfPrecurement.ModeName,
                mode_description = po.Noa.ModeOfPrecurement.ModeDescription,
                perf_sec_30 = po.Noa.PerfSec30.ToString(),
                perf_sec_5 = po.Noa.PerfSec5.ToString(),
                //po
                purchase_order_ID = (int?)po.PurchaseOrderId,
                purchase_order_number = po.PurchaseOrderNumber,
                TIN = po.Noa.Supplier.Tin,
                contact = po.Noa.Supplier.Contact,
                place_of_delivery = po.PlaceOfDelivery,
                payment_term = po.PaymentTerm,
                commodities_ID = (int?)po.CommoditiesId,
                commodities = po.Commodities.Commodities,
                prefix = po.Commodities.Prefix,
                delivery_term = po.DeliveryTerm,
                supplier_date = ((DateTime)po.SupplierDate).ToString("yyyy-MM-dd"),
                fund_cluster = po.FundCluster,
                funds_available = po.FundsAvailable.ToString(),
                ors_burs_number = po.OrsBursNumber,
                ors_burs_date = ((DateTime)po.OrsBursDate).ToString("yyyy-MM-dd"),
                amount = po.Amount.ToString(),
                auth_first_name = po.SignatureAuthorizedOfficial.FirstName,
                auth_last_name = po.SignatureAuthorizedOfficial.LastName,
                chief_first_name = po.SignatureChiefAccountant.FirstName,
                chief_last_name = po.SignatureChiefAccountant.LastName,
                //new
                date_needed_po = ((DateTime)po.DateNeeded).ToString("yyyy-MM-dd"),
                auth_user_position = authUserPosition,
                chief_accountant_user_position = chiefAccountantUserPosition
            });



            //This will assign list of noa_details separately
            noa_detail_list = new List<Noa_details>();
            if (po.Noa.NoaDetailsTbls != null)
            {
                foreach (var data in po.Noa.NoaDetailsTbls)
                {
                    var uomName = await _context.UomTbls.Where(x => x.UomId == data.UomId).Select(x => x.UomName).FirstOrDefaultAsync();

                    noa_detail_list.Add(new Noa_details
                    {
                        quantity = data.Quantity.ToString(),
                        item_number = data.ItemNumber,
                        uom_name = uomName,
                        unit_cost = data.UnitCost.ToString(),
                        total_cost = data.TotalCost.ToString(),
                        description = data.Description,
                        stock_property_number = data.StockPropertyNumber,
                    });
                }
            }

            noa_terms_list = new List<Noa_terms>();
            if (po.Noa.TermsAndConditionTbls != null)
            {
                foreach (var datas in po.Noa.TermsAndConditionTbls)
                {
                    noa_terms_list.Add(new Noa_terms
                    {
                        number = (int)datas.Number,
                        description = datas.Description,
                    });
                }

            }

            UserWebReport.Report.RegisterData(po_list, "PoRef");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_terms_list, "NoaRefTerm");//pass the data to fast report
            UserWebReport.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report

            ViewBag.WebReport = UserWebReport;
            if (UserWebReport.Report.Prepare())
            {
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> LeaveApplication(string title, string response, string response2, string response3)
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();

            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;






            leave_list = new List<LeaveAppPrint>();
            leave_name_list = new List<leave_names>();
            //leave_name_desc_list = new List<leave_names_desc>();
            string decode = Base64Decode(response);//base64 to string

            var leave = System.Text.Json.JsonSerializer.Deserialize<LeaveAppPrint>(decode);
            fileName = "/" + title + "_report.frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);

            leave_list.Add(new LeaveAppPrint()
            {
                first_name = leave.first_name,
                middle_name = leave.middle_name,
                last_name = leave.last_name,
                department_name = leave.department_name,
                date_of_filing = leave.date_of_filing,
                position_name = leave.position_name,
                leave_type_name = leave.leave_type_name,
                other_remarks = leave.other_remarks,
                to_needed_date = leave.to_needed_date,
                from_needed_date = leave.from_needed_date,
                no_days = leave.no_days,
                PGHimageUrl = leave.PGHimageUrl,
                leaveDetails = leave.leaveDetails,
                imageUrl = leave.imageUrl,
                imageUrlDept = leave.imageUrlDept,
                leave_balance = leave.leave_balance,
                UPimageUrlPNG = leave.UPimageUrlPNG,
                imageUrlHR = leave.imageUrlHR,
                remarks = leave.remarks,
                approved_date = leave.approved_date,
                leave_list = leave.leave_list,
                status = leave.status,
                vacation_balance = leave.vacation_balance,
                sick_balance = leave.sick_balance,
                //leave_desc = leave.leave_desc,


            });
            leave_name_list = leave.leave_list;
            //leave_name_desc_list = leave.leave_desc;

            UserWebReport.Report.RegisterData(leave_list, "leaveRequest");
            UserWebReport.Report.RegisterData(leave_name_list, "leave_name_ref");
            //UserWebReport.Report.RegisterData(leave_name_desc_list, "leave_name_desc_ref");

            ViewData["ReportName"] = "leaveapplication";
            ViewData["Resp"] = response;
            ViewData["Resp2"] = response2;
            ViewData["Resp3"] = response3;


            ViewBag.WebReport = UserWebReport;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (UserWebReport.Report.Prepare())
            {
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> TimeAttendance(string response, string title)
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();

            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = false;
            UserWebReport.ReportPrepared = false;






            Time_Record = new List<TimeAttendance>();
            //leave_name_desc_list = new List<leave_names_desc>();
            string decode = Base64Decode(response);//base64 to string

            List<TimeAttendance> record = System.Text.Json.JsonSerializer.Deserialize<List<TimeAttendance>>(decode);
            //var record = Newtonsoft.Json.JsonConvert.DeserializeObject<TimeAttendance>(decode);
            fileName = "/" + title + ".frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);

            Time_Record = record;
           

            UserWebReport.Report.RegisterData(Time_Record, "TimeAttendance_ref");

          

            ViewBag.WebReport = UserWebReport;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (UserWebReport.Report.Prepare())
            {
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }


        [HttpGet("[action]")]
        //residency method parameters(filename, form data, signature filepath)
        public async Task<IActionResult> Residency(string title, string response, string signature)
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport UserWebReport = new WebReport();

            UserWebReport.Toolbar.Exports = new ExportMenuSettings()
            {
                ExportTypes = Exports.All
            };

            UserWebReport.Toolbar.ShowPrevButton = true;
            UserWebReport.Toolbar.ShowNextButton = true;
            UserWebReport.Toolbar.ShowLastButton = true;
            UserWebReport.Toolbar.ShowFirstButton = true;
            UserWebReport.Toolbar.ShowZoomButton = true;
            UserWebReport.Toolbar.ShowPrint = true;
            UserWebReport.Toolbar.Exports.Show = false;
            UserWebReport.ReportPrepared = false;


            string decode = Base64Decode(response);//base64 to string
            signature = Base64Decode(signature);
       

            ApplicationForm_model application_details = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationForm_model>(decode);
            fileName = "/" + title + "_report.frx";
            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            UserWebReport.Report.Load(path);

            application_details.signature = signature + "/" + application_details.signature; //Path + / + file name
            application_form_list.Add(application_details);
            educational_Backgrounds_list = application_details.educational_background;
            work_experience_list = application_details.work_experiences;
            recognition_list = application_details.recognitions;
            references_list = application_details.references;

            UserWebReport.Report.RegisterData(application_form_list, "appForm_ref");
            UserWebReport.Report.RegisterData(educational_Backgrounds_list, "education_ref");
            UserWebReport.Report.RegisterData(work_experience_list, "work_exp_ref");
            UserWebReport.Report.RegisterData(recognition_list, "recognitions_ref");
            UserWebReport.Report.RegisterData(references_list, "references_ref");
            


            ViewBag.WebReport = UserWebReport;

            if (UserWebReport.Report.Prepare())
            {
                return View("Views/Home/ReportView.cshtml");
            }
            else
            {
                return null;
            }
        }




        public IActionResult saveFile(string title, string response, string response2, string response3)
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            //MapPath mapPath = new MapPath(_env);
            noa_list = new List<Noa>();
            leave_list = new List<LeaveAppPrint>();


            //string path = Path.Combine(_env.WebRootPath + "/noa_report.frx");
            string path = Path.Combine(_env.WebRootPath + "/" + title + "_report.frx");
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            rep.Load(path);


            string decode = Base64Decode(response);//base64 to string

            if (title.ToLower().Equals("noa"))
            {
                var noa_details = Base64Decode(response2);//base64 to string
                var noa_terms = Base64Decode(response3);//base64 to string
                var noa_data = System.Text.Json.JsonSerializer.Deserialize<Noa>(decode);


                noa_list.Add(new Noa()
                {
                    noa_contract_ID = noa_data.noa_contract_ID,
                    noa_title = noa_data.noa_title,
                    grand_total = noa_data.grand_total,
                    grand_total_amount_in_words = noa_data.grand_total_amount_in_words,
                    date_needed = DateTime.ParseExact(noa_data.date_needed, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                    supplier_name = noa_data.supplier_name,
                    delivery_address = noa_data.delivery_address,
                    contact = noa_data.contact,
                    position = noa_data.position,
                    fax_number = noa_data.fax_number,
                    pur_tbl = noa_data.pur_tbl,
                    attention_title = noa_data.attention_title,
                    contact_person = noa_data.contact_person,
                    first_name = noa_data.first_name,
                    last_name = noa_data.last_name,
                    department_name = noa_data.department_name,
                    mode_name = noa_data.mode_name,
                    mode_description = noa_data.mode_description,
                    perf_sec_30 = noa_data.perf_sec_30,
                    perf_sec_5 = noa_data.perf_sec_5,
                });

                //This will assign list of noa_details separately
                noa_detail_list = new List<Noa_details>();
                if (noa_details != null)
                {

                    var noa_details_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_details>>(noa_details);
                    foreach (var data in noa_details_converted)
                    {
                        noa_detail_list.Add(new Noa_details
                        {
                            quantity = data.quantity,
                            item_number = data.item_number,
                            uom_name = data.uom_name,
                            unit_cost = data.unit_cost,
                            total_cost = data.total_cost,
                            description = data.description,
                            stock_property_number = data.stock_property_number,
                        });
                    }
                }

                noa_terms_list = new List<Noa_terms>();
                if (noa_terms != null)
                {
                    var noa_terms_converted = System.Text.Json.JsonSerializer.Deserialize<List<Noa_terms>>(noa_terms);
                    foreach (var datas in noa_terms_converted)
                    {
                        noa_terms_list.Add(new Noa_terms
                        {
                            number = datas.number,
                            description = datas.description,
                        });
                    }

                }
            }
            else if (title.ToLower().Equals("leaveapplication"))
            {
                var leave = System.Text.Json.JsonSerializer.Deserialize<LeaveAppPrint>(decode);

                leave_list.Add(new LeaveAppPrint()
                {
                    first_name = leave.first_name,
                    middle_name = leave.middle_name,
                    last_name = leave.last_name,
                    department_name = leave.department_name,
                    date_of_filing = leave.date_of_filing,
                    position_name = leave.position_name,
                    leave_type_name = leave.leave_type_name,
                    other_remarks = leave.other_remarks,
                    imageUrl = leave.imageUrl,
                    imageUrlDept = leave.imageUrlDept,

                });

                rep.Report.RegisterData(leave_list, "leaveRequest");

            }
            else if (title.ToLower().Equals("appForm"))
            {
                var signature = Base64Decode(response2);

                ApplicationForm_model application_details = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationForm_model>(decode);

                //application_form_list = new List<ApplicationForm_model>();
                //educational_Backgrounds_list = new List<Educational_background>();
                //work_experience_list = new List<Work_experience>();
                //recognition_list = new List<Recognitions>();
                //references_list = new List<References>();

                application_details.signature = signature + "/" + application_details.signature;
                application_form_list.Add(application_details);
                educational_Backgrounds_list = application_details.educational_background;
                work_experience_list = application_details.work_experiences;
                recognition_list = application_details.recognitions;
                references_list = application_details.references;

                rep.Report.RegisterData(application_form_list, "appForm_ref");
                rep.Report.RegisterData(educational_Backgrounds_list, "education_ref");
                rep.Report.RegisterData(work_experience_list, "work_exp_ref");
                rep.Report.RegisterData(recognition_list, "recognitions_ref");
                rep.Report.RegisterData(references_list, "references_ref");
            }

            if (rep.Prepare())
            {
                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject Report";
                pdfExport.Title = "Report Report";
                MemoryStream ms = new MemoryStream();
                rep.Report.Export(pdfExport, ms);
                rep.Dispose();
                pdfExport.Dispose();
                ms.Position = 0;

                return File(ms, "application/pdf", title + "_report_" + DateTime.Now.ToString("mm-dd-yyyy") + ".pdf");
            }
            else
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}