using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Procurement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_product_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    address = table.Column<string>(type: "NVarchar(150)", nullable: false),
                    phone_number = table.Column<string>(type: "NVarchar(14)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "NVarchar(100)", nullable: false),
                    product_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_m_product_category_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "m_product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trans_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_m_user_user_id",
                        column: x => x.user_id,
                        principalTable: "m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "m_product_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    product_code = table.Column<string>(type: "NVarchar(6)", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_price_m_product_product_id",
                        column: x => x.product_id,
                        principalTable: "m_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_product_price_m_user_user_id",
                        column: x => x.user_id,
                        principalTable: "m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    purchase_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_price_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_m_product_price_product_price_id",
                        column: x => x.product_price_id,
                        principalTable: "m_product_price",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_t_purchase_purchase_id",
                        column: x => x.purchase_id,
                        principalTable: "t_purchase",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "m_product_category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("00c644b0-c027-41b0-99f9-5973995d8394"), "Perabotan Rumah Tangga" },
                    { new Guid("05a254b6-0960-4dd1-a1a0-66028ee6990b"), "Otomotif" },
                    { new Guid("19ce01d3-23a0-46cb-85c4-cef8206ddf11"), "Peralatan Sekolah" },
                    { new Guid("745c1ba7-e4c6-4bbb-b6cb-28b082707fb3"), "Elektronik" },
                    { new Guid("a95d824a-2b44-4b9b-a1c3-0afa4da9ba14"), "Lainya" },
                    { new Guid("bdc428c1-01fb-40f6-928d-44a3f258ecc3"), "Makanan/Minuman" },
                    { new Guid("eaf3509a-8d38-479f-840a-b418d6cf7513"), "Pakaian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_product_product_category_id",
                table: "m_product",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_price_product_code",
                table: "m_product_price",
                column: "product_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_product_price_product_id",
                table: "m_product_price",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_price_user_id",
                table: "m_product_price",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_email",
                table: "m_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_user_phone_number",
                table: "m_user",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_user_id",
                table: "t_purchase",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_product_price_id",
                table: "t_purchase_detail",
                column: "product_price_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_purchase_id",
                table: "t_purchase_detail",
                column: "purchase_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_purchase_detail");

            migrationBuilder.DropTable(
                name: "m_product_price");

            migrationBuilder.DropTable(
                name: "t_purchase");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "m_user");

            migrationBuilder.DropTable(
                name: "m_product_category");
        }
    }
}
