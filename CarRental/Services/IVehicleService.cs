using CarRental.Models;

namespace CarRental.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(string type);
        Task<Vehicle?> GetVehicleByIdAsync(int id);
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);
        Task<bool> UpdateVehicleLocationAsync(int vehicleId, double latitude, double longitude);
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    }
} 