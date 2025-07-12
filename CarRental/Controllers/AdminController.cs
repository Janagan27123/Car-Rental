using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Services;
using CarRental.Models;
using CarRental.Data;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        private readonly CarRentalContext _context;

        public AdminController(IAdminService adminService, IVehicleService vehicleService, IBookingService bookingService, CarRentalContext context)
        {
            _adminService = adminService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var admin = await _adminService.AuthenticateAsync(username, password);
            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            HttpContext.Session.SetString("AdminUsername", admin.Username);
            HttpContext.Session.SetString("AdminFullName", admin.FullName ?? admin.Username);
            
            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            var bookings = await _bookingService.GetAllBookingsAsync();

            ViewBag.TotalVehicles = vehicles.Count();
            ViewBag.TotalBookings = bookings.Count();
            ViewBag.PendingBookings = bookings.Count(b => b.Status == "Pending");

            return View();
        }

        public async Task<IActionResult> Vehicles()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);
        }

        public IActionResult CreateVehicle()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.VehicleTypes = new SelectList(new[] { "CAR", "VAN", "BUS", "JEEP" });
            return View(new Vehicle());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            if (ModelState.IsValid)
            {
                await _vehicleService.AddVehicleAsync(vehicle);
                return RedirectToAction(nameof(Vehicles));
            }

            ViewBag.VehicleTypes = new SelectList(new[] { "CAR", "VAN", "BUS", "JEEP" });
            return View(vehicle);
        }

        public async Task<IActionResult> EditVehicle(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.VehicleTypes = new SelectList(new[] { "CAR", "VAN", "BUS", "JEEP" });
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicle(Vehicle vehicle)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            if (ModelState.IsValid)
            {
                await _vehicleService.UpdateVehicleAsync(vehicle);
                return RedirectToAction(nameof(Vehicles));
            }

            ViewBag.VehicleTypes = new SelectList(new[] { "CAR", "VAN", "BUS", "JEEP" });
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var success = await _vehicleService.DeleteVehicleAsync(id);
            return RedirectToAction(nameof(Vehicles));
        }

        public async Task<IActionResult> Bookings()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, string status)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction(nameof(Login));
            }

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                await _bookingService.UpdateBookingAsync(booking);
            }

            return RedirectToAction(nameof(Bookings));
        }

        public IActionResult ContactMessages()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));
            var messages = _context.ContactMessages.OrderByDescending(m => m.SentAt).ToList();
            return View(messages);
        }

        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUsername"));
        }
    }
} 