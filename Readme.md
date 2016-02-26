In SQLCMD:
CREATE DATABASE hair_salon;
GO
USE hair_salon;
GO
CREATE TABLE clients(id int identity(1,1), name varchar(255));
CREATE TABLE create table stylists(id int identity(1,1), name varchar(255), client_id int);
GO
