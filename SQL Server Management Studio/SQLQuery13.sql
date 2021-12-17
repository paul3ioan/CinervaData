select 
	distinct Properties.Name,Properties.Description, Properties.Address
	from Properties
	inner join Rooms on Rooms.PropertyId = Properties.Id
	left join RoomReservations on Rooms.Id = RoomReservations.RoomId
	left join Reservations on ReservationId = Reservations.Id 
	group by Properties.Name,CheckOutDate, CheckInDate,TotalRooms, Properties.Description, Address
	having  (month(CheckInDate) = 3 and year(CheckInDate) = 2022 and datediff(day, CheckInDate, CheckOutDate) < 31) or CheckInDate is null