Select Top 1 Properties.Name
	from GeneralFeatures 
	Inner join PropertyFacilities on PropertyFacilities.FacilityId = GeneralFeatures.Id
	inner join Properties on Properties.Id = PropertyFacilities.PropertyId
	inner join Rooms on Rooms.PropertyId = Properties.Id
	left join RoomReservations on RoomReservations.RoomId = Rooms.Id
	left join Reservations on Reservations.Id = ReservationId
	where GeneralFeatures.Name = 'spa'
	group by Rooms.Id,CheckInDate, CheckOutDate,Rating, Properties.Name
	having CheckInDate is null or (CheckInDate >'2021-1-4' and CheckOutDate < '2021-1-3')
	order by Rating desc