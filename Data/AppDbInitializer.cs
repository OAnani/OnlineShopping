using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopping.Data.Static;
using OnlineShopping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Movies

                //Actors & Movies
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Clothes"
                        },
                        new Category()
                        {
                            Name = "Shoes"
                        },
                         new Category()
                        {
                            Name = "Bags"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Languages.Any())
                {
                    context.Languages.AddRange(new List<Language>()
                    {
                        new Language()
                        {
                            Name="English"
                        },
                        new Language()
                        {
                            Name="Arabic"
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name="Sweatshirt",
                            Price = 749.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/14597698/1916cc66bedc3f9c700cfa57181b028dda1ce413/1/image-thumb__3579583__product_zoom_medium_606x504/1916cc66bedc3f9c700cfa57181b028dda1ce413.jpg",
                            CategoryId=1
                        },
                        new Product()
                        {
                            Name="Sweatpants",
                            Price = 499.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/14441539/40158f1117652c474afac2103675d15bcdc2f605/2/image-thumb__3422825__product_zoom_medium_606x504/7ea8a0ba2358ab7cdf5b7e60dd5cf1cb955dd339.jpg",
                            CategoryId=1
                        },
                        new Product()
                        {
                            Name="Shoes",
                            Price = 990.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/13605636/73bea836d6de4859276ec2989832b82b9c1ccbd9/2/image-thumb__2724152__product_zoom_medium_606x504/52c00bb89cd7bd9954b8f787fb880a16533237f6.jpg",
                             CategoryId=2
                        },
                        new Product()
                        {
                            Name="Sandlas",
                            Price = 1699.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/14317491/3f143d28b1578a634c1e2540fcffba6f4894f719/2/image-thumb__3205086__product_zoom_medium_606x504/10b8b6bbf48cf7a6ebacff7efd6aac53ddff7eb8.jpg",
                             CategoryId=2
                        },
                        new Product()
                        {
                            Name="Green bag",
                            Price = 749.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/13996570/edaabd36f216abbdfc8d8212c47631b898e7c8f8/2/image-thumb__3145373__product_zoom_medium_606x504/a1485e5d48c24110b18d171355e517f84fa7b75d.jpg",
                            CategoryId=3
                        },
                        new Product()
                        {
                            Name="Orange bag",
                            Price = 449.00,
                            ImageURL = "https://eg.hm.com/assets/styles/HNM/13686941/769192c8fe2433b00088a058fc2069f29d340f17/2/image-thumb__3016071__product_zoom_medium_606x504/9caadd122d9c647afe42a00c813dcb016ad2fedb.jpg",
                            CategoryId=3
                        },
                    });
                    context.SaveChanges();
                }
                //if (!context.ProductNames.Any())
                //{
                //    context.ProductNames.AddRange(new List<ProductNameTranslation>()
                //    {
                //        new ProductNameTranslation()
                //        {
                //            ProudctID=1,
                //            LanguageID=1,
                //            ProductName="Printed sweatshirt"
                //        },
                //         new ProductNameTranslation()
                //        {
                //            ProudctID=2,
                //            LanguageID=1,
                //            ProductName="jogger"
                //        },
                //        new ProductNameTranslation()
                //        {
                //            ProudctID=3,
                //            LanguageID=1,
                //            ProductName="Sneakers"
                //        },
                //        new ProductNameTranslation()
                //        {
                //            ProudctID=4,
                //            LanguageID=1,
                //            ProductName="Sandals"
                //        },
                //        new ProductNameTranslation()
                //        {
                //            ProudctID=5,
                //            LanguageID=1,
                //            ProductName="Green bag"
                //        },
                //        new ProductNameTranslation()
                //        {
                //            ProudctID=6,
                //            LanguageID=1,
                //            ProductName="Ornage bag"
                //        },

                //    });
                //    context.SaveChanges();
                //}
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@shopping.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@shopping.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
