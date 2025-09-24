using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data;

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
    {
    }
    
    public DbSet<VCard> VCards { get; set; }
    public DbSet<Telephone> Telephones { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure VCard entity
        modelBuilder.Entity<VCard>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            // Configure complex types
            entity.ComplexProperty(e => e.Language);
            entity.ComplexProperty(e => e.Organization);
            entity.ComplexProperty(e => e.Address);
            entity.ComplexProperty(e => e.Email);
            entity.ComplexProperty(e => e.Geography);
            entity.ComplexProperty(e => e.PublicKey);
            entity.ComplexProperty(e => e.Url);
            
            // Configure one-to-many relationship with Telephones
            entity.HasMany(e => e.Telephones)
                .WithOne(t => t.VCard)
                .HasForeignKey(t => t.VCardId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configure Telephone entity
        modelBuilder.Entity<Telephone>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Types)
                .HasColumnType("nvarchar(max)");
        });
    }
}