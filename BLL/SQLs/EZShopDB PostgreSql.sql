-- Create the Categories table
CREATE TABLE Categories (
     Id SERIAL PRIMARY KEY,
     Name VARCHAR(100) NOT NULL,
     Description TEXT
);

-- Create the Countries table
CREATE TABLE Countries (
     Id SERIAL PRIMARY KEY,
     Name VARCHAR(100) NOT NULL
);

-- Create the Cities table
CREATE TABLE Cities (
     Id SERIAL PRIMARY KEY,
     Name VARCHAR(125) NOT NULL,
     CountryId INT NOT NULL,
     FOREIGN KEY (CountryId) REFERENCES Countries(Id)
);

-- Create the Roles table
CREATE TABLE Roles (
     Id SERIAL PRIMARY KEY,
     RoleName VARCHAR(5) NOT NULL
);

-- Create the Users table
CREATE TABLE Users (
     Id SERIAL PRIMARY KEY,
     UserName VARCHAR(10) NOT NULL,
     Password VARCHAR(8) NOT NULL,
     IsActive BOOLEAN NOT NULL,
     RoleId INT NOT NULL,
     FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- Create the Stores table
CREATE TABLE Stores (
     Id SERIAL PRIMARY KEY,
     Name VARCHAR(200) NOT NULL,
     IsVirtual BOOLEAN NOT NULL,
     CountryId INT,
     CityId INT,
     FOREIGN KEY (CountryId) REFERENCES Countries(Id),
     FOREIGN KEY (CityId) REFERENCES Cities(Id)
);

-- Create the Products table
CREATE TABLE Products (
     Id SERIAL PRIMARY KEY,
     Name VARCHAR(150) NOT NULL,
     UnitPrice NUMERIC(18, 2) NOT NULL,
     StockAmount INT,
     ExpirationDate TIMESTAMP,
     CategoryId INT NOT NULL,
     FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Create the ProductStores table
CREATE TABLE ProductStores (
     Id SERIAL PRIMARY KEY,
     ProductId INT NOT NULL,
     StoreId INT NOT NULL,
     FOREIGN KEY (ProductId) REFERENCES Products(Id),
     FOREIGN KEY (StoreId) REFERENCES Stores(Id)
);

-- Insert data into Categories table
INSERT INTO Categories (Id, Name, Description) VALUES
(1, 'Computer', 'Laptops, desktops and computer peripherals'),
(2, 'Home Theater System', NULL),
(3, 'Phone', 'IOS and Android Phones'),
(4, 'Food', NULL),
(5, 'Medicine', 'Antibiotics, Vitamins, Pain Killers, etc.'),
(6, 'Software', 'Operating Systems, Antivirus Software, Office Software
and Video Games');

-- Adjust sequence for Categories
SELECT setval('categories_id_seq', (SELECT MAX(Id) FROM Categories));

-- Insert data into Countries table
INSERT INTO Countries (Id, Name) VALUES
(1, 'Türkiye'),
(2, 'United States of America');

-- Adjust sequence for Countries
SELECT setval('countries_id_seq', (SELECT MAX(Id) FROM Countries));

-- Insert data into Cities table
INSERT INTO Cities (Id, Name, CountryId) VALUES
(1, 'Ankara', 1),
(2, 'Ýstanbul', 1),
(3, 'Ýzmir', 1),
(4, 'New York', 2);

-- Adjust sequence for Cities
SELECT setval('cities_id_seq', (SELECT MAX(Id) FROM Cities));

-- Insert data into Roles table
INSERT INTO Roles (Id, RoleName) VALUES
(1, 'Admin'),
(2, 'User');

-- Adjust sequence for Roles
SELECT setval('roles_id_seq', (SELECT MAX(Id) FROM Roles));

-- Insert data into Users table
INSERT INTO Users (Id, UserName, Password, IsActive, RoleId) VALUES
(1, 'admin', 'admin', TRUE, 1),
(2, 'user', 'user', TRUE, 2);

-- Adjust sequence for Users
SELECT setval('users_id_seq', (SELECT MAX(Id) FROM Users));

-- Insert data into Stores table
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES
(1, 'Hepsiburada', TRUE, 1, 2),
(2, 'Vatan', FALSE, 1, 1),
(3, 'Migros', FALSE, 1, 2),
(4, 'Teknosa', FALSE, 1, 2),
(5, 'Ýtopya', FALSE, 1, 3),
(6, 'Sahibinden', TRUE, 1, 1);

-- Adjust sequence for Stores
SELECT setval('stores_id_seq', (SELECT MAX(Id) FROM Stores));

-- Insert data into Products table
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate,
CategoryId) VALUES
(1, 'Laptop', 3000.50, 10, NULL, 1),
(2, 'Mouse', 20.50, NULL, NULL, 1),
(3, 'Keyboard', 40.00, 45, NULL, 1),
(4, 'Monitor', 2500.00, 20, NULL, 1),
(5, 'Speaker', 2500.00, 70, NULL, 2),
(6, 'Receiver', 5000.00, 30, NULL, 2),
(7, 'Equalizer', 1000.00, 40, NULL, 2),
(8, 'iPhone', 10000.00, 20, NULL, 3),
(9, 'Apple', 10.50, 500, '2024-12-31', 4),
(10, 'Chocolate', 2.50, 125, '2025-09-18', 4),
(11, 'Antibiotic', 35.00, 5, '2027-05-19', 5);

-- Adjust sequence for Products
SELECT setval('products_id_seq', (SELECT MAX(Id) FROM Products));

-- Insert data into ProductStores table
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 2, 2),
(4, 3, 1),
(5, 3, 5),
(6, 3, 6),
(7, 4, 4),
(8, 4, 2),
(9, 5, 4),
(10, 6, 1),
(11, 6, 6),
(12, 8, 4),
(13, 8, 2),
(14, 8, 1),
(15, 8, 6),
(16, 9, 3),
(17, 10, 3),
(18, 11, 3);

-- Adjust sequence for ProductStores
SELECT setval('productstores_id_seq', (SELECT MAX(Id) FROM
ProductStores));