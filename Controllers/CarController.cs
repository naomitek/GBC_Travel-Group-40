using GBC_Travel_Group_40.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace GBC_Travel_Group_40.Controllers
{
    [Route("Car")]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var cars = _db.Cars.ToList();
            return View(cars);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var car = _db.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cars car)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Add(car);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string pickupLocation, string dropOffLocation, string rentaldate)
        {
            var carList = from c in _db.Cars select c;
            bool searchPerformed = !String.IsNullOrEmpty(pickupLocation) && !String.IsNullOrEmpty(dropOffLocation) && !String.IsNullOrEmpty(dropOffLocation);
            if (searchPerformed)
            {
                DateTime parsedrentaldate;
                if (DateTime.TryParseExact(rentaldate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedrentaldate))
                {
                    // Filter cars on user input
                    carList = carList.Where(f =>
                      f.OrigenLocation.Contains(pickupLocation) &&
                      f.DestinationLocation.Contains(dropOffLocation) &&
                      f.DepartureTime.Date == parsedrentaldate.Date);
                     
                }
            }
            var car = await carList.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = $"pickupLocation: {pickupLocation}, dropOffLocation: {dropOffLocation}, rentaldate: {rentaldate}";
            //display results in Index 
            return View("Index", car);
        }


            private bool CarsExists(int id)
        {
            return _db.Cars.Any(e => e.CarId == id);
        }
    }
}

