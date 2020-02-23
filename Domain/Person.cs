using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public Guid PersonId { get; set; }
        
        [MinLength(1), MaxLength(50)] 
        public string FirstName { get; set; } = default!;

        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; } = default!;

        [MinLength(1), MaxLength(50)]
        public string? MiddleName { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? Occupation { get; set; }

        public string? BirthSurname { get; set; }

        public ICollection<Relationship>? PersonOneRelationships { get; set; }
        public ICollection<Relationship>? PersonTwoRelationships { get; set; }
    }
}
