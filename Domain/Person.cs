using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Person: DomainEntity
    {

        [MinLength(1), MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string LastName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string? MiddleName { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? Occupation { get; set; }

        public string? BirthSurname { get; set; }

        [InverseProperty(nameof(Relationship.PersonOne))]
        public ICollection<Relationship>? PersonOneRelationships { get; set; }

        [InverseProperty(nameof(Relationship.PersonTwo))]
        public ICollection<Relationship>? PersonTwoRelationships { get; set; }

        public ICollection<PersonFamily>? Families { get; set; }

        public Sex Sex { get; set; } = Sex.Undefined;
        [MinLength(11), MaxLength(25)]
        public string? IdCode { get; set; }
        // App User 
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

    }
}