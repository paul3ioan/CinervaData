select 
	Sum(Rooms.PricePerNight) as Lost_Income
	from Properties
	inner join Rooms on Rooms.PropertyId = Properties.Id
	left join RoomReservations on Rooms.Id = RoomReservations.RoomId
	left join Reservations on Reservations.Id = RoomReservations.ReservationId and Reservations.CheckInDate = cast(getdate() as date)
	where Properties.Name = 'Unirea Hotel' 
	group by Reservations.Id
	having Reservations.Id is not null