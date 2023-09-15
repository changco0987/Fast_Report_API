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

namespace Fast_Report_API.Controllers
{
    //[Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  IWebHostEnvironment _env;
        private List<Noa> noa_list;
        private List<Noa_details> noa_detail_list;

        private string fileName;


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
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Generate(string response) 
        {

            FastReport.Utils.Config.WebMode = true;
            WebReport rep = new WebReport();
            //MapPath mapPath = new MapPath(_env);

            

            //rep.Report.SetParameterValue("param1", noa_list[0].noa_title);
            //rep.Report.SetParameterValue("param2","2nd parameter");
            //rep.Report.SetParameterValue("param3","3rd parameter");
            rep.Toolbar.Show = false;



            noa_list = new List<Noa>();
            string decode = Base64Decode(response);//base64 to string
            var noa_data = System.Text.Json.JsonSerializer.Deserialize<Noa>(decode);

            //Add another condition statement if there's new file/report to print
            if (noa_data.title.ToLower().Equals("noa")) 
            {
                fileName = "/" + noa_data.title + "_report.frx";
                string path = Path.Combine(_env.WebRootPath + fileName);
                //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
                rep.Report.Load(path);

                noa_list.Add(new Noa()
                {
                    noa_contract_ID = noa_data.noa_contract_ID,
                    noa_title = noa_data.noa_title,
                    grand_total = noa_data.grand_total,
                    grand_total_amount_in_words = noa_data.grand_total_amount_in_words,
                    date_needed = noa_data.date_needed,
                    supplier_name = noa_data.supplier_name,
                    contact_person = noa_data.contact_person,
                    first_name = noa_data.first_name,
                    last_name = noa_data.last_name,
                    department_name = noa_data.department_name
                });

                //This will assign list of noa_details separately
                noa_detail_list = new List<Noa_details>();

                foreach (var data in noa_data.noa_details)
                {
                    noa_detail_list.Add(data);
                }


                rep.Report.RegisterData(noa_list, "NoaRef");//pass the data to fast report
                rep.Report.RegisterData(noa_detail_list, "NoaDetailsRef");//pass the data to fast report
            }

           
            ViewBag.WebReport = rep;
            //ViewBag.Message = decode;
            //ViewBag.base64 = Base64Decode("aGVsbG8=");
            if (rep.Report.Prepare())
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

        public FileResult saveFile() 
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            //MapPath mapPath = new MapPath(_env);


            string path = Path.Combine(_env.WebRootPath + fileName);
            //string path = mapPath.MapVirtualPathToPhysical("~/noa_report.frx");
            rep.Load(path);

            //List<Models.Noa> noa_list = new List<Models.Noa>();
            ////noa_list.Add(new Models.Noa() { noa_contract_ID = 43, suppier_name = "Maynilad", noa_title = "Patubig" });
            //rep.SetParameterValue("param1", "1st parameter");
            //rep.SetParameterValue("param2", "2nd parameter");
            //rep.SetParameterValue("param3", "3rd parameter");
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

                return File(ms, "application/pdf", "noa_report.pdf");
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