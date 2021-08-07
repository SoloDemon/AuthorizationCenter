using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Core.Migrations.ApplicationDb
{
    public partial class InitialApplicationDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetRoles",
                    table => new
                    {
                        Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        IsDeleted = table.Column<bool>("tinyint(1)", nullable: false),
                        Description = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        OrderSort = table.Column<int>("int", nullable: false),
                        Enabled = table.Column<bool>("tinyint(1)", nullable: false),
                        CreateId = table.Column<int>("int", nullable: true),
                        CreateBy = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CreateTime = table.Column<DateTime>("datetime(6)", nullable: true),
                        ModifyId = table.Column<int>("int", nullable: true),
                        ModifyBy = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ModifyTime = table.Column<DateTime>("datetime(6)", nullable: true),
                        Name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        NormalizedName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ConcurrencyStamp = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetUsers",
                    table => new
                    {
                        Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        RealName = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Sex = table.Column<byte>("tinyint unsigned", nullable: false),
                        Age = table.Column<int>("int", maxLength: 3, nullable: false),
                        Qicq = table.Column<int>("int", maxLength: 20, nullable: false),
                        Province = table.Column<string>("varchar(20)", maxLength: 20, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        City = table.Column<string>("varchar(20)", maxLength: 20, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Country = table.Column<string>("varchar(20)", maxLength: 20, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Portrait = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        NickName = table.Column<string>("varchar(20)", maxLength: 20, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        IsDelete = table.Column<bool>("tinyint(1)", nullable: false),
                        WeChatOpenId = table.Column<string>("varchar(100)", maxLength: 100, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        UserName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        NormalizedUserName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Email = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        NormalizedEmail = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        EmailConfirmed = table.Column<bool>("tinyint(1)", nullable: false),
                        PasswordHash = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        SecurityStamp = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ConcurrencyStamp = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        PhoneNumber = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        PhoneNumberConfirmed = table.Column<bool>("tinyint(1)", nullable: false),
                        TwoFactorEnabled = table.Column<bool>("tinyint(1)", nullable: false),
                        LockoutEnd = table.Column<DateTimeOffset>("datetime(6)", nullable: true),
                        LockoutEnabled = table.Column<bool>("tinyint(1)", nullable: false),
                        AccessFailedCount = table.Column<int>("int", nullable: false)
                    },
                    constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetRoleClaims",
                    table => new
                    {
                        Id = table.Column<int>("int", nullable: false)
                            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                        RoleId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        ClaimType = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ClaimValue = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                        table.ForeignKey(
                            "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                            x => x.RoleId,
                            "AspNetRoles",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetUserClaims",
                    table => new
                    {
                        Id = table.Column<int>("int", nullable: false)
                            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                        UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        ClaimType = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ClaimValue = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                        table.ForeignKey(
                            "FK_AspNetUserClaims_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetUserLogins",
                    table => new
                    {
                        LoginProvider = table.Column<string>("varchar(255)", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ProviderKey = table.Column<string>("varchar(255)", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        ProviderDisplayName = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
                        table.ForeignKey(
                            "FK_AspNetUserLogins_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetUserRoles",
                    table => new
                    {
                        UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        RoleId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});
                        table.ForeignKey(
                            "FK_AspNetUserRoles_AspNetRoles_RoleId",
                            x => x.RoleId,
                            "AspNetRoles",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_AspNetUserRoles_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                    "AspNetUserTokens",
                    table => new
                    {
                        UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                        LoginProvider = table.Column<string>("varchar(255)", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Name = table.Column<string>("varchar(255)", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Value = table.Column<string>("longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});
                        table.ForeignKey(
                            "FK_AspNetUserTokens_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "AspNetUsers");
        }
    }
}