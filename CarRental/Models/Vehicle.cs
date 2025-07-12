using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // CAR, VAN, BUS, JEEP
        
        [Required]
        [Range(1, 20)]
        public int Seats { get; set; }
        
        [Required]
        [Range(0, 100000)]
        public decimal PricePerDay { get; set; }
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        // Vehicle Tracking Properties
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public DateTime LastLocationUpdate { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; } = true;
        
        // Navigation Properties
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
} 