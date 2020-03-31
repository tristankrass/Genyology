using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Family> Families { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;

        public DbSet<PersonFamily> PersonFamilies { get; set; } = default!;
        public DbSet<FamilyRelationship> FamilyRelationships { get; set; } = default!;
        public DbSet<Relationship> Relationships { get; set; } = default!;
        public DbSet<RelationshipRole> RelationshipRoles { get; set; } = default!;
        public DbSet<RelationshipType> RelationshipTypes { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Person>()
                .HasMany(p => p.PersonOneRelationships)
                .WithOne(r => r.PersonOne!)
                .HasForeignKey(a => a.PersonOneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.PersonTwoRelationships)
                .WithOne(r => r.PersonTwo!)
                .HasForeignKey(a => a.PersonTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}