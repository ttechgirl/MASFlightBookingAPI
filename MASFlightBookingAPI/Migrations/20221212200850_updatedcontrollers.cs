﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MASFlightBookingAPI.Migrations
{
    public partial class updatedcontrollers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");*/
        }
    }
}
