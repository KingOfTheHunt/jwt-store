using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtStore.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "VARCHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpiresAt = table.Column<TimeOnly>(type: "TIME", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<TimeOnly>(type: "TIME", nullable: true),
                    PasswordHash = table.Column<string>(type: "NVARCHAR", nullable: false),
                    PasswordRestCode = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    Image = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
