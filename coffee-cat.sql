--create database CoffeeCat;

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
('Coffee Shop 1', 'shop1@cpp.com', '123 Main St', '123456789', 1,''),
( 'Coffee Shop 2', 'shop2@cpp.com', '456 Elm St', '987654321', 1,'');
INSERT INTO Areas ( area_name, area_enabled,shop_id)
VALUES
('floor1', 1,1),
( 'floor2', 1,1);
INSERT INTO Users ( customer_name, customer_email, customer_password, customer_telephone, customer_enabled, role_id, shop_id)
VALUES
( 'John Doe', 'admin@gmail.com', 'an123456', '123456789', 1, 1, 1),
( 'Jane Smith', 'owner@gmail.com', 'an123456', '987654321', 1, 2, 2),
( 'Tin', 'staff@gmail.com', 'an123456', '123456789', 1, 3, 1),
( 'Kaido', 'cus@gmail.com', 'an123456', '987654321', 1, 4, 2);

INSERT INTO Cats ( cat_name, cat_image, cat_enabled, area_id)
VALUES
( 'Fluffy', 'fluffy.jpg', 1, 1),
( 'Whiskers', 'whiskers.jpg', 1, 1),
( 'Snowball', 'snowball.jpg', 1, 2);
