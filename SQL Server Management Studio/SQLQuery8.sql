Select
	Properties.Name,Count(Rooms.Id) as FreeRoomsForNovember
	from Countries 
	Inner join Cities on Cities.CountryId = Countries.Id
	Inner join Properties on Properties.CityId = Cities.Id
	Inner join Rooms on Rooms.PropertyId = Properties.Id
	Left join RoomReservations on RoomReservations.RoomId = Rooms.Id
	left join Reservations on RoomReservations.ReservationId = Reservations.Id
	inner join RoomCategories on RoomCategories.Id = Rooms.RoomCategoryId
	where Countries.Name = 'Romania' and (month(Reservations.CheckInDate) = 11 or Reservations.CheckInDate is null) and RoomCategories.PricePerNight between 70 and 100
	group by  Properties.Name, Reservations.CheckInDate
	having Sum(datediff(day, Reservations.CheckInDate ,Reservations.CheckOutDate) ) < 30 or Reservations.CheckInDate is null