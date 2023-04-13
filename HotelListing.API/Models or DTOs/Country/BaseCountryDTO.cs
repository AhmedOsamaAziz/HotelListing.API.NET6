using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models_or_DTOs.Country
{
    public class BaseCountryDTO
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
