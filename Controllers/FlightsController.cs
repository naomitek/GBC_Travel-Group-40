using GBC_Travel_Group_40.Data;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Drawing.Printing;
using System.Globalization;

namespace GBC_Travel_Group_40.Controllers
{
    [Route("Flights")]
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FlightsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("")]
        public IActionResult Index(int? numPassengers)
        {
            ViewData["numPassengers"] = numPassengers;
            var flights = _db.Flights.ToList();
            ViewBag.flightList = flights;
            return View(flights);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var flight = _db.Flights.FirstOrDefault(f => f.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpGet("Create")] 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flights flight)
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Add(flight);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }
        
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string leavingFrom, string goingTo, string departureDate, string numPassengers)
        {
            var flightList = from f in _db.Flights select f;
            bool searchPerformed = !String.IsNullOrEmpty(goingTo) && !String.IsNullOrEmpty(leavingFrom) && !String.IsNullOrEmpty(departureDate);
            int parsedPassengers = int.Parse(numPassengers);

            if (searchPerformed)
            {
                DateTime parsedDepartureDate;
                if (DateTime.TryParseExact(departureDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDepartureDate))
                {
                    // Filter flights on user input
                    flightList = flightList.Where(f =>
                      f.Origen.Contains(leavingFrom) &&
                      f.Destination.Contains(goingTo) &&
                      f.DepartureTime.Date == parsedDepartureDate.Date &&
                      f.NumOfPassengers + parsedPassengers <= f.MaxSeat);
                }
            }
            var flights = await flightList.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = $"Leaving: {leavingFrom}, Going: {goingTo}, Departure Date: {departureDate}, Passengers: {numPassengers}";
            //display results in Index 
            return View("Index", flights);
        }

        /*
        [HttpGet("Filters")]
        public async Task<IActionResult> Filters(List<Flights> flightList, string? PriceRange, string? airline)
        {            
            if (!String.IsNullOrEmpty(PriceRange) && PriceRange!="p4")
            {
                decimal checkPriceRange = 500;
                if (PriceRange == "p2") { checkPriceRange = 300; }
                if (PriceRange == "p1") { checkPriceRange = 100; }
                // update flights list on PriceRange
                var flights = _db.Flights.Where(f => f == flightList);
                return View("Index", flights);
            }
            return View("Index", flightList);
        }
        */

        private bool FlightsExists(int id)
        {
            return _db.Flights.Any(e => e.FlightId == id);
        } 
    }
}
