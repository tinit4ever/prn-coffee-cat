create database CoffeeCat;
drop database CoffeeCat;
CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name VARCHAR(255) NOT NULL,
    role_enabled BIT NULL
);

CREATE TABLE Shops (
    shop_id INT PRIMARY KEY IDENTITY(1,1),
    shop_name VARCHAR(255) NOT NULL,
    shop_email VARCHAR(255) NULL,
    shop_address VARCHAR(255) NULL,
    shop_telephone VARCHAR(255) NULL,
	shop_image VARCHAR(255) NULL,
    shop_enabled BIT NULL
);
CREATE TABLE Areas (
    area_id INT PRIMARY KEY IDENTITY(1,1),
	area_name VARCHAR(255) NOT NULL,
	 area_enabled BIT NULL,
	 shop_id INT,
	 CONSTRAINT fk_areas_coffee_shops FOREIGN KEY (shop_id) REFERENCES Shops(shop_id)
	 );
CREATE TABLE Users (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    customer_name VARCHAR(255) NOT NULL,
    customer_email VARCHAR(255) NOT NULL,
    customer_password VARCHAR(255) NOT NULL,
    customer_telephone VARCHAR(255) NULL,
    customer_enabled BIT NULL,
    role_id INT,
    shop_id INT,
    CONSTRAINT fk_users_roles FOREIGN KEY (role_id) REFERENCES Roles(role_id),
    CONSTRAINT fk_users_coffee_shops FOREIGN KEY (shop_id) REFERENCES Shops(shop_id)
);


CREATE TABLE Cats (
    cat_id INT PRIMARY KEY IDENTITY(1,1),
    cat_name VARCHAR(255) NOT NULL,
    cat_image VARCHAR(255) NULL,
    cat_enabled BIT NULL,
    area_id INT,
    CONSTRAINT fk_cats_coffee_shops FOREIGN KEY (area_id) REFERENCES Areas(area_id)
);

CREATE TABLE Tables (
    table_id INT PRIMARY KEY IDENTITY(1,1),
    table_name VARCHAR(255) NOT NULL,
    table_capacity INT NULL,
    table_status BIT DEFAULT 'TRUE',
    table_enabled BIT,
    area_id INT,
    CONSTRAINT fk_tables_coffee_shops FOREIGN KEY (area_id) REFERENCES Areas(area_id)
);



CREATE TABLE MenuItems (
    item_id INT PRIMARY KEY IDENTITY(1,1),
    item_name VARCHAR(255) NOT NULL,
    item_price DECIMAL(8, 2)NULL,
    item_enabled BIT NULL,
    shop_id INT,
    CONSTRAINT fk_menu_items_shops FOREIGN KEY (shop_id) REFERENCES Shops(shop_id)
);

CREATE TABLE ShopVouchers (
    voucher_id INT PRIMARY KEY IDENTITY(1,1),
    voucher_code VARCHAR(255) NOT NULL,
    voucher_discount DECIMAL(10, 2) NULL,
    voucher_expiry_date DATE NULL,
    voucher_enabled BIT NULL,
    coffee_shop_id INT,
    CONSTRAINT fk_vouchers_coffee_shops FOREIGN KEY (coffee_shop_id) REFERENCES Shops(shop_id)
);

CREATE TABLE UserVouchers (
    user_voucher_id INT PRIMARY KEY IDENTITY(1,1),
    voucher_id INT,
    used BIT DEFAULT 'FALSE',
    voucher_enabled BIT NULL,
    CONSTRAINT fk_user_vouchers_vouchers FOREIGN KEY (voucher_id) REFERENCES ShopVouchers(voucher_id)
);

CREATE TABLE Bookings (
    booking_id INT PRIMARY KEY IDENTITY(1,1),
    booking_code VARCHAR(255) NOT NULL,
    booking_start_time DATETIME NULL,
    booking_end_time DATETIME NULL,
    booking_enabled BIT NULL,
    customer_id INT,
    CONSTRAINT fk_bookings_users FOREIGN KEY (customer_id) REFERENCES Users(customer_id)
);

CREATE TABLE BookingTables (
    booking_id INT,
    table_id INT,
    PRIMARY KEY (booking_id, table_id),
    FOREIGN KEY (booking_id) REFERENCES Bookings(booking_id),
    FOREIGN KEY (table_id) REFERENCES Tables(table_id)
);

CREATE TABLE BookingMenuItems (
    booking_id INT,
    item_id INT,
    PRIMARY KEY (booking_id, item_id),
    FOREIGN KEY (booking_id) REFERENCES Bookings(booking_id),
    FOREIGN KEY (item_id) REFERENCES MenuItems(item_id)
);

INSERT INTO Roles ( role_name, role_enabled)
VALUES
( 'Admin', 1),
( 'Owner', 1),
( 'Staff', 1),
( 'Cutomer', 1);
INSERT INTO Shops ( shop_name, shop_email, shop_address, shop_telephone,shop_enabled,shop_image)
VALUES
('Coffee Binh Tan', 'binhtan@cpp.com', '123 Main St', '081234567', 1,''),
('Coffee Tan binh', 'tanbinh@cpp.com', '456 Elm St', '0898745632', 1,''),
('Coffee Thu Duc', 'thuduc@cpp.com', '789 Main St', '0899675408', 0,''),
('Coffee Ha Noi', 'hanoi@cpp.com', '111 Main St', '0862513970', 1,''),
('Coffee Quang Ngai', 'quangngai@cpp.com', '458 Main St', '0399675408', 1,''),
('Coffee Hue', 'hue@cpp.com', '888 Main St', '0903901492', 0,''),
('Coffee Thanh Hoa', 'thanhhoa@cpp.com', '999 Main St', '0848640542', 1,''),
('Coffee Tay Bac ', 'taybac@cpp.com', '645 Main St', '0322114478', 0,''),
('Coffee Ca Mau', 'camau@cpp.com', '354 Main St', '0866778812', 1,''),
('Coffee Quan Ninh', 'quanninh@cpp.com', '213 Main St', '0903648721', 1,'');
INSERT INTO Areas ( area_name, area_enabled,shop_id)
VALUES
('floor1', 1,1),
('floor2', 1,1),
('floor3', 1,1),
('floor4', 0,1),
('floor5', 1,1),
('floor1', 1,2),
('floor2', 1,2),
('floor3', 1,2),
('floor4', 0,2),
('floor5', 1,2),
('floor6', 1,2),
('floor1', 1,3),
('floor2', 1,3),
('floor3', 1,3),
('floor4', 0,3),
('floor1', 1,4),
('floor2', 1,4),
('floor1', 1,5),
('floor2', 0,5),
('floor3', 1,5),
('floor4', 1,5);
INSERT INTO Users ( customer_name, customer_email, customer_password, customer_telephone, customer_enabled, role_id, shop_id)
VALUES
( 'John Doe', 'admin@gmail.com', 'an123456', '123456789', 1, 1, 1),
( 'Jane Smith', 'owner@gmail.com', 'an123456', '987654321', 1, 2, 2),
( 'Tin', 'staff@gmail.com', 'an123456', '123456789', 1, 3, 1),
( 'Kaido', 'cus@gmail.com', 'an123456', '987654321', 1, 4, 2),
( 'tri', 'tri@gmail.com', 'an123456', '0399675408', 1, 4, 2),
( 'long', 'long@gmail.com', 'an123456', '0866557951', 1, 2, 2),
( 'minh', 'minh@gmail.com', 'an123456', '0399885645', 1, 4, 2);

INSERT INTO Cats ( cat_name, cat_image, cat_enabled, area_id)
VALUES
( 'Fluffy', 'fluffy.jpg', 1, 1),
( 'Whiskers', 'whiskers.jpg', 1, 1),
( 'Snowball', 'snowball.jpg', 1, 2),
( 'Tom', 'tom.jpg', 1, 2),
( 'Black', 'black.jpg', 1, 3),
( 'Onw', 'onw.jpg', 1, 4),
( 'Kiki', 'kiki.jpg', 1, 5),
( 'Mimi', 'mimi.jpg', 1, 5),
( 'Wawa', 'wawa.jpg', 1, 6);
select * from MenuItems where shop_id = 1;
INSERT INTO MenuItems (item_name, item_price, item_enabled, shop_id)
values
('Pepsi',10000, 1, 1),
('Cocacola',10000, 1, 1),
('Sting',10000, 1, 1),
('Avocado smoothie',20000, 1, 1),
('Lemon smoothie',15000, 1, 1),
('Mango smoothie',35000, 1, 1),
('Milk Tea',18000, 1, 1),
('Black Coffee',20000, 1, 1),
('Weasel Cafe',20000, 1, 1),
('Mocha',20000, 1, 1),
('Tea',15000, 1, 1),
('Cappuccino',50000, 1, 1),
('Strawberry Cake',25000, 1, 1),
('Coffee cake',25000, 1, 1),
('Cheesecake',25000, 1, 1),
('Chocolate cake',25000, 1, 1),

('Pepsi',10000, 1, 2),
('Cocacola',10000, 1, 2),
('Sting',10000, 1, 2),
('Avocado smoothie',20000, 1, 2),
('Lemon smoothie',15000, 1, 2),
('Mango smoothie',35000, 1, 2),
('Milk Tea',18000, 1, 2),
('Black Coffee',20000, 1, 2),
('Weasel Cafe',20000, 1, 2),
('Mocha',20000, 1, 2),
('Tea',15000, 1, 2),
('Cappuccino',50000, 1, 2),
('Strawberry Cake',25000, 1, 2),
('Coffee cake',25000, 1, 2),
('Cheesecake',25000, 1, 2),
('Chocolate cake',25000, 1, 2),

('Pepsi',10000, 1, 3),
('Cocacola',10000, 1, 3),
('Sting',10000, 1, 3),
('Avocado smoothie',20000, 1, 3),
('Lemon smoothie',15000, 1, 3),
('Mango smoothie',35000, 1, 3),
('Milk Tea',18000, 1, 3),
('Black Coffee',20000, 1, 3),
('Weasel Cafe',20000, 1, 3),
('Mocha',20000, 1, 3),
('Tea',15000, 1, 3),
('Cappuccino',50000, 1, 3),
('Strawberry Cake',25000, 1, 3),
('Coffee cake',25000, 1, 3),
('Cheesecake',25000, 1, 3),
('Chocolate cake',25000, 1, 3),

('Pepsi',10000, 1, 4),
('Cocacola',10000, 1, 4),
('Sting',10000, 1, 4),
('Avocado smoothie',20000, 1, 4),
('Lemon smoothie',15000, 1, 4),
('Mango smoothie',35000, 1, 4),
('Milk Tea',18000, 1, 4),
('Black Coffee',20000, 1, 4),
('Weasel Cafe',20000, 1, 4),
('Mocha',20000, 1, 4),
('Tea',15000, 1, 4),
('Cappuccino',50000, 1, 4),
('Strawberry Cake',25000, 1, 4),
('Coffee cake',25000, 1, 4),
('Cheesecake',25000, 1, 4),
('Chocolate cake',25000, 1, 4),

('Pepsi',10000, 1, 5),
('Cocacola',10000, 1, 5),
('Sting',10000, 1, 5),
('Avocado smoothie',20000, 1, 5),
('Lemon smoothie',15000, 1, 5),
('Mango smoothie',35000, 1, 5),
('Milk Tea',18000, 1, 5),
('Black Coffee',20000, 1, 5),
('Weasel Cafe',20000, 1, 5),
('Mocha',20000, 1, 5),
('Tea',15000, 1, 5),
('Cappuccino',50000, 1, 5),
('Strawberry Cake',25000, 1, 5),
('Coffee cake',25000, 1, 5),
('Cheesecake',25000, 1, 5),
('Chocolate cake',25000, 1, 5),

('Pepsi',10000, 1, 6),
('Cocacola',10000, 1, 6),
('Sting',10000, 1, 6),
('Avocado smoothie',20000, 1, 6),
('Lemon smoothie',15000, 1, 6),
('Mango smoothie',35000, 1, 6),
('Milk Tea',18000, 1, 6),
('Black Coffee',20000, 1, 6),
('Weasel Cafe',20000, 1, 6),
('Mocha',20000, 1, 6),
('Tea',15000, 1, 6),
('Cappuccino',50000, 1, 6),
('Strawberry Cake',25000, 1, 6),
('Coffee cake',25000, 1, 6),
('Cheesecake',25000, 1, 6),
('Chocolate cake',25000, 1, 6);
INSERT INTO Tables(table_name, table_capacity, table_status, table_enabled, area_id)
values
('ban xanh', 4, 1, 1, 1),
('ban quy toc', 1, 1, 1, 1),
('ban binh dan', 5, 1, 1, 1),
('ban do', 3, 1, 1, 2),
('ban kitty', 3, 1, 1, 2),
('ban doremon', 3, 1, 1, 2),
('ban maval', 3, 1, 1, 2),
('ban vang', 4, 1, 1, 3),
('ban xanh', 4, 1, 1, 4),
('ban xanh', 4, 1, 1, 5);
