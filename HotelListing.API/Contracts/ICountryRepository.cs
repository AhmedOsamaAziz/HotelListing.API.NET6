﻿using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetailsAsync(int id);
    }
}
