﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHashedPasswordColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "HashedPassword");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "Password");
        }
    }
}
