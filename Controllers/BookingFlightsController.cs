using GBC_Travel_Group_40.Data;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Cryptography;

namespace GBC_Travel_Group_40.Controllers
{
    [Route("BookingFlights")]
    public class BookingFlightsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookingFlightsController(ApplicationDbContext context)
        {
            _db = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            //ViewBag.flight = _db.Flights.Find(bookingF.FlightId);
            //ViewBag.passengers = _db.Passengers.Find(bookingF.PassengerId);
            var bookingF = _db.BookingFlights.ToList();
            return View(bookingF);
        }

        [HttpGet("MakeBooking")]
        public IActionResult MakeBooking(int fid, int numPassengers)
        {
            ViewBag.fid = fid;
            ViewData["numPassengers"] = numPassengers;
            return View();
        }
        [HttpPost("MakeBooking")]
        [ValidateAntiForgeryToken]
        public IActionResult MakeBooking(Passengers passenger, int fid, int numPassengers)
        {
            if (ModelState.IsValid)
            {
                _db.Passengers.Add(passenger);
                _db.SaveChanges();
                var bookingF = new BookingFlights
                {
                    PassengerId = passenger.PassengerId,
                    FlightId = fid
                };
                _db.BookingFlights.Add(bookingF);
                _db.SaveChanges();

                var flight = _db.Flights.Find(bookingF.FlightId);
                flight.NumOfPassengers++;
                _db.Update(flight);
                // add to cart
                var item = new Cart
                {
                    ProductType = "Flight",
                    FlightId = bookingF.FlightId,
                    Price = flight.Price
                };
                _db.Carts.Add(item);
                _db.SaveChanges();
                numPassengers--;
                if (numPassengers > 0)
                {
                    return RedirectToAction("MakeBooking", new { fid, numPassengers });
                }
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }

     /*
        [HttpGet("Details")]
        public IActionResult Details(BookingFlights bookingF, int parsedPassengers)
        {
            var flight = _db.Flights.Find(bookingF.FlightId);
            var passengers = _db.Passengers.Find(bookingF.PassengerId);
            ViewBag.flight = flight;
            ViewBag.passengers = passengers;
            return View();
        }
     */
        /*
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingFlights bookingF, int parsedPassengers)
        {
            if (ModelState.IsValid)
            {
                _db.BookingFlights.Add(bookingF);
                var flight = _db.Flights.Find(bookingF.FlightId);
                flight.NumOfPassengers++;
                _db.Update(flight);
                _db.SaveChanges();
            }
            if(parsedPassengers > 0)
            {
                parsedPassengers = parsedPassengers--;
                return RedirectToAction("Create");
            }
            return RedirectToAction("Index");
        }
        */
    }
}
