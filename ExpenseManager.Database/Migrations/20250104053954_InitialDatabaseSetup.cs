using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseManager.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "DefaultExpenseCategories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserExpenseCategories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpenseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExpenseCategories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalSchema: "dbo",
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_UserExpenseCategories_UserExpenseCategoryId",
                        column: x => x.UserExpenseCategoryId,
                        principalSchema: "dbo",
                        principalTable: "UserExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DefaultExpenseCategories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Rent, Mortgage, Property Taxes, Home Insurance", "Housing" },
                    { 2, "Electricity, Gas, Water, Internet, Cable/Streaming", "Utilities" },
                    { 3, "Food, Beverages, Household Supplies", "Groceries" },
                    { 4, "Gas, Public Transport, Car Maintenance, Parking", "Transportation" },
                    { 5, "Restaurants, Cafes, Fast Food", "Dining Out" },
                    { 6, "Movies, Concerts, Sports, Hobbies", "Entertainment" },
                    { 7, "Clothing, Shoes, Accessories, Electronics", "Shopping" },
                    { 8, "Doctor visits, Prescriptions, Medical Supplies", "Healthcare" },
                    { 9, "Hair Salons, Gyms, Cosmetics", "Personal Care" },
                    { 10, "Tuition fees, Books, School Supplies", "Education" },
                    { 11, "Flights, Hotels, Car Rentals, Tours", "Travel" },
                    { 12, "Birthdays, Holidays, Special Occasions", "Gifts" },
                    { 13, "Food, Vet bills, Supplies", "Pets" },
                    { 14, "Streaming services, Gym memberships, Magazines", "Subscriptions" },
                    { 15, "Health insurance, Life insurance, Car insurance", "Insurance" },
                    { 16, "Loans, Credit Cards", "Debt Repayment" },
                    { 17, "Retirement savings, Emergency fund", "Savings" },
                    { 18, "Donations	Non-profit organizations", "Charitable" },
                    { 19, "Repairs, Renovations, Landscaping", "Home Improvement" },
                    { 20, "Miscellaneous expenses not fitting other categories", "Other" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PaymentTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Debit Card" },
                    { 3, "Credit Card" },
                    { 4, "Net Banking" },
                    { 5, "UPI" },
                    { 6, "Check" },
                    { 7, "Other (Unspecified)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentTypeId",
                schema: "dbo",
                table: "Expenses",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserExpenseCategoryId",
                schema: "dbo",
                table: "Expenses",
                column: "UserExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                schema: "dbo",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenseCategories_UserId",
                schema: "dbo",
                table: "UserExpenseCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "dbo",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "dbo",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultExpenseCategories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Expenses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserExpenseCategories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
