using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Fast_Report_API.Controllers
{
    public class MapPath : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public MapPath(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string MapVirtualPathToPhysical(string virtualPath)
        {
            //string webRootPath = _hostEnvironment.WebRootPath;
            string localRootPath = _hostEnvironment.ContentRootPath;
            string physicalPath = Path.Combine(localRootPath, virtualPath);
            return physicalPath;
        }
    }
}
