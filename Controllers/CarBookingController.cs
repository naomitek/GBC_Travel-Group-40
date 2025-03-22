using GBC_Travel_Group_40.Models;
using GBC_Travel_Group_40.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;


namespace GBC_Travel_Group_40.Controllers
{
    [Route("CarBooking")]
    public class CarBookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CarBookingController(ApplicationDbContext context)
        {
            _db = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var bookingC = _db.CarBookings.ToList();
            return View(bookingC);
        }

        [HttpGet("Create")]
        public IActionResult Create(int fid)
        {
            var car = _db.Cars.Find(fid);
            if (car != null)
            {
                
                ViewBag.fid = fid;
                
                return View(new CarBookings { CarId = fid });
            }
            else
            {

                return RedirectToAction("Index", "Cart");
            }
        }




        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarBookings bookingC)
        {
            if (ModelState.IsValid)
            {
                _db.CarBookings.Add(bookingC);
                
                var car = _db.Cars.Find(bookingC.CarId);
                _db.Update(car);
                // add to the cart
                var item = new Cart
                {
                    ProductType = "Car",
                    CarId = bookingC.CarId,
                    Price = car.Pricing
                };
                _db.Carts.Add(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}

