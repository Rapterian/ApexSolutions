using Microsoft.EntityFrameworkCore;
using ApexSolutions.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ApexCare.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to set up the DbContext with options (like connection string, etc.)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets represent tables in the database
        public DbSet<Client> Clients { get; set; }                 // Table for Clients
        public DbSet<Technician> Technicians { get; set; }         // Table for Technicians
        public DbSet<ServiceRequest> ServiceRequests { get; set; } // Table for Service Requests
        public DbSet<Contract> Contracts { get; set; }             // Table for Contracts
        public DbSet<Feedback> Feedbacks { get; set; }             // Table for Feedbacks

        // Optional: Override the OnModelCreating method to further customize table mappings, constraints, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Configure a relationship between ServiceRequest and Client (one-to-many)
            /*
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Client)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // Configure cascading deletes
            */
            /*
            // Example: Configure a relationship between ServiceRequest and Technician (many-to-one)
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Technician)
                .WithMany(t => t.ServiceRequests)
                .HasForeignKey(sr => sr.TechnicianId)
                .OnDelete(DeleteBehavior.SetNull); // Set null if the technician is deleted
            */
        }
    }
}
