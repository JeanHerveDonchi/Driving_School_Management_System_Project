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

-- Add the LearnersLicenceNumber column back to the students table
ALTER TABLE students
ADD LearnersLicenceNumber BIGINT NULL;
GO

USE DrivingSchoolDB;
GO
-- Create a filtered unique index to allow multiple NULL values but enforce uniqueness on non-NULL values
CREATE UNIQUE INDEX IX_Unique_LearnersLicenceNumber
ON students (LearnersLicenceNumber)
WHERE LearnersLicenceNumber IS NOT NULL;
GO

ALTER TABLE Lessons
ADD LessonType VARCHAR(255) NOT NULL DEFAULT 'Theoric';

ALTER TABLE Lessons
ALTER COLUMN PickupLocation VARCHAR(255) NULL;

USE DrivingSchoolDB;
GO

-- Modify the CarId column to be nullable
ALTER TABLE Lessons
ALTER COLUMN CarId INT NULL;


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



-- Insert students with some having learners licence numbers and some being NULL
INSERT INTO Students (FirstName, LastName, DateOfBirth, HasLearnersLicence, AdmissionDate, PhoneNumber, Email, LearnersLicenceNumber, Age, Address, Duefees)
VALUES
('Alexander', 'Green', '2004-02-15', 1, '2020-01-10', '5061234560', 'alexander.green@example.com', 100000001, 20, '123 Cedar St, Moncton, NB', 0),
('Emma', 'White', '2005-03-25', 0, '2021-02-14', '5062345671', 'emma.white@example.com', NULL, 19, '456 Birch St, Moncton, NB', 0),
('Benjamin', 'Clark', '2003-06-10', 1, '2019-05-22', '5063456782', 'benjamin.clark@example.com', 100000002, 21, '789 Elm St, Moncton, NB', 0),
('Olivia', 'Hall', '2004-09-05', 1, '2020-08-30', '5064567893', 'olivia.hall@example.com', 100000003, 20, '321 Oak St, Moncton, NB', 0),
('Lucas', 'Allen', '2002-11-20', 0, '2018-10-12', '5065678904', 'lucas.allen@example.com', NULL, 21, '654 Pine St, Moncton, NB', 0),
('Sophia', 'Young', '2005-01-30', 1, '2021-12-15', '5066789015', 'sophia.young@example.com', 100000004, 19, '987 Maple St, Moncton, NB', 0),
('Mason', 'King', '2004-04-17', 0, '2020-03-20', '5067890126', 'mason.king@example.com', NULL, 20, '135 Spruce St, Moncton, NB', 0),
('Isabella', 'Wright', '2003-07-12', 1, '2019-06-25', '5068901237', 'isabella.wright@example.com', 100000005, 21, '246 Walnut St, Moncton, NB', 0),
('Liam', 'Scott', '2002-10-02', 1, '2018-09-28', '5069012348', 'liam.scott@example.com', 100000006, 21, '357 Hickory St, Moncton, NB', 0),
('Ava', 'Adams', '2003-12-23', 1, '2019-11-18', '5060123459', 'ava.adams@example.com', 100000007, 21, '468 Redwood St, Moncton, NB', 0),
('Ethan', 'Baker', '2004-03-08', 0, '2020-02-14', '5062345670', 'ethan.baker@example.com', NULL, 20, '579 Cedar St, Moncton, NB', 0),
('Mia', 'Mitchell', '2005-06-01', 1, '2021-05-05', '5063456781', 'mia.mitchell@example.com', 100000008, 19, '680 Birch St, Moncton, NB', 0),
('James', 'Carter', '2002-09-15', 0, '2018-08-22', '5064567892', 'james.carter@example.com', NULL, 21, '123 Maple St, Moncton, NB', 0),
('Amelia', 'Parker', '2004-12-04', 1, '2020-11-10', '5065678903', 'amelia.parker@example.com', 100000009, 20, '456 Oak St, Moncton, NB', 0),
('Aiden', 'Evans', '2003-02-28', 1, '2019-01-20', '5066789014', 'aiden.evans@example.com', 100000010, 21, '789 Pine St, Moncton, NB', 0),
('Charlotte', 'Edwards', '2005-08-09', 0, '2021-07-30', '5067890125', 'charlotte.edwards@example.com', NULL, 19, '321 Spruce St, Moncton, NB', 0),
('Matthew', 'Collins', '2004-11-01', 1, '2020-10-05', '5068901236', 'matthew.collins@example.com', 100000011, 20, '654 Walnut St, Moncton, NB', 0),
('Abigail', 'Reed', '2003-05-14', 0, '2019-04-18', '5069012347', 'abigail.reed@example.com', NULL, 21, '987 Hickory St, Moncton, NB', 0),
('Jackson', 'Stewart', '2002-07-25', 1, '2018-06-29', '5060123458', 'jackson.stewart@example.com', 100000012, 21, '135 Redwood St, Moncton, NB', 0),
('Emily', 'Morris', '2004-01-16', 1, '2020-12-12', '5062345679', 'emily.morris@example.com', 100000013, 20, '246 Cedar St, Moncton, NB', 0);



-- Insert into Instructors table
INSERT INTO Instructors (FirstName, LastName, DateOfBirth, HiredDate, PhoneNumber, Email, LicenceNumber, Age, Address)
VALUES
('John', 'Doe', '1980-04-23', '2019-05-20', '5061234567', 'johndoe@example.com', 123456789, 44, '123 Main St, Moncton, NB'),
('Jane', 'Smith', '1975-09-15', '2018-07-18', '5062345608', 'janesmith@example.com', 234567890, 48, '456 Elm St, Moncton, NB'),
('Michael', 'Johnson', '1982-01-10', '2020-03-15', '5063456780', 'michaeljohnson@example.com', 345678901, 42, '789 Maple St, Moncton, NB'),
('Emily', 'Davis', '1978-06-30', '2017-11-25', '5064567890', 'emilydavis@example.com', 456789012, 46, '321 Oak St, Moncton, NB'),
('William', 'Brown', '1985-03-05', '2019-02-20', '5065678901', 'williambrown@example.com', 567890123, 39, '654 Pine St, Moncton, NB'),
('Jessica', 'Williams', '1990-12-12', '2021-09-01', '5066789012', 'jessicawilliams@example.com', 678901234, 33, '987 Cedar St, Moncton, NB'),
('David', 'Miller', '1988-08-18', '2022-01-10', '5067890123', 'davidmiller@example.com', 789012345, 35, '135 Spruce St, Moncton, NB'),
('Sarah', 'Wilson', '1983-11-03', '2018-04-30', '5068901234', 'sarahwilson@example.com', 890123456, 40, '246 Birch St, Moncton, NB'),
('James', 'Moore', '1979-07-27', '2019-07-14', '5069012345', 'jamesmoore@example.com', 901234567, 44, '357 Redwood St, Moncton, NB'),
('Laura', 'Taylor', '1987-02-17', '2020-06-25', '5060123456', 'laurataylor@example.com', 123123123, 37, '468 Ash St, Moncton, NB'),
('Daniel', 'Anderson', '1981-05-11', '2021-08-05', '5062345678', 'danielanderson@example.com', 234234234, 43, '579 Walnut St, Moncton, NB'),
('Sophia', 'Thomas', '1992-10-25', '2019-11-20', '5063456789', 'sophiathomas@example.com', 345345345, 31, '680 Hickory St, Moncton, NB');





