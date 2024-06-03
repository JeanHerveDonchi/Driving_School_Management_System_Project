-- Drop the database if it exists
IF DB_ID('DrivingSchoolDB') IS NOT NULL
BEGIN
    DROP DATABASE DrivingSchoolDB;
END
GO

-- Create the new database
CREATE DATABASE DrivingSchoolDB;
GO

-- Use the new database
USE DrivingSchoolDB;
GO

-- Create Students table
CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    HasLearnersLicence BIT NOT NULL,
    AdmissionDate DATE NOT NULL,
    PhoneNumber VARCHAR(15) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Age INT NOT NULL,
    Address VARCHAR(255) NOT NULL,
    DueFees DECIMAL(10, 2) DEFAULT 0
);
GO

-- Create Instructors table
CREATE TABLE Instructors (
    InstructorID INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    HiredDate DATE NOT NULL,
    PhoneNumber VARCHAR(15) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    LicenceNumber BIGINT UNIQUE NOT NULL,
    Age INT NOT NULL,
    Address VARCHAR(255) NOT NULL
);
GO

-- Create Cars table
CREATE TABLE Cars (
    CarID INT PRIMARY KEY IDENTITY,
    Make VARCHAR(255) NOT NULL,
    Model VARCHAR(255) NOT NULL,
    Year INT NOT NULL,
    LicensePlate VARCHAR(255) UNIQUE NOT NULL,
    VIN VARCHAR(255) UNIQUE NOT NULL
);
GO

-- Create Lessons table
CREATE TABLE Lessons (
    LessonID INT PRIMARY KEY IDENTITY,
    LessonDate DATETIME NOT NULL,
    LessonStartTime TIME NOT NULL,
    LessonEndTime TIME NOT NULL,
    PickupLocation VARCHAR(255) NOT NULL,
    LessonMinutesDuration INT NOT NULL,
    InstructorID INT NOT NULL,
    StudentID INT NOT NULL,
    CarID INT NOT NULL,
    FOREIGN KEY (InstructorID) REFERENCES Instructors(InstructorID),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CarID) REFERENCES Cars(CarID)
);
GO

-- Create Login table
CREATE TABLE Login (
    LoginID INT PRIMARY KEY IDENTITY,
    Username VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);
GO

USE DrivingSchoolDB;
GO

-- Add check constraint on the Students table
ALTER TABLE Students
ADD CONSTRAINT CHK_StudentAge CHECK (Age >= 16);
GO

USE DrivingSchoolDB;
GO

INSERT INTO Cars (Make, Model, Year, LicensePlate, VIN) VALUES
('Toyota', 'Camry', 2015, 'ABC1234', '1HGBH41JXMN109186'),
('Honda', 'Civic', 2016, 'XYZ5678', '2HGCM82633A123456'),
('Ford', 'Focus', 2017, 'LMN3456', '1FTRX18W1XKA12345'),
('Chevrolet', 'Malibu', 2018, 'QRS7890', '1GCHK23U53F123456'),
('Nissan', 'Altima', 2019, 'TUV9012', '3N1BC13E29L123456'),
('Hyundai', 'Elantra', 2020, 'UVW2345', 'KMHDN46D95U123456'),
('Kia', 'Optima', 2021, 'JKL4567', 'KNAGM4A71F5123456'),
('Mazda', '3', 2022, 'NOP6789', 'JM1BL1VF1A1123456');
GO

USE DrivingSchoolDB;
GO

-- Insert into Login table
INSERT INTO Login (Username, PasswordHash) VALUES
('jeanherve01', 'simplepass1'),
('hervedonchi01', 'simplepass2');
GO

USE DrivingSchoolDB;
GO

-- Insert into Students table
INSERT INTO Students (FirstName, LastName, DateOfBirth, HasLearnersLicence, AdmissionDate, PhoneNumber, Email, Age, Address, DueFees) VALUES
('John', 'Smith', '1998-05-15', 1, '2024-05-01', '5065551234', 'john.smith@example.com', 26, '123 Main St, Moncton, NB', 0),
('Emily', 'Johnson', '1999-08-21', 1, '2024-05-02', '5065552345', 'emily.johnson@example.com', 25, '456 Maple Ave, Moncton, NB', 0),
('Michael', 'Williams', '2000-10-30', 1, '2024-05-03', '5065553456', 'michael.williams@example.com', 24, '789 Elm St, Moncton, NB', 0),
('Emma', 'Brown', '2001-12-12', 0, '2024-05-04', '5065554567', 'emma.brown@example.com', 23, '987 Oak St, Moncton, NB', 0),
('Matthew', 'Jones', '2002-02-25', 1, '2024-05-05', '5065555678', 'matthew.jones@example.com', 22, '654 Birch St, Moncton, NB', 0),
('Olivia', 'Miller', '2003-04-03', 0, '2024-05-06', '5065556789', 'olivia.miller@example.com', 21, '321 Pine St, Moncton, NB', 0),
('William', 'Davis', '2004-06-17', 1, '2024-05-07', '5065557890', 'william.davis@example.com', 20, '123 Cedar St, Moncton, NB', 0);
GO

-- Insert into Instructors table
INSERT INTO Instructors (FirstName, LastName, DateOfBirth, HiredDate, PhoneNumber, Email, LicenceNumber, Age, Address) VALUES
('Sarah', 'Taylor', '1980-03-10', '2015-05-01', '5066661234', 'sarah.taylor@example.com', 12345679, 42, '789 Maple Ave, Moncton, NB'),
('David', 'Anderson', '1975-08-15', '2016-06-01', '5066662345', 'david.anderson@example.com', 23456789, 47, '456 Elm St, Moncton, NB'),
('Jennifer', 'Thomas', '1982-11-20', '2017-07-01', '5066663456', 'jennifer.thomas@example.com', 34567890, 39, '123 Oak St, Moncton, NB'),
('James', 'Clark', '1978-04-25', '2018-08-01', '5066664567', 'james.clark@example.com', 45678901, 44, '987 Pine St, Moncton, NB'),
('Jessica', 'White', '1985-06-30', '2019-09-01', '5066665678', 'jessica.white@example.com', 56789012, 39, '654 Birch St, Moncton, NB'),
('Daniel', 'Harris', '1972-09-05', '2020-10-01', '5066666789', 'daniel.harris@example.com', 67890123, 49, '321 Cedar St, Moncton, NB'),
('Michelle', 'Martin', '1970-12-10', '2021-11-01', '5066667890', 'michelle.martin@example.com', 78901234, 51, '987 Maple Ave, Moncton, NB'),
('Christopher', 'Lee', '1968-02-15', '2022-12-01', '5066668901', 'christopher.lee@example.com', 89012345, 54, '456 Elm St, Moncton, NB'),
('Amanda', 'Garcia', '1973-05-20', '2015-01-01', '5067771234', 'amanda.garcia@example.com', 90123456, 48, '123 Oak St, Moncton, NB'),
('Robert', 'Martinez', '1976-08-25', '2016-02-01', '5067772345', 'robert.martinez@example.com', 12345678, 45, '789 Pine St, Moncton, NB'),
('Ashley', 'Hernandez', '1981-11-30', '2017-03-01', '5067773456', 'ashley.hernandez@example.com', 23456779, 40, '456 Maple Ave, Moncton, NB'),
('Matthew', 'Lopez', '1977-04-05', '2018-04-01', '5067774567', 'matthew.lopez@example.com', 34567892, 45, '789 Cedar St, Moncton, NB'),
('Brittany', 'Gonzalez', '1984-06-10', '2019-05-01', '5067775678', 'brittany.gonzalez@example.com', 45679901, 38, '321 Elm St, Moncton, NB'),
('Jonathan', 'Perez', '1971-09-15', '2020-06-01', '5067776789', 'jonathan.perez@example.com', 56789912, 51, '987 Maple Ave, Moncton, NB');
GO


ALTER TABLE Lessons
ADD LessonType VARCHAR(255) NOT NULL DEFAULT 'Theoric';