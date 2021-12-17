Select RoomCategories.Name,RoomCategories.PricePerNight
	from RoomCategories	
		Inner join Countries on Countries.Name = 'Romania'
		Inner join Cities on Cities.CountryId = Countries.Id
		Inner join Properties on Properties.CityId = Cities.Id and Properties.Name = 'InterContinental'		
		Inner join Rooms on Rooms.PropertyId = Properties.Id
		where RoomCategories.Id = Rooms.RoomCategoryId;
		
