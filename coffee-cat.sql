
Create DATABASE CoffeeCat

USE CoffeeCat

CREATE TABLE Users (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(255) NOT NULL,
    customer_email VARCHAR(255) NOT NULL,
    customer_password VARCHAR(255) NOT NULL,
    customer_telephone VARCHAR(255) NOT NULL,
    customer_enabled BIT NOT NULL
);

CREATE TABLE Roles (
    role_id INT PRIMARY KEY,
    role_name VARCHAR(255) NOT NULL,
    role_enabled BIT NOT NULL
);

-----------------------------------------------------------------------------------------------
CREATE TABLE CoffeeShops (
    coffee_shop_id INT PRIMARY KEY,
    coffee_shop_name VARCHAR(255) NOT NULL,
    coffee_shop_email VARCHAR(255) NOT NULL,
    coffee_shop_address VARCHAR(255) NOT NULL,
    coffee_shop_telephone VARCHAR(255) NOT NULL,
    coffee_shop_enabled BIT NOT NULL
);

CREATE TABLE Cats (
    cat_id INT PRIMARY KEY,
    cat_name VARCHAR(255) NOT NULL,
    cat_image VARCHAR(255) NOT NULL,
    cat_enabled BIT NOT NULL
);

CREATE TABLE Tables (
    table_id INT PRIMARY KEY,
    table_name VARCHAR(255) NOT NULL,
    table_capacity INT NOT NULL,
    table_status BIT DEFAULT 'TRUE',
    table_enabled BIT
);

CREATE TABLE Menus (
    menu_id INT PRIMARY KEY,
    menu_name VARCHAR(255) NOT NULL,
    menu_enabled BIT NOT NULL
);

CREATE TABLE MenuItems (
    item_id INT PRIMARY KEY,
    item_name VARCHAR(255) NOT NULL,
    item_price DECIMAL(8, 2) NOT NULL,
    item_enabled BIT NOT NULL
);

CREATE TABLE ShopVouchers (
    voucher_id INT PRIMARY KEY,
    voucher_code VARCHAR(255) NOT NULL,
    voucher_discount DECIMAL(10, 2) NOT NULL,
    voucher_expiry_date DATE NOT NULL,
    voucher_enabled BIT NOT NULL
);

CREATE TABLE UserVouchers (
    user_voucher_id INT PRIMARY KEY,
    voucher_id INT,
    used BIT DEFAULT 'FALSE',
	voucher_enabled BIT NOT NULL
);


CREATE TABLE Bookings (
    booking_id INT PRIMARY KEY,
    booking_code VARCHAR(255) NOT NULL,
    booking_start_time DATETIME NOT NULL,
    booking_end_time DATETIME NOT NULL,
    booking_enabled BIT NOT NULL
);


ALTER TABLE Users
ADD role_id INT,
CONSTRAINT fk_users_roles
    FOREIGN KEY (role_id) 
    REFERENCES Roles(role_id);

-- CoffeeShops and Users relationship
ALTER TABLE Users
ADD coffee_shop_id INT,
 CONSTRAINT fk_users_coffee_shops
    FOREIGN KEY (coffee_shop_id) 
    REFERENCES CoffeeShops(coffee_shop_id);

-- Cats and CoffeeShops relationship
ALTER TABLE Cats
ADD coffee_shop_id INT,
CONSTRAINT fk_cats_coffee_shops
    FOREIGN KEY (coffee_shop_id) 
    REFERENCES CoffeeShops(coffee_shop_id);

-- Tables and CoffeeShops relationship
ALTER TABLE Tables
ADD coffee_shop_id INT,
CONSTRAINT fk_tables_coffee_shops
    FOREIGN KEY (coffee_shop_id) 
    REFERENCES CoffeeShops(coffee_shop_id);

-- Menus and CoffeeShops relationship
ALTER TABLE Menus
ADD coffee_shop_id INT,
CONSTRAINT fk_menus_coffee_shops
    FOREIGN KEY (coffee_shop_id) 
    REFERENCES CoffeeShops(coffee_shop_id);

-- MenuItems and Menus relationship
ALTER TABLE MenuItems
ADD menu_id INT,
CONSTRAINT fk_menu_items_menus
    FOREIGN KEY (menu_id) 
    REFERENCES Menus(menu_id);

-- Vouchers and CoffeeShops relationship
ALTER TABLE ShopVouchers
ADD coffee_shop_id INT,
CONSTRAINT fk_vouchers_coffee_shops
    FOREIGN KEY (coffee_shop_id) 
    REFERENCES CoffeeShops(coffee_shop_id);

-- Vouchers and Users relationship
ALTER TABLE UserVouchers
ADD voucher_id INT,
CONSTRAINT fk_user_vouchers_users
        FOREIGN KEY (user_id) 
        REFERENCES Users(customer_id),
CONSTRAINT fk_user_vouchers_vouchers
        FOREIGN KEY (voucher_id) 
        REFERENCES Vouchers(voucher_id)

-- Bookings and Users relationship
ALTER TABLE Bookings
ADD customer_id INT,
CONSTRAINT fk_bookings_users
    FOREIGN KEY (customer_id) 
    REFERENCES Users(customer_id);