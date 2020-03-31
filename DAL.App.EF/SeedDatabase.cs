using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class SeedDatabase
    {


        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            Console.WriteLine("Adding admin to the Database!");

            var roleNames = new string[] { "User", "Admin" };
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;

                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }

            }

            var userName = "tristan@tristan.ee";
            var passWord = "Test@123";
            var firstName = "tristan";
            var lastName = "tristan";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.EmailConfirmed = true;

                var newUser = userManager.CreateAsync(user, passWord);

                if (!newUser.Result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }


            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }

        public static void SeedInitialData(ApplicationDbContext ctx)
        {
            Console.WriteLine("Adding dummy data to the database");
            SeedPersonData(ctx);
        }


        private static void SeedPersonData(ApplicationDbContext ctx)
        {
            var superUser =  ctx.Users.Single(u => u.UserName.Equals("tristan@tristan.ee"));
            var superUserId = superUser.Id;

            string[] names = new[] { "Tristan Krass", "Morris Mason", "Rose Mason", "Madeleine Mason", "Sophie Mason", "Charlotte Mason", "Isabelle Mason" };
            string[] familyNames = new[] { "Krass", "Mason", "Rose" };
            string[] relationShipTypes = new[] { "Mom-Son", "Dad-Son", "Brother-Brother", "Brother-Sister", "Married" };
            string[] relationShipRoles = new[] { "Mom", "Son", "Brother", "Sister", "Dad", "Wife", "Husband" };
            DateTime[] dateTimes = new[] {
                new DateTime(1998, 6, 20),
                new DateTime(1950, 1, 12), new DateTime(1975, 3, 5),
                new DateTime(2004, 2, 15), new DateTime(1980, 12, 12),
                new DateTime(1970, 4, 20), new DateTime(1975, 12, 31), };
            ;

            foreach (var familyName in familyNames)
            {
                var family = new Family()
                {
                    FamilyName = familyName
                };

                MetaData.AddInitialClassMetaDataSeeding(family, superUserId);

                 ctx.Families.Add(family);
                 ctx.SaveChanges();
            }


            int i = 0;

            foreach (var name in names)
            {
                var person = new Person()
                {
                    FirstName = name.Split()[0],
                    LastName = name.Split()[1],
                    DateOfBirth = dateTimes[i],
                    AppUserId = superUserId,
                    Sex = i % 2 == 0 ? Sex.FEMALE : Sex.MALE
                };

                MetaData.AddInitialClassMetaDataSeeding(person, superUserId);

                ctx.Persons.Add(person);

                i++;
                ctx.SaveChanges();
            }

          

            foreach (var role in relationShipRoles)
            {
                var rL = new RelationshipRole()
                {
                    RelationshipRoleName = role

                };
                MetaData.AddInitialClassMetaDataSeeding(rL, superUserId);

                ctx.RelationshipRoles.Add(rL);
            }
            ctx.SaveChanges();

            foreach (var relationShipType in relationShipTypes)
            {
                var relationshipType = new RelationshipType() { RelationshipTypeName = relationShipType };
                MetaData.AddInitialClassMetaDataSeeding(relationshipType, superUserId);
            }

            ctx.SaveChanges();

        }

        


    }

}
