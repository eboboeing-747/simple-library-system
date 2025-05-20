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
        private static JwtOptions _jwtOptions = new JwtOptions();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            Program._jwtOptions = new JwtOptions(builder.Configuration);

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

            builder.Services.AddScoped<JwtOptions>();

            builder.Services.AddScoped<UserService>();

            /*
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
//
                string jwtSecretKey = builder.Configuration["JwtConfig:Key"] ?? throw new Exception("no \"JwtConfig\".\"Key\" property in appsettings.Development.json");
//
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                    ValidateLifetime = true,
                };
            });
            builder.Services.AddAuthorization();
            */

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        /*
        public static JwtOptions JwtOptions
        {
            get
            {
                return Program._jwtOptions;
            }
        }
        */
    }
}
