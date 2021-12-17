Select 
	Count(UserId) as numberDistinctGuests
	from Cities
	Inner join Properties on Properties.CityId = Cities.Id
	Inner join Rooms on Rooms.PropertyId = Properties.Id
	Inner join RoomReservations on RoomReservations.RoomId = Rooms.Id
	Inner join Reservations on Reservations.Id = RoomReservations.ReservationId
	Inner join Users on Users.Id = Reservations.UserId
	where  Cities.Name = 'Iasi'
	
	