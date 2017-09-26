/*
File Name - aswally.sql
Programmers Name - Arindm Sharma
Date of first useful version - 12/06/2016
Desription - The following script runs queries to create a Db called ASWally as per wallys worlds specifications.
*/

-- dropping table if it already exists to start fresh--
DROP database IF EXISTS ASWally;

-- create database--
create database ASWally;

-- use the created database--
use aswally;

-- creating customer table--
CREATE TABLE Customer (
    CustomerID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    PhoneNo VARCHAR(255)
);

-- creating branch table--
CREATE TABLE Branch (
    BranchID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    BranchName VARCHAR(255)
);

-- creating product table--
CREATE TABLE Product (
    ProductID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(255),
    Price FLOAT,
    Quantity INT
);

-- creating orders table--
CREATE TABLE orders (
    OrderID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    OrderDate TIMESTAMP,
    BranchID BIGINT,
    CustomerID BIGINT,
    FOREIGN KEY (BranchID)
        REFERENCES Branch (BranchID),
    FOREIGN KEY (CustomerID)
        REFERENCES customer (CustomerID),
    OrderStatus VARCHAR(255)
);

-- creating orderline table--
CREATE TABLE orderLine (
    OrderID BIGINT,
    ProductID BIGINT,
    FOREIGN KEY (OrderID)
        REFERENCES orders (OrderID),
    FOREIGN KEY (ProductID)
        REFERENCES product (ProductID),
    Quantity INT
);

-- inserting into customer table--
INSERT INTO customer (FirstName, LastName, PhoneNo)
VALUES ('Norbert','Mika','416-555-1111'),
('Russell','Foubert','519-555-2222'),
('Sean','Clarke','519-555-3333');

-- inserting into branch table--
INSERT INTO branch (BranchName)
VALUES ('Sports World'),
('Cambridge Mall'),
('St. Jacobs');

-- inserting into product table--
INSERT INTO product (ProductName, Price, Quantity)
VALUES ('Disco Queen Wallpaper (roll)','12.95','56'),
('Countryside Wallpaper (roll)','11.95','24'),
('Victorian Lace Wallpaper (roll)','14.95','44'),
('Drywall Tape (roll)','3.95','120'),
('Drywall Tape (pkg 10)','36.95','30'),
('Drywall Repair Compound (tube)','6.95','90');

-- inserting into orders table--
INSERT INTO orders (OrderDate,BranchID,CustomerID,OrderStatus)
VALUES ('2016-09-20',1,3,'PAID'),
('2016-10-06',2,2,'PEND'),
('2016-11-02',3,1,'PAID');

-- inserting into orderline table--
insert into orderline (OrderID,ProductID,Quantity)
values (1,3,4),
(1,6,1),
(1,4,2);

-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity - 4
WHERE
    ProductID = 3;

-- updating product table quantities--    
UPDATE product 
SET 
    quantity = quantity - 1
WHERE
    ProductID = 6;
    
-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity - 2
WHERE
    ProductID = 4;
    
-- inserting into orderline table--
insert into orderline (OrderID,ProductID,Quantity)
values (2,2,10);

-- updating orders table status--
UPDATE orders 
SET 
    OrderStatus = 'CNCL'
WHERE
    OrderID = 2;
    
-- updating orders table date--
UPDATE orders 
SET 
    OrderDate = '2016-10-20'
WHERE
    OrderID = 3;
    
-- inserting into orderline table--
insert into orderline (OrderID,ProductID,Quantity)
values (3,1,12),
(3,4,3);

-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity - 12
WHERE
    ProductID = 1;
    
-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity - 3
WHERE
    ProductID = 4;
    
-- updating orders table status--
UPDATE orders 
SET 
    OrderStatus = 'RFND'
WHERE
    OrderID = 3;
    
-- updating orders table orderdate--
UPDATE orders 
SET 
    OrderDate = '2016-11-04'
WHERE
    OrderID = 3;

-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity + 12
WHERE
    ProductID = 1;
   
-- updating product table quantities--
UPDATE product 
SET 
    quantity = quantity + 3
WHERE
    ProductID = 4;