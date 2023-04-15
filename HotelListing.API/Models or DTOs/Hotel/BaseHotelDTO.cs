using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models_or_DTOs.Hotel
{
    public abstract class BaseHotelDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double? Rating { get; set; }
        [Required]
        public int CountryID { get; set; }
    }
}
