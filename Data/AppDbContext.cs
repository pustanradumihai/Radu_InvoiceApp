using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<DoctorContract> DoctorContracts { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}