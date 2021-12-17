Select FirstName, LastName, Email, PhoneNumber
from Users
Inner Join Reservations on Users.Id = Reservations.UserId
where Reservations.Paid = 1;