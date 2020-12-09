using HotelExercise;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

var factory = new HotelContextFactory();
using var context = factory.CreateDbContext(args);

Console.WriteLine("Welcome to the Hotel Manager\n");

Console.Write("Choose an option (0..add hotel | 1..see existing hotels | 2..exit): ");
var option = Int32.Parse(Console.ReadLine());

switch (option)
{
    case 0:
        Console.WriteLine("Insert some data");
        var keepRunning = 0;
        var addRooms = 0;
        var newHotel = new Hotel();
        var newRoom = new RoomType();
        var newPrice = new Price();
        do
        {
            Console.Write("Hotelname: ");
            newHotel.Name = Console.ReadLine();

            Console.Write("Street: ");
            newHotel.Street = Console.ReadLine();

            Console.Write("Zip Code: ");
            newHotel.ZIPCode = Int32.Parse(Console.ReadLine());

            Console.Write("City: ");
            newHotel.City = Console.ReadLine();

            await context.AddAsync(newHotel);

            await context.SaveChangesAsync();

            Console.Write("Specialties: ");
            var specialties = Console.ReadLine().Split(", ").Select(x => new Specialty { Name = x, Hotel = newHotel });

            Console.WriteLine("Rooms:");
            newRoom.Hotel = newHotel;
            do
            {
                Console.Write("Amount: ");
                newRoom.Amount = Int32.Parse(Console.ReadLine());

                Console.Write("Size: ");
                newRoom.Size = Int32.Parse(Console.ReadLine());

                Console.Write("Title: ");
                newRoom.Title = Console.ReadLine();

                Console.Write("Disability access (yes/no): ");
                var access = Console.ReadLine();
                if (access == "true")
                {
                    newRoom.DisabilityAccesible = true;
                }
                else
                {
                    newRoom.DisabilityAccesible = false;
                }

                await context.AddAsync(newRoom);
                await context.SaveChangesAsync();

                Console.Write("Price per night: ");
                newPrice.PricePerNight = Int32.Parse(Console.ReadLine());
                newPrice.RoomTypeID = newRoom.Id;
                newPrice.From = DateTime.Today.AddDays(1);
                newPrice.To = newPrice.From.AddYears(1);

                Console.Write("Do you wish to add more rooms? (0..no | 1..yes) ");
                addRooms = Int32.Parse(Console.ReadLine());
                await context.AddAsync(newPrice);
            } while (addRooms == 1);

            foreach (var specialty in specialties)
            {
                await context.AddAsync(specialty);
            }

            Console.WriteLine("Added Hotel " + newHotel.Name);
            Console.Write("Do you wish to continue? (0..no | 1..yes) ");

            keepRunning = Int32.Parse(Console.ReadLine());
        } while (keepRunning == 1);

        await context.SaveChangesAsync();
        Console.WriteLine("Saved Hotels to Database!");
        break;
    case 1:
        Console.WriteLine("These are all the hotels which are stored in the database");
        Console.WriteLine("=========================================================");
        Console.WriteLine();

        var hotels = await context.Hotel.ToListAsync();

        foreach (var hotel in hotels)
        {
            Console.WriteLine($"Hotelname: {hotel.Name}");
            Console.WriteLine($"Address: {hotel.Address}");
            Console.WriteLine("-----------------------------------------------------");

        }
        break;
    default:
        break;
}

class HotelContext : DbContext
{
    public DbSet<Hotel> Hotel { get; set; }
    public DbSet<Specialty> Specialty { get; set; }
    public DbSet<RoomType> RoomType { get; set; }
    public DbSet<Price> Price { get; set; }

    //public DbSet<List<Price>> Prices { get; set; }
    public HotelContext(DbContextOptions<HotelContext> options)
    : base(options)
    { }
}

class HotelContextFactory : IDesignTimeDbContextFactory<HotelContext>
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public HotelContext CreateDbContext(string[]? args = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<HotelContext>();
        optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new HotelContext(optionsBuilder.Options);
    }
}
