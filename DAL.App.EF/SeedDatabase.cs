using System;
using System.Collections.Generic;
using System.Text;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF
{
    public class SeedDatabase
    {


        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {

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

                var result = userManager.CreateAsync(user, passWord).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }


            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }
    }

}
