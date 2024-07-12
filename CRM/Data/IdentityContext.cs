using CRM.Models;
using CRM.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            builder.Entity<Project>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Project)
                .HasForeignKey(k => k.ProjectId);

            builder.Entity<Employee>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Employees)
                .HasForeignKey(k => k.ProjectId);
        }
    }
}
