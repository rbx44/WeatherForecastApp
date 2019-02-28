--This is version 1 of create scripts, includes all tables

--+++++++++++++++++++++++++++++++++++++++++++
--Create Scripts
--+++++++++++++++++++++++++++++++++++++++++++


create table [User] (
UserId varchar(30) not null, 
CreatedByName varchar(100) not null,
CreatedOn datetime not null,
LastUpdatedByName varchar(100) not null,
LastUpdatedOn datetime not null,
Constraint PK_User PRIMARY KEY (UserId));
GO


Create table Location (
LocationId int identity(1,1) not null, 
City varchar(50) not null,
CreatedByName varchar(100) not null,
CreatedOn datetime not null,
LastUpdatedByName varchar(100) not null,
LastUpdatedOn datetime not null,
Constraint PK_Location PRIMARY KEY (LocationId));
GO


Create table User_Location (
UserId varchar(30) not null,
LocationId int not null, 
RequestedOn datetime not null,
CONSTRAINT FK_GroupMember_User FOREIGN KEY (UserId) References [User](UserId),
CONSTRAINT FK_GroupMember_Location FOREIGN KEY (LocationId) References Location(LocationId));
GO

