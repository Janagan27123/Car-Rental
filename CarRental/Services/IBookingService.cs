using CarRental.Models;

namespace CarRental.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByStatusAsync(string status);
        Task<decimal> CalculateTotalAmountAsync(int vehicleId, int rentalDays);
    }
} 