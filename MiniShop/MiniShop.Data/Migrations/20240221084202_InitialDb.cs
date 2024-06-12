using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNumber = table.Column<string>(type: "TEXT", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    User = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderState = table.Column<int>(type: "INTEGER", nullable: false),
                    ConversationId = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')"),
                    Price = table.Column<decimal>(type: "real", nullable: false),
                    Properties = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsHome = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a901197-15c1-4784-a6be-2481266f213f", null, "Süper Yönetici haklarını taşıyan rol", "SuperAdmin", "SUPERADMIN" },
                    { "524532f4-49a0-4a47-bc10-2c969a7f1f50", null, "Yönetici haklarını taşıyan rol", "Admin", "ADMIN" },
                    { "62d3b177-b0c5-4fc5-b9b7-74f8507485c8", null, "Müşteri haklarını taşıyan rol", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8924424f-3137-477b-be84-330356b6ff1c", 0, "Halilpaşa Konağı Cad Kemeraltı Sokak No:4 D:2 Üsküdar", "İstanbul", "9f8f55ee-3a63-46ad-8a5e-7228a0cb4b90", new DateTime(1993, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "aysenumay@gmail.com", true, "Ayşen Umay", "Kadın", "Ergül", false, null, "AYSENUMAY@GMAIL.COM", "AYSENUMAY", "AQAAAAIAAYagAAAAEE4pW1YSmhOjYNW5RUhgCNQRBw6UCqWrDDbcsshGduebgNU6F6jIzY7SfAGXSGI+NQ==", "05387653498", false, "4ffdd3f2-a401-4fff-a19e-f5054f8962ba", false, "aysenumay" },
                    { "8b85d4ca-3604-4bd1-95a4-9591b0752df5", 0, "Halilpaşa Konağı Cad Kemeraltı Sokak No:4 D:2 Üsküdar", "İstanbul", "62dc0ae4-c3cc-4dfb-bc38-f4243b0ebd0a", new DateTime(1993, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "selinmete@gmail.com", true, "Selin", "Kadın", "Mete", false, null, "SELINMETE@GMAIL.COM", "SELINMETE", "AQAAAAIAAYagAAAAEB2HAakcoLRjhDmlmAIUHafmgu78Q+8LB+Ya14tz1OI8qsY0D1VBXpo3jpym6qYS5g==", "05387653498", false, "f65e090d-0ed2-4b26-a2bb-98cecb81501f", false, "selinmete" },
                    { "9ebacf04-0ece-401e-b6a5-2e09668dba3a", 0, "Halilpaşa Konağı Cad Kemeraltı Sokak No:4 D:2 Üsküdar", "İstanbul", "336eb1e7-55bd-4d17-80f9-00adaab095be", new DateTime(1993, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "kemaldurukan@gmail.com", true, "Kemal", "Erkek", "Durukan", false, null, "KEMALDURUKAN@GMAIL.COM", "KEMALDURUKAN", "AQAAAAIAAYagAAAAEOB1QTFai0JVnPfRZHf3+/Vc6aQYpBPwd12peGz8Gsjy8Vb7DVuZ6kSMMN6dVCHFBg==", "05387653498", false, "839ed36b-3e05-4949-aebe-e8755dc22fa2", false, "kemaldurukan" },
                    { "b78ec7be-604a-4882-9023-67e4d7493e8f", 0, "Halilpaşa Konağı Cad Kemeraltı Sokak No:4 D:2 Üsküdar", "İstanbul", "f2ca29db-bd88-4630-93b4-3691c3de69bb", new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "denizfoca@gmail.com", true, "Deniz", "Erkek", "Foça", false, null, "DENIZFOCA@GMAIL.COM", "DENIZFOCA", "AQAAAAIAAYagAAAAENZ3560mEOYIOBx9SZ4pEzvrwXfnGG1cKDDjoQNLNkYXAXrx9G8tSYZwaRVN+3WVJQ==", "05342347689", false, "55d7a694-1109-4190-b96c-0f9a99fe30e8", false, "denizfoca" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedDate", "Name", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5860), "TV kategorisi", true, false, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5875), "Televizyon", "televizyon" },
                    { 2, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5881), "Bilgisayar kategorisi", true, false, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5881), "Bilgisayar", "bilgisayar" },
                    { 3, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5883), "Elektronik Eşya kategorisi", true, false, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5883), "Elektronik Eşya", "elektronik-esya" },
                    { 4, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5884), "Beyaz Eşya kategorisi", true, false, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5884), "Beyaz Eşya", "beyaz-esya" },
                    { 5, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5885), "Telefon kategorisi", true, false, new DateTime(2024, 2, 21, 11, 42, 1, 768, DateTimeKind.Local).AddTicks(5886), "Telefon", "telefon" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "ImageUrl", "IsActive", "IsDeleted", "IsHome", "ModifiedDate", "Name", "Price", "Properties", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1529), "1.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1537), "IPhone 14", 59000m, "Harika bir telefon", "iphone-14" },
                    { 2, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1542), "2.png", true, false, false, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1542), "IPhone 14 Pro", 69000m, "Bu da harika bir telefon", "iphone-14-pro" },
                    { 3, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1544), "3.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1544), "Samsung S23", 49000m, "İdare eder", "samsung-s23" },
                    { 4, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1545), "4.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1546), "Xaomi Note 4", 39000m, "Harika bir telefon", "xaomi-note-4" },
                    { 5, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1547), "5.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1548), "MacBook Air M2", 52000m, "M2nin gücü", "macbook-air-m2" },
                    { 6, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1568), "6.png", true, false, false, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1568), "MacBook Pro M3", 79000m, "16 Gb ram", "macbook-pro-m3" },
                    { 7, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1570), "7.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1570), "Vestel Çamaşır Makinesi X65", 19000m, "Akıllı makine", "vestel-camasir-makinesi-x65" },
                    { 8, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1571), "8.png", true, false, false, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1572), "Arçelik Çamaşır Makinesi A-4", 21000m, "Süper hızlı makine", "arcelik-camasir-makinesi-a-4" },
                    { 9, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1573), "9.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1573), "Hoop Dijital Radyo X96", 1250m, "Klasik radyo keyfi", "hoop-dijital-radyo-x96" },
                    { 10, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1574), "10.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1575), "Xaomi Dijital Baskül", 2100m, "Kilonuzu kontrol edin", "xaomi-dijital-baskul" },
                    { 11, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1576), "11.png", true, false, true, new DateTime(2024, 2, 21, 11, 42, 1, 769, DateTimeKind.Local).AddTicks(1576), "Blaupunkt AC69 Led TV", 9800m, "Android tv", "blaupunkt-ac69-led-tv" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "CreatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b78ec7be-604a-4882-9023-67e4d7493e8f" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8b85d4ca-3604-4bd1-95a4-9591b0752df5" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9ebacf04-0ece-401e-b6a5-2e09668dba3a" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8924424f-3137-477b-be84-330356b6ff1c" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "62d3b177-b0c5-4fc5-b9b7-74f8507485c8", "8924424f-3137-477b-be84-330356b6ff1c" },
                    { "524532f4-49a0-4a47-bc10-2c969a7f1f50", "8b85d4ca-3604-4bd1-95a4-9591b0752df5" },
                    { "524532f4-49a0-4a47-bc10-2c969a7f1f50", "9ebacf04-0ece-401e-b6a5-2e09668dba3a" },
                    { "0a901197-15c1-4784-a6be-2481266f213f", "b78ec7be-604a-4882-9023-67e4d7493e8f" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 5, 1 },
                    { 3, 2 },
                    { 5, 2 },
                    { 3, 3 },
                    { 5, 3 },
                    { 3, 4 },
                    { 5, 4 },
                    { 2, 5 },
                    { 3, 5 },
                    { 2, 6 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 7 },
                    { 3, 8 },
                    { 4, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 4, 10 },
                    { 1, 11 },
                    { 3, 11 },
                    { 4, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
