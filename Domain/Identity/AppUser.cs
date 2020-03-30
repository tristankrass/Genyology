using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<Person>? Persons { get; set; }

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

    }
}
