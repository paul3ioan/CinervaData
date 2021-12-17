select Properties.Name
	from Countries	
	inner join Cities on Cities.CountryId = Countries.Id
	inner join Properties on Cities.Id = Properties.CityId
	inner join Rooms on Rooms.PropertyId = Properties.Id
	inner join RoomCategories on RoomCategories.BedsCount = 2 and Rooms.RoomCategoryId = RoomCategories.Id
	left join RoomReservations on RoomReservations.RoomId = Rooms.Id
	left join Reservations on ReservationId = Reservations.Id
	where Countries.Name = 'Belgia' and RoomCategories.PricePerNight between 100 and 150
	group by Properties.Name,CheckInDate,CheckOutDate
	having CheckInDate is null or (CheckInDate > '2021-01-05' and CheckOutDate < '2021-01-03')