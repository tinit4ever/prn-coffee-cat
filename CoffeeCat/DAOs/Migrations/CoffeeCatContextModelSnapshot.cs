﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAOs.Migrations
{
    [DbContext(typeof(CoffeeCatContext))]
    partial class CoffeeCatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingMenuItem", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int")
                        .HasColumnName("booking_id");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    b.HasKey("BookingId", "ItemId")
                        .HasName("PK__BookingM__98C3854C126606DC");

                    b.HasIndex("ItemId");

                    b.ToTable("BookingMenuItems", (string)null);
                });

            modelBuilder.Entity("BookingTable", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int")
                        .HasColumnName("booking_id");

                    b.Property<int>("TableId")
                        .HasColumnType("int")
                        .HasColumnName("table_id");

                    b.HasKey("BookingId", "TableId")
                        .HasName("PK__BookingT__06C24D43F35D9D33");

                    b.HasIndex("TableId");

                    b.ToTable("BookingTables", (string)null);
                });

            modelBuilder.Entity("Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("booking_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<string>("BookingCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("booking_code");

                    b.Property<bool?>("BookingEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("booking_enabled");

                    b.Property<DateTime?>("BookingEndTime")
                        .HasColumnType("datetime")
                        .HasColumnName("booking_end_time");

                    b.Property<DateTime?>("BookingStartTime")
                        .HasColumnType("datetime")
                        .HasColumnName("booking_start_time");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.HasKey("BookingId")
                        .HasName("PK__Bookings__5DE3A5B1ECC64A8B");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Entities.Cat", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cat_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatId"));

                    b.Property<bool?>("CatEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("cat_enabled");

                    b.Property<string>("CatImage")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("cat_image");

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("cat_name");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    b.HasKey("CatId")
                        .HasName("PK__Cats__DD5DDDBDE7D68875");

                    b.HasIndex("ShopId");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("Entities.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<bool?>("MenuEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("menu_enabled");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("menu_name");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    b.HasKey("MenuId")
                        .HasName("PK__Menus__4CA0FADC85A485C2");

                    b.HasIndex("ShopId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Entities.MenuItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<bool?>("ItemEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("item_enabled");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("item_name");

                    b.Property<decimal?>("ItemPrice")
                        .HasColumnType("decimal(8, 2)")
                        .HasColumnName("item_price");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    b.HasKey("ItemId")
                        .HasName("PK__MenuItem__52020FDD6B2F2778");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool?>("RoleEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("role_enabled");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__760965CC523B3956");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Entities.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopId"));

                    b.Property<string>("ShopAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_address");

                    b.Property<string>("ShopArea")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_area");

                    b.Property<string>("ShopEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_email");

                    b.Property<bool?>("ShopEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("shop_enabled");

                    b.Property<string>("ShopImage")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_image");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_name");

                    b.Property<string>("ShopTelephone")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("shop_telephone");

                    b.HasKey("ShopId")
                        .HasName("PK__Shops__AD08178626C72522");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Entities.ShopVoucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("voucher_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherId"));

                    b.Property<int?>("CoffeeShopId")
                        .HasColumnType("int")
                        .HasColumnName("coffee_shop_id");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("voucher_code");

                    b.Property<decimal?>("VoucherDiscount")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("voucher_discount");

                    b.Property<bool?>("VoucherEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("voucher_enabled");

                    b.Property<DateOnly?>("VoucherExpiryDate")
                        .HasColumnType("date")
                        .HasColumnName("voucher_expiry_date");

                    b.HasKey("VoucherId")
                        .HasName("PK__ShopVouc__80B6FFA819D61EEE");

                    b.HasIndex("CoffeeShopId");

                    b.ToTable("ShopVouchers");
                });

            modelBuilder.Entity("Entities.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("table_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int?>("ShopId")
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    b.Property<int?>("TableCapacity")
                        .HasColumnType("int")
                        .HasColumnName("table_capacity");

                    b.Property<bool?>("TableEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("table_enabled");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("table_name");

                    b.Property<bool?>("TableStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("table_status");

                    b.HasKey("TableId")
                        .HasName("PK__Tables__B21E8F2439121994");

                    b.HasIndex("ShopId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_email");

                    b.Property<bool?>("CustomerEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("customer_enabled");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_name");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_password");

                    b.Property<string>("CustomerTelephone")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_telephone");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    b.HasKey("CustomerId")
                        .HasName("PK__Users__CD65CB859EEED23D");

                    b.HasIndex("RoleId");

                    b.HasIndex("ShopId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.UserVoucher", b =>
                {
                    b.Property<int>("UserVoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_voucher_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserVoucherId"));

                    b.Property<bool?>("Used")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("used");

                    b.Property<bool?>("VoucherEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("voucher_enabled");

                    b.Property<int?>("VoucherId")
                        .HasColumnType("int")
                        .HasColumnName("voucher_id");

                    b.HasKey("UserVoucherId")
                        .HasName("PK__UserVouc__6A698A79176C981C");

                    b.HasIndex("VoucherId");

                    b.ToTable("UserVouchers");
                });

            modelBuilder.Entity("BookingMenuItem", b =>
                {
                    b.HasOne("Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FK__BookingMe__booki__6B24EA82");

                    b.HasOne("Entities.MenuItem", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK__BookingMe__item___6C190EBB");
                });

            modelBuilder.Entity("BookingTable", b =>
                {
                    b.HasOne("Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FK__BookingTa__booki__6754599E");

                    b.HasOne("Entities.Table", null)
                        .WithMany()
                        .HasForeignKey("TableId")
                        .IsRequired()
                        .HasConstraintName("FK__BookingTa__table__68487DD7");
                });

            modelBuilder.Entity("Entities.Booking", b =>
                {
                    b.HasOne("Entities.User", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_bookings_users");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Entities.Cat", b =>
                {
                    b.HasOne("Entities.Shop", "Shop")
                        .WithMany("Cats")
                        .HasForeignKey("ShopId")
                        .HasConstraintName("fk_cats_coffee_shops");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Entities.Menu", b =>
                {
                    b.HasOne("Entities.Shop", "Shop")
                        .WithMany("Menus")
                        .HasForeignKey("ShopId")
                        .HasConstraintName("fk_menus_coffee_shops");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Entities.MenuItem", b =>
                {
                    b.HasOne("Entities.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("fk_menu_items_menus");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Entities.ShopVoucher", b =>
                {
                    b.HasOne("Entities.Shop", "CoffeeShop")
                        .WithMany("ShopVouchers")
                        .HasForeignKey("CoffeeShopId")
                        .HasConstraintName("fk_vouchers_coffee_shops");

                    b.Navigation("CoffeeShop");
                });

            modelBuilder.Entity("Entities.Table", b =>
                {
                    b.HasOne("Entities.Shop", "Shop")
                        .WithMany("Tables")
                        .HasForeignKey("ShopId")
                        .HasConstraintName("fk_tables_coffee_shops");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.HasOne("Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_users_roles");

                    b.HasOne("Entities.Shop", "Shop")
                        .WithMany("Users")
                        .HasForeignKey("ShopId")
                        .HasConstraintName("fk_users_coffee_shops");

                    b.Navigation("Role");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Entities.UserVoucher", b =>
                {
                    b.HasOne("Entities.ShopVoucher", "Voucher")
                        .WithMany("UserVouchers")
                        .HasForeignKey("VoucherId")
                        .HasConstraintName("fk_user_vouchers_vouchers");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Entities.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Entities.Shop", b =>
                {
                    b.Navigation("Cats");

                    b.Navigation("Menus");

                    b.Navigation("ShopVouchers");

                    b.Navigation("Tables");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Entities.ShopVoucher", b =>
                {
                    b.Navigation("UserVouchers");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
