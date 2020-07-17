using System;
using System.Text;
using System.Threading.Tasks;
using EstateApp.Data.Entities;
using EstateApp.Web.Interfaces;
using EstateApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace EstateApp.Web.Services
{
    // To ease the work of our Accounts Controller
    public class AccountsService : IAccountsService
    {
        // Dependency Injection
        // Constructor
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // public async Task<ApplicationUser> LoginAsync(LoginModel model)
        // {
        //     // Check if model is null
        //     if (model is null) throw new ArgumentNullException(message: "Invalid details provided", null);

        //     // Log in the user
        //     // Confirm that Password is correct
        //     var result = await _userManager.Conf
        // }

        public async Task<ApplicationUser> CreateUserAsync(RegisterModel model)
        {
            // Check if model is null
            if (model is null) throw new ArgumentNullException(message: "Invalid details provide", null);

            // throw new NotImplementedException();

            // Create new App user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            // The new user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(message: AddErrors(result), null);
            }

            return user;
        }

        // Errors Helper Method
        private string AddErrors(IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.Append(error.Description + " ");
            }

            return sb.ToString();
        }
    }
}