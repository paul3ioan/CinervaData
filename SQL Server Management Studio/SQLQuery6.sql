Select Top 5
	Properties.Name	
	from Properties
	Inner Join Reservations on Month( Reservations.CheckInDate) = 11
	Inner Join RoomReservations on RoomReservations.ReservationId = Reservations.Id
	Inner Join Rooms on Rooms.PropertyId =Properties.Id and RoomReservations.RoomId = Rooms.Id
	Inner Join Countries on Countries.Name = 'Romania'
	Inner Join Cities on Cities.CountryId = Countries.Id
	where Properties.CityId = Cities.Id
	group by Properties.Name
	order by Count(Properties.Name) desc;
		
	