using copyqr.Models;
using copyqr.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace copyqr.Controllers
{
    public class HomeController : Controller
    {
        private readonly QrInfoContext _context;

        public HomeController(QrInfoContext context)
        {
            _context = context;
            
        }
        public IActionResult Index() => View();
        public IActionResult ScannedQr() => View(_context.QrInfos.ToList());

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}