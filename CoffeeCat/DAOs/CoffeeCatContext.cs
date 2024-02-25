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

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Cat> Cats { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopVoucher> ShopVouchers { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("server=(local); database=CoffeeCat; uid=sa; pwd=12345; TrustServerCertificate=true;");
    */
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var connectionString = configuration.GetConnectionString("CoffeeCatDb");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__5DE3A5B1ECC64A8B");

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
                        j.HasKey("BookingId", "ItemId").HasName("PK__BookingM__98C3854C126606DC");
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
                        j.HasKey("BookingId", "TableId").HasName("PK__BookingT__06C24D43F35D9D33");
                        j.ToTable("BookingTables");
                        j.IndexerProperty<int>("BookingId").HasColumnName("booking_id");
                        j.IndexerProperty<int>("TableId").HasColumnName("table_id");
                    });
        });

        modelBuilder.Entity<Cat>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Cats__DD5DDDBDE7D68875");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.CatEnabled).HasColumnName("cat_enabled");
            entity.Property(e => e.CatImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cat_image");
            entity.Property(e => e.CatName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cat_name");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Cats)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_cats_coffee_shops");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menus__4CA0FADC85A485C2");

            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.MenuEnabled).HasColumnName("menu_enabled");
            entity.Property(e => e.MenuName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("menu_name");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Menus)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_menus_coffee_shops");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__MenuItem__52020FDD6B2F2778");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemEnabled).HasColumnName("item_enabled");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("item_price");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("fk_menu_items_menus");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC523B3956");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleEnabled).HasColumnName("role_enabled");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shops__AD08178626C72522");

            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.ShopAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_address");
            entity.Property(e => e.ShopArea)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_area");
            entity.Property(e => e.ShopEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_email");
            entity.Property(e => e.ShopEnabled).HasColumnName("shop_enabled");
            entity.Property(e => e.ShopName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_name");
            entity.Property(e => e.ShopTelephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_telephone");
         
            entity.Property(e => e.ShopImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shop_image");
        });


    modelBuilder.Entity<ShopVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ShopVouc__80B6FFA819D61EEE");

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
            entity.HasKey(e => e.TableId).HasName("PK__Tables__B21E8F2439121994");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.TableCapacity).HasColumnName("table_capacity");
            entity.Property(e => e.TableEnabled).HasColumnName("table_enabled");
            entity.Property(e => e.TableName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("table_name");
            entity.Property(e => e.TableStatus)
                .HasDefaultValue(true)
                .HasColumnName("table_status");

            entity.HasOne(d => d.Shop).WithMany(p => p.Tables)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("fk_tables_coffee_shops");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Users__CD65CB859EEED23D");

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
            entity.HasKey(e => e.UserVoucherId).HasName("PK__UserVouc__6A698A79176C981C");

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
