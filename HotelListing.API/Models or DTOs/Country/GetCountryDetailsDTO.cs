using HotelListing.API.Models_or_DTOs.Hotel;

namespace HotelListing.API.Models_or_DTOs.Country
{
    public class GetCountryDetailsDTO : BaseCountryDTO
    {
        public int ID { get; set; }

        public List<GetHotelDTO>? Hotels { get; set; }
    }
}
