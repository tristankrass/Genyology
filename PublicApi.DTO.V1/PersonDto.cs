using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class PersonDto
    {
        private int _sex = 0;
        public Guid Id { get; set; }
        
        [MinLength(1), MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string LastName { get; set; } = default!;

        [MinLength(1), MaxLength(128)] public string? MiddleName { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        [MinLength(11), MaxLength(25)]
        public string? IdCode { get; set; }

        public int Sex
        {
            get => _sex;
            set => _sex = value >= 0 && value < 3 ? value : 0;
        }

        // App User 
        public Guid AppUserId { get; set; } = default!;
    }
}
