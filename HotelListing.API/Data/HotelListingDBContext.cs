using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDBContext : DbContext
    {
        public HotelListingDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    ID = 1,
                    Name = "Egypt",
                    ShortName = "EG"
                },
                new Country
                {
                    ID = 2,
                    Name = "Dubai",
                    ShortName = "DUB"
                }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    ID = 1,
                    Name = "Marriot",
                    Address = "Giza, Egypt",
                    CountryID = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    ID = 2,
                    Name = "BINGUIN",
                    Address = "Dahab, Dubai",
                    CountryID = 2,
                    Rating = 4.5
                }
                );
        }



    }
}
