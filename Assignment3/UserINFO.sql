Create Database WebUser;

CREATE TABLE Users(
	UserId INT PRIMARY KEY IDENTITY(1,1),
	UserName VARCHAR(100),
	Email VARCHAR(400),
	PW VARCHAR(100)
);

drop table Users;

SELECT * FROM Users;

Insert INTO Users
VALUES ('Jl','jj@gmail.com', '123ABC');

CREATE PROCEDURE GetUserInfo
	@UserN NVARCHAR(100)
AS
BEGIN
	SELECT UserName, Email, PW
	FROM Users
	WHERE UserName LIKE '%' + @UserN + '%'
END

EXEC GetUserInfo 'j';

drop PROCEDURE GetUserInfo;