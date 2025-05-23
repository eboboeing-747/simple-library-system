using IlsDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IlsDb.Utility;
using IlsDb.Service;
using IlsDb.Repository;

namespace IlsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            string corsPolicy = "localCorsPolicy";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: corsPolicy,
                    policy =>
                    {
                        policy
                        .WithOrigins(
                            "http://localhost:5173",
                            "http://127.0.0.1:5173",
                            "https://localhost:5173",
                            "https://127.0.0.1:5173"
                        )
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryDbContext)));
            });

            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddScoped<JwtOptions>(provider =>
            {
                return new JwtOptions(configuration);
            });

            builder.Services.AddScoped<UserService>();

            JwtOptions jwtOptions = new JwtOptions(configuration);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
                        )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jwtToken"];

                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseCors(corsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
