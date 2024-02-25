using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    role_enabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__760965CC523B3956", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    shop_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shop_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    shop_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    shop_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    shop_telephone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    shop_enabled = table.Column<bool>(type: "bit", nullable: true),
                    shop_area = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    shop_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shops__AD08178626C72522", x => x.shop_id);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cat_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    cat_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    cat_enabled = table.Column<bool>(type: "bit", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cats__DD5DDDBDE7D68875", x => x.cat_id);
                    table.ForeignKey(
                        name: "fk_cats_coffee_shops",
                        column: x => x.shop_id,
                        principalTable: "Shops",
                        principalColumn: "shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    menu_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menu_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    menu_enabled = table.Column<bool>(type: "bit", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menus__4CA0FADC85A485C2", x => x.menu_id);
                    table.ForeignKey(
                        name: "fk_menus_coffee_shops",
                        column: x => x.shop_id,
                        principalTable: "Shops",
                        principalColumn: "shop_id");
                });

            migrationBuilder.CreateTable(
                name: "ShopVouchers",
                columns: table => new
                {
                    voucher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    voucher_discount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    voucher_expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    voucher_enabled = table.Column<bool>(type: "bit", nullable: true),
                    coffee_shop_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShopVouc__80B6FFA819D61EEE", x => x.voucher_id);
                    table.ForeignKey(
                        name: "fk_vouchers_coffee_shops",
                        column: x => x.coffee_shop_id,
                        principalTable: "Shops",
                        principalColumn: "shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    table_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    table_capacity = table.Column<int>(type: "int", nullable: true),
                    table_status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    table_enabled = table.Column<bool>(type: "bit", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tables__B21E8F2439121994", x => x.table_id);
                    table.ForeignKey(
                        name: "fk_tables_coffee_shops",
                        column: x => x.shop_id,
                        principalTable: "Shops",
                        principalColumn: "shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    customer_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    customer_password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    customer_telephone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    customer_enabled = table.Column<bool>(type: "bit", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    shop_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CD65CB859EEED23D", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_users_coffee_shops",
                        column: x => x.shop_id,
                        principalTable: "Shops",
                        principalColumn: "shop_id");
                    table.ForeignKey(
                        name: "fk_users_roles",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    item_price = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    item_enabled = table.Column<bool>(type: "bit", nullable: true),
                    menu_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuItem__52020FDD6B2F2778", x => x.item_id);
                    table.ForeignKey(
                        name: "fk_menu_items_menus",
                        column: x => x.menu_id,
                        principalTable: "Menus",
                        principalColumn: "menu_id");
                });

            migrationBuilder.CreateTable(
                name: "UserVouchers",
                columns: table => new
                {
                    user_voucher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    used = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    voucher_enabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserVouc__6A698A79176C981C", x => x.user_voucher_id);
                    table.ForeignKey(
                        name: "fk_user_vouchers_vouchers",
                        column: x => x.voucher_id,
                        principalTable: "ShopVouchers",
                        principalColumn: "voucher_id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    booking_start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    booking_end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    booking_enabled = table.Column<bool>(type: "bit", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__5DE3A5B1ECC64A8B", x => x.booking_id);
                    table.ForeignKey(
                        name: "fk_bookings_users",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "BookingMenuItems",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingM__98C3854C126606DC", x => new { x.booking_id, x.item_id });
                    table.ForeignKey(
                        name: "FK__BookingMe__booki__6B24EA82",
                        column: x => x.booking_id,
                        principalTable: "Bookings",
                        principalColumn: "booking_id");
                    table.ForeignKey(
                        name: "FK__BookingMe__item___6C190EBB",
                        column: x => x.item_id,
                        principalTable: "MenuItems",
                        principalColumn: "item_id");
                });

            migrationBuilder.CreateTable(
                name: "BookingTables",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    table_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingT__06C24D43F35D9D33", x => new { x.booking_id, x.table_id });
                    table.ForeignKey(
                        name: "FK__BookingTa__booki__6754599E",
                        column: x => x.booking_id,
                        principalTable: "Bookings",
                        principalColumn: "booking_id");
                    table.ForeignKey(
                        name: "FK__BookingTa__table__68487DD7",
                        column: x => x.table_id,
                        principalTable: "Tables",
                        principalColumn: "table_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingMenuItems_item_id",
                table: "BookingMenuItems",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customer_id",
                table: "Bookings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTables_table_id",
                table: "BookingTables",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_shop_id",
                table: "Cats",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_menu_id",
                table: "MenuItems",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_shop_id",
                table: "Menus",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShopVouchers_coffee_shop_id",
                table: "ShopVouchers",
                column: "coffee_shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_shop_id",
                table: "Tables",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_shop_id",
                table: "Users",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_voucher_id",
                table: "UserVouchers",
                column: "voucher_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingMenuItems");

            migrationBuilder.DropTable(
                name: "BookingTables");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "UserVouchers");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "ShopVouchers");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
