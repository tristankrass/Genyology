using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Person
    {
        public Guid PersonId { get; set; }

        [MinLength(1), MaxLength(50)] public string FirstName { get; set; } = default!;

        [MinLength(1), MaxLength(50)] public string LastName { get; set; } = default!;

        [MinLength(1), MaxLength(50)] public string? MiddleName { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? Occupation { get; set; }

        public string? BirthSurname { get; set; }

        [InverseProperty(nameof(Relationship.PersonOneId))]
        public ICollection<Relationship>? PersonOneRelationships { get; set; }

        [InverseProperty(nameof(Relationship.PersonTwoId))]
        public ICollection<Relationship>? PersonTwoRelationships { get; set; }


        public ICollection<Family>? Families { get; set; }
    }
}