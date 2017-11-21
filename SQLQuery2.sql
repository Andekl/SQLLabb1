CREATE TABLE laptop (
 model int NOT NULL,
 speed int NOT NULL,
 ram int NOT NULL,
 hd int NOT NULL,
 screen nvarchar(8) NOT NULL,
 price int NOT NULL
)
INSERT INTO laptop (model, speed, ram, hd, screen, price) VALUES
(2001, 700, 64, 5, '12,1', 1448),
(2002, 800, 96, 10, '15,1', 2584),
(2003, 850, 64, 10, '15,1', 2738),
(2004, 550, 32, 5, '12,1', 999),
(2005, 600, 64, 6, '12,1', 2399),
(2006, 800, 96, 20, '15,7', 2999),
(2007, 850, 128, 20, '15,0', 3099),
(2008, 650, 64, 10, '12,1', 1249),
(2009, 750, 256, 20, '15,1', 2599),
(2010, 366, 64, 10, '12,1', 1499);

-- --------------------------------------------------------
CREATE TABLE pc (
 model int NOT NULL,
 speed int NOT NULL,
 ram int NOT NULL,
 hd int NOT NULL,
 rd nvarchar(10) NOT NULL,
 price int NOT NULL
)
INSERT INTO pc (model, speed, ram, hd, rd, price) VALUES
(1001, 700, 64, 10, '48xCD', 799),
(1002, 1500, 128, 60, '12xDVD', 2499),
(1003, 866, 128, 20, '8xDVD', 1999),
(1004, 866, 64, 10, '12xDVD', 999),
(1005, 1000, 128, 20, '12xDVD', 1499),
(1006, 1300, 256, 40, '16xDVD', 2119),
(1007, 1400, 128, 80, '12xDVD', 2299),
(1008, 700, 64, 30, '24xCD', 999),
(1009, 1200, 128, 80, '16xDVD', 1699),
(1010, 750, 64, 30, '40xCD', 699),
(1011, 1100, 128, 60, '16xDVD', 1299),
(1012, 350, 64, 7, '48xCD', 799),
(1013, 733, 256, 60, '12xDVD', 2499);

-- --------------------------------------------------------
CREATE TABLE printer (
 model int NOT NULL,
 color nvarchar(5) NOT NULL,
 [type] nvarchar(10) NOT NULL,
 price int NOT NULL
)
INSERT INTO printer (model, color, type, price) VALUES
(3001, 'true', 'ink-jet', 231),
(3002, 'true', 'ink-jet', 267),
(3003, 'false', 'laser', 390),
(3004, 'true', 'ink-jet', 439),
(3005, 'true', 'bubble', 200),
(3006, 'true', 'laser', 1999),
(3007, 'false', 'laser', 350);

-- --------------------------------------------------------
CREATE TABLE product (
 maker nvarchar(1) NOT NULL,
 model int NOT NULL,
 [type] nvarchar(10) NOT NULL
)
INSERT INTO product (maker, model, type) VALUES
('A', 1001, 'pc'),
('A', 1002, 'pc'),
('A', 1003, 'pc'),
('B', 1004, 'pc'),
('B', 1005, 'pc'),
('B', 1006, 'pc'),
('C', 1007, 'pc'),
('C', 1008, 'pc'),
('D', 1009, 'pc'),
('D', 1010, 'pc'),
('D', 1011, 'pc'),
('E', 1012, 'pc'),
('E', 1013, 'pc'),
('B', 2001, 'laptop'),
('B', 2002, 'laptop'),
('B', 2003, 'laptop'),
('A', 2004, 'laptop'),
('A', 2005, 'laptop'),
('A', 2006, 'laptop'),
('D', 2007, 'pc'),
('C', 2008, 'pc'),
('C', 2009, 'pc'),
('E', 2010, 'pc'),
('F', 3001, 'printer'),
('C', 3002, 'printer'),
('C', 3003, 'printer'),
('F', 3004, 'printer'),
('G', 3005, 'printer'),
('C', 3006, 'printer'),
('H', 3007, 'printer');

ALTER TABLE laptop
 ADD PRIMARY KEY (model);
ALTER TABLE pc
 ADD PRIMARY KEY (model);
ALTER TABLE printer
 ADD PRIMARY KEY (model);
ALTER TABLE product
 ADD PRIMARY KEY (model);
