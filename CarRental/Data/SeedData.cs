using CarRental.Models;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Data
{
    public static class SeedData
    {
        public static void Initialize(CarRentalContext context)
        {
            // Check if data already exists
            if (context.Vehicles.Any())
            {
                return;
            }

            // Add sample vehicles
            var vehicles = new Vehicle[]
            {
                new Vehicle
                {
                    Name = "Honda Civic",
                    Type = "CAR",
                    Seats = 4,
                    PricePerDay = 15000,
                    ImageUrl = "/images/honda-civic.jpg",
                    Description = "100% conditioned Honda Civic for comfortable rides",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Toyota Aqua",
                    Type = "CAR",
                    Seats = 4,
                    PricePerDay = 10000,
                    ImageUrl = "/images/toyota-aqua.jpg",
                    Description = "Fuel efficient Toyota Aqua hybrid",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Suzuki Swift",
                    Type = "CAR",
                    Seats = 4,
                    PricePerDay = 8000,
                    ImageUrl = "/images/suzuki-swift.jpg",
                    Description = "Compact and economical Suzuki Swift",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Toyota Hiace",
                    Type = "VAN",
                    Seats = 12,
                    PricePerDay = 20000,
                    ImageUrl = "/images/toyota-hiace.jpg",
                    Description = "Spacious van for group travel",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Mitsubishi Delica",
                    Type = "VAN",
                    Seats = 7,
                    PricePerDay = 25000,
                    ImageUrl = "/images/mitsubishi-delica.jpg",
                    Description = "Luxury van with premium features",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Toyota Coaster",
                    Type = "BUS",
                    Seats = 25,
                    PricePerDay = 35000,
                    ImageUrl = "/images/toyota-coaster.jpg",
                    Description = "Large bus for big groups and tours",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                },
                new Vehicle
                {
                    Name = "Jeep Wrangler",
                    Type = "JEEP",
                    Seats = 5,
                    PricePerDay = 18000,
                    ImageUrl = "/images/jeep-wrangler.jpg",
                    Description = "Adventure jeep for off-road trips",
                    Latitude = 9.6615,
                    Longitude = 80.0255,
                    IsAvailable = true
                }
            };

            context.Vehicles.AddRange(vehicles);

            // Add admin user
            var adminUser = new AdminUser
            {
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                FullName = "Administrator",
                Email = "admin@carrental.com",
                IsActive = true
            };

            context.AdminUsers.Add(adminUser);

            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 