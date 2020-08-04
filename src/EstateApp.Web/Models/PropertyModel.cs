using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EstateApp.Web.Models
{
    public class PropertyModel
    {
        [DisplayName("Title")]
        [Required]
        public string Title { get; set; } // 3 Bedroom flat in Abule-Egba.

        [DisplayName("ImageUrl")]
        public string ImageUrl { get; set; }

        [DisplayName("Price")]
        [Required]
        public double Price { get; set; }

        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Number of Rooms")]
        [Required]
        public int NumberOfRooms { get; set; }

        [DisplayName("Number of Baths")]
        [Required]
        public int NumberOfBaths { get; set; }

        [DisplayName("Number of Toilets")]
        [Required]
        public int NumberOfToilets { get; set; }

        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }

        [DisplayName("Contact Phone Number")]
        [Required]
        public string ContactPhoneNumber { get; set; }
    }
}