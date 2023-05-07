# NetWorthCalculator-ASP.NET-MVC-5

DataBase: 
CREATE TABLE NetWorthItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    Type VARCHAR(10) NOT NULL CHECK (Type IN ('Asset', 'Liability'))
);

CREATE TABLE NetWorth (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Date DATE NOT NULL,
    Advice VARCHAR(100) NULL
);

CREATE TABLE NetWorthItemResult (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NetWorthId INT NOT NULL,
    Item INT NOT NULL,
    Value DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_NetWorthItemResult_NetWorth FOREIGN KEY (NetWorthId) REFERENCES NetWorth(Id),
    CONSTRAINT FK_NetWorthItemResult_NetWorthItem FOREIGN KEY (Item) REFERENCES NetWorthItem(Id)
); 


CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL DEFAULT 'User',
    PictureUrl VARCHAR(255) NOT NULL DEFAULT 'https://static.vecteezy.com/system/resources/thumbnails/010/260/479/small/default-avatar-profile-icon-of-social-media-user-in-clipart-style-vector.jpg',
    Active BIT NOT NULL DEFAULT 1
);



ALTER TABLE NetWorth
ADD UserId INT NOT NULL DEFAULT 0;



ALTER TABLE Users
ALTER COLUMN password VARCHAR(200)
