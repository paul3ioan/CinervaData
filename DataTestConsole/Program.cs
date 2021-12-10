using System;
using Cinerva.Data;
//using System.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace DataTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var cinerva = new CinervaDBContext();
            // SQL 2
            var propertiesFromIasi = cinerva.Properties
                .Include(x => x.City)
                .Where(x => x.City.Name == "Iasi")
                .ToList();

            //Sql 3
            var userWhoPaid = cinerva.Reservations
                .Include(x => x.User)
                .Where(x => x.Paid == true)
                .Select(x => new { x.User.FirstName, x.User.LastName, x.User.Email, x.User.PhoneNumber })
                .Distinct()
                .ToList();

            //Sql 4
            int x = 1;
           
            
            var UsersWithPropertiesFromBrasov = cinerva.Properties.Include(x => x.City).Include(x => x.User)
                .Where(x=> x.City.Name == "Brasov")
                .Select(x => new {x.User.FirstName, x.User.LastName }).OrderBy(x=> x.FirstName).ThenBy(x=> x.LastName).ToList();
            //Sql 5
            var roomCateg = cinerva.Rooms
               .Where(x => x.Property.City.Country.Name == "Romania" && x.Property.Name == "InterContinental")
               .Select(x => new { x.RoomCategory.Name, x.RoomCategory.PricePerNight }).Distinct().ToList();
            var roomCateg2 = cinerva.Rooms
               .Where(room => room.Property.City.Country.Name == "Romania" && room.Property.Name == "InterContinental")
               .GroupBy(room => room.RoomCategory.Name, room => new { room.RoomCategory.Name, room.RoomCategory.PricePerNight }).ToList();

            var reviews = cinerva.Reviews.ToList();
            var roles = cinerva.Roles.ToList();
            var reservations = cinerva.Reservations.ToList();
            var users = cinerva.Users.ToList();
            var prop = cinerva.PropertyImages.ToList();
            var rooms = cinerva.Rooms.ToList();
           // var roomCateg = cinerva.RoomCategories.ToList();
            var Prop = cinerva.PropertyFacilities.ToList();
            var properties = cinerva.Properties.ToList();
            var propType = cinerva.PropertyTypes.ToList();
            var cities = cinerva.Cities.ToList();      
            var countries = cinerva.Countries.ToList();
            var user = cinerva.Users.FirstOrDefault(x => x.FirstName == "Quinten");
            if (user == null) throw new ArgumentNullException();
            //user.Role = cinerva.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            //user.Reviews = cinerva.Reviews.Where(x => x.UserId == user.Id).ToList();
            //user.Reservations = cinerva.Reservations.Where(x => x.UserId == user.Id).ToList();
            //user.Properties = cinerva.Properties.Where(x => x.UserId == user.Id).ToList();
        }
    }
}
