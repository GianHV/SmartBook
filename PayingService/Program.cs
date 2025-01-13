
using Common.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PayingService.Middlewares;
using System.Net;
using System.Security.Cryptography;
using VNPAY.NET;

namespace PayingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<IVnpay, Vnpay>();

            builder.Services.AddControllers(u => u.Filters.Add(new GlobalExceptionHandler()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                var rsaKey = RSA.Create();
                string xmlKey = File.ReadAllText(builder.Configuration.GetSection("Jwt:PublicKeyPath").Value);
                rsaKey.FromXmlString(xmlKey);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsaKey),

                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var response = new ApiResponse
                        {
                            StatusCode = HttpStatusCode.Unauthorized,
                            IsSuccess = false,
                            ErrorMessages = new List<string> { "You must be authenticated to access this resource" }
                        };

                        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(jsonResponse);
                    }
                };
            }
         );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
