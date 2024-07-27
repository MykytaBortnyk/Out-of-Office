using CRM.Models;
using CRM.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using System.Reflection.Emit;

namespace CRM.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().ToTable("Employees", t => t.ExcludeFromMigrations());
            builder.Entity<Project>().ToTable("Projects", t => t.ExcludeFromMigrations());
            builder.Entity<ApprovalRequest>().ToTable("ApprovalRequests", t => t.ExcludeFromMigrations());
            builder.Entity<LeaveRequest>().ToTable("LeaveRequests", t => t.ExcludeFromMigrations());

            builder.Entity<Project>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Project)
                .HasForeignKey(k => k.ProjectId);

            builder.Entity<Employee>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Employees)
                .HasForeignKey(k => k.ProjectId);

            var AdminRoleId = "57F6DB8F-3BC6-4049-BA52-024A580E6EBD";
            var HrRoleId = "2EB69CE7-C566-497C-95A2-66B576F971B6";
            var PrRoleId = "6BF97E0C-235F-4C17-96C8-EFCBF346CB36";
            var EmployeeRoleId = "1A27DD9E-5F9C-4BBC-83C8-2E344909F85D";
            var UserId = "2BFD4986-EDCA-4FCF-95D5-CE28C15A4B67";

            #region Roles seeding
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Name = Models.Enums.Positions.Administrator.GetDisplayName(),
                NormalizedName = Models.Enums.Positions.Administrator.GetDisplayName().ToUpper(),
                Id = new Guid(AdminRoleId),
                ConcurrencyStamp = AdminRoleId,
            });

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Name = Models.Enums.Positions.HrManager.GetDisplayName(),
                    NormalizedName = Models.Enums.Positions.HrManager.GetDisplayName().ToUpper(),
                    Id = new Guid(HrRoleId),
                    ConcurrencyStamp = HrRoleId
                });
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Name = Models.Enums.Positions.ProjectManager.GetDisplayName(),
                    NormalizedName = Models.Enums.Positions.ProjectManager.GetDisplayName().ToUpper(),
                    Id = new Guid(PrRoleId),
                    ConcurrencyStamp = PrRoleId
                });
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Name = Models.Enums.Positions.Employee.GetDisplayName(),
                    NormalizedName = Models.Enums.Positions.Employee.GetDisplayName().ToUpper(),
                    Id = new Guid(EmployeeRoleId),
                    ConcurrencyStamp = EmployeeRoleId
                });
            #endregion
            #region Admin user seeding
            ///We pretend that Employee Table is already exists with known Employees, but there's no AspNetUsers table
            ///In given case, I use an existing Employee with a Position = Administrator
            var user = new ApplicationUser()
            {
                Id = new Guid(UserId),
                Email = "user@example.com",
                EmailConfirmed = true,
                UserName = "user@example.com",
                NormalizedUserName = "user@example.com".ToUpper(),
                SecurityStamp = AdminRoleId,
                UserRoleId = new Guid(AdminRoleId),
                Employee = null,
                EmployeeId = 3
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, "$0M3_StR0Ng");

            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityUserRole<Guid>>()
                .HasKey(p => new { p.UserId, p.RoleId });

            builder.Entity<IdentityUserRole<Guid>>()
                .HasData(new IdentityUserRole<Guid>
                {
                    RoleId = new Guid(AdminRoleId),
                    UserId = new Guid(UserId)
                });
            #endregion
        }
    }
}
