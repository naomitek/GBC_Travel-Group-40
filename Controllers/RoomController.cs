using GBC_Travel_Group_40.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_40.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace GBC_Travel_Group_40.Controllers
{
    [Route("Rooms")]
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RoomController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var rooms = _db.Rooms.ToList();
            return View(rooms);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var room = _db.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rooms room)
        {
            if (ModelState.IsValid)
            {
                _db.Rooms.Add(room);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string location, string checking, string checkout, int numberOfBeds)
        {
            var roomList = _db.Rooms.AsQueryable();
            
            bool searchPerformed = !String.IsNullOrEmpty(location) && !String.IsNullOrEmpty(checking) && !String.IsNullOrEmpty(checkout) && (numberOfBeds == 1 || numberOfBeds == 2);

            if (searchPerformed)
            {
                DateTime parsedCheckingDate, parsedCheckoutDate;
                if (DateTime.TryParseExact(checking, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedCheckingDate) &&
                    DateTime.TryParseExact(checkout, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedCheckoutDate))
                {
                    roomList = roomList.Where(room =>
                        room.Location.Contains(location) &&
                        room.Checking.Date <= parsedCheckingDate.Date &&
                        room.Checkout.Date >= parsedCheckoutDate.Date &&
                        room.NumberOfBeds == numberOfBeds);
                }
            }
            var rooms = await roomList.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = $"Location: {location}, Checking: {checking}, Checkout: {checkout}, Beds: {numberOfBeds}";
            return View("Index", rooms);
        }

        private bool RoomsExists(int id)
        {
            return _db.Cars.Any(e => e.CarId == id);
        }
    }
}

