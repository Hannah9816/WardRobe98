using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WardRobe.Data;

namespace WardRobe.Controllers
{
    [Route("trip")]
    public class TripCalController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public TripCalController(ApplicationDbContext _db, UserManager<IdentityUser> userManager)
        {
            db = _db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("findall")]
        public IActionResult FindAllEvents()
        {
            var userid = _userManager.GetUserId(HttpContext.User);


            var events = db.Trip.Where(e => e.UserId == userid).Select(e => new
            {
                id = e.ID,
                title = e.TripName,
                start = e.Date.ToString(),
                end = e.EndDate.ToString(),

            }).ToList();

            return new JsonResult(events);
        }
    }
}