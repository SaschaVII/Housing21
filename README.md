# Housing21
A ASP.NET webform for a job interview at Housing 21.

## Instructions
The database is not included in this project and one needs to be set up. For demonstration purposes I have done this on my machine, but if run in a different environment the following steps need to be completed:

### 1. Create Database
Create a SQL database (e.g. SQL Express) and run the following script:
```sql
USE UserInformationDB;

-- Create Users Table
CREATE TABLE Person (
    PersonID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    DateOfBirth DATE,
    TelephoneNumber NVARCHAR(20),
    Email NVARCHAR(100)
);

-- Insert Dummy Data
INSERT INTO Person (Name, DateOfBirth, TelephoneNumber, Email)
VALUES ('Sascha Kuhness', '1996-03-26', '+1234567890', 'sascha.kuhness@example.com');

INSERT INTO Person (Name, DateOfBirth, TelephoneNumber, Email)
VALUES ('Kacey Boutcher', '1998-01-20', '+9876543210', 'kacey.boutcher@example.com');

INSERT INTO Person (Name, DateOfBirth, TelephoneNumber, Email)
VALUES ('Brandon Boutcher', '1995-06-21', '+1122334455', 'brandon.boutcher@example.com');

SELECT * FROM Person;
```

### 2. Create Stored Procedures
Run the following script in order to create the two stored procedures necessary:
```sql
USE UserInformationDB;

CREATE PROCEDURE psel_GetPersons
AS
BEGIN
    SELECT * FROM Person;
END;

ALTER PROCEDURE pins_Person
    @Name NVARCHAR(100),
    @DateOfBirth DATE,
    @TelephoneNumber NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    -- Name must not be NULL
    IF @Name IS NULL OR LEN(@Name) = 0
    BEGIN
        RAISERROR('Invalid Name. Name must be non-empty and less than or equal to 100 characters.', 16, 1)
        RETURN;
    END

	-- DOB must not be NULL
    IF @DateOfBirth IS NULL OR @DateOfBirth > GETDATE()
    BEGIN
        RAISERROR('Invalid Date of Birth. Please provide a valid date.', 16, 1)
        RETURN;
    END

	-- Email must not be NULL
	-- Email must be a valid email format
    IF @Email IS NULL OR @Email NOT LIKE '_%@__%.__%'
    BEGIN
        RAISERROR('Invalid Email. Please provide a valid email.', 16, 1)
        RETURN;
    END

	-- TelephoneNumber must not be NULL
	-- Must also start with "+" followed by 10 digits
    IF @TelephoneNumber IS NULL OR LEN(@Name) = 0
		OR @TelephoneNumber NOT LIKE '+[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
    BEGIN
        RAISERROR('Invalid Telephone Number. Please provide a valid telephone nunmber.', 16, 1)
        RETURN;
    END

    -- Insert data into the Person table if validation passes
    INSERT INTO Person (Name, DateOfBirth, TelephoneNumber, Email)
    VALUES (@Name, @DateOfBirth, @TelephoneNumber, @Email);
END;
```

### 3. Check the Connection String:
Open _Housing21UI/appsettings.json_ and adjust the **UserInformationDB** Connection String if necessary.

### 4. Build and Debug the Application
At this point you should be good to go and the application should run as intended.
