using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using HotelListing.API.Controllers;
using HotelListing.API.Configurations;
using HotelListing.API.Contracts;
using HotelListing.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --1--[CORS]
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

// --2-- [Serilog]
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));


//--3-- [EntityFramew]
var connectionString = builder.Configuration.GetConnectionString("HotelListingDBConnectionString");
builder.Services.AddDbContext<HotelListingDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//--4--[Auto Mapper]
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//--5--[repository]
builder.Services.AddScoped<ICountryRepository, CountryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();


app.Run();
