-- Drop the database if it exists
DROP DATABASE IF EXISTS EZShopDB;

-- Create the database
CREATE DATABASE EZShopDB;

-- Use the new database
USE EZShopDB;

-- Create the Categories table
CREATE TABLE Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description TEXT
);

-- Create the Products table
CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    StockAmount INT,
    ExpirationDate DATETIME,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Create the ProductStores table
CREATE TABLE ProductStores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    StoreId INT NOT NULL
);

-- Create the Roles table
CREATE TABLE Roles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RoleName VARCHAR(5) NOT NULL
);

-- Create the Stores table
CREATE TABLE Stores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    IsVirtual BOOLEAN NOT NULL,
    CountryId INT,
    CityId INT
);

-- Create the Users table
CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserName VARCHAR(10) NOT NULL,
    Password VARCHAR(8) NOT NULL,
    IsActive BOOLEAN NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- Create the Countries table
CREATE TABLE Countries (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Create the Cities table
CREATE TABLE Cities (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(125) NOT NULL,
    CountryId INT NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Countries(Id)
);

-- Insert data into Categories table
INSERT INTO Categories (Id, Name, Description) VALUES (1, 'Computer', 'Laptops, desktops and computer peripherals');
INSERT INTO Categories (Id, Name, Description) VALUES (2, 'Home Theater System', NULL);
INSERT INTO Categories (Id, Name, Description) VALUES (3, 'Phone', 'IOS and Android Phones');
INSERT INTO Categories (Id, Name, Description) VALUES (4, 'Food', NULL);
INSERT INTO Categories (Id, Name, Description) VALUES (5, 'Medicine', 'Antibiotics, Vitamins, Pain Killers, etc.');
INSERT INTO Categories (Id, Name, Description) VALUES (6, 'Software', 'Operating Systems, Antivirus Software, Office Software and Video Games');

-- Insert data into Products table
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (1, 'Laptop', 3000.50, 10, NULL, 1);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (2, 'Mouse', 20.50, NULL, NULL, 1);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (3, 'Keyboard', 40.00, 45, NULL, 1);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (4, 'Monitor', 2500.00, 20, NULL, 1);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (5, 'Speaker', 2500.00, 70, NULL, 2);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (6, 'Receiver', 5000.00, 30, NULL, 2);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (7, 'Equalizer', 1000.00, 40, NULL, 2);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (8, 'iPhone', 10000.00, 20, NULL, 3);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (9, 'Apple', 10.50, 500, '2024-12-31', 4);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (10, 'Chocolate', 2.50, 125, '2025-09-18', 4);
INSERT INTO Products (Id, Name, UnitPrice, StockAmount, ExpirationDate, CategoryId) VALUES (11, 'Antibiotic', 35.00, 5, '2027-05-19', 5);

-- Insert data into ProductStores table
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (1, 1, 1);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (2, 2, 1);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (3, 2, 2);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (4, 3, 1);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (5, 3, 5);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (6, 3, 6);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (7, 4, 4);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (8, 4, 2);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (9, 5, 4);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (10, 6, 1);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (11, 6, 6);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (12, 8, 4);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (13, 8, 2);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (14, 8, 1);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (15, 8, 6);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (16, 9, 3);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (17, 10, 3);
INSERT INTO ProductStores (Id, ProductId, StoreId) VALUES (18, 11, 3);

-- Insert data into Roles table
INSERT INTO Roles (Id, RoleName) VALUES (1, 'Admin');
INSERT INTO Roles (Id, RoleName) VALUES (2, 'User');

-- Insert data into Countries table
INSERT INTO Countries (Id, Name) VALUES (1, 'Türkiye');
INSERT INTO Countries (Id, Name) VALUES (2, 'United States of America');

-- Insert data into Cities table
INSERT INTO Cities (Id, Name, CountryId) VALUES (1, 'Ankara', 1);
INSERT INTO Cities (Id, Name, CountryId) VALUES (2, 'İstanbul', 1);
INSERT INTO Cities (Id, Name, CountryId) VALUES (3, 'İzmir', 1);
INSERT INTO Cities (Id, Name, CountryId) VALUES (4, 'New York', 2);

-- Insert data into Stores table
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (1, 'Hepsiburada', 1, 1, 2);
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (2, 'Vatan', 0, 1, 1);
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (3, 'Migros', 0, 1, 2);
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (4, 'Teknosa', 0, 1, 2);
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (5, 'İtopya', 0, 1, 3);
INSERT INTO Stores (Id, Name, IsVirtual, CountryId, CityId) VALUES (6, 'Sahibinden', 1, 1, 1);

-- Insert data into Users table
INSERT INTO Users (Id, UserName, Password, IsActive, RoleId) VALUES (1, 'admin', 'admin', 1, 1);
INSERT INTO Users (Id, UserName, Password, IsActive, RoleId) VALUES (2, 'user', 'user', 1, 2);

-- Add foreign key constraints
ALTER TABLE Products ADD CONSTRAINT FK_Products_Categories_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(Id);
ALTER TABLE ProductStores ADD CONSTRAINT FK_ProductStores_Products_ProductId FOREIGN KEY (ProductId) REFERENCES Products(Id);
ALTER TABLE ProductStores ADD CONSTRAINT FK_ProductStores_Stores_StoreId FOREIGN KEY (StoreId) REFERENCES Stores(Id);
ALTER TABLE Cities ADD CONSTRAINT FK_Cities_Countries FOREIGN KEY (CountryId) REFERENCES Countries(Id);
ALTER TABLE Stores ADD CONSTRAINT FK_Stores_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id);
ALTER TABLE Stores ADD CONSTRAINT FK_Stores_Countries FOREIGN KEY (CountryId) REFERENCES Countries(Id);
ALTER TABLE Users ADD CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id);



-- MySQL Scaffolding an Existing Database in EF Core: https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html
-- dotnet ef dbcontext scaffold "server=127.0.0.1;uid=root;pwd=root;database=EZShopDB;" MySql.EntityFrameworkCore -outputdir DAL -f