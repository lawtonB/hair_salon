Hair Salon

This site allows a user to enter the names of a stylist and the names of the clients belonging to that stylist using a sql database. To create the database:

In SQLCMD:
CREATE DATABASE hair_salon;
GO
USE hair_salon;
GO
CREATE TABLE stylists(id int identity(1,1), name varchar(255));
CREATE TABLE create table clients(id int identity(1,1), name varchar(255), client_id int);
GO

To implement the sql database files, please download the .sql files from the project folder and import them into Microsoft SQL Server Management Studio
Languages Used

    HTML
    C#
        Nancy
        Razor
    & coded in Atom, databases in SQL

(c) LawtonB 2016
