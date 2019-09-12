﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TaxiApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using TaxiApplication.Data;

namespace TaxiApplication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            CreateRolesAdmin();
            //CreateTaxiMakes();
        //    CreateModels();
        }

        public void CreateRolesAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //creating role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            Admin ad = context.Admins.ToList().Find(x => x.Email == "TopTwoButNot2@gmail.com");
            if(ad==null)
            {
                Admin newAdmin = new Admin
                {
                    AdminID = Guid.NewGuid().ToString(),
                    FirstName = "Administrator",
                    LastName = "Admin",
                    Email = "TopTwoButNot2@gmail.com"
                    
                };
                context.Admins.Add(newAdmin);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newAdmin.Email;
                user.Email = newAdmin.Email;
                string password = "Password@01";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Admin");
            }
            //populating initial role
           

            //creating other roles
            if (!roleManager.RoleExists("RankManager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "RankManager";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Owner"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Owner";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Driver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Passenger"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Passenger";
                roleManager.Create(role);
            }
        }

        public void CreateTaxiMakes()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var taximakes = db.TaxiMakes.ToList();

            string[] makes = { "Toyota", "Nissan", "Mercedes" };
          
            if(taximakes.Count()==0)
            {
                for (int i = 0; i < makes.Length; i++)
                {
                    TaxiMake tm = new TaxiMake()
                    {
                        MakeId = Guid.NewGuid().ToString(),
                        MakeType = makes[i]
                    };
                    db.TaxiMakes.Add(tm);
                    db.SaveChanges();
                }
            }
            
           
        }

        //public void CreateModels()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    string[] models = { "Quantum", "Siyaya", "Sprinter", "Impendulo", "Inyathi" };
        //    int[] seats = { 14, 15, 22, 14, 13 };

        //    var Models = db.TaxiModels.ToList();

        //    if (Models.Count() == 0)
        //    {
        //        for (int i = 0; i < models.Length; i++)
        //        {
        //            TaxiModel m = new TaxiModel()
        //            {
        //                TaxiModelID = Guid.NewGuid().ToString(),
        //                TaxiModelName = models[i],
        //                Seats = seats[i]
        //            };
        //            db.TaxiModels.Add(m);
        //            db.SaveChanges();
        //        }
        //    }
        //    CreateTaxiMakes();

        //}
    }
}