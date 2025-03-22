using GBC_Travel_Group_40.Models;
using GBC_Travel_Group_40.Data;
using Microsoft.AspNetCore.Mvc;


namespace GBC_Travel_Group_40.Controllers
{
    [Route("RoomBooking")]
    public class RoomBookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RoomBookingController(ApplicationDbContext context)
        {
            _db = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {

            var bookingR = _db.RoomBookings.ToList();
            return View(bookingR);
        }

        [HttpGet("Create")]
        public IActionResult Create(int fid)
        {
            var room = _db.Rooms.Find(fid);
            if (room != null)
            {

                ViewBag.fid = fid;
                return View(new RoomBookings { RoomId = fid });
            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomBookings bookingR)
        {
            if (ModelState.IsValid)
            {
                _db.RoomBookings.Add(bookingR);
                var room = _db.Rooms.Find(bookingR.RoomId);
                _db.Update(room);
                // add to the cart
                var item = new Cart
                {
                    ProductType = "Hotel",
                    RoomId = bookingR.RoomId,
                    Price = room.PricePerNight
                };
                _db.Add(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index","Cart");
        }
    }
}

