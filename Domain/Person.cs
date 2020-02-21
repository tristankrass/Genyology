using System;

namespace Domain
{
    public class Person
    {
        public Guid PersonId { get; set; }


        [MinLength()]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Occupation { get; set; }

        public string BirthSurname { get; set; }
    }
}
