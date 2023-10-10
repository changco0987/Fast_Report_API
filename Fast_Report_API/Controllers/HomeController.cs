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

namespace Fast_Report_API.Controllers
{
    //[Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _env;
        private List<Noa> noa_list;
        private List<Noa_terms> noa_terms_list;
        private List<LeaveAppPrint> leave_list;
        private List<Noa_details> noa_detail_list;


        private string fileName;


        //public WebReport UserWebReport { get; set; }

        Report Report { get; set; }
        DataSet DataSet { get; }
        ToolbarSettings Toolbar { get; }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
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


            if (rep.Report.Prepare())
            {
                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject Report";
                pdfExport.Title = "Report Report";
                MemoryStream ms = new MemoryStream();
                rep.Export(pdfExport, ms);
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