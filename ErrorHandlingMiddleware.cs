using DemoAPI.Models;
using System.Net;
using System.Text.Json;

namespace DemoAPI
{
    // Middleware class for handling errors uniformly across the entire application
    public class ErrorHandlingMiddleware
    {
        // Holds a reference to the next delegate/middleware in the pipeline
        private readonly RequestDelegate _next;

        // Constructor that accepts the next middleware delegate
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Main middleware method that is called on each HTTP request
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // If an unhandled exception occurs, call the exception handler
                await HandleExceptionAsync(context, ex);
            }
        }





        // Static method to handle exceptions and format them into a uniform response
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Set the response content type to JSON for returning structured data
            context.Response.ContentType = "application/json";

            // Set the response status code to 500 (Internal Server Error)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Create a uniform API response with the error message and a general description
            var response = new ApiResponse<string>(
                new List<string> { ex.Message }, // List of error messages
                "An unexpected error occurred. Middleware"   // General message
            );

            // Serialize the response to JSON format
            var jsonResponse = JsonSerializer.Serialize(response);

            // Write the JSON response to the HTTP response
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
