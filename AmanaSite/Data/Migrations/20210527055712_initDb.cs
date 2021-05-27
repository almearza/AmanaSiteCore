using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmanaSite.Data.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Baladyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baladyat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MAndFType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAndFType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlidersShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanExpire = table.Column<bool>(type: "bit", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlidersShows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyEducation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyEducation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyEvaluateEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evaluate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyEvaluateEmployee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyGender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyGender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyNationalty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationalty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyNationalty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyReferencesType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyReferencesType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTransactionCompletion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTransactionCompletion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyVisitAvg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitAvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyVisitAvg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AmanaLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmanaLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmanaLinks_LinkType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LinkType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAndF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    MAndFDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAndF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAndF_MAndFType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MAndFType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    NewsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    NewsResource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_NewsTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NewsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmanaServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    LangCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmanaServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmanaServices_ServiceTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SAgeId = table.Column<int>(type: "int", nullable: false),
                    SEducationId = table.Column<int>(type: "int", nullable: false),
                    SEvalEmpId = table.Column<int>(type: "int", nullable: false),
                    SGenderId = table.Column<int>(type: "int", nullable: false),
                    SNatId = table.Column<int>(type: "int", nullable: false),
                    SRefTypeId = table.Column<int>(type: "int", nullable: false),
                    STransTypeId = table.Column<int>(type: "int", nullable: false),
                    STRanCompId = table.Column<int>(type: "int", nullable: false),
                    SVisitAvgId = table.Column<int>(type: "int", nullable: false),
                    NationalityOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyAge_SAgeId",
                        column: x => x.SAgeId,
                        principalTable: "SurveyAge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyEducation_SEducationId",
                        column: x => x.SEducationId,
                        principalTable: "SurveyEducation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyEvaluateEmployee_SEvalEmpId",
                        column: x => x.SEvalEmpId,
                        principalTable: "SurveyEvaluateEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyGender_SGenderId",
                        column: x => x.SGenderId,
                        principalTable: "SurveyGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyNationalty_SNatId",
                        column: x => x.SNatId,
                        principalTable: "SurveyNationalty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyReferencesType_SRefTypeId",
                        column: x => x.SRefTypeId,
                        principalTable: "SurveyReferencesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyTransactionCompletion_STRanCompId",
                        column: x => x.STRanCompId,
                        principalTable: "SurveyTransactionCompletion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyTransactionType_STransTypeId",
                        column: x => x.STransTypeId,
                        principalTable: "SurveyTransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyData_SurveyVisitAvg_SVisitAvgId",
                        column: x => x.SVisitAvgId,
                        principalTable: "SurveyVisitAvg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmanaLinks_TypeId",
                table: "AmanaLinks",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AmanaServices_TypeId",
                table: "AmanaServices",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MAndF_TypeId",
                table: "MAndF",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_News_TypeId",
                table: "News",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SAgeId",
                table: "SurveyData",
                column: "SAgeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SEducationId",
                table: "SurveyData",
                column: "SEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SEvalEmpId",
                table: "SurveyData",
                column: "SEvalEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SGenderId",
                table: "SurveyData",
                column: "SGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SNatId",
                table: "SurveyData",
                column: "SNatId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SRefTypeId",
                table: "SurveyData",
                column: "SRefTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_STRanCompId",
                table: "SurveyData",
                column: "STRanCompId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_STransTypeId",
                table: "SurveyData",
                column: "STransTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyData_SVisitAvgId",
                table: "SurveyData",
                column: "SVisitAvgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmanaLinks");

            migrationBuilder.DropTable(
                name: "AmanaServices");

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
                name: "Baladyat");

            migrationBuilder.DropTable(
                name: "MAndF");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "SlidersShows");

            migrationBuilder.DropTable(
                name: "SurveyData");

            migrationBuilder.DropTable(
                name: "LinkType");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MAndFType");

            migrationBuilder.DropTable(
                name: "NewsTypes");

            migrationBuilder.DropTable(
                name: "SurveyAge");

            migrationBuilder.DropTable(
                name: "SurveyEducation");

            migrationBuilder.DropTable(
                name: "SurveyEvaluateEmployee");

            migrationBuilder.DropTable(
                name: "SurveyGender");

            migrationBuilder.DropTable(
                name: "SurveyNationalty");

            migrationBuilder.DropTable(
                name: "SurveyReferencesType");

            migrationBuilder.DropTable(
                name: "SurveyTransactionCompletion");

            migrationBuilder.DropTable(
                name: "SurveyTransactionType");

            migrationBuilder.DropTable(
                name: "SurveyVisitAvg");
        }
    }
}
