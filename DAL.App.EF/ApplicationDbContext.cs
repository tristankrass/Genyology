using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Family> Families { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Relationship> Relationships { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<RelationshipType> RelationshipTypes { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship between roles and relationships
            modelBuilder.Entity<Role>()
                .HasMany(role => role.RelationshipsOne)
                .WithOne(relationship => relationship.RoleOne)
                .HasForeignKey(relationship => relationship.RoleOneId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Role>()
                .HasMany(role => role.RelationshipsTwo)
                .WithOne(r => r.RoleTwo)
                .HasForeignKey(relationship => relationship.RoleTwoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            // Relationship between roles and relationships
            modelBuilder.Entity<Person>()
                .HasMany(p=> p.PersonOneRelationships)
                .WithOne(relationship => relationship.PersonOne)
                .HasForeignKey(relationship => relationship.PersonOneId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Person>()
                .HasMany(p => p.PersonTwoRelationships)
                .WithOne(relationship => relationship.PersonTwo)
                .HasForeignKey(relationship => relationship.PersonTwoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}