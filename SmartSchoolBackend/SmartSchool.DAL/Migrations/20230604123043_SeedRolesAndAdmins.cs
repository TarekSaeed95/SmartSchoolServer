using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var Admin = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
       
            };

            var Teacher = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Teacher",
                NormalizedName = "Teacher".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()

            };

            var Parent = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Parent",
                NormalizedName = "Parent".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()

            };

            var Student = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Student",
                NormalizedName = "Student".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()

            };



            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },

               values: new object[] { Admin.Id, Admin.Name, Admin.NormalizedName, Admin.ConcurrencyStamp }
               );

            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },

            values: new object[] { Teacher.Id, Teacher.Name, Teacher.NormalizedName, Teacher.ConcurrencyStamp }
            );

            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },

            values: new object[] { Parent.Id, Parent.Name, Parent.NormalizedName, Parent.ConcurrencyStamp }
            );

            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },

            values: new object[] { Student.Id, Student.Name, Student.NormalizedName, Student.ConcurrencyStamp }
            );


            var passwordHasher = new PasswordHasher<IdentityUser>();

            var Admin1 = new IdentityUser
            {
                UserName = "Admin1",
                Email = "Admin1@gmail.com",
                NormalizedUserName = "ADMIN1",
                NormalizedEmail = "ADMIN1@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            Admin1.PasswordHash = passwordHasher.HashPassword(Admin1, "#Smartschool2023");


            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { Admin1.Id, Admin1.UserName, Admin1.NormalizedUserName, Admin1.Email, Admin1.NormalizedEmail, Admin1.EmailConfirmed, Admin1.PasswordHash, Admin1.SecurityStamp, Guid.NewGuid().ToString(), Admin1.PhoneNumberConfirmed, Admin1.TwoFactorEnabled, null, Admin1.LockoutEnabled, Admin1.AccessFailedCount }
            );
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { Admin1.Id, Admin.Id }
            );

            var Admin2 = new IdentityUser
            {
                UserName = "Admin2",
                Email = "Admin2@gmail.com",
                NormalizedUserName = "ADMIN2",
                NormalizedEmail = "ADMIN2@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            Admin2.PasswordHash = passwordHasher.HashPassword(Admin2, "#Smartschool2023");


            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { Admin2.Id, Admin2.UserName, Admin2.NormalizedUserName, Admin2.Email, Admin2.NormalizedEmail, Admin2.EmailConfirmed, Admin2.PasswordHash, Admin2.SecurityStamp, Guid.NewGuid().ToString(), Admin2.PhoneNumberConfirmed, Admin2.TwoFactorEnabled, null, Admin2.LockoutEnabled, Admin2.AccessFailedCount }
            );
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { Admin2.Id, Admin.Id }
            );

            var Admin3 = new IdentityUser
            {
                UserName = "SmartSchool",
                Email = "SmartSchool045@gmail.com",
                NormalizedUserName = "SMARTSCHOOL",
                NormalizedEmail = "SMARTSCHOOL045@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            Admin3.PasswordHash = passwordHasher.HashPassword(Admin3, "#Smartschool2023");


            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { Admin3.Id, Admin3.UserName, Admin3.NormalizedUserName, Admin3.Email, Admin3.NormalizedEmail, Admin3.EmailConfirmed, Admin3.PasswordHash, Admin3.SecurityStamp, Guid.NewGuid().ToString(), Admin3.PhoneNumberConfirmed, Admin3.TwoFactorEnabled, null, Admin3.LockoutEnabled, Admin3.AccessFailedCount }
            );
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { Admin3.Id, Admin.Id }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles]");
            migrationBuilder.Sql("DELETE FROM AspNetUsers");
            migrationBuilder.Sql("DELETE FROM AspNetUserRoles");
        }
    }
}
