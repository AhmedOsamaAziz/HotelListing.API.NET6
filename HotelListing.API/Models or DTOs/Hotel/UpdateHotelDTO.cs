using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models_or_DTOs.Hotel
{
    public class UpdateHotelDTO : BaseHotelDTO
    {
        [Required]
        public int ID { get; set; }
    }
}
