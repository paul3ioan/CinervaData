Select Top 1 Max(CheckInDate)	
	from Reservations
	inner join RoomReservations on RoomReservations.ReservationId = Reservations.Id
	inner join Rooms on Rooms.Id = RoomReservations.RoomId
	inner join Properties on Properties.Id = Rooms.PropertyId
	where Properties.Name = 'InterContinental'
	group by month(CheckInDate);
	