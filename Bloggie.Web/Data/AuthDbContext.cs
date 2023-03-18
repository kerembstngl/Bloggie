using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Seed Roles (user, admin, superadmin)
            var adminRoleId = "fbc1a14c-dd72-4ba4-bf59-3932c1fb153a";
            var superAdminRoleId = "aa6d6fbd-a585-4f97-949a-0b1b79ccca8e";
            var userRoleId = "7fc51aff-ae13-459e-960d-3ad0028d5a91";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = "adminRoleId",
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = "aa6d6fbd-a585-4f97-949a-0b1b79ccca8e",
                    ConcurrencyStamp = superAdminRoleId
                },
                 new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = "userRoleId",
                    ConcurrencyStamp = userRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed SuperAdminUser

            var superAdminId = "cae0c706-826f-4342-8696-894e2dd8e11f";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // Add All roles to SuperAdmin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                },
            };





        }
    }
}
