using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly CarRentalContext _context;
        private readonly IVehicleService _vehicleService;

        public BookingService(CarRentalContext context, IVehicleService vehicleService)
        {
            _context = context;
            _vehicleService = vehicleService;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Vehicle)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(b => b.BookingID == id);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            booking.TotalAmount = await CalculateTotalAmountAsync(booking.VehicleID, booking.RentalDays);
            booking.BookingDate = DateTime.Now;
            booking.Status = "Pending";

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByStatusAsync(string status)
        {
            return await _context.Bookings
                .Include(b => b.Vehicle)
                .Where(b => b.Status == status)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<decimal> CalculateTotalAmountAsync(int vehicleId, int rentalDays)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
                return 0;

            return vehicle.PricePerDay * rentalDays;
        }
    }
} 