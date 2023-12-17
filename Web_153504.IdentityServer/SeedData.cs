using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using Web_153504.IdentityServer.Data;
using Web_153504.IdentityServer.Models;

namespace Web_153504.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();


                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                

                var user = roleMgr.FindByNameAsync("user").Result;

                if (user == null)
                {
                    user = new IdentityRole
                    {
                        Name = "user",
                    };
                    var result = roleMgr.CreateAsync(user).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("user role create");
                }
                else
                {
                    Log.Debug("user role already exists");
                }

                var admin = roleMgr.FindByNameAsync("admin").Result;

                if (admin == null)
                {
                    admin = new IdentityRole
                    {
                        Name = "admin",
                    };
                    var result = roleMgr.CreateAsync(admin).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("admin role create");
                }
                else
                {
                    Log.Debug("admin role already exists");
                }

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var alice = userMgr.FindByNameAsync("user").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "user",
                        Email = "user@email.com",
                        EmailConfirmed = true,
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "User"),
                            new Claim(JwtClaimTypes.Role, "USER"),
                            new Claim(JwtClaimTypes.GivenName, "1"),
                            new Claim(JwtClaimTypes.FamilyName, "2"),
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("user created");
                }
                else
                {
                    Log.Debug("user already exists");
                }

                var bob = userMgr.FindByNameAsync("admin").Result;
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@email.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "admin"),
                            new Claim(JwtClaimTypes.Role, "ADMIN"),
                            new Claim(JwtClaimTypes.GivenName, "1"),
                            new Claim(JwtClaimTypes.FamilyName, "2"),
                            new Claim("location", "somewhere")
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("bob created");
                }
                else
                {
                    Log.Debug("bob already exists");
                }



            }
        }
    }
}