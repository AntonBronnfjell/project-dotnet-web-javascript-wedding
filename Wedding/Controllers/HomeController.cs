using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wedding.Models;

namespace Wedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WeddingContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            var contextOptions = new DbContextOptionsBuilder<WeddingContext>()
                .UseSqlServer(@"Data Source=a-n.ddns.net;Initial Catalog=wedding;Persist Security Info=True;User ID=sa;Password=p402Escalante")
                .Options;
            _context = new WeddingContext(contextOptions);
            _logger = logger;
        }


        [Route("Invitation/{Uuid}")]
        public ActionResult Index(Guid Uuid)
        { 
            List<Redeem> _redeemCode = _context.Redeems.ToList();
            List<Guest> _guestUser = _context.Guests.ToList();
            List<Peer> _peerUser = _context.Peers.ToList();
            List<Kid> _kidUser = _context.Kids.ToList();

            List<Guest> _viewBagGuest = new List<Guest>();
            List<Peer> _viewBagPeer = new List<Peer>();
            List<Kid> _viewBagKid = new List<Kid>();
            foreach (Redeem redeem in _redeemCode)
            {
                if (redeem.Code.Equals(Uuid))
                {
                    foreach(Guest guest in _guestUser)
                    {
                        if(guest.Code.Equals(redeem.Id))
                        {
                            _viewBagGuest.Add(guest);
                        }
                    }
                }
            }

            foreach (Peer peer in _peerUser)
            {
                if (peer.Id.Equals(_viewBagGuest.First().Code))
                {
                    _viewBagPeer.Add(peer);
                }
            }

            foreach (Kid kid in _kidUser)
            {
                if (kid.Id.Equals(_viewBagGuest.First().Code))
                {
                    _viewBagKid.Add(kid);
                }
            }

            ViewBag.Guests = _viewBagGuest;
            ViewBag.Peer = _viewBagPeer;
            ViewBag.Kid = _viewBagKid;

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
            return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
        }
    }
}