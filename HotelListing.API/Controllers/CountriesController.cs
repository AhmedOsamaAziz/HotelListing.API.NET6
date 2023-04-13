using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models_or_DTOs.Country;
using AutoMapper;
using HotelListing.API.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            this._countryRepository = countryRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync();
            var mappedCountries = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(mappedCountries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailsDTO>> GetCountry(int id)
        {
            var country = await _countryRepository.GetDetailsAsync(id);
            
            if (country == null)
            {
                return NotFound();
            }

            var mappedCountry = _mapper.Map<GetCountryDetailsDTO>(country);
            return Ok(mappedCountry);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO countryDTO)
        {
            if (id != countryDTO.ID)
            {
                return BadRequest();
            }

            //_context.Entry(countryDTO).State = EntityState.Modified;
            var country = await _countryRepository.GetAsync(id);
            if(country == null)
            {
                return NotFound();
            }

            // assign values from left side to the right side, and becaus country already tracked i can save changes directlly
            _mapper.Map(countryDTO, country);

            try
            {
                await _countryRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _countryRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountryDTO)
        {

            var country = _mapper.Map<Country>(createCountryDTO);
            await _countryRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.ID }, createCountryDTO);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_countryRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryRepository.DeleteAsync(country.ID);

            return NoContent();
        }

        //private bool CountryExists(int id)
        //{
        //    return (_context.Countries?.Any(e => e.ID == id)).GetValueOrDefault();
        //}
    }
}
