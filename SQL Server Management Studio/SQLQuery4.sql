Select FirstName, LastName 
	from Users
		Inner Join Properties on Properties.CityId = 42	
		Inner Join Roles on Roles.Name = 'PropertyAdmin'
		where Properties.AdministratorId = Users.Id and Users.RoleId = Roles.Id
		order by FirstName,LastName;
	