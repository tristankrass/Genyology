using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        
        [MinLength(1), MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string LastName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string? MiddleName { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }
        
        // App User 
        public Guid AppUserId { get; set; } = default!;
    }
}
