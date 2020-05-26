using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.May2020.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
