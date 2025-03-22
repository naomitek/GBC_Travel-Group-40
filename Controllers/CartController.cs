using GBC_Travel_Group_40.Data;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group_40.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var cartList = _db.Carts.ToList();
            return View(cartList);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var item = _db.Carts.FirstOrDefault(p => p.ProductId == id);
            if (item.CarId != null)
            {
                var bookingcar = _db.CarBookings.Find(item.CarId);
                ViewBag.bookingcar = bookingcar;
                ViewBag.car = _db.Cars.Find(item.CarId);
            }

            if (item.FlightId != null) 
            {
                var bookingflight = _db.BookingFlights.Find(item.FlightId);
                ViewBag.bookingflight = bookingflight;
                var flight = _db.Flights.Find(bookingflight.FlightId);
                ViewBag.flight = flight;
                ViewBag.passenger = _db.Passengers.Find(bookingflight.PassengerId);
            }
            if (item.RoomId != null) 
            { 
                var bookingroom = _db.RoomBookings.Find(item.RoomId);
                ViewBag.bookingroom = bookingroom;
                ViewBag.room = _db.Rooms.Find(item.RoomId); }
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _db.Carts.FirstOrDefault(i => i.ProductId == id);
            if (item.CarId != null) { ViewBag.car = _db.Cars.Find(item.CarId); }
            if (item.FlightId != null) { ViewBag.flight = _db.Flights.Find(item.FlightId); }
            if (item.RoomId != null) { ViewBag.room = _db.Rooms.Find(item.RoomId); }
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _db.Carts.Find(id);
            if (item != null)
            {
                _db.Carts.Remove(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpGet("Payment")]
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost("Payment")]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(decimal price)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }
    }
}