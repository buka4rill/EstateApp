using System.Collections.Generic;
using System.Threading.Tasks;
using EstateApp.Data.Entities;
using EstateApp.Web.Models;

namespace EstateApp.Web.Interfaces
{
    public interface IPropertyService
    {
        Task AddProperty(PropertyModel model);

        IEnumerable<Property> GetAllProperties();
    }
}