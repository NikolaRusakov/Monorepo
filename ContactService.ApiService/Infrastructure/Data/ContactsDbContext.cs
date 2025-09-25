using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data;

public class ContactsDbContext : DbContext
{
	public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
		: base(options) { }

	public DbSet<VCard> VCards { get; set; }
	public DbSet<Telephone> Telephones { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Configure VCard entity
		modelBuilder.Entity<VCard>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

			entity.Property(e => e.UpdatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");
			// Configure complex types
			entity.ComplexProperty(e => e.Language).IsRequired();
			entity.ComplexProperty(e => e.Organization).IsRequired();
			entity.ComplexProperty(e => e.Address).IsRequired();
			entity.ComplexProperty(e => e.Email).IsRequired();
			entity.ComplexProperty(e => e.Geography).IsRequired();
			entity.ComplexProperty(e => e.PublicKey).IsRequired();
			entity.ComplexProperty(e => e.Url).IsRequired();

			// Configure one-to-many relationship with Telephones
			entity
				.HasMany(e => e.Telephones)
				.WithOne(t => t.VCard)
				.HasForeignKey(t => t.VCardId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		// Configure Telephone entity
		modelBuilder.Entity<Telephone>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.Property(e => e.Types).HasColumnType("varchar(255)");
		});
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql("contactsdb"); // This is looking for a connection string named "contactsdb"
		}
	}
}
