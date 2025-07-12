# Car Rental Website with Vehicle Tracking

A complete ASP.NET MVC car rental website with vehicle tracking functionality, built using .NET 6, Entity Framework Core, and MS SQL Server.

## Features

### 🚗 Core Features
- **Vehicle Management**: Add, edit, delete vehicles with details
- **Booking System**: Complete booking workflow with customer information
- **Vehicle Tracking**: Real-time location tracking with Google Maps integration
- **Admin Panel**: Secure admin interface for managing vehicles and bookings
- **Responsive Design**: Modern UI with Bootstrap 5

### 📍 Vehicle Tracking System
- **Location Updates**: Admin can update vehicle locations
- **Google Maps Integration**: Visual map display for vehicle locations
- **Real-time Tracking**: Track vehicle positions on admin dashboard
- **Location History**: Store and display last location update time

### 🎨 User Interface
- **Home Page**: Hero section with search functionality
- **Vehicle Listing**: Filter by type, seats, price, availability
- **Vehicle Details**: Complete vehicle information with booking option
- **Booking Form**: User-friendly booking process
- **Admin Dashboard**: Statistics and vehicle tracking map

## Technology Stack

- **Backend**: ASP.NET Core 6 MVC
- **Database**: MS SQL Server with Entity Framework Core
- **Frontend**: Bootstrap 5, Font Awesome, Google Maps API
- **Authentication**: Session-based admin authentication
- **Architecture**: Repository pattern with service layer

## Database Schema

### Tables
1. **Vehicles**
   - VehicleID (Primary Key)
   - Name, Type, Seats, PricePerDay
   - ImageUrl, Description
   - Latitude, Longitude (Tracking)
   - LastLocationUpdate, IsAvailable

2. **Bookings**
   - BookingID (Primary Key)
   - VehicleID (Foreign Key)
   - CustomerName, Phone, Email
   - PickupDate, RentalDays, Status
   - BookingDate, TotalAmount

3. **AdminUsers**
   - UserID (Primary Key)
   - Username, PasswordHash
   - FullName, Email, CreatedDate, IsActive

## Setup Instructions

### Prerequisites
- Visual Studio 2022 or later
- .NET 6 SDK
- MS SQL Server (LocalDB or Express)
- Google Maps API Key (optional, for maps functionality)

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd CarRental
   ```

2. **Open in Visual Studio**
   - Open `CarRental.sln` in Visual Studio
   - Restore NuGet packages

3. **Database Setup**
   - The application uses LocalDB by default
   - Database will be created automatically on first run
   - Sample data will be seeded automatically

4. **Configuration**
   - Update connection string in `appsettings.json` if needed
   - For Google Maps: Replace `YOUR_GOOGLE_MAPS_API_KEY` in views

5. **Run the Application**
   ```bash
   dotnet run
   ```
   Or press F5 in Visual Studio

### Default Admin Credentials
- **Username**: admin
- **Password**: admin123

## Project Structure

```
CarRental/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   ├── VehicleController.cs
│   ├── BookingController.cs
│   └── AdminController.cs
├── Models/              # Data Models
│   ├── Vehicle.cs
│   ├── Booking.cs
│   └── AdminUser.cs
├── Services/            # Business Logic
│   ├── IVehicleService.cs
│   ├── VehicleService.cs
│   ├── IBookingService.cs
│   ├── BookingService.cs
│   ├── IAdminService.cs
│   └── AdminService.cs
├── Data/               # Database Context
│   ├── CarRentalContext.cs
│   └── SeedData.cs
├── Views/              # Razor Views
│   ├── Home/
│   ├── Vehicle/
│   ├── Booking/
│   └── Admin/
└── wwwroot/           # Static Files
```

## Key Features Implementation

### Vehicle Tracking
- **Location Storage**: Latitude/Longitude in database
- **Admin Updates**: Location update through admin panel
- **Map Display**: Google Maps integration in vehicle details
- **Real-time Updates**: AJAX calls for location updates

### Booking System
- **Form Validation**: Client and server-side validation
- **Price Calculation**: Automatic total amount calculation
- **Status Management**: Booking status tracking
- **Email Integration**: Ready for email notifications

### Admin Panel
- **Secure Login**: Session-based authentication
- **Dashboard**: Statistics and overview
- **CRUD Operations**: Full vehicle management
- **Booking Management**: View and update booking status

## API Endpoints

### Vehicle Tracking
- `POST /Vehicle/UpdateLocation` - Update vehicle location
- `GET /Vehicle/GetVehicleLocation/{id}` - Get vehicle location

### Booking
- `POST /Booking/CalculateTotal` - Calculate booking total
- `POST /Booking/Book` - Create new booking

## Customization

### Adding New Vehicle Types
1. Update `VehicleType` enum in models
2. Add new vehicle type to seed data
3. Update admin forms and filters

### Google Maps Integration
1. Get API key from Google Cloud Console
2. Replace `YOUR_GOOGLE_MAPS_API_KEY` in views
3. Customize map styling as needed

### Database Changes
1. Update models as needed
2. Run `Add-Migration` command
3. Update database with `Update-Database`

## Security Features

- **Password Hashing**: SHA256 password encryption
- **Session Management**: Secure admin sessions
- **Input Validation**: Comprehensive form validation
- **SQL Injection Protection**: Entity Framework parameterized queries

## Performance Optimizations

- **Async Operations**: All database operations are async
- **Indexing**: Database indexes on frequently queried columns
- **Lazy Loading**: Entity Framework lazy loading for related data
- **Caching**: Session-based caching for admin data

## Future Enhancements

- **Real-time Updates**: SignalR for live location updates
- **Mobile App**: React Native or Flutter mobile app
- **Payment Integration**: Online payment processing
- **Email Notifications**: Automated email confirmations
- **Advanced Analytics**: Booking analytics and reports

## Troubleshooting

### Common Issues

1. **Database Connection Error**
   - Check connection string in `appsettings.json`
   - Ensure SQL Server is running
   - Verify LocalDB installation

2. **Google Maps Not Loading**
   - Check API key configuration
   - Verify internet connection
   - Check browser console for errors

3. **Admin Login Issues**
   - Verify admin credentials
   - Check session configuration
   - Clear browser cache

### Logs
- Application logs are in the console output
- Database logs can be enabled in `appsettings.json`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is licensed under the MIT License.

## Support

For support and questions:
- Email: support@carrental.com
- Phone: +94 77 7484820

---

**Happy Coding! 🚗💨** 