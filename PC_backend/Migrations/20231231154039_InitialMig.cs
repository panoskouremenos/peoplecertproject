using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PC_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EshopProduct",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AvailableStock = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EshopPro__B40CC6ED8F2F8F97", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RoleDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__8AFACE3A66966242", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Usertbl",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usertbl__1788CCACB137FABB", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Usertbl__RoleID__40F9A68C",
                        column: x => x.RoleID,
                        principalTable: "UserRole",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NativeLanguage = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LandlineNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MobileNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__DF539BFCC8236A3C", x => x.CandidateID);
                    table.ForeignKey(
                        name: "FK__Candidate__UserI__43D61337",
                        column: x => x.UserID,
                        principalTable: "Usertbl",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CandidateAddress",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    AddressLine2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CountryOfResidence = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    StateTerritoryProvince = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TownCity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__091C2A1BF29327D8", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK__Candidate__Candi__498EEC8D",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                });

            migrationBuilder.CreateTable(
                name: "CandidatePhotoID",
                columns: table => new
                {
                    PhotoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    PhotoIDType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PhotoIDNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhotoIDIssueDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__21B7B5824D841CCC", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK__Candidate__Candi__46B27FE2",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    CertificateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    AssessmentTestCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ExaminationDate = table.Column<DateTime>(type: "date", nullable: true),
                    ScoreReportDate = table.Column<DateTime>(type: "date", nullable: true),
                    CandidateScore = table.Column<int>(type: "int", nullable: true),
                    MaximumScore = table.Column<int>(type: "int", nullable: true),
                    PercentageScore = table.Column<double>(type: "float", nullable: true),
                    AssessmentResultLabel = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Certific__BBF8A7E194BC76C0", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK__Certifica__Candi__4C6B5938",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    TestCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateTaken = table.Column<DateTime>(type: "date", nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: true),
                    PassingMark = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exam__297521A754380D3D", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK__Exam__CandidateI__5224328E",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                });

            migrationBuilder.CreateTable(
                name: "ExamVoucher",
                columns: table => new
                {
                    VoucherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsUsed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ExamVouc__3AEE79C17C99702A", x => x.VoucherID);
                    table.ForeignKey(
                        name: "FK__ExamVouch__Candi__625A9A57",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                    table.ForeignKey(
                        name: "FK__ExamVouch__Produ__6166761E",
                        column: x => x.ProductID,
                        principalTable: "EshopProduct",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "CertificateTopicMarks",
                columns: table => new
                {
                    CertificateTopicMarksID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateID = table.Column<int>(type: "int", nullable: true),
                    TopicDesc = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NumberOfAwardedMarks = table.Column<int>(type: "int", nullable: true),
                    NumberOfPossibleMarks = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Certific__2C7F0986A9C2E005", x => x.CertificateTopicMarksID);
                    table.ForeignKey(
                        name: "FK__Certifica__Certi__4F47C5E3",
                        column: x => x.CertificateID,
                        principalTable: "Certificate",
                        principalColumn: "CertificateID");
                });

            migrationBuilder.CreateTable(
                name: "ExamResult",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamID = table.Column<int>(type: "int", nullable: true),
                    CandidateID = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "date", nullable: true),
                    Passed = table.Column<int>(type: "int", nullable: true),
                    CertificateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ExamResu__97690228236B9C66", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK__ExamResul__Candi__5BAD9CC8",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "CandidateID");
                    table.ForeignKey(
                        name: "FK__ExamResul__Certi__5CA1C101",
                        column: x => x.CertificateID,
                        principalTable: "Certificate",
                        principalColumn: "CertificateID");
                    table.ForeignKey(
                        name: "FK__ExamResul__ExamI__5AB9788F",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ExamID");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamID = table.Column<int>(type: "int", nullable: true),
                    QuestionText = table.Column<string>(type: "text", nullable: true),
                    QuestionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06F8C7ACDC3F7", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK__Question__ExamID__55009F39",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ExamID");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: true),
                    AnswerText = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Answer__D482502411C318F2", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK__Answer__Question__57DD0BE4",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateIndex(
                name: "idx_answer_question",
                table: "Answer",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAddress_CandidateID",
                table: "CandidateAddress",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePhotoID_CandidateID",
                table: "CandidatePhotoID",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "idx_candidate_email",
                table: "Candidates",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "idx_candidate_name",
                table: "Candidates",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserID",
                table: "Candidates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "idx_certificate_candidate",
                table: "Certificate",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "idx_certificate_title",
                table: "Certificate",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateTopicMarks_CertificateID",
                table: "CertificateTopicMarks",
                column: "CertificateID");

            migrationBuilder.CreateIndex(
                name: "idx_exam_candidate",
                table: "Exam",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "idx_exam_date",
                table: "Exam",
                column: "DateTaken");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_CandidateID",
                table: "ExamResult",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_CertificateID",
                table: "ExamResult",
                column: "CertificateID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_ExamID",
                table: "ExamResult",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamVoucher_CandidateID",
                table: "ExamVoucher",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamVoucher_ProductID",
                table: "ExamVoucher",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "idx_question_exam",
                table: "Question",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_Usertbl_RoleID",
                table: "Usertbl",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "CandidateAddress");

            migrationBuilder.DropTable(
                name: "CandidatePhotoID");

            migrationBuilder.DropTable(
                name: "CertificateTopicMarks");

            migrationBuilder.DropTable(
                name: "ExamResult");

            migrationBuilder.DropTable(
                name: "ExamVoucher");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "EshopProduct");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Usertbl");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
