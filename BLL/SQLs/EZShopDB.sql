USE [EZShopDB]
go
delete from Products
delete from Categories
delete from Users
delete from Roles
delete from ProductStores
delete from Stores
delete from Cities
delete from Countries
go
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Computer', N'Laptops, desktops and computer peripherals')
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Home Theater System', NULL)
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Phone', N'IOS and Android Phones')
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Food', NULL)
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (5, N'Medicine', N'Antibiotics, Vitamins, Pain Killers, etc.')
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (6, N'Software', N'Operating Systems, Antivirus Software, Office Software and Video Games')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (1, N'Laptop', CAST(3000.50 AS Decimal(18, 2)), 10, NULL, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (2, N'Mouse', CAST(20.50 AS Decimal(18, 2)), NULL, NULL, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (3, N'Keyboard', CAST(40.00 AS Decimal(18, 2)), 45, NULL, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (4, N'Monitor', CAST(2500.00 AS Decimal(18, 2)), 20, NULL, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (5, N'Speaker', CAST(2500.00 AS Decimal(18, 2)), 70, NULL, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (6, N'Receiver', CAST(5000.00 AS Decimal(18, 2)), 30, NULL, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (7, N'Equalizer', CAST(1000.00 AS Decimal(18, 2)), 40, NULL, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (8, N'iPhone', CAST(10000.00 AS Decimal(18, 2)), 20, NULL, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (9, N'Apple', CAST(10.50 AS Decimal(18, 2)), 500, CAST(N'2024-12-31T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (10, N'Chocolate', CAST(2.50 AS Decimal(18, 2)), 125, CAST(N'2025-09-18T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount], [ExpirationDate], [CategoryId]) VALUES (11, N'Antibiotic', CAST(35.00 AS Decimal(18, 2)), 5, CAST(N'2027-05-19T00:00:00.0000000' AS DateTime2), 5)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 
GO
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'T�rkiye')
GO
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (2, N'United States of America')
GO
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 
GO
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (1, N'Ankara', 1)
GO
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (2, N'�stanbul', 1)
GO
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (3, N'�zmir', 1)
GO
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (4, N'New York', 2)
GO
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Stores] ON 
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (1, N'Hepsiburada', 1, 1, 2)
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (2, N'Vatan', 0, 1, 1)
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (3, N'Migros', 0, 1, 2)
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (4, N'Teknosa', 0, 1, 2)
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (5, N'�topya', 0, 1, 3)
GO
INSERT [dbo].[Stores] ([Id], [Name], [IsVirtual], [CountryId], [CityId]) VALUES (6, N'Sahibinden', 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Stores] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductStores] ON 
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (2, 2, 1)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (3, 2, 2)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (4, 3, 1)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (5, 3, 5)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (6, 3, 6)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (7, 4, 4)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (8, 4, 2)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (9, 5, 4)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (10, 6, 1)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (11, 6, 6)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (12, 8, 4)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (13, 8, 2)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (14, 8, 1)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (15, 8, 6)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (16, 9, 3)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (17, 10, 3)
GO
INSERT [dbo].[ProductStores] ([Id], [ProductId], [StoreId]) VALUES (18, 11, 3)
GO
SET IDENTITY_INSERT [dbo].[ProductStores] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [IsActive], [RoleId]) VALUES (1, N'admin', N'admin', 1, 1)
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [IsActive], [RoleId]) VALUES (2, N'user', N'user', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF