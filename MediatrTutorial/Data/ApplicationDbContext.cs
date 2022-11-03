namespace MediatrTutorial.Data;

using Domain;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; } = default!;
}