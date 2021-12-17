Select 
	Top 10 Properties.Name, Sum(Reservations.Price) as LastYearIncome 
	,Cities.Name as City
	, Properties.Rating
	from Countries
	inner join Cities on Cities.CountryId = Countries.Id
	inner join Properties on Properties.CityId = Cities.Id
	inner join Rooms on Rooms.PropertyId = Properties.Id
	inner join RoomReservations on RoomReservations.RoomId = Rooms.Id
	inner join Reservations on year(Reservations.CheckInDate) < 2022 and Reservations.Id = RoomReservations.ReservationId and Reservations.IsCanceled = 0
	where Countries.Name ='Romania'
	group by Properties.Name, Cities.Name, Properties.Rating
	order by Sum(Reservations.Price) desc