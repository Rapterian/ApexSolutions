USE [Service Request Management System];
GO

--CLIENT TABLE
-- Insert a new client
CREATE PROCEDURE InsertClient
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(15),
    @Address VARCHAR(255)
AS
BEGIN
    INSERT INTO Client (Name, Email, PhoneNumber, Address)
    VALUES (@Name, @Email, @PhoneNumber, @Address);
END;
GO

-- Retrieve all clients
CREATE PROCEDURE GetClients
AS
BEGIN
    SELECT * FROM Client;
END;
GO

-- Update a client's information
CREATE PROCEDURE UpdateClient
    @ClientID INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(15),
    @Address VARCHAR(255)
AS
BEGIN
    UPDATE Client
    SET Name = @Name,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        Address = @Address
    WHERE ClientID = @ClientID;
END;
GO

-- Delete a client
CREATE PROCEDURE DeleteClient
    @ClientID INT
AS
BEGIN
    DELETE FROM Client
    WHERE ClientID = @ClientID;
END;
GO


--TECHNICAL TABLE 
-- Insert a new technician
CREATE PROCEDURE InsertTechnician
    @Name VARCHAR(100),
    @Skills VARCHAR(255),
    @AvailabilityStatus BIT,
    @AssignedRequestIDs VARCHAR(255)
AS
BEGIN
    INSERT INTO Technician (Name, Skills, AvailabilityStatus, AssignedRequestIDs)
    VALUES (@Name, @Skills, @AvailabilityStatus, @AssignedRequestIDs);
END;
GO

-- Retrieve all technicians
CREATE PROCEDURE GetTechnicians
AS
BEGIN
    SELECT * FROM Technician;
END;
GO

-- Update a technician's information
CREATE PROCEDURE UpdateTechnician
    @TechnicianID INT,
    @Name VARCHAR(100),
    @Skills VARCHAR(255),
    @AvailabilityStatus BIT,
    @AssignedRequestIDs VARCHAR(255)
AS
BEGIN
    UPDATE Technician
    SET Name = @Name,
        Skills = @Skills,
        AvailabilityStatus = @AvailabilityStatus,
        AssignedRequestIDs = @AssignedRequestIDs
    WHERE TechnicianID = @TechnicianID;
END;
GO

-- Delete a technician
CREATE PROCEDURE DeleteTechnician
    @TechnicianID INT
AS
BEGIN
    DELETE FROM Technician
    WHERE TechnicianID = @TechnicianID;
END;
GO

--SERVICE REQUEST TABLE:
-- Insert a new service request
CREATE PROCEDURE InsertServiceRequest
    @ClientID INT,
    @TechnicianID INT,
    @Description TEXT,
    @ServiceType VARCHAR(50),
    @RequestDate DATETIME,
    @Status VARCHAR(20),
    @Priority VARCHAR(20),
    @EstimatedCompletionTime DATETIME,
    @Location VARCHAR(255)
AS
BEGIN
    INSERT INTO ServiceRequest (ClientID, TechnicianID, Description, ServiceType, RequestDate, Status, Priority, EstimatedCompletionTime, Location)
    VALUES (@ClientID, @TechnicianID, @Description, @ServiceType, @RequestDate, @Status, @Priority, @EstimatedCompletionTime, @Location);
END;
GO

-- Retrieve all service requests
CREATE PROCEDURE GetServiceRequests
AS
BEGIN
    SELECT * FROM ServiceRequest;
END;
GO

-- Update a service request
CREATE PROCEDURE UpdateServiceRequest
    @ServiceRequestID INT,
    @TechnicianID INT,
    @Description TEXT,
    @ServiceType VARCHAR(50),
    @Status VARCHAR(20),
    @Priority VARCHAR(20),
    @EstimatedCompletionTime DATETIME,
    @ActualCompletionTime DATETIME,
    @Location VARCHAR(255)
AS
BEGIN
    UPDATE ServiceRequest
    SET TechnicianID = @TechnicianID,
        Description = @Description,
        ServiceType = @ServiceType,
        Status = @Status,
        Priority = @Priority,
        EstimatedCompletionTime = @EstimatedCompletionTime,
        ActualCompletionTime = @ActualCompletionTime,
        Location = @Location
    WHERE ServiceRequestID = @ServiceRequestID;
END;
GO

-- Delete a service request
CREATE PROCEDURE DeleteServiceRequest
    @ServiceRequestID INT
AS
BEGIN
    DELETE FROM ServiceRequest
    WHERE ServiceRequestID = @ServiceRequestID;
END;
GO


--CONTRACT TABLE:
-- Insert a new contract
CREATE PROCEDURE InsertContract
    @ClientID INT,
    @StartDate DATETIME,
    @EndDate DATETIME,
    @Terms TEXT
AS
BEGIN
    INSERT INTO Contract (ClientID, StartDate, EndDate, Terms)
    VALUES (@ClientID, @StartDate, @EndDate, @Terms);
END;
GO

-- Retrieve all contracts
CREATE PROCEDURE GetContracts
AS
BEGIN
    SELECT * FROM Contract;
END;
GO

-- Update a contract
CREATE PROCEDURE UpdateContract
    @ContractID INT,
    @StartDate DATETIME,
    @EndDate DATETIME,
    @Terms TEXT
AS
BEGIN
    UPDATE Contract
    SET StartDate = @StartDate,
        EndDate = @EndDate,
        Terms = @Terms
    WHERE ContractID = @ContractID;
END;
GO

-- Delete a contract
CREATE PROCEDURE DeleteContract
    @ContractID INT
AS
BEGIN
    DELETE FROM Contract
    WHERE ContractID = @ContractID;
END;
GO
