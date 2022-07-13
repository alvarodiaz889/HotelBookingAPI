using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using HotelBookingAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static HotelBookingAPI.Middlewares.ExceptionMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("ApiConnection");
builder.Services.AddDbContext<ApiDBContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<BookingOptions>(
    builder.Configuration.GetSection(BookingOptions.SECTION));

builder.Services.AddScoped<IBookingService>(ctx => {
    var db = ctx.GetRequiredService<ApiDBContext>();
    var opt = ctx.GetRequiredService<IOptions<BookingOptions>>();
    var map = ctx.GetRequiredService<IMapper>();
    return new BookingService(db, opt, map);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//apply pending migrations on the DB
using var scope = (app as IApplicationBuilder).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
scope.ServiceProvider.GetRequiredService<ApiDBContext>().Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//custom middleware to handle exceptions
ExceptionsMiddleware.HandleExceptionsAndLogging(app);

app.Run();


