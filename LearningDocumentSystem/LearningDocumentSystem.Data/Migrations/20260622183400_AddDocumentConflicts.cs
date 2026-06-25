using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningDocumentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentConflicts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentConflicts",
                columns: table => new
                {
                    ConflictID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    ConflictingDocumentID = table.Column<int>(type: "int", nullable: false),
                    ChunkID = table.Column<int>(type: "int", nullable: false),
                    ConflictingChunkID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConflicts", x => x.ConflictID);
                    table.ForeignKey(
                        name: "FK_DocumentConflicts_DocumentChunks_ChunkID",
                        column: x => x.ChunkID,
                        principalTable: "DocumentChunks",
                        principalColumn: "ChunkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentConflicts_DocumentChunks_ConflictingChunkID",
                        column: x => x.ConflictingChunkID,
                        principalTable: "DocumentChunks",
                        principalColumn: "ChunkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentConflicts_Documents_ConflictingDocumentID",
                        column: x => x.ConflictingDocumentID,
                        principalTable: "Documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentConflicts_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConflicts_ChunkID",
                table: "DocumentConflicts",
                column: "ChunkID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConflicts_ConflictingChunkID",
                table: "DocumentConflicts",
                column: "ConflictingChunkID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConflicts_ConflictingDocumentID",
                table: "DocumentConflicts",
                column: "ConflictingDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConflicts_DocumentID",
                table: "DocumentConflicts",
                column: "DocumentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentConflicts");
        }
    }
}
