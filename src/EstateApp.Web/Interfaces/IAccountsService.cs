using System.Threading.Tasks;
using EstateApp.Data.Entities;
using EstateApp.Web.Models;

namespace EstateApp.Web.Interfaces
{
    // Linked to the Account Service
    public interface IAccountsService
    {
        Task<ApplicationUser> CreateUserAsync(RegisterModel model);
    }
}