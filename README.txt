тк в своем проекте, мне не удалось "мигрировать таблицы БД в приложение" из-за непонятной для меня ошибки;
 прикрепляю sql-запрос для создания базы данных по моему варианту в тз:

///таблица пользователей

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    CreatedBy INT,
    DeletedBy INT
);

///таблица отелей

CREATE TABLE Hotels (
    HotelID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Description TEXT,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    CreatedBy INT,
    DeletedBy INT
);

///таблица бронирований
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    HotelID INT NOT NULL,
    CheckIn DATETIME NOT NULL,
    CheckOut DATETIME NOT NULL,
    Guests INT NOT NULL,
    Rooms INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    CreatedBy INT,
    DeletedBy INT
);

///ключи для таблицы пользователей
ALTER TABLE Users
ADD CONSTRAINT FK_Users_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Users_DeletedBy FOREIGN KEY (DeletedBy) REFERENCES Users(UserID);

///ключи для таблицы отелей
ALTER TABLE Hotels
ADD CONSTRAINT FK_Hotels_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Hotels_DeletedBy FOREIGN KEY (DeletedBy) REFERENCES Users(UserID);

///ключи для таблицы бронирований
ALTER TABLE Bookings
ADD CONSTRAINT FK_Bookings_UserID FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Bookings_HotelID FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID),
    CONSTRAINT FK_Bookings_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Bookings_DeletedBy FOREIGN KEY (DeletedBy) REFERENCES Users(UserID);