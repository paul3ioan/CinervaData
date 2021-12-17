select 
	distinct Properties.Name
	from Countries
	inner join Cities on Cities.CountryId = Countries.Id	
	inner join Properties on Properties.CityId = Cities.Id and Properties.Rating >= 3
	inner join Rooms on Rooms.PropertyId = Properties.Id
	inner join RoomFacilities on RoomFacilities.RoomId = Rooms.Id
	inner join RoomFeatures on RoomFeatures.Id = RoomFacilities.FacilityId and RoomFeatures.Name='ocean view'
	left join RoomReservations on RoomReservations.RoomId = Rooms.Id
	left join Reservations on ReservationId = Reservations.Id and year(CheckInDate) = 2022
	where Countries.Name = 'Greece'
	group by Properties.Name, CheckInDate,CheckOutDate,TotalRooms
	having (month(CheckInDate) = 7 and 
		Sum( datediff(day,CheckInDate, CheckOutDate)) < TotalRooms * 31) or CheckInDate is null