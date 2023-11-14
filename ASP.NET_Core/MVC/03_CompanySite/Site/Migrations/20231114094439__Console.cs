using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site.Migrations
{
    /// <inheritdoc />
    public partial class _Console : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "56c7c067-7822-4954-9e23-97d72423d276", "AQAAAAIAAYagAAAAEEKkNwjnGKRdRcT0Zm5sorYoNxyeBDuL1zE4xf87vUR9Qy3FDMrdJGbcgId33y7G/w==" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 44, 39, 383, DateTimeKind.Utc).AddTicks(7847));

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 44, 39, 383, DateTimeKind.Utc).AddTicks(7662));

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 44, 39, 383, DateTimeKind.Utc).AddTicks(7781));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "03a93ed9-49e5-4a4c-8248-ef190dcd81a7", "AQAAAAIAAYagAAAAENK5z3sTkZ9zCt8s0HTK5LRqiXBeAOisjpC49FFLxB3cr0IosVrkjCZ6K0WLfgW/3Q==" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 29, 42, 800, DateTimeKind.Utc).AddTicks(6891));

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 29, 42, 800, DateTimeKind.Utc).AddTicks(6804));

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"),
                column: "DateAdded",
                value: new DateTime(2023, 11, 14, 9, 29, 42, 800, DateTimeKind.Utc).AddTicks(6868));
        }
    }
}
