using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstateApp.Data.DataBaseContexts.ApplicationDbContext;
using EstateApp.Data.Entities;
using EstateApp.Web.Interfaces;
using EstateApp.Web.Models;

namespace EstateApp.Web.Services
{
    public class PropertyService : IPropertyService
    {
        // Constructor
        private readonly ApplicationDbContext _dbContext;
        public PropertyService(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Property> GetAllProperties()
        {
            // Need to return a list of properties

            // We can paginate later

            return _dbContext.Properties;
        }

        public async Task AddProperty(PropertyModel model)
        {
            // New instance of Property Entity
            var property = new Property
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Description = model.Description,
                NumberOfRooms = model.NumberOfRooms,
                NumberOfBaths = model.NumberOfBaths,
                NumberOfToilets = model.NumberOfToilets,
                Address = model.Address,
                ContactPhoneNumber = model.ContactPhoneNumber
            };

            // Save to DB
            await _dbContext.AddAsync(property);
            await _dbContext.SaveChangesAsync();
        }
    }
}