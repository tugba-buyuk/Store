using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
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
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ColorName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CouponCodes",
                columns: table => new
                {
                    CouponCodeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CouponCodeName = table.Column<string>(type: "TEXT", nullable: false),
                    CouponCodeDiscount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    StripeCouponId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCodes", x => x.CouponCodeId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    FullAddress = table.Column<string>(type: "TEXT", nullable: false),
                    GiftWrap = table.Column<bool>(type: "INTEGER", nullable: false),
                    Shipped = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrderStatus = table.Column<string>(type: "TEXT", nullable: false),
                    OrderAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CouponCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    ShowCase = table.Column<bool>(type: "INTEGER", nullable: false),
                    MainImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Sizes = table.Column<string>(type: "TEXT", nullable: false),
                    ColorNames = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrls = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    CartLineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.CartLineId);
                    table.ForeignKey(
                        name: "FK_CartLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_CartLine_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrdImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrdImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrdImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52d3c648-ab9a-44d6-9af4-aa10397a3e2d", null, "Editor", "EDITOR" },
                    { "553b8637-0b8b-4167-8cbc-67c2b33a9a50", null, "Admin", "ADMIN" },
                    { "a33a1c02-7d96-42f2-9c10-c52422f921d7", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Book" },
                    { 2, "Electronic" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "CityName", "CountryId" },
                values: new object[,]
                {
                    { 1, "Adana", 1 },
                    { 2, "Adıyaman", 1 },
                    { 3, "Afyonkarahisar", 1 },
                    { 4, "Ağrı", 1 },
                    { 5, "Amasya", 1 },
                    { 6, "Ankara", 1 },
                    { 7, "Antalya", 1 },
                    { 8, "Artvin", 1 },
                    { 9, "Aydın", 1 },
                    { 10, "Balıkesir", 1 },
                    { 11, "Bilecik", 1 },
                    { 12, "Bingöl", 1 },
                    { 13, "Bitlis", 1 },
                    { 14, "Bolu", 1 },
                    { 15, "Burdur", 1 },
                    { 16, "Bursa", 1 },
                    { 17, "Çanakkale", 1 },
                    { 18, "Çankırı", 1 },
                    { 19, "Çorum", 1 },
                    { 20, "Denizli", 1 },
                    { 21, "Diyarbakır", 1 },
                    { 22, "Edirne", 1 },
                    { 23, "Elazığ", 1 },
                    { 24, "Erzincan", 1 },
                    { 25, "Erzurum", 1 },
                    { 26, "Eskişehir", 1 },
                    { 27, "Gaziantep", 1 },
                    { 28, "Giresun", 1 },
                    { 29, "Gümüşhane", 1 },
                    { 30, "Hakkari", 1 },
                    { 31, "Hatay", 1 },
                    { 32, "Isparta", 1 },
                    { 33, "Mersin", 1 },
                    { 34, "İstanbul", 1 },
                    { 35, "İzmir", 1 },
                    { 36, "Kars", 1 },
                    { 37, "Kastamonu", 1 },
                    { 38, "Kayseri", 1 },
                    { 39, "Kırklareli", 1 },
                    { 40, "Kırşehir", 1 },
                    { 41, "Kocaeli", 1 },
                    { 42, "Konya", 1 },
                    { 43, "Kütahya", 1 },
                    { 44, "Malatya", 1 },
                    { 45, "Manisa", 1 },
                    { 46, "Kahramanmaraş", 1 },
                    { 47, "Mardin", 1 },
                    { 48, "Muğla", 1 },
                    { 49, "Muş", 1 },
                    { 50, "Nevşehir", 1 },
                    { 51, "Niğde", 1 },
                    { 52, "Ordu", 1 },
                    { 53, "Rize", 1 },
                    { 54, "Sakarya", 1 },
                    { 55, "Samsun", 1 },
                    { 56, "Siirt", 1 },
                    { 57, "Sinop", 1 },
                    { 58, "Sivas", 1 },
                    { 59, "Tekirdağ", 1 },
                    { 60, "Tokat", 1 },
                    { 61, "Trabzon", 1 },
                    { 62, "Tunceli", 1 },
                    { 63, "Şanlıurfa", 1 },
                    { 64, "Uşak", 1 },
                    { 65, "Van", 1 },
                    { 66, "Yozgat", 1 },
                    { 67, "Zonguldk", 1 },
                    { 68, "Aksaray", 1 },
                    { 69, "Bayburt", 1 },
                    { 70, "Karaman", 1 },
                    { 71, "Kırıkkale", 1 },
                    { 72, "Batman", 1 },
                    { 73, "Şırnak", 1 },
                    { 74, "Bartın", 1 },
                    { 75, "Ardahan", 1 },
                    { 76, "Iğdır", 1 },
                    { 77, "Yalova", 1 },
                    { 78, "Karabük", 1 },
                    { 79, "Kilis", 1 },
                    { 80, "Osmaniye", 1 },
                    { 81, "Düzce", 1 }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "ColorName" },
                values: new object[,]
                {
                    { 1, "Red" },
                    { 2, "Blue" },
                    { 3, "Green" },
                    { 4, "Yellow" },
                    { 5, "Purple" },
                    { 6, "Orange" },
                    { 7, "Pink" },
                    { 8, "Brown" },
                    { 9, "Black" },
                    { 10, "White" },
                    { 11, "Gray" },
                    { 12, "Cyan" },
                    { 13, "Magenta" },
                    { 14, "Lime" },
                    { 15, "Indigo" },
                    { 16, "Violet" },
                    { 17, "Maroon" },
                    { 18, "Olive" },
                    { 19, "Navy" },
                    { 20, "Teal" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[] { 1, "Türkiye" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "ColorNames", "Gender", "ImageUrls", "MainImageUrl", "Price", "ProductName", "ShowCase", "Sizes", "Summary" },
                values: new object[,]
                {
                    { 1, 2, "[]", "", "[]", null, 15000m, "Phone", false, "[]", "" },
                    { 2, 2, "[]", "", "[]", null, 25000m, "Notebook", false, "[]", "" },
                    { 3, 2, "[]", "", "[]", null, 1500m, "Keyboard", false, "[]", "" },
                    { 4, 2, "[]", "", "[]", null, 5000m, "Monitor", false, "[]", "" },
                    { 5, 2, "[]", "", "[]", null, 5000m, "Mouse", false, "[]", "" },
                    { 6, 1, "[]", "", "[]", null, 50m, "Savaş Sanatı", true, "[]", "" },
                    { 7, 1, "[]", "", "[]", null, 60m, "Yüzbaşının Kızı", true, "[]", "" },
                    { 8, 1, "[]", "", "[]", null, 500m, "İtiraflar", true, "[]", "" }
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
                name: "IX_CartLine_OrderId",
                table: "CartLine",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProductId",
                table: "CartLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PrdImage_ProductId",
                table: "PrdImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
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
                name: "CartLine");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "CouponCodes");

            migrationBuilder.DropTable(
                name: "PrdImage");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
