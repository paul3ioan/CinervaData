CREATE TABLE Roles(
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL,
	Primary Key(Id)
)
CREATE TABLE Users(
	Id int NOT NULL,
	FirstName nvarchar(200) NOT NULL,
	LastName nvarchar(200) NOT NULL,
	RoleId int NOT NULL,
	Email nvarchar(200) NOT NULL,
	PhoneNumber varchar(14),
	isDeleted bit default 0,
	isBanned bit default 0,
	Password char(100) NOT NULL,
	Primary Key(Id),
	Foreign Key(RoleId) REFERENCES Roles(Id)
)
Create table Countries(
	Id int not null identity(1,1),
	Name nvarchar(100) not null,
	Primary key(Id)
)
CREATE TABLE Cities(
	Id int NOT NULL IDENTITY(1,1),
	Name nvarchar(100) NOT NULL,
	CountryId int Not NULL
	Primary Key(Id)
	Foreign key(CountryId) references Countries(Id)
)
Create table RoomFeatures(
	Id int not null identity(1,1),
	Name nvarchar(500),
	IconUrl nvarchar(500),
	Primary key(Id)
)
Create table RoomCategories(
	Id int not null identity(1,1),
	Name varchar(100) not null,
	BedsCount int not null,
	pricePerNight decimal(20,2) not null,
	Description nvarchar(500),
	primary key(Id)
)

Create table PropertyType(
	Id int not null identity(1,1),
	type nvarchar(50) not null
	primary key(Id)
)
Create table Properties(
	Id int not null identity(1,1),
	Name nvarchar(250) not null,
	Rating decimal(2,1),
	Description nvarchar(MAX),
	Address nvarchar(200) not null,
	CityId int not null,
	AdministratorId int not null,
	NumberOfDayForRefounds int not null,
	PropertyTypeId int not null,
	isDeleted bit,
	phone char(14),
	TotalRooms int,
	primary key(Id),
	foreign key(CityId) references Cities(Id),
	foreign key(AdministratorId) references Users(Id),
	foreign key(PropertyTypeId) references PropertyType(Id)
)
Create table Rooms(
	Id int not null,
	RoomCategory int not null,
	PropertyId int not null,
	isDeleted bit,
	primary key(Id),
	foreign key(RoomCategory) references RoomCategories(Id),
	foreign key(PropertyId) references Properties(Id)
)
Create table RoomFacilities(
	RoomId int not null,
	FacilityId int not null,
	Primary key(Roomid,FacilityId),
	foreign key(FacilityId) references RoomFeatures(Id), 
	 foreign key(RoomId) references Rooms(Id)
)
create table ProperyImages(
	 Id int not null identity(1,1),
	 imageURL nvarchar(2500) not null,
	 ProperyId int not null,
	 primary key(Id),
	 foreign key(ProperyId) references Properties(Id))
create table Reservations(
	Id int not null identity(1,1),
	UserId int not null,
	CheckInDate date not null,
	CheckOutDate date not null,
	price decimal(20,2) not null,
	isCanceled bit,
	isCancelRequest bit,
	paid bit,
	primary key(Id),
	foreign key(UserId) references Users(Id),
)
create table RoomReservations(
	Id int not null identity(1,1),
	RoomId int not null,
	ReservationId int not null,
	primary key(Id),
	foreign key(RoomId) references Rooms(Id),
	foreign key(ReservationId) references Reservations(Id)
)
create table Reviews(
	Id int not null identity(1,1),
	UserId int not null,
	PropertyId int not null,
	rating int not null,
	description nvarchar(1000) not null,
	ReviewDate date not null,
	primary key(Id,UserId),
	foreign key(UserId) references Users(Id),
	foreign key(PropertyId) references Properties(Id)
)
create table GeneralFeatures(
	Id int not null identity(1,1),
	Name nvarchar(500) not  null,
	IconUrl nvarchar(500) not null,
	primary key(Id)
)
create table PropertyFacilities(
	PropertyId int not null,
	FacilityId int not null,
	primary key(PropertyId, FacilityId),
	foreign key(FacilityId) references GeneralFeatures(Id),
	foreign key(PropertyId) references Properties(Id)
)