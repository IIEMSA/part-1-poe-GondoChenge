USE master
IF EXISTS (SELECT * FROM sys.databases WHERE name ='VenueBookingSystemDB')
DROP DATABASE VenueBookingSystemDB
CREATE DATABASE VenueBookingSystemDB

USE VenueBookingSystemDB




CREATE TABLE Venue (
    venue_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    venue_name VARCHAR (50) NOT NULL,
    location_of_venue VARCHAR (50) NOT NULL,
    capacity INT NOT NULL,
    image_url VARCHAR (1000) NOT NULL
);

INSERT INTO Venue (venue_name, location_of_venue, capacity, image_url) 
VALUES ('The Sandton Ballroom', 'Sandton', '100', 'https://www.bing.com/images/search?view=detailV2&ccid=qryN0eo0&id=4D8F87A2E5625A836C07C7812FA62C1CF7617215&thid=OIP.qryN0eo0QeX1wkZWln6fRgAAAA&mediaurl=https%3a%2f%2feventopediacdn.azureedge.net%2fcontent%2fz-dis-43.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.aabc8dd1ea3441e5f5c24656967e9f46%3frik%3dFXJh9xwspi%252bBxw%26pid%3dImgRaw%26r%3d0&exph=203&expw=340&q=the+sandton+venue&simid=607995927973674693&FORM=IRPRST&ck=39914864F8789258B8A6446FB9CD442D&selectedIndex=0&itb=0' );

SELECT * FROM Venue



CREATE TABLE Event_ (
    event_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    event_name VARCHAR (50) NOT NULL,
    event_date DATE NOT NULL,
    description_ VARCHAR (1000) NOT NULL,
    venue_id INT NOT NULL, 
	FOREIGN KEY (venue_id) references Venue(venue_id)
);

INSERT INTO Event_ (event_name, event_date, description_, venue_id) 
VALUES ('Royale Party', '2025-11-30', 'Exclusively for the 1%', '1' );

SELECT * FROM Event_


CREATE TABLE Booking (
    booking_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    event_id INT NOT NULL,
	FOREIGN KEY (event_id) references Event_(event_id),
    venue_id INT NOT NULL, 
	FOREIGN KEY (venue_id) references Venue(venue_id),
	booking_date DATE NOT NULL
);


INSERT INTO Booking (event_id, venue_id, booking_date) 
VALUES ('1', '1', '2025-07-01');

SELECT * FROM Booking