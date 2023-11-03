using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PLDataLayer.Migrations
{
    public partial class base0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aboutes",
                columns: table => new
                {
                    About_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About_Title = table.Column<string>(maxLength: 50, nullable: false),
                    About_Text = table.Column<string>(nullable: false),
                    About_Image = table.Column<string>(maxLength: 50, nullable: true),
                    About_HImage = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aboutes", x => x.About_Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleGroups",
                columns: table => new
                {
                    AG_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AG_Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleGroups", x => x.AG_Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    CI_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CI_Phone1 = table.Column<string>(maxLength: 10, nullable: false),
                    CI_Phone2 = table.Column<string>(maxLength: 10, nullable: true),
                    CI_Fax1 = table.Column<string>(maxLength: 10, nullable: false),
                    CI_Fax2 = table.Column<string>(maxLength: 10, nullable: true),
                    CI_Email1 = table.Column<string>(maxLength: 50, nullable: true),
                    CI_Email2 = table.Column<string>(maxLength: 50, nullable: true),
                    CI_Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    CI_Address2 = table.Column<string>(maxLength: 200, nullable: true),
                    CI_ContactTime = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.CI_Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    CM_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CM_FullName = table.Column<string>(maxLength: 50, nullable: false),
                    CM_Subject = table.Column<string>(maxLength: 50, nullable: false),
                    CM_Email = table.Column<string>(maxLength: 100, nullable: true),
                    CM_Message = table.Column<string>(nullable: false),
                    CM_Date = table.Column<DateTime>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.CM_Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseGroups",
                columns: table => new
                {
                    CourseGroup_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseGroup_Title = table.Column<string>(maxLength: 50, nullable: true),
                    CourseGroup_Image = table.Column<string>(maxLength: 100, nullable: true),
                    CourseGroup_Comment = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.CourseGroup_Id);
                    table.ForeignKey(
                        name: "FK_CourseGroups_CourseGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CourseGroups",
                        principalColumn: "CourseGroup_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    CourseLevel_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseLevel_Title = table.Column<string>(maxLength: 50, nullable: false),
                    CourseLevel_HasUploadFile = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => x.CourseLevel_Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatuses",
                columns: table => new
                {
                    CourseStatus_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseStatus_Title = table.Column<string>(maxLength: 50, nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatuses", x => x.CourseStatus_Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTypeofMeasurments",
                columns: table => new
                {
                    CTM_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTM_Title = table.Column<string>(maxLength: 100, nullable: false),
                    CTM_Comment = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTypeofMeasurments", x => x.CTM_Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailBanks",
                columns: table => new
                {
                    EBId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EBEmail = table.Column<string>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailBanks", x => x.EBId);
                });

            migrationBuilder.CreateTable(
                name: "GalleryGroups",
                columns: table => new
                {
                    GG_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GG_Title = table.Column<string>(maxLength: 50, nullable: false),
                    GG_IntroImage = table.Column<string>(maxLength: 50, nullable: false),
                    GG_Comment = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryGroups", x => x.GG_Id);
                });

            migrationBuilder.CreateTable(
                name: "Headers",
                columns: table => new
                {
                    Header_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header_Title = table.Column<string>(maxLength: 50, nullable: false),
                    Header_Text = table.Column<string>(maxLength: 200, nullable: true),
                    Header_About = table.Column<string>(maxLength: 200, nullable: false),
                    Header_Btn1Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Btn1Link = table.Column<string>(maxLength: 100, nullable: true),
                    Header_Btn2Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Btn2Link = table.Column<string>(maxLength: 100, nullable: true),
                    Header_Social1Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social1Class = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social1Link = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social2Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social2Class = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social2Link = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social3Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social3Class = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social3Link = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social4Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social4Class = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social4Link = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social5Text = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social5Class = table.Column<string>(maxLength: 50, nullable: true),
                    Header_Social5Link = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headers", x => x.Header_id);
                });

            migrationBuilder.CreateTable(
                name: "InstaPosts",
                columns: table => new
                {
                    InstaPostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstaPostTitle = table.Column<string>(maxLength: 50, nullable: false),
                    InstaPostText = table.Column<string>(nullable: false),
                    InstaPostImage = table.Column<string>(maxLength: 50, nullable: true),
                    InstaPostLink = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Photographer = table.Column<string>(maxLength: 50, nullable: true),
                    InstaPostDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaPosts", x => x.InstaPostId);
                });

            migrationBuilder.CreateTable(
                name: "NewsGroups",
                columns: table => new
                {
                    NewsGroup_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsGroup_Title = table.Column<string>(maxLength: 50, nullable: false),
                    NewsgGroup_Comment = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsGroups", x => x.NewsGroup_Id);
                });

            migrationBuilder.CreateTable(
                name: "PackInfos",
                columns: table => new
                {
                    PackInfo_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackInfo_PackName = table.Column<string>(maxLength: 50, nullable: false),
                    PackInfo_Title = table.Column<string>(maxLength: 200, nullable: false),
                    PackInfo_Comment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackInfos", x => x.PackInfo_Id);
                });

            migrationBuilder.CreateTable(
                name: "PageInfos",
                columns: table => new
                {
                    PInfo_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PInfo_PageName = table.Column<string>(maxLength: 100, nullable: false),
                    PInfo_Title = table.Column<string>(maxLength: 200, nullable: false),
                    PInfo_Comment = table.Column<string>(nullable: true),
                    PInfo_Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageInfos", x => x.PInfo_Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Publisher_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher_Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Publisher_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 30, nullable: false),
                    RoleTitle = table.Column<string>(maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Update = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Remove = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Separators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    BgImage = table.Column<string>(maxLength: 50, nullable: true),
                    FileLink = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Separators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteFAQs",
                columns: table => new
                {
                    SiteFAQ_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteFAQ_Date = table.Column<DateTime>(nullable: true),
                    SiteFAQ_Name = table.Column<string>(maxLength: 100, nullable: false),
                    SiteFAQ_Email = table.Column<string>(maxLength: 100, nullable: true),
                    SiteFAQ_Subject = table.Column<string>(maxLength: 100, nullable: false),
                    SiteFAQ_Question = table.Column<string>(maxLength: 500, nullable: true),
                    SiteFAQ_Reply = table.Column<string>(maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteFAQs", x => x.SiteFAQ_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    SliderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderImage = table.Column<string>(maxLength: 100, nullable: true),
                    SliderText1 = table.Column<string>(maxLength: 200, nullable: true),
                    SliderText1Class = table.Column<string>(maxLength: 100, nullable: true),
                    SliderText2 = table.Column<string>(maxLength: 200, nullable: true),
                    SliderText2Class = table.Column<string>(maxLength: 100, nullable: true),
                    SliderButton1Text = table.Column<string>(maxLength: 100, nullable: true),
                    SliderButton1Link = table.Column<string>(maxLength: 100, nullable: true),
                    SliderButton2Text = table.Column<string>(maxLength: 100, nullable: true),
                    SliderButton2Link = table.Column<string>(maxLength: 100, nullable: true),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.SliderId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(maxLength: 30, nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "StaticDiscounts",
                columns: table => new
                {
                    SD_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SD_Code = table.Column<string>(maxLength: 50, nullable: false),
                    SD_IsGeneral = table.Column<bool>(nullable: false),
                    SD_StartDate = table.Column<DateTime>(nullable: true),
                    SD_EndDate = table.Column<DateTime>(nullable: true),
                    SD_UsableCount = table.Column<int>(nullable: true),
                    SD_Remain = table.Column<int>(nullable: true),
                    SD_Used = table.Column<int>(nullable: false),
                    SD_MinCourseValue = table.Column<int>(nullable: true),
                    SD_MaxCourseValue = table.Column<int>(nullable: true),
                    SD_Percent = table.Column<float>(nullable: false),
                    SD_IsActive = table.Column<bool>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    SD_Comment = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Op_Creator_urID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticDiscounts", x => x.SD_Id);
                });

            migrationBuilder.CreateTable(
                name: "SteppedDiscountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteppedDiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    TL_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TL_Title = table.Column<string>(maxLength: 50, nullable: false),
                    TL_Text = table.Column<string>(maxLength: 200, nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.TL_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    UserLevel_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserLevel_Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Update = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Remove = table.Column<string>(maxLength: 50, nullable: true),
                    UserLevel_HasUpload = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => x.UserLevel_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    UQ_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UQ_FullName = table.Column<string>(maxLength: 50, nullable: false),
                    UQ_Email = table.Column<string>(maxLength: 50, nullable: false),
                    UQ_Subject = table.Column<string>(maxLength: 50, nullable: false),
                    UQ_Question = table.Column<string>(maxLength: 500, nullable: false),
                    UQ_Reply = table.Column<string>(maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UQ_Date = table.Column<DateTime>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestions", x => x.UQ_Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Article_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article_Code = table.Column<string>(maxLength: 50, nullable: false),
                    Article_Title = table.Column<string>(maxLength: 50, nullable: false),
                    Article_Date = table.Column<DateTime>(nullable: false),
                    Article_Abstract = table.Column<string>(maxLength: 1000, nullable: false),
                    Article_Text = table.Column<string>(nullable: false),
                    Article_Image = table.Column<string>(maxLength: 50, nullable: true),
                    Article_txtFile = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    AG_Id = table.Column<int>(nullable: false),
                    ArticleGroupAG_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Article_Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleGroups_ArticleGroupAG_Id",
                        column: x => x.ArticleGroupAG_Id,
                        principalTable: "ArticleGroups",
                        principalColumn: "AG_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Title = table.Column<string>(maxLength: 50, nullable: false),
                    Course_Duration = table.Column<string>(maxLength: 100, nullable: false),
                    Course_StartDate = table.Column<DateTime>(nullable: false),
                    Course_EndDate = table.Column<DateTime>(nullable: false),
                    Course_StartTime = table.Column<string>(maxLength: 100, nullable: true),
                    Course_EndTime = table.Column<string>(maxLength: 100, nullable: true),
                    Course_Comment = table.Column<string>(nullable: true),
                    Course_Image = table.Column<string>(maxLength: 50, nullable: true),
                    Course_EndDateRegistration = table.Column<DateTime>(nullable: false),
                    Course_Capacity = table.Column<int>(nullable: false),
                    Course_Fee = table.Column<int>(nullable: false),
                    Course_about = table.Column<string>(maxLength: 200, nullable: true),
                    Course_abstract = table.Column<string>(maxLength: 500, nullable: true),
                    Course_Tags = table.Column<string>(maxLength: 500, nullable: true),
                    Course_CreateDate = table.Column<DateTime>(nullable: false),
                    Course_UpdateDate = table.Column<DateTime>(nullable: true),
                    Course_IsActive = table.Column<bool>(nullable: false),
                    Course_Visits = table.Column<int>(nullable: false),
                    CourseStatus_Id = table.Column<int>(nullable: false),
                    CourseGroup_Id = table.Column<int>(nullable: false),
                    CourseLevel_Id = table.Column<int>(nullable: false),
                    CTM_Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    CourseLink = table.Column<string>(maxLength: 100, nullable: true),
                    SteppedDiscountCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Course_Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTypeofMeasurments_CTM_Id",
                        column: x => x.CTM_Id,
                        principalTable: "CourseTypeofMeasurments",
                        principalColumn: "CTM_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseGroups_CourseGroup_Id",
                        column: x => x.CourseGroup_Id,
                        principalTable: "CourseGroups",
                        principalColumn: "CourseGroup_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseLevels_CourseLevel_Id",
                        column: x => x.CourseLevel_Id,
                        principalTable: "CourseLevels",
                        principalColumn: "CourseLevel_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseStatuses_CourseStatus_Id",
                        column: x => x.CourseStatus_Id,
                        principalTable: "CourseStatuses",
                        principalColumn: "CourseStatus_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Gallery_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gallery_Title = table.Column<string>(maxLength: 50, nullable: false),
                    Gallery_File = table.Column<string>(maxLength: 50, nullable: false),
                    Gallery_Comment = table.Column<string>(maxLength: 300, nullable: true),
                    Gallery_Date = table.Column<DateTime>(nullable: false),
                    GG_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Gallery_Id);
                    table.ForeignKey(
                        name: "FK_Galleries_GalleryGroups_GG_Id",
                        column: x => x.GG_Id,
                        principalTable: "GalleryGroups",
                        principalColumn: "GG_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    News_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    News_Code = table.Column<string>(maxLength: 6, nullable: false),
                    News_Date = table.Column<DateTime>(nullable: false),
                    News_Title = table.Column<string>(maxLength: 100, nullable: false),
                    News_Abstract = table.Column<string>(maxLength: 2000, nullable: false),
                    News_Text = table.Column<string>(nullable: false),
                    News_Tags = table.Column<string>(maxLength: 300, nullable: true),
                    News_Image = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NewsGroup_Id = table.Column<int>(nullable: false),
                    Publisher_Id = table.Column<int>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.News_Id);
                    table.ForeignKey(
                        name: "FK_News_NewsGroups_NewsGroup_Id",
                        column: x => x.NewsGroup_Id,
                        principalTable: "NewsGroups",
                        principalColumn: "NewsGroup_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Publishers_Publisher_Id",
                        column: x => x.Publisher_Id,
                        principalTable: "Publishers",
                        principalColumn: "Publisher_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RP_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(maxLength: 50, nullable: false),
                    StateId = table.Column<int>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                    table.ForeignKey(
                        name: "FK_Counties_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteppedDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 200, nullable: false),
                    RegDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteppedDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SteppedDiscounts_SteppedDiscountTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SteppedDiscountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimelineComponents",
                columns: table => new
                {
                    TC_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC_Date = table.Column<string>(maxLength: 50, nullable: false),
                    TC_Title = table.Column<string>(maxLength: 50, nullable: false),
                    TC_Text = table.Column<string>(maxLength: 300, nullable: false),
                    TC_Image = table.Column<string>(maxLength: 50, nullable: true),
                    TC_Rank = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    TL_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimelineComponents", x => x.TC_Id);
                    table.ForeignKey(
                        name: "FK_TimelineComponents_Timelines_TL_Id",
                        column: x => x.TL_Id,
                        principalTable: "Timelines",
                        principalColumn: "TL_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEpisodes",
                columns: table => new
                {
                    CourseEpisode_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Id = table.Column<int>(nullable: false),
                    CourseEpisode_Title = table.Column<string>(maxLength: 400, nullable: false),
                    CourseEpisode_Time = table.Column<TimeSpan>(nullable: false),
                    CourseEpisode_FileName = table.Column<string>(nullable: true),
                    IsFree = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    Course_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEpisodes", x => x.CourseEpisode_Id);
                    table.ForeignKey(
                        name: "FK_CourseEpisodes_Courses_Course_Id1",
                        column: x => x.Course_Id1,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseFiles",
                columns: table => new
                {
                    CF_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CF_File = table.Column<string>(maxLength: 100, nullable: true),
                    CF_Title = table.Column<string>(maxLength: 100, nullable: false),
                    CF_Comment = table.Column<string>(maxLength: 300, nullable: true),
                    CF_Type = table.Column<string>(maxLength: 50, nullable: true),
                    CF_Date = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CF_IsFree = table.Column<bool>(nullable: false),
                    CF_Download = table.Column<int>(nullable: false),
                    CF_DownloadersPhone = table.Column<string>(nullable: true),
                    CF_DownloadersEmail = table.Column<string>(nullable: true),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    Course_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFiles", x => x.CF_Id);
                    table.ForeignKey(
                        name: "FK_CourseFiles_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStaticDiscounts",
                columns: table => new
                {
                    CSD_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Id = table.Column<int>(nullable: false),
                    SD_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStaticDiscounts", x => x.CSD_Id);
                    table.ForeignKey(
                        name: "FK_CourseStaticDiscounts_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStaticDiscounts_StaticDiscounts_SD_Id",
                        column: x => x.SD_Id,
                        principalTable: "StaticDiscounts",
                        principalColumn: "SD_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFiles",
                columns: table => new
                {
                    NF_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NF_File = table.Column<string>(maxLength: 100, nullable: true),
                    NF_Comment = table.Column<string>(maxLength: 300, nullable: true),
                    NF_Type = table.Column<string>(maxLength: 50, nullable: true),
                    NF_Date = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true),
                    News_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFiles", x => x.NF_Id);
                    table.ForeignKey(
                        name: "FK_NewsFiles_News_News_Id",
                        column: x => x.News_Id,
                        principalTable: "News",
                        principalColumn: "News_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(maxLength: 200, nullable: true),
                    UserFamily = table.Column<string>(maxLength: 200, nullable: true),
                    UserNC = table.Column<string>(maxLength: 10, nullable: true),
                    UserOrgCode = table.Column<string>(nullable: true),
                    UserBirthDate = table.Column<string>(maxLength: 50, nullable: true),
                    UserFatherName = table.Column<string>(maxLength: 50, nullable: true),
                    UserRestAddress = table.Column<string>(maxLength: 200, nullable: true),
                    UserAvatar = table.Column<string>(maxLength: 200, nullable: true),
                    LevelOfEducation = table.Column<string>(maxLength: 50, nullable: true),
                    EducationFile = table.Column<string>(maxLength: 50, nullable: true),
                    UserUniversity = table.Column<string>(maxLength: 50, nullable: true),
                    UserYearofGraduataion = table.Column<int>(nullable: false),
                    UserBiography = table.Column<string>(maxLength: 2000, nullable: true),
                    UserLabel = table.Column<string>(maxLength: 50, nullable: true),
                    UserDescription = table.Column<string>(maxLength: 200, nullable: true),
                    UserSex = table.Column<string>(maxLength: 50, nullable: true),
                    UserCellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CellphoneConfirmCode = table.Column<string>(maxLength: 100, nullable: true),
                    UserCellPhoneConfirmed = table.Column<bool>(nullable: false),
                    UserEmail = table.Column<string>(maxLength: 50, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserActiveCode = table.Column<string>(maxLength: 100, nullable: true),
                    UserIsActive = table.Column<bool>(nullable: false),
                    UserPassword = table.Column<string>(maxLength: 50, nullable: true),
                    LastPassword = table.Column<string>(maxLength: 50, nullable: true),
                    UserRegisteredDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChangedPass = table.Column<bool>(nullable: false),
                    UserNCFile = table.Column<string>(maxLength: 50, nullable: true),
                    UserContractFile = table.Column<string>(maxLength: 50, nullable: true),
                    Sky_userId = table.Column<int>(nullable: true),
                    CountyId = table.Column<int>(nullable: true),
                    UserLevel_Id = table.Column<int>(nullable: true),
                    OP_Create = table.Column<string>(nullable: true),
                    OP_Update = table.Column<string>(nullable: true),
                    OP_Remove = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserLevels_UserLevel_Id",
                        column: x => x.UserLevel_Id,
                        principalTable: "UserLevels",
                        principalColumn: "UserLevel_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SteppedDiscountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StId = table.Column<int>(nullable: false),
                    FromPerson = table.Column<int>(nullable: true),
                    ToPerson = table.Column<int>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    Percent = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteppedDiscountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SteppedDiscountDetails_SteppedDiscounts_StId",
                        column: x => x.StId,
                        principalTable: "SteppedDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    URId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserRoleCode = table.Column<int>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Update = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Remove = table.Column<string>(maxLength: 50, nullable: true),
                    RoomLink = table.Column<string>(maxLength: 200, nullable: true),
                    room_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.URId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUsers",
                columns: table => new
                {
                    CU_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Id = table.Column<int>(nullable: false),
                    CU_CreateDate = table.Column<DateTime>(nullable: false),
                    StaticDiscountCode = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OP_Create = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Update = table.Column<string>(maxLength: 50, nullable: true),
                    OP_Remove = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PayValue = table.Column<int>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    DisValue = table.Column<int>(nullable: false),
                    URId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUsers", x => x.CU_Id);
                    table.ForeignKey(
                        name: "FK_CourseUsers_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUsers_UserRoles_URId",
                        column: x => x.URId,
                        principalTable: "UserRoles",
                        principalColumn: "URId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    UserMessages_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMessages_Date = table.Column<DateTime>(nullable: false),
                    UserMessages_Title = table.Column<string>(maxLength: 50, nullable: false),
                    UserMessages_Question = table.Column<string>(maxLength: 200, nullable: true),
                    UserMessages_Response = table.Column<string>(nullable: true),
                    SenderId = table.Column<int>(nullable: false),
                    ReciverId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    UserRoleURId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.UserMessages_Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_UserMessages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UserMessages",
                        principalColumn: "UserMessages_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessages_UserRoles_UserRoleURId",
                        column: x => x.UserRoleURId,
                        principalTable: "UserRoles",
                        principalColumn: "URId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleStaticDiscounts",
                columns: table => new
                {
                    URSD_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URId = table.Column<int>(nullable: false),
                    SD_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleStaticDiscounts", x => x.URSD_Id);
                    table.ForeignKey(
                        name: "FK_UserRoleStaticDiscounts_StaticDiscounts_SD_Id",
                        column: x => x.SD_Id,
                        principalTable: "StaticDiscounts",
                        principalColumn: "SD_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleStaticDiscounts_UserRoles_URId",
                        column: x => x.URId,
                        principalTable: "UserRoles",
                        principalColumn: "URId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseLevels",
                columns: new[] { "CourseLevel_Id", "CourseLevel_HasUploadFile", "CourseLevel_Title", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update" },
                values: new object[,]
                {
                    { 1, false, "مقدماتی", false, null, null, null },
                    { 2, false, "متوسط", false, null, null, null },
                    { 3, false, "پیشرفته", false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseStatuses",
                columns: new[] { "CourseStatus_Id", "CourseStatus_Title", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update" },
                values: new object[,]
                {
                    { 1, "حضوری", false, null, null, null },
                    { 2, "غیر حضوری", false, null, null, null },
                    { 3, "دانلودی", false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseTypeofMeasurments",
                columns: new[] { "CTM_Id", "CTM_Comment", "CTM_Title", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update" },
                values: new object[,]
                {
                    { 2, null, "آزمون تشریحی", false, null, null, null },
                    { 6, null, "بدون آزمون", false, null, null, null },
                    { 5, null, "مصاحبه فنی", false, null, null, null },
                    { 4, null, "آزمون عملی", false, null, null, null },
                    { 3, null, "آزمون شفاهی", false, null, null, null },
                    { 1, null, "آزمون چهار گزینه ای", false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "OP_Create", "OP_Remove", "OP_Update", "ParentId", "PermissionTitle" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "مدیریت" },
                    { 2, null, null, null, null, "مشاهده دوره ها" },
                    { 3, null, null, null, null, "مشاهده فراگیران" },
                    { 4, null, null, null, null, "مشاهده اساتید" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Publisher_Id", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "Publisher_Title" },
                values: new object[] { 1, false, null, null, null, "روابط عمومی" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "RoleName", "RoleTitle" },
                values: new object[,]
                {
                    { 1, false, null, null, null, "Admin", "مدیر سایت" },
                    { 2, false, null, null, null, "Operator", "اپراتور" },
                    { 3, false, null, null, null, "Teacher", "مدرس" },
                    { 4, false, null, null, null, "Student", "فراگیر" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateName" },
                values: new object[,]
                {
                    { 31, false, null, null, null, "یزد" },
                    { 30, false, null, null, null, "همدان" },
                    { 29, false, null, null, null, "هرمزگان" },
                    { 28, false, null, null, null, "مرکزی" },
                    { 27, false, null, null, null, "مازندران" },
                    { 26, false, null, null, null, "لرستان" },
                    { 24, false, null, null, null, "گلستان" },
                    { 25, false, null, null, null, "گیلان" },
                    { 8, false, null, null, null, "تهران" },
                    { 1, false, null, null, null, "آذربایجان شرقی" },
                    { 2, false, null, null, null, "آذربایجان غربی" },
                    { 3, false, null, null, null, "اردبیل" },
                    { 4, false, null, null, null, "اصفهان" },
                    { 5, false, null, null, null, "البرز" },
                    { 6, false, null, null, null, "ایلام" },
                    { 7, false, null, null, null, "بوشهر" },
                    { 23, false, null, null, null, "کهکیلویه و بویراحمد" },
                    { 9, false, null, null, null, "چهار محال و بختیاری" },
                    { 10, false, null, null, null, "خراسان جنوبی" },
                    { 11, false, null, null, null, "خراسان رضوی" },
                    { 13, false, null, null, null, "خوزستان" },
                    { 14, false, null, null, null, "زنجان" },
                    { 15, false, null, null, null, "سمنان" },
                    { 16, false, null, null, null, "سیستان و بلوچستان" },
                    { 17, false, null, null, null, "فارس" },
                    { 18, false, null, null, null, "قزوین" },
                    { 19, false, null, null, null, "قم" },
                    { 20, false, null, null, null, "کردستان" },
                    { 21, false, null, null, null, "کرمان" },
                    { 22, false, null, null, null, "کرمانشاه" },
                    { 12, false, null, null, null, "خراسان شمالی" }
                });

            migrationBuilder.InsertData(
                table: "UserLevels",
                columns: new[] { "UserLevel_Id", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "UserLevel_HasUpload", "UserLevel_Title" },
                values: new object[,]
                {
                    { 5, false, null, null, null, true, "مدیر فروش" },
                    { 6, false, null, null, null, true, "مدیر آموزش" },
                    { 4, false, null, null, null, true, "مشاور هادی" },
                    { 7, false, null, null, null, true, "مدیر توسعه" },
                    { 2, false, null, null, null, false, "بازاریاب" },
                    { 1, false, null, null, null, false, "متقاضی آزاد" },
                    { 3, false, null, null, null, true, "نماینده" },
                    { 8, false, null, null, null, true, "مدیر ارشد" }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateId" },
                values: new object[,]
                {
                    { 705, "زنجان", false, null, null, null, 14 },
                    { 906, "سنندج", false, null, null, null, 20 },
                    { 905, "دلبران", false, null, null, null, 20 },
                    { 904, "زرینه", false, null, null, null, 20 },
                    { 903, "بوئین سفلی", false, null, null, null, 20 },
                    { 902, "سروآباد", false, null, null, null, 20 },
                    { 901, "توپ آغاج", false, null, null, null, 20 },
                    { 900, "قروه", false, null, null, null, 20 },
                    { 899, "دستجرد", false, null, null, null, 19 },
                    { 898, "قنوات", false, null, null, null, 19 },
                    { 897, "جعفریه", false, null, null, null, 19 },
                    { 896, "سلفچگان", false, null, null, null, 19 },
                    { 895, "قم", false, null, null, null, 19 },
                    { 894, "کهک", false, null, null, null, 19 },
                    { 893, "تاکستان", false, null, null, null, 18 },
                    { 892, "قزوین", false, null, null, null, 18 },
                    { 907, "یاسوکند", false, null, null, null, 20 },
                    { 891, "آبیک", false, null, null, null, 18 },
                    { 908, "موچش", false, null, null, null, 20 },
                    { 910, "مریوان", false, null, null, null, 20 },
                    { 925, "پیرتاج", false, null, null, null, 20 },
                    { 924, "بلبان آباد", false, null, null, null, 20 },
                    { 923, "سقز", false, null, null, null, 20 },
                    { 922, "دزج", false, null, null, null, 20 },
                    { 921, "کانی دینار", false, null, null, null, 20 },
                    { 920, "کانی سور", false, null, null, null, 20 },
                    { 919, "اورامان تخت", false, null, null, null, 20 },
                    { 918, "بیجار", false, null, null, null, 20 },
                    { 917, "شویشه", false, null, null, null, 20 },
                    { 916, "برده رشه", false, null, null, null, 20 },
                    { 915, "دیواندره", false, null, null, null, 20 },
                    { 914, "بابارشانی", false, null, null, null, 20 },
                    { 913, "دهگلان", false, null, null, null, 20 },
                    { 912, "صاحب", false, null, null, null, 20 },
                    { 911, "سریش آباد", false, null, null, null, 20 },
                    { 909, "بانه", false, null, null, null, 20 },
                    { 890, "دانسفهان", false, null, null, null, 18 },
                    { 889, "آوج", false, null, null, null, 18 },
                    { 888, "اسفرورین", false, null, null, null, 18 },
                    { 867, "آباده", false, null, null, null, 17 },
                    { 866, "هماشهر", false, null, null, null, 17 },
                    { 865, "اسیر", false, null, null, null, 17 },
                    { 864, "لار", false, null, null, null, 17 },
                    { 863, "کره ای", false, null, null, null, 17 },
                    { 862, "رونیز", false, null, null, null, 17 },
                    { 861, "داریان", false, null, null, null, 17 },
                    { 860, "مادر سلیمان", false, null, null, null, 17 },
                    { 859, "عمادده", false, null, null, null, 17 },
                    { 858, "اقلید", false, null, null, null, 17 },
                    { 857, "صفاشهر", false, null, null, null, 17 },
                    { 856, "میانشهر", false, null, null, null, 17 },
                    { 855, "مصیری", false, null, null, null, 17 },
                    { 854, "خانمین", false, null, null, null, 17 },
                    { 853, "قطب آباد", false, null, null, null, 17 },
                    { 868, "لامرد", false, null, null, null, 17 },
                    { 869, "سگزآباد", false, null, null, null, 18 },
                    { 870, "بیدستان", false, null, null, null, 18 },
                    { 871, "کوهین", false, null, null, null, 18 },
                    { 887, "معلم کلایه", false, null, null, null, 18 },
                    { 886, "محمود آباد نمونه", false, null, null, null, 18 },
                    { 885, "محمدیه", false, null, null, null, 18 },
                    { 884, "بوئین زهرا", false, null, null, null, 18 },
                    { 883, "ضیاد آباد", false, null, null, null, 18 },
                    { 882, "سیردان", false, null, null, null, 18 },
                    { 881, "خاکعلی", false, null, null, null, 18 },
                    { 926, "کامیاران", false, null, null, null, 20 },
                    { 880, "الوند", false, null, null, null, 18 },
                    { 878, "نرجه", false, null, null, null, 18 },
                    { 877, "اقبالیه", false, null, null, null, 18 },
                    { 876, "شریفیه", false, null, null, null, 18 },
                    { 875, "شال", false, null, null, null, 18 },
                    { 874, "آبگرم", false, null, null, null, 18 },
                    { 873, "خرمدشت", false, null, null, null, 18 },
                    { 872, "رازمیان", false, null, null, null, 18 },
                    { 879, "ارداق", false, null, null, null, 18 },
                    { 927, "آرمرده", false, null, null, null, 20 },
                    { 928, "چناره", false, null, null, null, 20 },
                    { 929, "کهنوج", false, null, null, null, 21 },
                    { 983, "خاتون آباد", false, null, null, null, 21 },
                    { 982, "راور", false, null, null, null, 21 },
                    { 981, "گلباف", false, null, null, null, 21 },
                    { 980, "نودژ", false, null, null, null, 21 },
                    { 979, "زرند", false, null, null, null, 21 },
                    { 978, "بزنجان", false, null, null, null, 21 },
                    { 977, "باغین", false, null, null, null, 21 },
                    { 976, "قلعه گنج", false, null, null, null, 21 },
                    { 975, "نجف شهر", false, null, null, null, 21 },
                    { 974, "جیرفت", false, null, null, null, 21 },
                    { 973, "کاظم آباد", false, null, null, null, 21 },
                    { 972, "فهرج", false, null, null, null, 21 },
                    { 971, "بلورد", false, null, null, null, 21 },
                    { 970, "بهرمان", false, null, null, null, 21 },
                    { 969, "گلزار", false, null, null, null, 21 },
                    { 984, "نرمالشیر", false, null, null, null, 21 },
                    { 985, "دشتکار", false, null, null, null, 21 },
                    { 986, "مس سرچسمه", false, null, null, null, 21 },
                    { 987, "خواجو شهر", false, null, null, null, 21 },
                    { 1004, "تازه آباد", false, null, null, null, 22 },
                    { 1003, "بانوره", false, null, null, null, 22 },
                    { 1002, "شاهو", false, null, null, null, 22 },
                    { 1001, "سنقر", false, null, null, null, 22 },
                    { 1000, "انار", false, null, null, null, 21 },
                    { 999, "شهربابک", false, null, null, null, 21 },
                    { 998, "نگار", false, null, null, null, 21 },
                    { 968, "فاریاب", false, null, null, null, 21 },
                    { 997, "ارزوئیه", false, null, null, null, 21 },
                    { 995, "مردهک", false, null, null, null, 21 },
                    { 994, "محی آباد", false, null, null, null, 21 },
                    { 993, "زهکلوت", false, null, null, null, 21 },
                    { 992, "یزدان شهر", false, null, null, null, 21 },
                    { 991, "درب بهشت", false, null, null, null, 21 },
                    { 990, "راین", false, null, null, null, 21 },
                    { 989, "رابر", false, null, null, null, 21 },
                    { 996, "شهداد", false, null, null, null, 21 },
                    { 852, "قیر", false, null, null, null, 17 },
                    { 967, "دهج", false, null, null, null, 21 },
                    { 965, "ماهان", false, null, null, null, 21 },
                    { 944, "هنزا", false, null, null, null, 21 },
                    { 943, "زیدآباد", false, null, null, null, 21 },
                    { 942, "کشکوئیه", false, null, null, null, 21 },
                    { 941, "لاله زار", false, null, null, null, 21 },
                    { 940, "نظام شهر", false, null, null, null, 21 },
                    { 939, "جوزم", false, null, null, null, 21 },
                    { 938, "عنبر آباد", false, null, null, null, 21 },
                    { 937, "جوپار", false, null, null, null, 21 },
                    { 936, "کیانشهر", false, null, null, null, 21 },
                    { 935, "خانوک", false, null, null, null, 21 },
                    { 934, "بم", false, null, null, null, 21 },
                    { 933, "زنگی آباد", false, null, null, null, 21 },
                    { 932, "گنبکی", false, null, null, null, 21 },
                    { 931, "پاریز", false, null, null, null, 21 },
                    { 930, "بلوک", false, null, null, null, 21 },
                    { 945, "چترود", false, null, null, null, 21 },
                    { 946, "جبالبارز", false, null, null, null, 21 },
                    { 947, "سیرجان", false, null, null, null, 21 },
                    { 948, "رودبار", false, null, null, null, 21 },
                    { 964, "کوهبنان", false, null, null, null, 21 },
                    { 963, "ریحان", false, null, null, null, 21 },
                    { 962, "بروات", false, null, null, null, 21 },
                    { 961, "اختیار آباد", false, null, null, null, 21 },
                    { 960, "محمد آباد", false, null, null, null, 21 },
                    { 959, "هماشهر", false, null, null, null, 21 },
                    { 958, "رفسنجان", false, null, null, null, 21 },
                    { 966, "دوساری", false, null, null, null, 21 },
                    { 957, "بردسیر", false, null, null, null, 21 },
                    { 955, "خورسند", false, null, null, null, 21 },
                    { 954, "هجدک", false, null, null, null, 21 },
                    { 953, "اندوهجرد", false, null, null, null, 21 },
                    { 952, "منوجان", false, null, null, null, 21 },
                    { 951, "صفائیه", false, null, null, null, 21 },
                    { 950, "بافت", false, null, null, null, 21 },
                    { 949, "کرمان", false, null, null, null, 21 },
                    { 956, "امین شهر", false, null, null, null, 21 },
                    { 1005, "هلشی", false, null, null, null, 22 },
                    { 851, "بالاده", false, null, null, null, 17 },
                    { 849, "جنت شهر", false, null, null, null, 17 },
                    { 751, "اسپکه", false, null, null, null, 16 },
                    { 750, "بزمان", false, null, null, null, 16 },
                    { 749, "زرآباد", false, null, null, null, 16 },
                    { 748, "چاه بهار", false, null, null, null, 16 },
                    { 747, "زابلی", false, null, null, null, 16 },
                    { 746, "زاهدان", false, null, null, null, 16 },
                    { 745, "محمدآباد", false, null, null, null, 16 },
                    { 744, "گشت", false, null, null, null, 16 },
                    { 743, "پیشین", false, null, null, null, 16 },
                    { 742, "بمبپور", false, null, null, null, 16 },
                    { 741, "زهک", false, null, null, null, 16 },
                    { 740, "نوک آباد", false, null, null, null, 16 },
                    { 739, "هیدوچ", false, null, null, null, 16 },
                    { 738, "جالق", false, null, null, null, 16 },
                    { 737, "قصرقند", false, null, null, null, 16 },
                    { 752, "فنوج", false, null, null, null, 16 },
                    { 736, "بنت", false, null, null, null, 16 },
                    { 753, "سراوان", false, null, null, null, 16 },
                    { 755, "زابل", false, null, null, null, 16 },
                    { 770, "خومه زار", false, null, null, null, 17 },
                    { 769, "فدامی", false, null, null, null, 17 },
                    { 768, "کارزین", false, null, null, null, 17 },
                    { 767, "کازرون", false, null, null, null, 17 },
                    { 766, "نیک شهر", false, null, null, null, 16 },
                    { 765, "محمدان", false, null, null, null, 16 },
                    { 764, "کنارک", false, null, null, null, 16 },
                    { 763, "خاش", false, null, null, null, 16 },
                    { 762, "سوران", false, null, null, null, 16 },
                    { 761, "نصرت آباد", false, null, null, null, 16 },
                    { 760, "میرجاوه", false, null, null, null, 16 },
                    { 759, "سیرکان", false, null, null, null, 16 },
                    { 758, "سرباز", false, null, null, null, 16 },
                    { 757, "ایرانشهر", false, null, null, null, 16 },
                    { 756, "دوست محمد", false, null, null, null, 16 },
                    { 754, "ادیمی", false, null, null, null, 16 },
                    { 735, "راسک", false, null, null, null, 16 },
                    { 734, "نگور", false, null, null, null, 16 },
                    { 733, "گلمورتی", false, null, null, null, 16 },
                    { 712, "دامغان", false, null, null, null, 15 },
                    { 711, "مجن", false, null, null, null, 15 },
                    { 710, "ایوانکی", false, null, null, null, 15 },
                    { 709, "زرین آباد", false, null, null, null, 14 },
                    { 708, "ماه نشان", false, null, null, null, 14 },
                    { 707, "صایین قلعه", false, null, null, null, 14 },
                    { 706, "سهرود", false, null, null, null, 14 },
                    { 1318, "ندوشن", false, null, null, null, 31 },
                    { 704, "چورزق", false, null, null, null, 14 },
                    { 703, "گرماب", false, null, null, null, 14 },
                    { 702, "نور بهار", false, null, null, null, 14 },
                    { 701, "حلب", false, null, null, null, 14 },
                    { 700, "دندی", false, null, null, null, 14 },
                    { 699, "ابهر", false, null, null, null, 14 },
                    { 698, "قیدار", false, null, null, null, 14 },
                    { 713, "سرخه", false, null, null, null, 15 },
                    { 714, "مهدی شهر", false, null, null, null, 15 },
                    { 715, "شاهرود", false, null, null, null, 15 },
                    { 716, "سمنان", false, null, null, null, 15 },
                    { 732, "بنجار", false, null, null, null, 16 },
                    { 731, "شهرک علی اکبر", false, null, null, null, 16 },
                    { 730, "محمدی", false, null, null, null, 16 },
                    { 729, "آرادان", false, null, null, null, 15 },
                    { 728, "کلاته", false, null, null, null, 15 },
                    { 727, "بیارجمند", false, null, null, null, 15 },
                    { 726, "شهمیرزاد", false, null, null, null, 15 },
                    { 771, "سلطان آباد", false, null, null, null, 17 },
                    { 725, "میامی", false, null, null, null, 15 },
                    { 723, "بسطام", false, null, null, null, 15 },
                    { 722, "رودیان", false, null, null, null, 15 },
                    { 721, "درجزین", false, null, null, null, 15 },
                    { 720, "دیباج", false, null, null, null, 15 },
                    { 719, "کلاته خیج", false, null, null, null, 15 },
                    { 718, "گرمسار", false, null, null, null, 15 },
                    { 717, "کهن آباد", false, null, null, null, 15 },
                    { 724, "امیریه", false, null, null, null, 15 },
                    { 772, "فیروزآباد", false, null, null, null, 17 },
                    { 773, "دبیران", false, null, null, null, 17 },
                    { 774, "باب انار", false, null, null, null, 17 },
                    { 828, "فطرویه", false, null, null, null, 17 },
                    { 827, "اردکان", false, null, null, null, 17 },
                    { 826, "خوزی", false, null, null, null, 17 },
                    { 825, "جویم", false, null, null, null, 17 },
                    { 824, "حسامی", false, null, null, null, 17 },
                    { 823, "سورمق", false, null, null, null, 17 },
                    { 822, "شهر صدرا", false, null, null, null, 17 },
                    { 821, "سعادت شهر", false, null, null, null, 17 },
                    { 820, "بنارویه", false, null, null, null, 17 },
                    { 819, "سده", false, null, null, null, 17 },
                    { 818, "قادرآباد", false, null, null, null, 17 },
                    { 817, "زاهدشهر", false, null, null, null, 17 },
                    { 816, "کوپن", false, null, null, null, 17 },
                    { 815, "سیدان", false, null, null, null, 17 },
                    { 814, "دوزه", false, null, null, null, 17 },
                    { 829, "نودان", false, null, null, null, 17 },
                    { 830, "مبارک آباددیز", false, null, null, null, 17 },
                    { 831, "داراب", false, null, null, null, 17 },
                    { 832, "نورآباد", false, null, null, null, 17 },
                    { 848, "مهر", false, null, null, null, 17 },
                    { 847, "خشت", false, null, null, null, 17 },
                    { 846, "اهل", false, null, null, null, 17 },
                    { 845, "بهمن", false, null, null, null, 17 },
                    { 844, "لپویی", false, null, null, null, 17 },
                    { 843, "نوجین", false, null, null, null, 17 },
                    { 842, "خور", false, null, null, null, 17 },
                    { 813, "افزر", false, null, null, null, 17 },
                    { 841, "ایج", false, null, null, null, 17 },
                    { 839, "ششده", false, null, null, null, 17 },
                    { 838, "کوهنجان", false, null, null, null, 17 },
                    { 837, "مرودشت", false, null, null, null, 17 },
                    { 836, "خاوران", false, null, null, null, 17 },
                    { 835, "حاجی آباد", false, null, null, null, 17 },
                    { 834, "نوبندگان", false, null, null, null, 17 },
                    { 833, "کوار", false, null, null, null, 17 },
                    { 840, "مزایجان", false, null, null, null, 17 },
                    { 850, "مشکان", false, null, null, null, 17 },
                    { 812, "میمند", false, null, null, null, 17 },
                    { 810, "آباده طشک", false, null, null, null, 17 },
                    { 789, "کنار تخته", false, null, null, null, 17 },
                    { 788, "نی ریز", false, null, null, null, 17 },
                    { 787, "بیضا", false, null, null, null, 17 },
                    { 786, "وراوی", false, null, null, null, 17 },
                    { 785, "اوز", false, null, null, null, 17 },
                    { 784, "علامرودشت", false, null, null, null, 17 },
                    { 783, "ایزدخواست", false, null, null, null, 17 },
                    { 782, "شیراز", false, null, null, null, 17 },
                    { 781, "دهرم", false, null, null, null, 17 },
                    { 780, "بیرم", false, null, null, null, 17 },
                    { 779, "دژکرد", false, null, null, null, 17 },
                    { 778, "ارسنجان", false, null, null, null, 17 },
                    { 777, "قره بلاغ", false, null, null, null, 17 },
                    { 776, "سروستان", false, null, null, null, 17 },
                    { 775, "رامجرد", false, null, null, null, 17 },
                    { 790, "امام شهر", false, null, null, null, 17 },
                    { 791, "جهرم", false, null, null, null, 17 },
                    { 792, "بابامنیر", false, null, null, null, 17 },
                    { 793, "گراش", false, null, null, null, 17 },
                    { 809, "دوبرجی", false, null, null, null, 17 },
                    { 808, "گله دار", false, null, null, null, 17 },
                    { 807, "قائمیه", false, null, null, null, 17 },
                    { 806, "اشکنان", false, null, null, null, 17 },
                    { 805, "صغاد", false, null, null, null, 17 },
                    { 804, "زرقان", false, null, null, null, 17 },
                    { 803, "فراشبند", false, null, null, null, 17 },
                    { 811, "خرامه", false, null, null, null, 17 }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateId" },
                values: new object[,]
                {
                    { 802, "لطیفی", false, null, null, null, 17 },
                    { 800, "استهبان", false, null, null, null, 17 },
                    { 799, "خانه زنیان", false, null, null, null, 17 },
                    { 798, "خنچ", false, null, null, null, 17 },
                    { 797, "کامفیروز", false, null, null, null, 17 },
                    { 796, "حسن آباد", false, null, null, null, 17 },
                    { 795, "شهرپیر", false, null, null, null, 17 },
                    { 794, "فسا", false, null, null, null, 17 },
                    { 801, "بوانات", false, null, null, null, 17 },
                    { 1006, "جوانرود", false, null, null, null, 22 },
                    { 1007, "قصر شیرین", false, null, null, null, 22 },
                    { 1008, "نوسود", false, null, null, null, 22 },
                    { 1218, "رازقان", false, null, null, null, 28 },
                    { 1217, "آشتیان", false, null, null, null, 28 },
                    { 1216, "کمیجان", false, null, null, null, 28 },
                    { 1215, "نراق", false, null, null, null, 28 },
                    { 1214, "خنجین", false, null, null, null, 28 },
                    { 1213, "آستانه", false, null, null, null, 28 },
                    { 1212, "چمستان", false, null, null, null, 27 },
                    { 1211, "سورک", false, null, null, null, 27 },
                    { 1210, "رستمکلا", false, null, null, null, 27 },
                    { 1209, "کجور", false, null, null, null, 27 },
                    { 1208, "خرم آباد", false, null, null, null, 27 },
                    { 1207, "سرخرود", false, null, null, null, 27 },
                    { 1206, "رینه", false, null, null, null, 27 },
                    { 1205, "ساری", false, null, null, null, 27 },
                    { 1204, "مرزن آباد", false, null, null, null, 27 },
                    { 1219, "مهاجران", false, null, null, null, 28 },
                    { 1203, "خوش رودپی", false, null, null, null, 27 },
                    { 1220, "غرق آباد", false, null, null, null, 28 },
                    { 1222, "قورچی باشی", false, null, null, null, 28 },
                    { 1237, "کارچان", false, null, null, null, 28 },
                    { 1236, "پرندک", false, null, null, null, 28 },
                    { 1235, "دلیجان", false, null, null, null, 28 },
                    { 1234, "فرمهین", false, null, null, null, 28 },
                    { 1233, "نوبران", false, null, null, null, 28 },
                    { 1232, "توره", false, null, null, null, 28 },
                    { 1231, "اراک", false, null, null, null, 28 },
                    { 1230, "زاویه", false, null, null, null, 28 },
                    { 1229, "تفرش", false, null, null, null, 28 },
                    { 1228, "میلاجرد", false, null, null, null, 28 },
                    { 1227, "ساوه", false, null, null, null, 28 },
                    { 1226, "شازند", false, null, null, null, 28 },
                    { 1225, "محلات", false, null, null, null, 28 },
                    { 1224, "ساروق", false, null, null, null, 28 },
                    { 1223, "خشکرود", false, null, null, null, 28 },
                    { 1221, "خنداب", false, null, null, null, 28 },
                    { 1202, "قائم شهر", false, null, null, null, 27 },
                    { 1201, "عباس آباد", false, null, null, null, 27 },
                    { 1200, "زرگر محله", false, null, null, null, 27 },
                    { 1179, "فریدونکنار", false, null, null, null, 27 },
                    { 1178, "مرزیکلا", false, null, null, null, 27 },
                    { 1177, "نور", false, null, null, null, 27 },
                    { 1176, "کیاکلا", false, null, null, null, 27 },
                    { 1175, "خلیل آباد", false, null, null, null, 27 },
                    { 1174, "نوشهر", false, null, null, null, 27 },
                    { 1173, "رامسر", false, null, null, null, 27 },
                    { 1172, "محمود آباد", false, null, null, null, 27 },
                    { 1171, "گزنک", false, null, null, null, 27 },
                    { 1170, "پایین هولار", false, null, null, null, 27 },
                    { 1169, "کوهی خیل", false, null, null, null, 27 },
                    { 1168, "امیرکلا", false, null, null, null, 27 },
                    { 1167, "ارطه", false, null, null, null, 27 },
                    { 1166, "سلمان شهر", false, null, null, null, 27 },
                    { 1165, "گتاب", false, null, null, null, 27 },
                    { 1180, "زیرآب", false, null, null, null, 27 },
                    { 1181, "امامزاده عبدالله", false, null, null, null, 27 },
                    { 1182, "هچیرود", false, null, null, null, 27 },
                    { 1183, "فریم", false, null, null, null, 27 },
                    { 1199, "رویان", false, null, null, null, 27 },
                    { 1198, "شیرگاه", false, null, null, null, 27 },
                    { 1197, "شیرود", false, null, null, null, 27 },
                    { 1196, "بابلسر", false, null, null, null, 27 },
                    { 1195, "کتالم و سادات شهر", false, null, null, null, 27 },
                    { 1194, "نکا", false, null, null, null, 27 },
                    { 1193, "آمل", false, null, null, null, 27 },
                    { 1238, "نیمور", false, null, null, null, 28 },
                    { 1192, "آلاشت", false, null, null, null, 27 },
                    { 1190, "بابل", false, null, null, null, 27 },
                    { 1189, "بلده", false, null, null, null, 27 },
                    { 1188, "کلارآباد", false, null, null, null, 27 },
                    { 1187, "بهشهر", false, null, null, null, 27 },
                    { 1186, "پول", false, null, null, null, 27 },
                    { 1185, "نشتارود", false, null, null, null, 27 },
                    { 1184, "هادی شهر", false, null, null, null, 27 },
                    { 1191, "جویبار", false, null, null, null, 27 },
                    { 1239, "هندودر", false, null, null, null, 28 },
                    { 1240, "آوه", false, null, null, null, 28 },
                    { 1241, "جاورسیان", false, null, null, null, 28 },
                    { 1295, "مریانج", false, null, null, null, 30 },
                    { 1294, "شیرین سو", false, null, null, null, 30 },
                    { 1293, "فرسنج", false, null, null, null, 30 },
                    { 1292, "بهار", false, null, null, null, 30 },
                    { 1291, "سامن", false, null, null, null, 30 },
                    { 1290, "فامنین", false, null, null, null, 30 },
                    { 1289, "برزول", false, null, null, null, 30 },
                    { 1288, "جورقان", false, null, null, null, 30 },
                    { 1287, "آجین", false, null, null, null, 30 },
                    { 1286, "سرکان", false, null, null, null, 30 },
                    { 1285, "دمق", false, null, null, null, 30 },
                    { 1284, "زنگنه", false, null, null, null, 30 },
                    { 1283, "تخت", false, null, null, null, 29 },
                    { 1282, "بستک", false, null, null, null, 29 },
                    { 1281, "بندر لنگه", false, null, null, null, 29 },
                    { 1296, "فیروزان", false, null, null, null, 30 },
                    { 1297, "قروه درجزین", false, null, null, null, 30 },
                    { 1298, "ازندریان", false, null, null, null, 30 },
                    { 1299, "لالجین", false, null, null, null, 30 },
                    { 1315, "حمیدیا", false, null, null, null, 31 },
                    { 1314, "مهردشت", false, null, null, null, 31 },
                    { 1313, "مرودست", false, null, null, null, 31 },
                    { 1312, "قهاوند", false, null, null, null, 30 },
                    { 1311, "کبودرآهنگ", false, null, null, null, 30 },
                    { 1310, "مهاجران", false, null, null, null, 30 },
                    { 1309, "جوکار", false, null, null, null, 30 },
                    { 1280, "دشتی", false, null, null, null, 29 },
                    { 1308, "رزن", false, null, null, null, 30 },
                    { 1306, "همدان", false, null, null, null, 30 },
                    { 1305, "اسدآباد", false, null, null, null, 30 },
                    { 1304, "تویسرکان", false, null, null, null, 30 },
                    { 1303, "صالح آباد", false, null, null, null, 30 },
                    { 1302, "ملایر", false, null, null, null, 30 },
                    { 1301, "گیان", false, null, null, null, 30 },
                    { 1300, "گل تپه", false, null, null, null, 30 },
                    { 1307, "نهاوند", false, null, null, null, 30 },
                    { 1164, "ایزدشهر", false, null, null, null, 27 },
                    { 1279, "هرمز", false, null, null, null, 29 },
                    { 1277, "بندر جاسک", false, null, null, null, 29 },
                    { 1256, "کوهستک", false, null, null, null, 29 },
                    { 1255, "سندرک", false, null, null, null, 29 },
                    { 1254, "زیارتعلی", false, null, null, null, 29 },
                    { 1253, "بندرعباس", false, null, null, null, 29 },
                    { 1252, "سرگز", false, null, null, null, 29 },
                    { 1251, "کیش", false, null, null, null, 29 },
                    { 1250, "کوشکنار", false, null, null, null, 29 },
                    { 1249, "قشم", false, null, null, null, 29 },
                    { 1248, "گروک", false, null, null, null, 29 },
                    { 1247, "تیرور", false, null, null, null, 29 },
                    { 1246, "بیکاء", false, null, null, null, 29 },
                    { 1245, "شهباز", false, null, null, null, 28 },
                    { 1244, "داودآباد", false, null, null, null, 28 },
                    { 1243, "مامونیه", false, null, null, null, 28 },
                    { 1242, "خمین", false, null, null, null, 28 },
                    { 1257, "لمزان", false, null, null, null, 29 },
                    { 1258, "رویدر", false, null, null, null, 29 },
                    { 1259, "قلعه قاضی", false, null, null, null, 29 },
                    { 1260, "فارغان", false, null, null, null, 29 },
                    { 1276, "فین", false, null, null, null, 29 },
                    { 1275, "حاجی آباد", false, null, null, null, 29 },
                    { 1274, "چارک", false, null, null, null, 29 },
                    { 1273, "خمیر", false, null, null, null, 29 },
                    { 1272, "سوزا", false, null, null, null, 29 },
                    { 1271, "سیریک", false, null, null, null, 29 },
                    { 1270, "میناب", false, null, null, null, 29 },
                    { 1278, "گوهران", false, null, null, null, 29 },
                    { 1269, "دهبازر", false, null, null, null, 29 },
                    { 1267, "جناح", false, null, null, null, 29 },
                    { 1266, "کنگ", false, null, null, null, 29 },
                    { 1265, "پارسیان", false, null, null, null, 29 },
                    { 1264, "درگهان", false, null, null, null, 29 },
                    { 1263, "سردشت", false, null, null, null, 29 },
                    { 1262, "هشتبندی", false, null, null, null, 29 },
                    { 1261, "ابوموسی", false, null, null, null, 29 },
                    { 1268, "تازیان پایین", false, null, null, null, 29 },
                    { 1163, "کلاردشت", false, null, null, null, 27 },
                    { 1162, "تنکابن", false, null, null, null, 27 },
                    { 1161, "بهمنمیر", false, null, null, null, 27 },
                    { 1062, "سنگدوین", false, null, null, null, 24 },
                    { 1061, "گالیکش", false, null, null, null, 24 },
                    { 1060, "سرخنکلاته", false, null, null, null, 24 },
                    { 1059, "آق قلا", false, null, null, null, 24 },
                    { 1058, "نگین شهر", false, null, null, null, 24 },
                    { 1057, "بندر ترکمن", false, null, null, null, 24 },
                    { 1056, "مراوه", false, null, null, null, 24 },
                    { 1055, "کردکوی", false, null, null, null, 24 },
                    { 1054, "گنبد کاووس", false, null, null, null, 24 },
                    { 1053, "فراغی", false, null, null, null, 24 },
                    { 1052, "رامیان", false, null, null, null, 24 },
                    { 1051, "مزرعه", false, null, null, null, 24 },
                    { 1050, "سیمین شهر", false, null, null, null, 24 },
                    { 1049, "سوق", false, null, null, null, 23 },
                    { 1048, "چرام", false, null, null, null, 23 },
                    { 1063, "دلند", false, null, null, null, 24 },
                    { 1064, "بندر گز", false, null, null, null, 24 },
                    { 1065, "نوده خاندوز", false, null, null, null, 24 },
                    { 1066, "مینو دشت", false, null, null, null, 24 },
                    { 1082, "ماکلوان", false, null, null, null, 25 },
                    { 1081, "خشکبیجار", false, null, null, null, 25 },
                    { 1080, "شلمان", false, null, null, null, 25 },
                    { 1079, "منجیل", false, null, null, null, 25 },
                    { 1078, "جلین", false, null, null, null, 24 },
                    { 1077, "انبار آلوم", false, null, null, null, 24 },
                    { 1076, "آزاد شهر", false, null, null, null, 24 },
                    { 1047, "مارگون", false, null, null, null, 23 },
                    { 1075, "نوکنده", false, null, null, null, 24 },
                    { 1073, "فاضل آباد", false, null, null, null, 24 },
                    { 1072, "اینچه برون", false, null, null, null, 24 },
                    { 1071, "کلاله", false, null, null, null, 24 },
                    { 1070, "خان ببین", false, null, null, null, 24 },
                    { 1069, "علی آباد", false, null, null, null, 24 },
                    { 1068, "گمیش تپه", false, null, null, null, 24 },
                    { 1067, "گرگان", false, null, null, null, 24 },
                    { 1074, "تاتار علیا", false, null, null, null, 24 },
                    { 1083, "سنگر", false, null, null, null, 25 },
                    { 1046, "قلعه رئیسی", false, null, null, null, 23 },
                    { 1044, "باشت", false, null, null, null, 23 },
                    { 1023, "باینگان", false, null, null, null, 22 },
                    { 1022, "ریجاب", false, null, null, null, 22 },
                    { 1021, "سرپل ذهاب", false, null, null, null, 22 },
                    { 1020, "کنگاور", false, null, null, null, 22 },
                    { 1019, "میان راهان", false, null, null, null, 22 },
                    { 1018, "کرمانشاه", false, null, null, null, 22 },
                    { 1017, "ازگله", false, null, null, null, 22 },
                    { 1016, "پاوه", false, null, null, null, 22 },
                    { 1015, "روانسر", false, null, null, null, 22 },
                    { 1014, "سطر", false, null, null, null, 22 },
                    { 1013, "گیلانغرب", false, null, null, null, 22 },
                    { 1012, "حمیل", false, null, null, null, 22 },
                    { 1011, "بیستون", false, null, null, null, 22 },
                    { 1010, "کوزران", false, null, null, null, 22 },
                    { 1009, "کرند", false, null, null, null, 22 },
                    { 1024, "هرسین", false, null, null, null, 22 },
                    { 1025, "اسلام آباد غرب", false, null, null, null, 22 },
                    { 1026, "سرمست", false, null, null, null, 22 },
                    { 1027, "سومار", false, null, null, null, 22 },
                    { 1043, "مادوان", false, null, null, null, 23 },
                    { 1042, "دیشموک", false, null, null, null, 23 },
                    { 1041, "لیکک", false, null, null, null, 23 },
                    { 1040, "چیتاب", false, null, null, null, 23 },
                    { 1039, "دوگنبدان", false, null, null, null, 23 },
                    { 1038, "سرفاریاب", false, null, null, null, 23 },
                    { 1037, "یاسوج", false, null, null, null, 23 },
                    { 1045, "پاتاوه", false, null, null, null, 23 },
                    { 1036, "دهدشت", false, null, null, null, 23 },
                    { 1034, "لنده", false, null, null, null, 23 },
                    { 1033, "گراب سفلی", false, null, null, null, 23 },
                    { 1032, "گودین", false, null, null, null, 22 },
                    { 1031, "صحنه", false, null, null, null, 22 },
                    { 1030, "رباط", false, null, null, null, 22 },
                    { 1029, "گهواره", false, null, null, null, 22 },
                    { 1028, "نودشه", false, null, null, null, 22 },
                    { 1035, "سی سخت", false, null, null, null, 23 },
                    { 697, "نیک پی", false, null, null, null, 14 },
                    { 1084, "مرجقل", false, null, null, null, 25 },
                    { 1086, "رضوانشهر", false, null, null, null, 25 },
                    { 1140, "مومن آباد", false, null, null, null, 26 },
                    { 1139, "الشتر", false, null, null, null, 26 },
                    { 1138, "بروجرد", false, null, null, null, 26 },
                    { 1137, "هفت چشمه", false, null, null, null, 26 },
                    { 1136, "کوهدشت", false, null, null, null, 26 },
                    { 1135, "پلدختر", false, null, null, null, 26 },
                    { 1134, "شول آباد", false, null, null, null, 26 },
                    { 1133, "ویسیان", false, null, null, null, 26 },
                    { 1132, "بیران شهر", false, null, null, null, 26 },
                    { 1131, "چالانچولان", false, null, null, null, 26 },
                    { 1130, "رانکوه", false, null, null, null, 25 },
                    { 1129, "آستانه اشرفیه", false, null, null, null, 25 },
                    { 1128, "چابکسر", false, null, null, null, 25 },
                    { 1127, "سیاهکل", false, null, null, null, 25 },
                    { 1126, "حویق", false, null, null, null, 25 },
                    { 1141, "دورود", false, null, null, null, 26 },
                    { 1142, "زاغه", false, null, null, null, 26 },
                    { 1143, "چقابل", false, null, null, null, 26 },
                    { 1144, "الیگودرز", false, null, null, null, 26 },
                    { 1160, "کیاسر", false, null, null, null, 27 },
                    { 1159, "چالوس", false, null, null, null, 27 },
                    { 1158, "دابودشت", false, null, null, null, 27 },
                    { 1157, "پل سفید", false, null, null, null, 27 },
                    { 1156, "گلوگاه", false, null, null, null, 27 },
                    { 1155, "درب گنبد", false, null, null, null, 26 },
                    { 1154, "فیروز آباد", false, null, null, null, 26 },
                    { 1125, "گوراب زرمیخ", false, null, null, null, 25 },
                    { 1153, "اشترینان", false, null, null, null, 26 },
                    { 1151, "گراب", false, null, null, null, 26 },
                    { 1150, "ازنا", false, null, null, null, 26 },
                    { 1149, "سراب دوره", false, null, null, null, 26 },
                    { 1148, "سپیددشت", false, null, null, null, 26 },
                    { 1147, "نورآباد", false, null, null, null, 26 },
                    { 1146, "کوهنانی", false, null, null, null, 26 },
                    { 1145, "معمولان", false, null, null, null, 26 },
                    { 1152, "خرم آباد", false, null, null, null, 26 },
                    { 1085, "لیسار", false, null, null, null, 25 },
                    { 1124, "لولمان", false, null, null, null, 25 },
                    { 1122, "جیرنده", false, null, null, null, 25 },
                    { 1101, "توتکابن", false, null, null, null, 25 },
                    { 1100, "لاهیجان", false, null, null, null, 25 },
                    { 1099, "رستم آباد", false, null, null, null, 25 },
                    { 1098, "املش", false, null, null, null, 25 },
                    { 1097, "بندر انزلی", false, null, null, null, 25 },
                    { 1096, "کلاچای", false, null, null, null, 25 },
                    { 1095, "بازار جمعه", false, null, null, null, 25 },
                    { 1094, "چوبر", false, null, null, null, 25 },
                    { 1093, "فومن", false, null, null, null, 25 },
                    { 1092, "لشت نشاء", false, null, null, null, 25 },
                    { 1091, "اطاقوار", false, null, null, null, 25 },
                    { 1090, "لوشان", false, null, null, null, 25 },
                    { 1089, "احمد سرگوراب", false, null, null, null, 25 },
                    { 1088, "لوندویل", false, null, null, null, 25 },
                    { 1087, "رحیم آباد", false, null, null, null, 25 }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateId" },
                values: new object[,]
                {
                    { 1102, "لنگرود", false, null, null, null, 25 },
                    { 1103, "کوچصفهان", false, null, null, null, 25 },
                    { 1104, "صومعه سرا", false, null, null, null, 25 },
                    { 1105, "اسالم", false, null, null, null, 25 },
                    { 1121, "رودبنه", false, null, null, null, 25 },
                    { 1120, "آستارا", false, null, null, null, 25 },
                    { 1119, "بره سر", false, null, null, null, 25 },
                    { 1118, "پره سر", false, null, null, null, 25 },
                    { 1117, "هشتپر (تالش)", false, null, null, null, 25 },
                    { 1116, "واجارگاه", false, null, null, null, 25 },
                    { 1115, "ماسال", false, null, null, null, 25 },
                    { 1123, "چاف و چمخاله", false, null, null, null, 25 },
                    { 1114, "خمام", false, null, null, null, 25 },
                    { 1112, "رشت", false, null, null, null, 25 },
                    { 1111, "کومله", false, null, null, null, 25 },
                    { 1110, "رودبار", false, null, null, null, 25 },
                    { 1109, "شفت", false, null, null, null, 25 },
                    { 1108, "کیاشهر", false, null, null, null, 25 },
                    { 1107, "رودسر", false, null, null, null, 25 },
                    { 1106, "دیلمان", false, null, null, null, 25 },
                    { 1113, "ماسوله", false, null, null, null, 25 },
                    { 696, "خرمدره", false, null, null, null, 14 },
                    { 695, "سلطانیه", false, null, null, null, 14 },
                    { 694, "هیدج", false, null, null, null, 14 },
                    { 195, "قهدریجان", false, null, null, null, 4 },
                    { 194, "خورزوق", false, null, null, null, 4 },
                    { 193, "زاینده رود", false, null, null, null, 4 },
                    { 192, "اژیه", false, null, null, null, 4 },
                    { 191, "دیزیچه", false, null, null, null, 4 },
                    { 190, "فرخی", false, null, null, null, 4 },
                    { 189, "منظریه", false, null, null, null, 4 },
                    { 188, "نجف آباد", false, null, null, null, 4 },
                    { 187, "خوانسار", false, null, null, null, 4 },
                    { 186, "آران و بیدگل", false, null, null, null, 4 },
                    { 185, "مشکات", false, null, null, null, 4 },
                    { 184, "نیک آباد", false, null, null, null, 4 },
                    { 183, "علویجه", false, null, null, null, 4 },
                    { 182, "داران", false, null, null, null, 4 },
                    { 181, "رضوانشهر", false, null, null, null, 4 },
                    { 196, "شاهین شهر", false, null, null, null, 4 },
                    { 180, "گلشهر", false, null, null, null, 4 },
                    { 197, "بهارستان", false, null, null, null, 4 },
                    { 199, "دهاقان", false, null, null, null, 4 },
                    { 304, "زازران", false, null, null, null, 4 },
                    { 303, "سین", false, null, null, null, 4 },
                    { 302, "نایین", false, null, null, null, 4 },
                    { 301, "کوشک", false, null, null, null, 4 },
                    { 300, "زیباشهر", false, null, null, null, 4 },
                    { 209, "افوس", false, null, null, null, 4 },
                    { 208, "کامو و چوگان", false, null, null, null, 4 },
                    { 207, "مهاباد", false, null, null, null, 4 },
                    { 206, "کهریزسنگ", false, null, null, null, 4 },
                    { 205, "حنا", false, null, null, null, 4 },
                    { 204, "عسگران", false, null, null, null, 4 },
                    { 203, "گلپایگان", false, null, null, null, 4 },
                    { 202, "کوهپایه", false, null, null, null, 4 },
                    { 201, "بادرود", false, null, null, null, 4 },
                    { 200, "برف انبار", false, null, null, null, 4 },
                    { 198, "چمگردان", false, null, null, null, 4 },
                    { 179, "تودشک", false, null, null, null, 4 },
                    { 178, "میمه", false, null, null, null, 4 },
                    { 177, "بهاران", false, null, null, null, 4 },
                    { 156, "چرمین", false, null, null, null, 4 },
                    { 155, "قهجاورستان", false, null, null, null, 4 },
                    { 154, "لای بید", false, null, null, null, 4 },
                    { 153, "کلیشادوسودرجان", false, null, null, null, 4 },
                    { 152, "کمشچه", false, null, null, null, 4 },
                    { 151, "فولادشهر", false, null, null, null, 4 },
                    { 150, "هرند", false, null, null, null, 4 },
                    { 149, "مجلسی", false, null, null, null, 4 },
                    { 148, "خور", false, null, null, null, 4 },
                    { 147, "شهرضا", false, null, null, null, 4 },
                    { 146, "بافران", false, null, null, null, 4 },
                    { 145, "اصغر آباد", false, null, null, null, 4 },
                    { 144, "ابوزید آباد", false, null, null, null, 4 },
                    { 143, "کاشان", false, null, null, null, 4 },
                    { 142, "زواره", false, null, null, null, 4 },
                    { 157, "رزوه", false, null, null, null, 4 },
                    { 158, "فریدونشهر", false, null, null, null, 4 },
                    { 159, "طرق آباد", false, null, null, null, 4 },
                    { 160, "نصر آباد", false, null, null, null, 4 },
                    { 176, "حبیب آباد", false, null, null, null, 4 },
                    { 175, "سده لنجان", false, null, null, null, 4 },
                    { 174, "حسن آباد", false, null, null, null, 4 },
                    { 173, "گرگاب", false, null, null, null, 4 },
                    { 172, "ایمانشهر", false, null, null, null, 4 },
                    { 171, "دولت آباد", false, null, null, null, 4 },
                    { 170, "انارک", false, null, null, null, 4 },
                    { 305, "مبارکه", false, null, null, null, 4 },
                    { 169, "درچه", false, null, null, null, 4 },
                    { 167, "بویین و میاندشت", false, null, null, null, 4 },
                    { 166, "جوشقان قالی", false, null, null, null, 4 },
                    { 165, "اردستان", false, null, null, null, 4 },
                    { 164, "گلدشت", false, null, null, null, 4 },
                    { 163, "سمیرم", false, null, null, null, 4 },
                    { 162, "سفید شهر", false, null, null, null, 4 },
                    { 161, "برزک", false, null, null, null, 4 },
                    { 168, "کرکوند", false, null, null, null, 4 },
                    { 306, "ورزنه", false, null, null, null, 4 },
                    { 307, "ورنامخواست", false, null, null, null, 4 },
                    { 308, "شاپور آباد", false, null, null, null, 4 },
                    { 362, "دهلران", false, null, null, null, 6 },
                    { 361, "توحید", false, null, null, null, 6 },
                    { 360, "مورموری", false, null, null, null, 6 },
                    { 359, "ارکواز", false, null, null, null, 6 },
                    { 358, "دره شهر", false, null, null, null, 6 },
                    { 357, "میمه", false, null, null, null, 6 },
                    { 356, "بلاوه", false, null, null, null, 6 },
                    { 355, "سراب باغ", false, null, null, null, 6 },
                    { 354, "مهر", false, null, null, null, 6 },
                    { 353, "پهله", false, null, null, null, 6 },
                    { 352, "آسمان آباد", false, null, null, null, 6 },
                    { 351, "مهران", false, null, null, null, 6 },
                    { 350, "ایوان", false, null, null, null, 6 },
                    { 349, "ایلام", false, null, null, null, 6 },
                    { 348, "بدره", false, null, null, null, 6 },
                    { 363, "لومار", false, null, null, null, 6 },
                    { 364, "چوار", false, null, null, null, 6 },
                    { 365, "زرنه", false, null, null, null, 6 },
                    { 366, "صالح آباد", false, null, null, null, 6 },
                    { 382, "بندر دیر", false, null, null, null, 7 },
                    { 381, "چغادک", false, null, null, null, 7 },
                    { 380, "بنک", false, null, null, null, 7 },
                    { 379, "وحدتیه", false, null, null, null, 7 },
                    { 378, "بندر دیلم", false, null, null, null, 7 },
                    { 377, "کلمه", false, null, null, null, 7 },
                    { 376, "نخل تقی", false, null, null, null, 7 },
                    { 347, "موسیان", false, null, null, null, 6 },
                    { 375, "خورموج", false, null, null, null, 7 },
                    { 373, "اهرم", false, null, null, null, 7 },
                    { 372, "بندر ریگ", false, null, null, null, 7 },
                    { 371, "برازجان", false, null, null, null, 7 },
                    { 370, "ریز", false, null, null, null, 7 },
                    { 369, "دلگشا", false, null, null, null, 6 },
                    { 368, "ماژین", false, null, null, null, 6 },
                    { 367, "سرابله", false, null, null, null, 6 },
                    { 374, "دوراهک", false, null, null, null, 7 },
                    { 141, "دهق", false, null, null, null, 4 },
                    { 346, "شباب", false, null, null, null, 6 },
                    { 344, "فردیس", false, null, null, null, 5 },
                    { 323, "طالخونچه", false, null, null, null, 4 },
                    { 322, "جندق", false, null, null, null, 4 },
                    { 321, "قمصر", false, null, null, null, 4 },
                    { 320, "جوزدان", false, null, null, null, 4 },
                    { 319, "کمه", false, null, null, null, 4 },
                    { 318, "نوش آباد", false, null, null, null, 4 },
                    { 317, "نیاسر", false, null, null, null, 4 },
                    { 316, "محمد آباد", false, null, null, null, 4 },
                    { 315, "نطنز", false, null, null, null, 4 },
                    { 314, "دامنه", false, null, null, null, 4 },
                    { 313, "چادگان", false, null, null, null, 4 },
                    { 312, "باغ بهادران", false, null, null, null, 4 },
                    { 311, "اصفهان", false, null, null, null, 4 },
                    { 310, "وزوان", false, null, null, null, 4 },
                    { 309, "فلاورجان", false, null, null, null, 4 },
                    { 324, "خمینی شهر", false, null, null, null, 4 },
                    { 325, "باغشاد", false, null, null, null, 4 },
                    { 326, "دستگرد", false, null, null, null, 4 },
                    { 327, "ابریشم", false, null, null, null, 4 },
                    { 343, "کمال شهر", false, null, null, null, 5 },
                    { 342, "گلسار", false, null, null, null, 5 },
                    { 341, "تنکمان", false, null, null, null, 5 },
                    { 340, "گرمدره", false, null, null, null, 5 },
                    { 339, "کوهسار", false, null, null, null, 5 },
                    { 338, "اشتهارد", false, null, null, null, 5 },
                    { 337, "ماهدشت", false, null, null, null, 5 },
                    { 345, "آبدانان", false, null, null, null, 6 },
                    { 336, "هشتگرد", false, null, null, null, 5 },
                    { 334, "مشکین دشت", false, null, null, null, 5 },
                    { 333, "محمدشهر", false, null, null, null, 5 },
                    { 332, "شهرجدید هشتگرد", false, null, null, null, 5 },
                    { 331, "طالقان", false, null, null, null, 5 },
                    { 330, "کرج", false, null, null, null, 5 },
                    { 329, "آسارا", false, null, null, null, 5 },
                    { 328, "چهارباغ", false, null, null, null, 5 },
                    { 335, "نظرآباد", false, null, null, null, 5 },
                    { 140, "ونک", false, null, null, null, 4 },
                    { 139, "تیران", false, null, null, null, 4 },
                    { 138, "گوگد", false, null, null, null, 4 },
                    { 39, "مرند", false, null, null, null, 1 },
                    { 38, "کلیبر", false, null, null, null, 1 },
                    { 37, "جوان قلعه", false, null, null, null, 1 },
                    { 36, "آقکند", false, null, null, null, 1 },
                    { 35, "بخشایش", false, null, null, null, 1 },
                    { 34, "اهر", false, null, null, null, 1 },
                    { 33, "نظرکهریزی", false, null, null, null, 1 },
                    { 32, "لیلان", false, null, null, null, 1 },
                    { 31, "خسروشاه", false, null, null, null, 1 },
                    { 30, "خامنه", false, null, null, null, 1 },
                    { 29, "ممقان", false, null, null, null, 1 },
                    { 28, "مراغه", false, null, null, null, 1 },
                    { 27, "وایقان", false, null, null, null, 1 },
                    { 26, "قره آغاج", false, null, null, null, 1 },
                    { 25, "بناب مرند", false, null, null, null, 1 },
                    { 40, "اسکو", false, null, null, null, 1 },
                    { 41, "شندآباد", false, null, null, null, 1 },
                    { 42, "شربیان", false, null, null, null, 1 },
                    { 43, "گوگان", false, null, null, null, 1 },
                    { 59, "کلوانق", false, null, null, null, 1 },
                    { 58, "هوراند", false, null, null, null, 1 },
                    { 57, "بناب", false, null, null, null, 1 },
                    { 56, "ملکان", false, null, null, null, 1 },
                    { 55, "سراب", false, null, null, null, 1 },
                    { 54, "شبستر", false, null, null, null, 1 },
                    { 53, "آذرشهر", false, null, null, null, 1 },
                    { 24, "خواجه", false, null, null, null, 1 },
                    { 52, "خداجو", false, null, null, null, 1 },
                    { 50, "خاروانا", false, null, null, null, 1 },
                    { 49, "یامچی", false, null, null, null, 1 },
                    { 48, "هریس", false, null, null, null, 1 },
                    { 47, "آچاچی", false, null, null, null, 1 },
                    { 46, "جلفا", false, null, null, null, 1 },
                    { 45, "تبریز", false, null, null, null, 1 },
                    { 44, "بستان آباد", false, null, null, null, 1 },
                    { 51, "کوزه کنان", false, null, null, null, 1 },
                    { 60, "ترک", false, null, null, null, 1 },
                    { 23, "خمارلو", false, null, null, null, 1 },
                    { 21, "سیه رود", false, null, null, null, 1 },
                    { 1319, "یزد", false, null, null, null, 31 },
                    { 1320, "عقدا", false, null, null, null, 31 },
                    { 1321, "بهاباد", false, null, null, null, 31 },
                    { 1322, "ابرکوه", false, null, null, null, 31 },
                    { 1323, "زارچ", false, null, null, null, 31 },
                    { 1324, "نیر", false, null, null, null, 31 },
                    { 1325, "اردکان", false, null, null, null, 31 },
                    { 1326, "هرات", false, null, null, null, 31 },
                    { 1327, "بفروییه", false, null, null, null, 31 },
                    { 1328, "شاهدیه", false, null, null, null, 31 },
                    { 1329, "بافق", false, null, null, null, 31 },
                    { 1330, "خضرآباد", false, null, null, null, 31 },
                    { 1331, "میبد", false, null, null, null, 31 },
                    { 1332, "مهریز", false, null, null, null, 31 },
                    { 1333, "احمدآباد", false, null, null, null, 31 },
                    { 1, "کشکسرای", false, null, null, null, 1 },
                    { 2, "سهند", false, null, null, null, 1 },
                    { 3, "سیس", false, null, null, null, 1 },
                    { 4, "دوزدوزان", false, null, null, null, 1 },
                    { 20, "باسمنج", false, null, null, null, 1 },
                    { 19, "تیکمه داش", false, null, null, null, 1 },
                    { 18, "مبارک شهر", false, null, null, null, 1 },
                    { 17, "مهربان", false, null, null, null, 1 },
                    { 16, "شزفخانه", false, null, null, null, 1 },
                    { 15, "ایخچی", false, null, null, null, 1 },
                    { 14, "زنوز", false, null, null, null, 1 },
                    { 22, "میانه", false, null, null, null, 1 },
                    { 13, "تسوج", false, null, null, null, 1 },
                    { 11, "ترکمانچای", false, null, null, null, 1 },
                    { 10, "زرنق", false, null, null, null, 1 },
                    { 9, "هشترود", false, null, null, null, 1 },
                    { 8, "هادیشهر", false, null, null, null, 1 },
                    { 7, "سردرود", false, null, null, null, 1 },
                    { 6, "صوفیان", false, null, null, null, 1 },
                    { 5, "تیمورلو", false, null, null, null, 1 },
                    { 12, "ورزقان", false, null, null, null, 1 },
                    { 383, "کاکی", false, null, null, null, 7 },
                    { 61, "عجب شیر", false, null, null, null, 1 },
                    { 63, "تازه شهر", false, null, null, null, 2 },
                    { 117, "خلخال", false, null, null, null, 3 },
                    { 116, "مرادلو", false, null, null, null, 3 },
                    { 115, "اصلاندوز", false, null, null, null, 3 },
                    { 114, "نمین", false, null, null, null, 3 },
                    { 113, "جعفر آباد", false, null, null, null, 3 },
                    { 112, "مشگین شهر", false, null, null, null, 3 },
                    { 111, "تازه کندانگوت", false, null, null, null, 3 },
                    { 110, "اسلام آباد", false, null, null, null, 3 },
                    { 109, "اردبیل", false, null, null, null, 3 },
                    { 108, "نیر", false, null, null, null, 3 },
                    { 107, "کلور", false, null, null, null, 3 },
                    { 106, "فخر آباد", false, null, null, null, 3 },
                    { 105, "پارس آباد", false, null, null, null, 3 },
                    { 104, "قره ضیاءالدین", false, null, null, null, 2 },
                    { 103, "مهاباد", false, null, null, null, 2 },
                    { 118, "کوراییم", false, null, null, null, 3 },
                    { 119, "هیر", false, null, null, null, 3 },
                    { 120, "گیوی", false, null, null, null, 3 },
                    { 121, "گرمی", false, null, null, null, 3 },
                    { 137, "سجزی", false, null, null, null, 4 },
                    { 136, "خالدآباد", false, null, null, null, 4 },
                    { 135, "پیربکران", false, null, null, null, 4 },
                    { 134, "گلشن", false, null, null, null, 4 },
                    { 133, "زرین شهر", false, null, null, null, 4 },
                    { 132, "زیار", false, null, null, null, 4 },
                    { 131, "آبی بیگلو", false, null, null, null, 4 },
                    { 102, "ارومیه", false, null, null, null, 2 },
                    { 130, "آبی بیگلو", false, null, null, null, 3 },
                    { 128, "سرعین", false, null, null, null, 3 },
                    { 127, "رضی", false, null, null, null, 3 },
                    { 126, "قصابه", false, null, null, null, 3 },
                    { 125, "تازه کند", false, null, null, null, 3 },
                    { 124, "عنبران", false, null, null, null, 3 },
                    { 123, "هشتجین", false, null, null, null, 3 },
                    { 122, "لاهرود", false, null, null, null, 3 },
                    { 129, "بیله سوار", false, null, null, null, 3 },
                    { 62, "آبش احمد", false, null, null, null, 1 },
                    { 101, "محمدیار", false, null, null, null, 2 },
                    { 99, "کشاورز", false, null, null, null, 2 },
                    { 78, "مرگنلر", false, null, null, null, 2 },
                    { 77, "میاندوآب", false, null, null, null, 2 },
                    { 76, "نوشین", false, null, null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateId" },
                values: new object[,]
                {
                    { 75, "سیمینه", false, null, null, null, 2 },
                    { 74, "دیزج دیز", false, null, null, null, 2 },
                    { 73, "تکاب", false, null, null, null, 2 },
                    { 72, "ربط", false, null, null, null, 2 },
                    { 71, "نازک علیا", false, null, null, null, 2 },
                    { 70, "بازرگان", false, null, null, null, 2 },
                    { 69, "سیلوانه", false, null, null, null, 2 },
                    { 68, "باروق", false, null, null, null, 2 },
                    { 67, "گردکشانه", false, null, null, null, 2 },
                    { 66, "شاهین دژ", false, null, null, null, 2 },
                    { 65, "ایواوغلی", false, null, null, null, 2 },
                    { 64, "نالوس", false, null, null, null, 2 },
                    { 79, "سلماس", false, null, null, null, 2 },
                    { 80, "آواجیق", false, null, null, null, 2 },
                    { 81, "قطور", false, null, null, null, 2 },
                    { 82, "محمودآباد", false, null, null, null, 2 },
                    { 98, "سردشت", false, null, null, null, 2 },
                    { 97, "سیه چشمه", false, null, null, null, 2 },
                    { 96, "ماکو", false, null, null, null, 2 },
                    { 95, "شوط", false, null, null, null, 2 },
                    { 94, "قوشچی", false, null, null, null, 2 },
                    { 93, "چهاربرج", false, null, null, null, 2 },
                    { 92, "پیرانشهر", false, null, null, null, 2 },
                    { 100, "فیرورق", false, null, null, null, 2 },
                    { 91, "بوکان", false, null, null, null, 2 },
                    { 89, "اشنویه", false, null, null, null, 2 },
                    { 88, "میرآباد", false, null, null, null, 2 },
                    { 87, "پلدشت", false, null, null, null, 2 },
                    { 86, "خلیفان", false, null, null, null, 2 },
                    { 85, "سرو", false, null, null, null, 2 },
                    { 84, "نقده", false, null, null, null, 2 },
                    { 83, "خوی", false, null, null, null, 2 },
                    { 90, "زرآباد", false, null, null, null, 2 },
                    { 1316, "تفت", false, null, null, null, 31 },
                    { 384, "جم", false, null, null, null, 7 },
                    { 386, "بندر گناوه", false, null, null, null, 7 },
                    { 596, "گرمه", false, null, null, null, 12 },
                    { 595, "اسفراین", false, null, null, null, 12 },
                    { 594, "شوقان", false, null, null, null, 12 },
                    { 593, "قوشخانه", false, null, null, null, 12 },
                    { 592, "پیش قلعه", false, null, null, null, 12 },
                    { 591, "راز", false, null, null, null, 12 },
                    { 590, "چناران شهر", false, null, null, null, 12 },
                    { 589, "خلیل آباد", false, null, null, null, 11 },
                    { 588, "روداب", false, null, null, null, 11 },
                    { 587, "خواف", false, null, null, null, 11 },
                    { 586, "طرقبه", false, null, null, null, 11 },
                    { 585, "ریوش", false, null, null, null, 11 },
                    { 584, "سرخس", false, null, null, null, 11 },
                    { 583, "دولت آباد", false, null, null, null, 11 },
                    { 582, "بایک", false, null, null, null, 11 },
                    { 597, "قاضی", false, null, null, null, 12 },
                    { 581, "ملک آباد", false, null, null, null, 11 },
                    { 598, "شیروان", false, null, null, null, 12 },
                    { 600, "آشخانه", false, null, null, null, 12 },
                    { 615, "حمزه", false, null, null, null, 13 },
                    { 614, "شاوور", false, null, null, null, 13 },
                    { 613, "بیدروبه", false, null, null, null, 13 },
                    { 612, "هفتگل", false, null, null, null, 13 },
                    { 611, "لوجلی", false, null, null, null, 12 },
                    { 610, "فاروج", false, null, null, null, 12 },
                    { 609, "ایور", false, null, null, null, 12 },
                    { 608, "صفی آباد", false, null, null, null, 12 },
                    { 607, "سنخواست", false, null, null, null, 12 },
                    { 606, "زیارت", false, null, null, null, 12 },
                    { 605, "آوا", false, null, null, null, 12 },
                    { 604, "درق", false, null, null, null, 12 },
                    { 603, "بجنورد", false, null, null, null, 12 },
                    { 602, "جاجرم", false, null, null, null, 12 },
                    { 601, "تیتکانلو", false, null, null, null, 12 },
                    { 599, "خصار گرمخان", false, null, null, null, 12 },
                    { 580, "انابد", false, null, null, null, 11 },
                    { 579, "تربت جام", false, null, null, null, 11 },
                    { 578, "خرو", false, null, null, null, 11 },
                    { 557, "قوچان", false, null, null, null, 11 },
                    { 556, "مزدآوند", false, null, null, null, 11 },
                    { 555, "جغتای", false, null, null, null, 11 },
                    { 554, "مشهدریزه", false, null, null, null, 11 },
                    { 553, "کاخک", false, null, null, null, 11 },
                    { 552, "فرهادگاه", false, null, null, null, 11 },
                    { 551, "داورزن", false, null, null, null, 11 },
                    { 550, "صالح آباد", false, null, null, null, 11 },
                    { 549, "قدمگاه", false, null, null, null, 11 },
                    { 548, "رشتخوار", false, null, null, null, 11 },
                    { 547, "چاپشلو", false, null, null, null, 11 },
                    { 546, "عشق آباد", false, null, null, null, 11 },
                    { 545, "شادمهر", false, null, null, null, 11 },
                    { 544, "ششتمد", false, null, null, null, 11 },
                    { 543, "نشتیفان", false, null, null, null, 11 },
                    { 558, "یونسی", false, null, null, null, 11 },
                    { 559, "سنگان", false, null, null, null, 11 },
                    { 560, "نوخندان", false, null, null, null, 11 },
                    { 561, "کندر", false, null, null, null, 11 },
                    { 577, "شهرزو", false, null, null, null, 11 },
                    { 576, "لطف آباد", false, null, null, null, 11 },
                    { 575, "گلمکان", false, null, null, null, 11 },
                    { 574, "فیض آباد", false, null, null, null, 11 },
                    { 573, "سبزوار", false, null, null, null, 11 },
                    { 572, "قاسم آباد", false, null, null, null, 11 },
                    { 571, "فیروزه", false, null, null, null, 11 },
                    { 616, "گتوند", false, null, null, null, 13 },
                    { 570, "تایباد", false, null, null, null, 11 },
                    { 568, "سفید سنگ", false, null, null, null, 11 },
                    { 567, "باخرز", false, null, null, null, 11 },
                    { 566, "تربت حیدریه", false, null, null, null, 11 },
                    { 565, "رضویه", false, null, null, null, 11 },
                    { 564, "شهرآباد", false, null, null, null, 11 },
                    { 563, "احمدآباد صولت", false, null, null, null, 11 },
                    { 562, "نیشابور", false, null, null, null, 11 },
                    { 569, "بیدخت", false, null, null, null, 11 },
                    { 617, "شرافت", false, null, null, null, 13 },
                    { 618, "منصوریه", false, null, null, null, 13 },
                    { 619, "زهره", false, null, null, null, 13 },
                    { 673, "آزادی", false, null, null, null, 13 },
                    { 672, "هویزه", false, null, null, null, 13 },
                    { 671, "سیاه منصور", false, null, null, null, 13 },
                    { 670, "صیدون", false, null, null, null, 13 },
                    { 669, "ایذه", false, null, null, null, 13 },
                    { 668, "آغاجاری", false, null, null, null, 13 },
                    { 667, "ابوحمیظه", false, null, null, null, 13 },
                    { 666, "هندیجان", false, null, null, null, 13 },
                    { 665, "بهبهان", false, null, null, null, 13 },
                    { 664, "شوشتر", false, null, null, null, 13 },
                    { 663, "سماله", false, null, null, null, 13 },
                    { 662, "مینوشهر", false, null, null, null, 13 },
                    { 661, "گلگیر", false, null, null, null, 13 },
                    { 660, "حسینیه", false, null, null, null, 13 },
                    { 659, "قلعه خواجه", false, null, null, null, 13 },
                    { 674, "شوش", false, null, null, null, 13 },
                    { 675, "دزفول", false, null, null, null, 13 },
                    { 676, "جنت مکان", false, null, null, null, 13 },
                    { 677, "آبادان", false, null, null, null, 13 },
                    { 693, "کرسف", false, null, null, null, 14 },
                    { 692, "ارمغانخانه", false, null, null, null, 14 },
                    { 691, "آب بر", false, null, null, null, 14 },
                    { 690, "زرین رود", false, null, null, null, 14 },
                    { 689, "سجاس", false, null, null, null, 14 },
                    { 688, "صفی آباد", false, null, null, null, 13 },
                    { 687, "باغ ملک", false, null, null, null, 13 },
                    { 658, "شهر امام", false, null, null, null, 13 },
                    { 686, "الهایی", false, null, null, null, 13 },
                    { 684, "سوسنگرد", false, null, null, null, 13 },
                    { 683, "امیدیه", false, null, null, null, 13 },
                    { 682, "چمران", false, null, null, null, 13 },
                    { 681, "خنافره", false, null, null, null, 13 },
                    { 680, "مشراگه", false, null, null, null, 13 },
                    { 679, "خرمشهر", false, null, null, null, 13 },
                    { 678, "گوریه", false, null, null, null, 13 },
                    { 685, "شیبان", false, null, null, null, 13 },
                    { 542, "شاندیز", false, null, null, null, 11 },
                    { 657, "فتح المبین", false, null, null, null, 13 },
                    { 655, "ویس", false, null, null, null, 13 },
                    { 634, "دارخوین", false, null, null, null, 13 },
                    { 633, "ترکالکی", false, null, null, null, 13 },
                    { 632, "مقاومت", false, null, null, null, 13 },
                    { 631, "مسجد سلیمان", false, null, null, null, 13 },
                    { 630, "چوبیده", false, null, null, null, 13 },
                    { 629, "آبژدان", false, null, null, null, 13 },
                    { 628, "شمس آباد", false, null, null, null, 13 },
                    { 627, "حر", false, null, null, null, 13 },
                    { 626, "چم گلک", false, null, null, null, 13 },
                    { 625, "ملاثانی", false, null, null, null, 13 },
                    { 624, "چغامیش", false, null, null, null, 13 },
                    { 623, "میداود", false, null, null, null, 13 },
                    { 622, "کوت عبداله", false, null, null, null, 13 },
                    { 621, "بندر امام خمینی", false, null, null, null, 13 },
                    { 620, "رامهرمز", false, null, null, null, 13 },
                    { 635, "سردشت", false, null, null, null, 13 },
                    { 636, "لالی", false, null, null, null, 13 },
                    { 637, "کوت سید نعیم", false, null, null, null, 13 },
                    { 638, "حمیدیه", false, null, null, null, 13 },
                    { 654, "بستان", false, null, null, null, 13 },
                    { 653, "جایزان", false, null, null, null, 13 },
                    { 652, "بندر ماهشهر", false, null, null, null, 13 },
                    { 651, "شادگان", false, null, null, null, 13 },
                    { 650, "رامشیر", false, null, null, null, 13 },
                    { 649, "تشان", false, null, null, null, 13 },
                    { 648, "سرداران", false, null, null, null, 13 },
                    { 656, "اهواز", false, null, null, null, 13 },
                    { 647, "اروندکنار", false, null, null, null, 13 },
                    { 645, "سالند", false, null, null, null, 13 },
                    { 644, "الوان", false, null, null, null, 13 },
                    { 643, "اندیمشک", false, null, null, null, 13 },
                    { 642, "رفیع", false, null, null, null, 13 },
                    { 641, "میانرود", false, null, null, null, 13 },
                    { 640, "قلعه تل", false, null, null, null, 13 },
                    { 639, "دهدز", false, null, null, null, 13 },
                    { 646, "صالح شهر", false, null, null, null, 13 },
                    { 541, "کاشمر", false, null, null, null, 11 },
                    { 540, "قلندرآباد", false, null, null, null, 11 },
                    { 539, "نقاب", false, null, null, null, 11 },
                    { 440, "باغستان", false, null, null, null, 8 },
                    { 439, "آبسرد", false, null, null, null, 8 },
                    { 438, "رباط کریم", false, null, null, null, 8 },
                    { 437, "کهریزک", false, null, null, null, 8 },
                    { 436, "فرون آباد", false, null, null, null, 8 },
                    { 435, "لواسان", false, null, null, null, 8 },
                    { 434, "صفادشت", false, null, null, null, 8 },
                    { 433, "وحیدیه", false, null, null, null, 8 },
                    { 432, "بومهن", false, null, null, null, 8 },
                    { 431, "تهران", false, null, null, null, 8 },
                    { 430, "چهاردانگه", false, null, null, null, 8 },
                    { 429, "آبعلی", false, null, null, null, 8 },
                    { 428, "پرند", false, null, null, null, 8 },
                    { 427, "فشم", false, null, null, null, 8 },
                    { 426, "فیروزکوه", false, null, null, null, 8 },
                    { 441, "صالحیه", false, null, null, null, 8 },
                    { 442, "شهریار", false, null, null, null, 8 },
                    { 443, "قدس", false, null, null, null, 8 },
                    { 444, "تجریش", false, null, null, null, 8 },
                    { 460, "فرخ شهر", false, null, null, null, 9 },
                    { 459, "سامان", false, null, null, null, 9 },
                    { 458, "پردنجان", false, null, null, null, 9 },
                    { 457, "بروجن", false, null, null, null, 9 },
                    { 456, "منج", false, null, null, null, 9 },
                    { 455, "شهرکرد", false, null, null, null, 9 },
                    { 454, "سرخون", false, null, null, null, 9 },
                    { 425, "ورامین", false, null, null, null, 8 },
                    { 453, "سورشجان", false, null, null, null, 9 },
                    { 451, "گوجان", false, null, null, null, 9 },
                    { 450, "وردنجان", false, null, null, null, 9 },
                    { 449, "پردیس", false, null, null, null, 8 },
                    { 448, "دماوند", false, null, null, null, 8 },
                    { 447, "اسلامشهر", false, null, null, null, 8 },
                    { 446, "حسن آباد", false, null, null, null, 8 },
                    { 445, "شریف آباد", false, null, null, null, 8 },
                    { 452, "گهرو", false, null, null, null, 9 },
                    { 461, "صمصامی", false, null, null, null, 9 },
                    { 424, "گلستان", false, null, null, null, 8 },
                    { 422, "قرچک", false, null, null, null, 8 },
                    { 401, "سعد آباد", false, null, null, null, 7 },
                    { 400, "امام حسن", false, null, null, null, 7 },
                    { 399, "تنگ ارم", false, null, null, null, 7 },
                    { 398, "عسلویه", false, null, null, null, 7 },
                    { 397, "بادوله", false, null, null, null, 7 },
                    { 396, "بردستان", false, null, null, null, 7 },
                    { 395, "دلوار", false, null, null, null, 7 },
                    { 394, "سیراف", false, null, null, null, 7 },
                    { 393, "شبانکاره", false, null, null, null, 7 },
                    { 392, "انارستان", false, null, null, null, 7 },
                    { 391, "بوشکان", false, null, null, null, 7 },
                    { 390, "شنبه", false, null, null, null, 7 },
                    { 389, "خارک", false, null, null, null, 7 },
                    { 388, "آبدان", false, null, null, null, 7 },
                    { 387, "آباد", false, null, null, null, 7 },
                    { 402, "بندر کنگان", false, null, null, null, 7 },
                    { 403, "بوشهر", false, null, null, null, 7 },
                    { 404, "بردخون", false, null, null, null, 7 },
                    { 405, "آب پخش", false, null, null, null, 7 },
                    { 421, "کیلان", false, null, null, null, 8 },
                    { 420, "احمدآباد مستوفی", false, null, null, null, 8 },
                    { 419, "باقرشهر", false, null, null, null, 8 },
                    { 418, "پاکدشت", false, null, null, null, 8 },
                    { 417, "شمشک", false, null, null, null, 8 },
                    { 416, "ملارد", false, null, null, null, 8 },
                    { 415, "صبا شهر", false, null, null, null, 8 },
                    { 423, "فردوسیه", false, null, null, null, 8 },
                    { 414, "نسیم شهر", false, null, null, null, 8 },
                    { 412, "رودهن", false, null, null, null, 8 },
                    { 411, "نصیر شهر", false, null, null, null, 8 },
                    { 410, "ری", false, null, null, null, 8 },
                    { 409, "ارجمند", false, null, null, null, 8 },
                    { 408, "جوادآباد", false, null, null, null, 8 },
                    { 407, "پیشوا", false, null, null, null, 8 },
                    { 406, "شاهدشهر", false, null, null, null, 8 },
                    { 413, "اندیشه", false, null, null, null, 8 },
                    { 385, "دالکی", false, null, null, null, 7 },
                    { 462, "طاقانک", false, null, null, null, 9 },
                    { 464, "نقنه", false, null, null, null, 9 },
                    { 518, "بار", false, null, null, null, 11 },
                    { 517, "زهان", false, null, null, null, 10 },
                    { 516, "اسدیه", false, null, null, null, 10 },
                    { 515, "طبس", false, null, null, null, 10 },
                    { 514, "خضری دشت بیاض", false, null, null, null, 10 },
                    { 513, "سرایان", false, null, null, null, 10 },
                    { 512, "بشرویه", false, null, null, null, 10 },
                    { 511, "قهستان", false, null, null, null, 10 },
                    { 510, "خوسف", false, null, null, null, 10 },
                    { 509, "مود", false, null, null, null, 10 },
                    { 508, "آرین شهر", false, null, null, null, 10 },
                    { 507, "سه قلعه", false, null, null, null, 10 },
                    { 506, "حاجی آباد", false, null, null, null, 10 },
                    { 505, "گزیک", false, null, null, null, 10 },
                    { 504, "اسفدن", false, null, null, null, 10 },
                    { 519, "نیل شهر", false, null, null, null, 11 },
                    { 520, "جنگل", false, null, null, null, 11 },
                    { 521, "درود", false, null, null, null, 11 },
                    { 522, "رباط سنگ", false, null, null, null, 11 },
                    { 538, "کدکن", false, null, null, null, 11 },
                    { 537, "مشهد", false, null, null, null, 11 },
                    { 536, "بردسکن", false, null, null, null, 11 },
                    { 535, "نصرآباد", false, null, null, null, 11 },
                    { 534, "چکنه", false, null, null, null, 11 },
                    { 533, "کلات", false, null, null, null, 11 },
                    { 532, "درگز", false, null, null, null, 11 },
                    { 503, "نهبندان", false, null, null, null, 10 },
                    { 531, "چناران", false, null, null, null, 11 },
                    { 529, "باجگیران", false, null, null, null, 11 },
                    { 528, "سلامی", false, null, null, null, 11 }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "StateId" },
                values: new object[,]
                {
                    { 527, "همت آباد", false, null, null, null, 11 },
                    { 526, "کاریز", false, null, null, null, 11 },
                    { 525, "گناباد", false, null, null, null, 11 },
                    { 524, "فریمان", false, null, null, null, 11 },
                    { 523, "سلطان آباد", false, null, null, null, 11 },
                    { 530, "بجستان", false, null, null, null, 11 },
                    { 463, "کاج", false, null, null, null, 9 },
                    { 502, "فردوس", false, null, null, null, 10 },
                    { 500, "محمدشهر", false, null, null, null, 10 },
                    { 479, "بلداجی", false, null, null, null, 9 },
                    { 478, "دشتک", false, null, null, null, 9 },
                    { 477, "نافچ", false, null, null, null, 9 },
                    { 476, "شلمزار", false, null, null, null, 9 },
                    { 475, "فارسان", false, null, null, null, 9 },
                    { 474, "بن", false, null, null, null, 9 },
                    { 473, "چلیچه", false, null, null, null, 9 },
                    { 472, "فرادنبه", false, null, null, null, 9 },
                    { 471, "سردشت", false, null, null, null, 9 },
                    { 470, "هفشجان", false, null, null, null, 9 },
                    { 469, "بازفت", false, null, null, null, 9 },
                    { 468, "سودجان", false, null, null, null, 9 },
                    { 467, "دستنا", false, null, null, null, 9 },
                    { 466, "باباحیدر", false, null, null, null, 9 },
                    { 465, "لردگان", false, null, null, null, 9 },
                    { 480, "آلونی", false, null, null, null, 9 },
                    { 481, "گندمان", false, null, null, null, 9 },
                    { 482, "جونقان", false, null, null, null, 9 },
                    { 483, "ناغان", false, null, null, null, 9 },
                    { 499, "سر بیشه", false, null, null, null, 10 },
                    { 498, "دیهوک", false, null, null, null, 10 },
                    { 497, "نیمبلوک", false, null, null, null, 10 },
                    { 496, "آیسک", false, null, null, null, 10 },
                    { 495, "ارسک", false, null, null, null, 10 },
                    { 494, "طبس مسینا", false, null, null, null, 10 },
                    { 493, "عشق آباد", false, null, null, null, 10 },
                    { 501, "بیرجند", false, null, null, null, 10 },
                    { 492, "قاین", false, null, null, null, 10 },
                    { 490, "اسلامیه", false, null, null, null, 10 },
                    { 489, "مال خلیفه", false, null, null, null, 9 },
                    { 488, "سفیددشت", false, null, null, null, 9 },
                    { 487, "اردل", false, null, null, null, 9 },
                    { 486, "کیان", false, null, null, null, 9 },
                    { 485, "چلگرد", false, null, null, null, 9 },
                    { 484, "هارونی", false, null, null, null, 9 },
                    { 491, "شوسف", false, null, null, null, 10 },
                    { 1317, "اشکذر", false, null, null, null, 31 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "OP_Create", "OP_Remove", "OP_Update", "ParentId", "PermissionTitle" },
                values: new object[,]
                {
                    { 11, null, null, null, 1, "کاربران" },
                    { 26, null, null, null, 1, "تخفیف ها" },
                    { 12, null, null, null, 1, "نقشها و دسترسی ها" },
                    { 13, null, null, null, 1, "گروه های آموزش" },
                    { 14, null, null, null, 1, "دوره های آموزش" },
                    { 15, null, null, null, 1, "اخبار" },
                    { 16, null, null, null, 1, "اسلایدر" },
                    { 18, null, null, null, 1, "صفحه درباره ما" },
                    { 17, null, null, null, 1, "جدول زمانی" },
                    { 20, null, null, null, 1, "اطلاعات کلی سایت" },
                    { 21, null, null, null, 1, "اینستاگرام" },
                    { 22, null, null, null, 1, "سوالات متداول" },
                    { 23, null, null, null, 1, "اطلاعات صفحات" },
                    { 24, null, null, null, 1, "اطلاعات پکیج ها" },
                    { 25, null, null, null, 1, "جداکننده ها" },
                    { 19, null, null, null, 1, "اطلاعات تماس" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "OP_Create", "OP_Remove", "OP_Update", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 3, null, null, null, 3, 1 },
                    { 1, null, null, null, 1, 1 },
                    { 2, null, null, null, 2, 1 },
                    { 4, null, null, null, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "OP_Create", "OP_Remove", "OP_Update", "ParentId", "PermissionTitle" },
                values: new object[,]
                {
                    { 51, null, null, null, 11, "لیست کاربران" },
                    { 127, null, null, null, 23, "لیست" },
                    { 126, null, null, null, 22, "حذف" },
                    { 125, null, null, null, 22, "جزئیات" },
                    { 124, null, null, null, 22, "ویرایش" },
                    { 123, null, null, null, 22, "ثبت" },
                    { 122, null, null, null, 22, "لیست" },
                    { 121, null, null, null, 21, "حذف" },
                    { 120, null, null, null, 21, "ویرایش" },
                    { 119, null, null, null, 21, "ثبت" },
                    { 118, null, null, null, 21, "لیست" },
                    { 117, null, null, null, 20, "حذف" },
                    { 128, null, null, null, 23, "ثبت" },
                    { 116, null, null, null, 20, "جزئیات" },
                    { 114, null, null, null, 20, "ثبت" },
                    { 113, null, null, null, 20, "لیست" },
                    { 112, null, null, null, 19, "حذف" },
                    { 111, null, null, null, 19, "جزئیات" },
                    { 110, null, null, null, 19, "ویرایش" },
                    { 109, null, null, null, 19, "ثبت" },
                    { 108, null, null, null, 19, "لیست" },
                    { 107, null, null, null, 18, "حذف" },
                    { 106, null, null, null, 18, "جزئیات" },
                    { 157, null, null, null, 26, "فعال/غیرفعال کردن تخفیف ثابت" },
                    { 104, null, null, null, 18, "ثبت" },
                    { 115, null, null, null, 20, "ویرایش" },
                    { 129, null, null, null, 23, "ویرایش" },
                    { 130, null, null, null, 24, "لیست" },
                    { 131, null, null, null, 24, "ثبت" },
                    { 156, null, null, null, 26, "جزئیات تخفیف ثابت" },
                    { 155, null, null, null, 26, "ویرایش تخفیف ثابت" },
                    { 154, null, null, null, 26, "ثبت تخفیف ثابت" },
                    { 153, null, null, null, 26, "لیست تخفیفهای ثابت" },
                    { 152, null, null, null, 26, "حذف ردیف تخفیف پلکانی" },
                    { 151, null, null, null, 26, "جزئیات ردیف تخفیف پلکانی" },
                    { 150, null, null, null, 26, "ویرایش ردیف تخفیف پلکانی" },
                    { 149, null, null, null, 26, "ثبت ردیف تخفیف پلکانی" },
                    { 148, null, null, null, 26, "لیست ردیفهای تخفیف پلکانی" },
                    { 147, null, null, null, 26, "حذف تخفیف پلکانی" },
                    { 146, null, null, null, 26, "جزئیات تخفیف پلکانی" },
                    { 145, null, null, null, 26, "ویرایش تخفیف پلکانی" },
                    { 144, null, null, null, 26, "ثبت تخفیف پلکانی" },
                    { 143, null, null, null, 26, "لیست تخفیفهای پلکانی" },
                    { 142, null, null, null, 26, "ثبت نوع تخفیف پلکانی" },
                    { 141, null, null, null, 26, "حذف نوع تخفیف پلکانی" },
                    { 140, null, null, null, 26, "ویرایش نوع تخفیف پلکانی" },
                    { 139, null, null, null, 26, "ثبت نوع تخفیف پلکانی" },
                    { 138, null, null, null, 26, "لیست انواع تخفیف پلکانی " },
                    { 137, null, null, null, 25, "حذف" },
                    { 136, null, null, null, 25, "ویرایش" },
                    { 135, null, null, null, 25, "ثبت" },
                    { 134, null, null, null, 25, "لیست" },
                    { 133, null, null, null, 24, "حذف" },
                    { 132, null, null, null, 24, "ویرایش" },
                    { 103, null, null, null, 18, "لیست" },
                    { 102, null, null, null, 17, "حذف برنامه" },
                    { 105, null, null, null, 18, "ویرایش" },
                    { 100, null, null, null, 17, "ویرایش برنامه" },
                    { 101, null, null, null, 17, "جزئیات برنامه" },
                    { 75, null, null, null, 15, "ویرایش گروه خبر" },
                    { 74, null, null, null, 15, "ثبت گروه خبر" },
                    { 73, null, null, null, 15, "لیست گروههای خبر" },
                    { 72, null, null, null, 14, "حذف فراگیر از دوره" },
                    { 71, null, null, null, 14, "حذف فایل" },
                    { 70, null, null, null, 14, "ویرایش فایل" },
                    { 69, null, null, null, 14, "افزودن فایل" },
                    { 68, null, null, null, 14, "حذف دوره" },
                    { 67, null, null, null, 14, "جزئیات دوره" },
                    { 66, null, null, null, 14, "ویرایش دوره" },
                    { 65, null, null, null, 14, "ثبت دوره" },
                    { 64, null, null, null, 14, "لیست دوره ها" },
                    { 63, null, null, null, 13, "حذف گروه " },
                    { 62, null, null, null, 13, "ویرایش گروه " },
                    { 61, null, null, null, 13, "ایجاد گروه " },
                    { 60, null, null, null, 13, "لیست گروه ها" },
                    { 59, null, null, null, 12, "لیست دسترسی ها" },
                    { 58, null, null, null, 12, "مشاهده کاربران نقش" },
                    { 57, null, null, null, 12, "ویرایش نقش" },
                    { 56, null, null, null, 12, "حــذف نقش" },
                    { 55, null, null, null, 12, "ویرایش نقش" },
                    { 54, null, null, null, 12, "ایجاد نقش" },
                    { 53, null, null, null, 12, "لیست نقشها" },
                    { 52, null, null, null, 11, "ویرایش کاربر" },
                    { 77, null, null, null, 15, "لیست ناشران خبر" },
                    { 78, null, null, null, 15, "ثبت ناشر" },
                    { 76, null, null, null, 15, "حذف گروه خبر" },
                    { 80, null, null, null, 15, "حذف ناشر" },
                    { 99, null, null, null, 17, "ثبت برنامه" },
                    { 98, null, null, null, 17, "لیست برنامه ها" },
                    { 97, null, null, null, 17, "حذف جدول" },
                    { 96, null, null, null, 17, "جزئیات جدول" },
                    { 95, null, null, null, 17, "ویرایش جدول" },
                    { 94, null, null, null, 17, "ثبت جدول" },
                    { 93, null, null, null, 17, "لیست جدول" },
                    { 92, null, null, null, 16, "حذف اسلایدر" },
                    { 91, null, null, null, 16, "جزئیات اسلایدر" },
                    { 79, null, null, null, 15, "ویرایش ناشر" },
                    { 89, null, null, null, 16, "ثبت اسلایدر" },
                    { 88, null, null, null, 16, "لیست اسلایدرها" },
                    { 90, null, null, null, 16, "ویرایش اسلایدر" },
                    { 86, null, null, null, 15, "افزودن فایل" },
                    { 81, null, null, null, 15, "لیست اخبار" },
                    { 82, null, null, null, 15, "ثبت خبر" },
                    { 85, null, null, null, 15, "حذف خبر" },
                    { 84, null, null, null, 15, "جزئیات خبر" },
                    { 87, null, null, null, 15, "حذف فایل" },
                    { 83, null, null, null, 15, "ویرایش خبر" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "OP_Create", "OP_Remove", "OP_Update", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 9, null, null, null, 15, 1 },
                    { 11, null, null, null, 17, 1 },
                    { 5, null, null, null, 11, 1 },
                    { 16, null, null, null, 22, 1 },
                    { 15, null, null, null, 21, 1 },
                    { 17, null, null, null, 23, 1 },
                    { 6, null, null, null, 12, 1 },
                    { 10, null, null, null, 16, 1 },
                    { 8, null, null, null, 14, 1 },
                    { 7, null, null, null, 13, 1 },
                    { 13, null, null, null, 19, 1 },
                    { 18, null, null, null, 24, 1 },
                    { 14, null, null, null, 20, 1 },
                    { 19, null, null, null, 25, 1 },
                    { 12, null, null, null, 18, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CellphoneConfirmCode", "ChangedPass", "CountyId", "EducationFile", "IsDeleted", "LastPassword", "LevelOfEducation", "OP_Create", "OP_Remove", "OP_Update", "Sky_userId", "UserActiveCode", "UserAvatar", "UserBiography", "UserBirthDate", "UserCellPhone", "UserCellPhoneConfirmed", "UserContractFile", "UserDescription", "UserEmail", "UserFamily", "UserFatherName", "UserFirstName", "UserIsActive", "UserLabel", "UserLevel_Id", "UserNC", "UserNCFile", "UserName", "UserOrgCode", "UserPassword", "UserRegisteredDate", "UserRestAddress", "UserSex", "UserUniversity", "UserYearofGraduataion" },
                values: new object[] { 1, null, false, 330, null, false, "0491579241", "لیسانس", null, null, null, null, "6070ea3c0ebe484e92198fdd1d0a8b12", null, null, "1356/06/31", "09123689294", true, null, null, null, "دانش کار آراسته", "رضا", "فربد", true, null, null, "0491579241", null, "fdaneshkar", null, "6AB7EBCB18754C3BFEF65019EC08A8B9", new DateTime(2021, 11, 26, 21, 29, 0, 388, DateTimeKind.Local).AddTicks(1270), null, "مرد", null, 0 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "OP_Create", "OP_Remove", "OP_Update", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 20, null, null, null, 51, 1 },
                    { 84, null, null, null, 115, 1 },
                    { 83, null, null, null, 114, 1 },
                    { 82, null, null, null, 113, 1 },
                    { 81, null, null, null, 112, 1 },
                    { 80, null, null, null, 111, 1 },
                    { 79, null, null, null, 110, 1 },
                    { 78, null, null, null, 109, 1 },
                    { 77, null, null, null, 108, 1 },
                    { 85, null, null, null, 116, 1 },
                    { 76, null, null, null, 107, 1 },
                    { 74, null, null, null, 105, 1 },
                    { 73, null, null, null, 104, 1 },
                    { 72, null, null, null, 103, 1 },
                    { 71, null, null, null, 102, 1 },
                    { 70, null, null, null, 101, 1 },
                    { 69, null, null, null, 100, 1 },
                    { 68, null, null, null, 99, 1 },
                    { 67, null, null, null, 98, 1 },
                    { 75, null, null, null, 106, 1 },
                    { 66, null, null, null, 97, 1 },
                    { 86, null, null, null, 117, 1 },
                    { 88, null, null, null, 119, 1 },
                    { 106, null, null, null, 137, 1 },
                    { 105, null, null, null, 136, 1 },
                    { 104, null, null, null, 135, 1 },
                    { 103, null, null, null, 134, 1 },
                    { 102, null, null, null, 133, 1 },
                    { 101, null, null, null, 132, 1 },
                    { 100, null, null, null, 131, 1 },
                    { 99, null, null, null, 130, 1 },
                    { 87, null, null, null, 118, 1 },
                    { 98, null, null, null, 129, 1 },
                    { 96, null, null, null, 127, 1 },
                    { 95, null, null, null, 126, 1 },
                    { 94, null, null, null, 125, 1 },
                    { 93, null, null, null, 124, 1 },
                    { 92, null, null, null, 123, 1 },
                    { 91, null, null, null, 122, 1 },
                    { 90, null, null, null, 121, 1 },
                    { 89, null, null, null, 120, 1 },
                    { 97, null, null, null, 128, 1 },
                    { 64, null, null, null, 95, 1 },
                    { 65, null, null, null, 96, 1 },
                    { 62, null, null, null, 93, 1 },
                    { 39, null, null, null, 70, 1 },
                    { 38, null, null, null, 69, 1 },
                    { 37, null, null, null, 68, 1 },
                    { 36, null, null, null, 67, 1 },
                    { 35, null, null, null, 66, 1 },
                    { 34, null, null, null, 65, 1 },
                    { 33, null, null, null, 64, 1 },
                    { 32, null, null, null, 63, 1 },
                    { 31, null, null, null, 62, 1 },
                    { 30, null, null, null, 61, 1 },
                    { 29, null, null, null, 60, 1 },
                    { 28, null, null, null, 59, 1 },
                    { 27, null, null, null, 58, 1 },
                    { 26, null, null, null, 57, 1 },
                    { 25, null, null, null, 56, 1 },
                    { 24, null, null, null, 55, 1 },
                    { 23, null, null, null, 54, 1 },
                    { 22, null, null, null, 53, 1 },
                    { 21, null, null, null, 52, 1 },
                    { 63, null, null, null, 94, 1 },
                    { 41, null, null, null, 72, 1 },
                    { 40, null, null, null, 71, 1 },
                    { 43, null, null, null, 74, 1 },
                    { 61, null, null, null, 92, 1 },
                    { 60, null, null, null, 91, 1 },
                    { 59, null, null, null, 90, 1 },
                    { 58, null, null, null, 89, 1 },
                    { 57, null, null, null, 88, 1 },
                    { 56, null, null, null, 87, 1 },
                    { 55, null, null, null, 86, 1 },
                    { 42, null, null, null, 73, 1 },
                    { 53, null, null, null, 84, 1 },
                    { 54, null, null, null, 85, 1 },
                    { 51, null, null, null, 82, 1 },
                    { 50, null, null, null, 81, 1 },
                    { 49, null, null, null, 80, 1 },
                    { 48, null, null, null, 79, 1 },
                    { 47, null, null, null, 78, 1 },
                    { 46, null, null, null, 77, 1 },
                    { 45, null, null, null, 76, 1 },
                    { 44, null, null, null, 75, 1 },
                    { 52, null, null, null, 83, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "URId", "IsActive", "IsDeleted", "OP_Create", "OP_Remove", "OP_Update", "RegisterDate", "RoleId", "RoomLink", "UserId", "UserRoleCode", "room_id" },
                values: new object[,]
                {
                    { 3, true, false, null, null, null, new DateTime(2021, 11, 26, 21, 29, 0, 389, DateTimeKind.Local).AddTicks(9802), 3, null, 1, 992, null },
                    { 1, true, false, null, null, null, new DateTime(2021, 11, 26, 21, 29, 0, 389, DateTimeKind.Local).AddTicks(9026), 1, null, 1, 990, null },
                    { 2, true, false, null, null, null, new DateTime(2021, 11, 26, 21, 29, 0, 389, DateTimeKind.Local).AddTicks(9769), 2, null, 1, 991, null },
                    { 4, true, false, null, null, null, new DateTime(2021, 11, 26, 21, 29, 0, 389, DateTimeKind.Local).AddTicks(9805), 4, null, 1, 993, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleGroupAG_Id",
                table: "Articles",
                column: "ArticleGroupAG_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_StateId",
                table: "Counties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEpisodes_Course_Id1",
                table: "CourseEpisodes",
                column: "Course_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFiles_Course_Id",
                table: "CourseFiles",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CTM_Id",
                table: "Courses",
                column: "CTM_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseGroup_Id",
                table: "Courses",
                column: "CourseGroup_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevel_Id",
                table: "Courses",
                column: "CourseLevel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatus_Id",
                table: "Courses",
                column: "CourseStatus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStaticDiscounts_Course_Id",
                table: "CourseStaticDiscounts",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStaticDiscounts_SD_Id",
                table: "CourseStaticDiscounts",
                column: "SD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_Course_Id",
                table: "CourseUsers",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_URId",
                table: "CourseUsers",
                column: "URId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_GG_Id",
                table: "Galleries",
                column: "GG_Id");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsGroup_Id",
                table: "News",
                column: "NewsGroup_Id");

            migrationBuilder.CreateIndex(
                name: "IX_News_Publisher_Id",
                table: "News",
                column: "Publisher_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFiles_News_Id",
                table: "NewsFiles",
                column: "News_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SteppedDiscountDetails_StId",
                table: "SteppedDiscountDetails",
                column: "StId");

            migrationBuilder.CreateIndex(
                name: "IX_SteppedDiscounts_TypeId",
                table: "SteppedDiscounts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimelineComponents_TL_Id",
                table: "TimelineComponents",
                column: "TL_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ParentId",
                table: "UserMessages",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_UserRoleURId",
                table: "UserMessages",
                column: "UserRoleURId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleStaticDiscounts_SD_Id",
                table: "UserRoleStaticDiscounts",
                column: "SD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleStaticDiscounts_URId",
                table: "UserRoleStaticDiscounts",
                column: "URId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountyId",
                table: "Users",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLevel_Id",
                table: "Users",
                column: "UserLevel_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aboutes");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "CourseEpisodes");

            migrationBuilder.DropTable(
                name: "CourseFiles");

            migrationBuilder.DropTable(
                name: "CourseStaticDiscounts");

            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.DropTable(
                name: "EmailBanks");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Headers");

            migrationBuilder.DropTable(
                name: "InstaPosts");

            migrationBuilder.DropTable(
                name: "NewsFiles");

            migrationBuilder.DropTable(
                name: "PackInfos");

            migrationBuilder.DropTable(
                name: "PageInfos");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Separators");

            migrationBuilder.DropTable(
                name: "SiteFAQs");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "SteppedDiscountDetails");

            migrationBuilder.DropTable(
                name: "TimelineComponents");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.DropTable(
                name: "UserRoleStaticDiscounts");

            migrationBuilder.DropTable(
                name: "ArticleGroups");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "GalleryGroups");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "SteppedDiscounts");

            migrationBuilder.DropTable(
                name: "Timelines");

            migrationBuilder.DropTable(
                name: "StaticDiscounts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "CourseTypeofMeasurments");

            migrationBuilder.DropTable(
                name: "CourseGroups");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropTable(
                name: "CourseStatuses");

            migrationBuilder.DropTable(
                name: "NewsGroups");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "SteppedDiscountTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
