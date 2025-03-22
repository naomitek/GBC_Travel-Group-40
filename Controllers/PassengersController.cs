
using GBC_Travel_Group_40.Data;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GBC_Travel_Group_40.Controllers
{
    public class PassengersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PassengersController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var passengers = _db.Passengers.ToList();
            return View(passengers);
        }
        [HttpGet]
        public IActionResult Create(int fid)
        {
            ViewBag.fid = fid;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Passengers passenger, int fid)
        {
            if (ModelState.IsValid)
            {
                _db.Passengers.Add(passenger);
                _db.SaveChanges();
                ViewBag.pid = passenger.PassengerId;
                return RedirectToAction("Create", "BookingFlights", new { pid = passenger.PassengerId, fid = fid });
            }
            return View(passenger);
        }
    }
}