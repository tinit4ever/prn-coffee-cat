using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities;

public partial class CoffeeCatContext : DbContext
{
    public CoffeeCatContext()
    {
    }

    public CoffeeCatContext(DbContextOptions<CoffeeCatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Cat> Cats { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopVoucher> ShopVouchers { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("server=(local); database=CoffeeCat; uid=sa; pwd=12345; TrustServerCertificate=true;");*/
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var ConnectionStrings = configuration.GetConnectionString("CoffeeCatDb");
        optionsBuilder.UseSqlServer(ConnectionStrings);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Areas__985D6D6BE01AAF6F");

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.AreaEnabled).HasColumnName("area_enabled");
            entity.Property(e => e.AreaName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("area_name");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Areas)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_areas_coffee_shops");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__5DE3A5B12B6F506A");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("booking_code");
            entity.Property(e => e.BookingEnabled).HasColumnName("booking_enabled");
            entity.Property(e => e.BookingEndTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_end_time");
            entity.Property(e => e.BookingStartTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_start_time");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_bookings_users");

            entity.HasMany(d => d.Items).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingMenuItem",
                    r => r.HasOne<MenuItem>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookingMe__item___6C190EBB"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookingMe__booki__6B24EA82"),
                    j =>
                    {
                        j.HasKey("BookingId", "ItemId").HasName("PK__BookingM__98C3854C2D663866");
                        j.ToTable("BookingMenuItems");
                        j.IndexerProperty<int>("BookingId").HasColumnName("booking_id");
                        j.IndexerProperty<int>("ItemId").HasColumnName("item_id");
                    });

            entity.HasMany(d => d.Tables).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingTable",
                    r => r.HasOne<Table>().WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookingTa__table__68487DD7"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookingTa__booki__6754599E"),
                    j =>
                    {
                        j.HasKey("BookingId", "TableId").HasName("PK__BookingT__06C24D43F07BD482");
                        j.ToTable("BookingTables");
                        j.IndexerProperty<int>("BookingId").HasColumnName("booking_id");
                        j.IndexerProperty<int>("TableId").HasColumnName("table_id");
                    });
        });

        modelBuilder.Entity<Cat>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Cats__DD5DDDBD614E4A17");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.CatEnabled).HasColumnName("cat_enabled");
            entity.Property(e => e.CatImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cat_image");
            entity.Property(e => e.CatName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cat_name");

            entity.HasOne(d => d.Area).WithMany(p => p.Cats)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("fk_cats_coffee_shops");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__MenuItem__52020FDDF5C20E10");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemEnabled).HasColumnName("item_enabled");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("item_price");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_menu_items_shops");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC0B0CD1D7");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleEnabled).HasColumnName("role_enabled");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shops__AD08178659A2965B");

            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.ShopAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_address");
            entity.Property(e => e.ShopEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_email");
            entity.Property(e => e.ShopEnabled).HasColumnName("shop_enabled");
            entity.Property(e => e.ShopImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_image");
            entity.Property(e => e.ShopName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_name");
            entity.Property(e => e.ShopTelephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_telephone");
        });

        modelBuilder.Entity<ShopVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ShopVouc__80B6FFA872E037AD");

            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
            entity.Property(e => e.CoffeeShopId).HasColumnName("coffee_shop_id");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("voucher_code");
            entity.Property(e => e.VoucherDiscount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("voucher_discount");
            entity.Property(e => e.VoucherEnabled).HasColumnName("voucher_enabled");
            entity.Property(e => e.VoucherExpiryDate).HasColumnName("voucher_expiry_date");

            entity.HasOne(d => d.CoffeeShop).WithMany(p => p.ShopVouchers)
                .HasForeignKey(d => d.CoffeeShopId)
                .HasConstraintName("fk_vouchers_coffee_shops");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__Tables__B21E8F24BB963C89");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.TableCapacity).HasColumnName("table_capacity");
            entity.Property(e => e.TableEnabled).HasColumnName("table_enabled");
            entity.Property(e => e.TableName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("table_name");
            entity.Property(e => e.TableStatus)
                .HasDefaultValue(true)
                .HasColumnName("table_status");

            entity.HasOne(d => d.Area).WithMany(p => p.Tables)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("fk_tables_coffee_shops");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Users__CD65CB85E9F1BB80");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerEnabled).HasColumnName("customer_enabled");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer_password");
            entity.Property(e => e.CustomerTelephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer_telephone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_users_roles");

            entity.HasOne(d => d.Shop).WithMany(p => p.Users)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_users_coffee_shops");
        });

        modelBuilder.Entity<UserVoucher>(entity =>
        {
            entity.HasKey(e => e.UserVoucherId).HasName("PK__UserVouc__6A698A7959882F6E");

            entity.Property(e => e.UserVoucherId).HasColumnName("user_voucher_id");
            entity.Property(e => e.Used)
                .HasDefaultValue(false)
                .HasColumnName("used");
            entity.Property(e => e.VoucherEnabled).HasColumnName("voucher_enabled");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("fk_user_vouchers_vouchers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
