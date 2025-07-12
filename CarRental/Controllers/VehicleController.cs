using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index(string? type)
        {
            IEnumerable<Vehicle> vehicles;
            
            if (!string.IsNullOrEmpty(type))
            {
                vehicles = await _vehicleService.GetVehiclesByTypeAsync(type);
            }
            else
            {
                vehicles = await _vehicleService.GetAllVehiclesAsync();
            }

            ViewBag.SelectedType = type;
            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocation(int vehicleId, double latitude, double longitude)
        {
            var success = await _vehicleService.UpdateVehicleLocationAsync(vehicleId, latitude, longitude);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleLocation(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Json(new 
            { 
                latitude = vehicle.Latitude, 
                longitude = vehicle.Longitude,
                lastUpdate = vehicle.LastLocationUpdate
            });
        }
    }
} 