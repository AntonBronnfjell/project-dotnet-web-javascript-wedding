using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Wedding.Models;

namespace Wedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WeddingContext _context = new WeddingContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Route("Invitation/{Uuid}")]
        public ActionResult Index(Guid Uuid)
        {
            //List<Redeem> _redeemCode =_context.Redeems.ToList();
            //ViewBag.Uuid = _redeemCode;
            return View();
        }

        public IActionResult Index()
        {
            return View();
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