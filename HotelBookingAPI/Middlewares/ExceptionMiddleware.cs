using HotelBookingAPI.Services;
using System.Net;
using System.Text.Json;

namespace HotelBookingAPI.Middlewares
{
    public class ExceptionMiddleware
    {
		public class ExceptionsMiddleware
		{
            public static void HandleExceptionsAndLogging(IApplicationBuilder app)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.ToString());
                    }
                });
            }
        }
	}
}
