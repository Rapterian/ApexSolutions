-- Drop tables if they exist (optional)
DROP TABLE IF EXISTS Feedback;
DROP TABLE IF EXISTS Contract;
DROP TABLE IF EXISTS ServiceRequest;
DROP TABLE IF EXISTS Technician;
DROP TABLE IF EXISTS Client;

CREATE DATABASE "Service Request Management System";

USE [Service Request Management System];

-- Client Table
CREATE TABLE Client (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15) NOT NULL,
    Address VARCHAR(255) NULL
);

-- Technician Table
CREATE TABLE Technician (
    TechnicianID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Skills VARCHAR(255) NOT NULL,
    AvailabilityStatus BIT NOT NULL DEFAULT 1,
    AssignedRequestIDs VARCHAR(255) NULL
);

-- ServiceRequest Table
CREATE TABLE ServiceRequest (
    ServiceRequestID INT PRIMARY KEY IDENTITY(1,1),
    ClientID INT,
    TechnicianID INT,
    Description TEXT NOT NULL,
    ServiceType VARCHAR(50) NOT NULL,
    RequestDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL,
    Priority VARCHAR(20) NOT NULL,
    EstimatedCompletionTime DATETIME NULL,
    ActualCompletionTime DATETIME NULL,
    Location VARCHAR(255) NOT NULL,
    ClientFeedback TEXT NULL,
    Rating INT NULL,
    TechnicianFeedback TEXT NULL,
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
    FOREIGN KEY (TechnicianID) REFERENCES Technician(TechnicianID)
);

-- Contract Table
CREATE TABLE Contract (
    ContractID INT PRIMARY KEY IDENTITY(1,1),
    ClientID INT,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Terms TEXT NOT NULL,
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);

-- Feedback Table
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    ServiceRequestID INT,
    ClientID INT,
    Rating INT NOT NULL,
    Comments TEXT NULL,
    SubmittedDate DATETIME NOT NULL,
    FOREIGN KEY (ServiceRequestID) REFERENCES ServiceRequest(ServiceRequestID),
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);


-- Dummy Data Add:

USE [Service Request Management System]

-- Insert into Client Table
INSERT INTO Client (Name, Email, PhoneNumber, Address)
VALUES 
('John Doe', 'john.doe@example.com', '555-1234', '123 Elm Street'),
('Jane Smith', 'jane.smith@example.com', '555-5678', '456 Oak Avenue'),
('Michael Johnson', 'michael.j@example.com', '555-9876', '789 Pine Lane');

-- Insert into Technician Table
INSERT INTO Technician (Name, Skills, AvailabilityStatus, AssignedRequestIDs)
VALUES 
('Mike Ross', 'HVAC, Plumbing', 1, NULL),
('Rachel Zane', 'Electrical, Carpentry', 1, NULL),
('Harvey Specter', 'Appliance Repair', 0, NULL);

-- Insert into ServiceRequest Table
INSERT INTO ServiceRequest (ClientID, TechnicianID, Description, ServiceType, RequestDate, Status, Priority, EstimatedCompletionTime, ActualCompletionTime, Location, ClientFeedback, Rating, TechnicianFeedback)
VALUES 
(1, 1, 'Air conditioner repair', 'HVAC', GETDATE(), 'New', 'High', DATEADD(HOUR, 4, GETDATE()), NULL, '123 Elm Street', NULL, NULL, NULL),
(2, 2, 'Electrical wiring issue', 'Electrical', GETDATE(), 'In Progress', 'Medium', DATEADD(DAY, 1, GETDATE()), NULL, '456 Oak Avenue', NULL, NULL, NULL),
(3, 3, 'Appliance installation', 'Appliance Repair', GETDATE(), 'Completed', 'Low', DATEADD(DAY, 2, GETDATE()), GETDATE(), '789 Pine Lane', 'Great service', 5, 'Client was satisfied');

-- Insert into Contract Table
INSERT INTO Contract (ClientID, StartDate, EndDate, Terms)
VALUES 
(1, '2024-01-01', '2025-01-01', 'Annual maintenance contract covering HVAC and plumbing.'),
(2, '2024-05-01', '2024-11-01', 'Six-month electrical repair contract.'),
(3, '2024-07-01', '2025-07-01', 'Year-long appliance repair service.');

-- Insert into Feedback Table
INSERT INTO Feedback (ServiceRequestID, ClientID, Rating, Comments, SubmittedDate)
VALUES 
(1, 1, 4, 'Quick response, but technician was late.', GETDATE()),
(2, 2, 5, 'Excellent service!', GETDATE()),
(3, 3, 5, 'Very professional and timely.', GETDATE());