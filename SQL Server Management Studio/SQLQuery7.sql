Select distinct Properties.Name
	from Properties
	Inner Join GeneralFeatures on GeneralFeatures.Name= 'pool'
	Inner Join PropertyFacilities on PropertyFacilities.FacilityId = GeneralFeatures.Id
	Inner Join Reservations on Reservations.CheckInDate > getdate() and Reservations.CheckOutDate < getDate()
	Inner Join RoomReservations on RoomReservations.ReservationId = Reservations.Id
	Inner Join Rooms on Rooms.Id = RoomReservations.RoomId
	where Properties.Id = PropertyFacilities.PropertyId and Rooms.PropertyId = Properties.Id;