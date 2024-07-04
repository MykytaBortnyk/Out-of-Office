using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace CRM.Data
{
    public class CrmContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
