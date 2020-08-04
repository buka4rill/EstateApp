using System.Threading.Tasks;
using EstateApp.Web.Models;

namespace EstateApp.Web.Interfaces
{
    public interface IPropertyService
    {
        Task AddProperty(PropertyModel model);
    }
}