﻿ CREATE DATABASE Library2

	GO
	USE Library2
	GO
	CREATE TABLE Authors(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(30) NOT NULL,
	Surname NVARCHAR(30) NOT NULL,
	Age INT
	)
	GO
	INSERT INTO Authors([Name],[Surname],[Age])
	VALUES('Linus','Torvalds',35),
	('Leyla','Mammadova',30),
	('Akif','Mammadli',25)
	
	GO 
	
	CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(30) NOT NULL
	)
	
	GO
	
	INSERT INTO Categories([Name])
	VALUES('Dram'),('Adventure'),('Science'),
	('Programming')
	
	GO
	
	CREATE TABLE Books(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(30) NOT NULL,
	[Pages] INT NOT NULL,
	[AuthorId] INT FOREIGN KEY REFERENCES Authors(Id) ON DELETE CASCADE NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES Categories(Id) ON DELETE CASCADE NOT NULL
	)
	
	GO
	
	INSERT INTO Books([Name],[Pages],[AuthorId],[CategoryId])
	VALUES('Linux Essential',350,1,3),
	('C#',1200,2,4),
	('Tom Sawyer Adventure',70,3,2),
	('C++',400,1,3),
	('Design Patterns',350,2,4)
	
	GO
	
	--SP 
	CREATE PROCEDURE GetBookById
	@book_id INT
	AS
	BEGIN
	
	SELECT * FROM Books
	WHERE Books.Id=@book_id
	
	END
