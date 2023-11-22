using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Extensions;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Data;

public class Seed {

    public static async Task CreatedModels(IApplicationBuilder applicationBuilder) {

        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            byte[] default_image = ImageConverter.ImageToByteArray(
                $"{Directory.GetCurrentDirectory()}/wwwroot/img/running.webp");

            byte[] default_avatar = ImageConverter.ImageToByteArray(
                $"{Directory.GetCurrentDirectory()}/wwwroot/img/running.webp");


            AppUser appUser = new AppUser {
                Pace = 10,
                Mileage = 10,
                ProfileImageUrl = default_avatar,
                City = "City",
                State = "State",
                AddressId = 1,
                Address = new Address {
                    City = "City",
                    Street = "Street",
                    State = "state"
                }
            };

            context.Clubs.Add(new Club() {
                Id = 1,
                Title = "No title",
                Description = "Description",
                Image = default_image,
                AddressId= 1,
                Address = new Address {
                    City = "City",
                    Street = "Street",
                    State = "state"
                },
                ClubCategory = ClubCategory.City,
                AppUser = appUser
            });

            context.Races.Add(new Race() {
                Id = 1,
                Title = "Title",
                Description = "Desc",
                Image = default_image,
                StartTime = DateTime.Now,
                EntryFee = 3,
                Website = "web",
                Twitter = "Tw",
                Facebook = "Fc",
                Contact = "123",
                AddressId = 1,
                Address = new Address {
                    City = "City",
                    Street = "Street",
                    State = "state"
                },
                RaceCategory = RaceCategory.Marathon,
                AppUser = appUser                
            });

            context.Addresss.Add(new Address() {
                Id = 1,
                City = "City",
                Street = "Street",
                State = "state"
            });

            context.States.Add(new State() {
                Id = 1,
                StateCode = "123",
                StateName = "123"
            });

            context.Cities.Add(new City() {
                Id = 1,
                CityName = "CityName",
                StateCode = "222",
                Latitude = 1.2d,
                Longitude = 33.2,
                County = "Country"
            });

            context.SaveChanges();
        }
    }

    public static void SeedData(IApplicationBuilder applicationBuilder) {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            byte[] default_image = ImageConverter.ImageToByteArray(
                $"{Directory.GetCurrentDirectory()}/wwwroot/img/running.webp");

            context.Database.EnsureCreated();

            if (!context.Clubs.Any()) {
                context.Clubs.AddRange(new List<Club>() {

                    new Club() {
                        Title = "Running Club 1",
                        Image = default_image,
                        Description = "This is the description of the first cinema",
                        ClubCategory = ClubCategory.City,
                        Address = new Address() {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new Club() {
                        Title = "Running Club 2",
                        Image = default_image,
                        Description = "This is the description of the first cinema",
                        ClubCategory = ClubCategory.Endurance,
                        Address = new Address() {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new Club() {
                        Title = "Running Club 3",
                        Image = default_image,
                        Description = "This is the description of the first club",
                        ClubCategory = ClubCategory.Trail,
                        Address = new Address() {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new Club() {
                        Title = "Running Club 3",
                        Image = default_image,
                        Description = "This is the description of the first club",
                        ClubCategory = ClubCategory.City,
                        Address = new Address() {
                            Street = "123 Main St",
                            City = "Michigan",
                            State = "NC"
                        }
                    }
                });
                context.SaveChanges();
            }
            //Races
            if (!context.Races.Any()) {
                context.Races.AddRange(new List<Race>() {
                    new Race()
                    {
                        Title = "Running Race 1",
                        Image = default_image,
                        Description = "This is the description of the first race",
                        RaceCategory = RaceCategory.Marathon,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    },
                    new Race()
                    {
                        Title = "Running Race 2",
                        Image = default_image,
                        Description = "This is the description of the first race",
                        RaceCategory = RaceCategory.Ultra,
                        AddressId = 5,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    }
                });
                context.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder) {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "teddysmithdeveloper@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null) {
                var newAdminUser = new AppUser() {
                    Pace = 10,
                    Mileage = 20,
                    ProfileImageUrl = ImageConverter.ImageToByteArray($"{Directory.GetCurrentDirectory()}/wwwroot/img/avatar.jpg"),
                    City = "City",
                    State = "State",
                    UserName = "Администратор",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    Address = new Address() {
                        Street = "123 Main St",
                        City = "Charlotte",
                        State = "NC"
                    }
                };
                await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                //await userManager.CreateAsync(user, "Qwerty123$%^");
                //await dbContext.SaveChangesAsync(); // <- this is needed by UserManager
                //await userManager.AddClaimAsync(newAdminUser, new System.Security.Claims.Claim("testType", "testValue"));
                //// var context = applicationBuilder.ApplicationServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

                //var context = serviceScope.ServiceProvider.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
                //if (context != null) {
                //    context.SaveChanges();
                //}
            }

            string appUserEmail = "user@etickets.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            
            if (appUser == null) {
                var newAppUser = new AppUser() {
                    UserName = "app-user",
                    Email = appUserEmail,
                    EmailConfirmed = true,
                    Address = new Address() {
                        Street = "123 Main St",
                        City = "Charlotte",
                        State = "NC"
                    }
                };

                await userManager.CreateAsync(newAppUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}
