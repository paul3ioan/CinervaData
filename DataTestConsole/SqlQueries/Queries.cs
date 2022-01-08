using Cinerva.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTestConsole.SqlQueries
{
    public class Queries:IQueries
    {
        private static CinervaDBContext cinerva { get; set; }
        public Queries(CinervaDBContext dbContext)
        {
            cinerva = dbContext;
        }
        public  object Query2()
        {
            // Get all the properties in Iasi(Name, Description, Address, TotalRooms).
            var propertiesFromIasi = cinerva.Properties
                .Where(x => x.City.Name == "Iasi")
                .ToList();
            return propertiesFromIasi;
        }
        public  object Query3()
        {
            // Get the names, emails and phone numbers of the clients that made a reservation and paid for it.       
            var userWhoPaid = cinerva.Reservations
                .Where(x => x.Paid == true)
                .Select(x => new { x.User.FirstName, x.User.LastName, x.User.Email, x.User.PhoneNumber })
                .Distinct()
                .ToList();
            return userWhoPaid;
        }
        public  object Query4()
        {
            // Select first and last names for the users with admin role that administrates guest houses in Brasov and order them alphabetically by their first name and last name.
            var usersWithPropertiesFromBrasov = cinerva.Properties
              .Where(x => x.City.Name == "Brasov")
              .Select(x => new { x.User.FirstName, x.User.LastName })
              .OrderBy(x => x.FirstName)
              .ThenBy(x => x.LastName)
              .ToList();
            return usersWithPropertiesFromBrasov;
        }
        public  object Query5()
        {
            // Get all the room types and prices for rooms at InterContinental hotel in Romania.
            var roomCateg = cinerva.Rooms
              .Where(x => x.Property.City.Country.Name == "Romania" && x.Property.Name == "InterContinental")
              .Select(x => new { x.RoomCategory.Name, x.RoomCategory.PricePerNight })
              .Distinct()
              .ToList();
            return roomCateg;
        }
        public object Query6()
        {
            //  Get top 5 properties in Romania by reservation count in the month of November 2021.
            var startOfNov = new DateTime(2021, 11, 01);
            var endtOfNov = new DateTime(2021, 11, 30);
            var topProp = cinerva.RoomReservations
                .Where(x => x.Reservation.CheckInDate >= startOfNov && x.Reservation.CheckOutDate <= endtOfNov)
                .GroupBy(x => new { x.Room.Property.Name, totalReservations = x.Room.Reservations.Count() })
                .OrderByDescending(g => g.Key.totalReservations)
                .Select(g => g.Key.Name).Take(5).ToList();
            return topProp;
        }
        public object Query7()
        {
            // Get all hotels with pools with at least one room available for today
            return null;
        }
        public object Query8()
        {
            // Get the total number of available rooms from all properties in Romania price range between 70 and 100 euro for this month.
            var totalNumberOfRooms = cinerva.RoomReservations
                 .Where(x => x.Room.Property.City.Country.Name == "Romania"
                 && x.Reservation.CheckInDate.Month == 11 && x.Room.RoomCategory.PricePerNight >= 70 && x.Room.RoomCategory.PricePerNight <= 100)
                 .GroupBy(x => new { name = x.Room.Property.Name })
                 .Where(g => (g.Sum(x => (x.Reservation.CheckOutDate.Day - x.Reservation.CheckInDate.Day)) < 30))
                 .Select(g => new { property = g.Key.name, count = g.Count(), sum = g.Sum(x => (x.Reservation.CheckOutDate.Day - x.Reservation.CheckInDate.Day)) })
                 .ToList(); // not done
            return totalNumberOfRooms;
        }
        public object Query9()
        {
            //Get the highest rated property that has spa and rooms available for the next weekend.
            var roomCateg = cinerva.Rooms
              .Where(x => x.Property.City.Country.Name == "Romania" && x.Property.Name == "InterContinental")
              .Select(x => new { x.RoomCategory.Name, x.RoomCategory.PricePerNight })
              .Distinct()
              .ToList();
            return roomCateg;
        }
        public object Query10()
        {
            // Get the month of the year that has the most reservations for a certain hotel.

            var monthMostBusy = cinerva.RoomReservations
                .Where(x => x.Room.Property.Name == "InterContinental")
                .GroupBy(x => x.Reservation.CheckInDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList().First();
            return monthMostBusy;
        }
        public object Query11()
        {
            //  Find properties that have 2 double rooms available for the next week in Antwerp that cost between 100 and 150 euro.
            var nextWeek = new DateTime(2021, 12, 5);
            var endWeek = new DateTime(2021, 12, 12);
            var propWtihDoubleRooms = cinerva.Rooms
                .Where(x => x.RoomCategory.Name == "Double"
                && x.Property.City.Name == "Iasi"
                && x.RoomCategory.PricePerNight >= 100
                && x.RoomCategory.PricePerNight <= 150
                && (x.Reservations.Count() == 0
                || x.Reservations.Count(data => data.CheckInDate > endWeek) > 0)
                )
                .GroupBy(prop => prop.Property.Name)
                .Select(g => g.Key)
                .ToList();
            return propWtihDoubleRooms;
        }
        public object Query12()
        {
            // Get the number of distinct guests that have made bookings for 05/2021 in each hotel in Iasi.
            var startOfNov2021 = new DateTime(2021, 11, 01);
            var endOfNov2021 = new DateTime(2021, 11, 30);
            var distinctGuests = cinerva.RoomReservations.
                Where(res => res.Room.Property.City.Name == "Iasi"
                && res.Reservation.CheckInDate <= endOfNov2021 && res.Reservation.CheckInDate >= startOfNov2021)
                .GroupBy(room => new { Property_Name = room.Room.Property.Name }, u => new { User = u.Reservation.User.Id })
                .Select(g => new { g.Key.Property_Name, AllUsers = g.Count() })
                .ToList();
            return distinctGuests;
        }
        public object Query13()
        {
            return null;
        }
        public object Query14()
        {
            return null;
        }
        public object Query15()
        {
            return null;
        }
    }
}
