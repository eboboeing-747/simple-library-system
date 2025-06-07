using IlsDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IlsDb.Utility;
using IlsDb.Service;
using IlsDb.Repository;
using System.Xml;
using IlsDb.Entity.BaseEntity;
using IlsDb.Object;

namespace IlsApi
{
    public class Program
    {
        public async static Task Main(string[] args)
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
                            "https://localhost:5173",
                            "http://127.0.0.1:5173",
                            "https://127.0.0.1:5173",
                            "http://26.186.13.168:5173",
                            "https://26.186.13.168:5173"
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
            builder.Services.AddScoped<UserTypeRepository>();
            builder.Services.AddScoped<LibraryRepository>();
            builder.Services.AddScoped<SubsidiaryRepository>();
            builder.Services.AddScoped<BookRepository>();

            builder.Services.AddScoped<JwtOptions>(provider =>
            {
                return new JwtOptions(configuration);
            });

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserTypeService>();
            builder.Services.AddScoped<LibraryService>();
            builder.Services.AddScoped<SubsidiaryService>();
            builder.Services.AddScoped<BookService>();

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

            app.UseHttpsRedirection();

            app.UseCors(corsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await OnStartup(app);

            app.Run();
        }

        private async static Task OnStartup(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                UserTypeService userTypeService = scope.ServiceProvider.GetRequiredService<UserTypeService>();
                await userTypeService.CreateBaseTypes();
                await userTypeService.BindUserTypeIds();

                LibraryService libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
                await libraryService.CreateFirstLibraryIfNotExists();

                UserService userService = scope.ServiceProvider.GetRequiredService<UserService>();

                if (!userService.IsEmpty())
                    return; // script only activates at first start when "Users" table is empty

                UserCredentials adminCredentials = GetAdminCredentials();
                await userService.CreateFirstAdmin(adminCredentials);
            }
        }

        static UserCredentials GetAdminCredentials()
        {
            Console.WriteLine("creating first user\nit will automatically be promoted to admin and will later be used to create a library\nyou will be able to configure admin user from your browser");

            string? adminLogin = string.Empty;
            string adminPassword = string.Empty;
            string verifyPassword = string.Empty;

            while (true)
            {
                Console.Write("admin login: ");
                adminLogin = Console.ReadLine();

                if (adminLogin == null)
                    continue;

                if (adminLogin.Length < 4)
                {
                    Console.WriteLine("login lendth can not subceed 4 symbols");
                    continue;
                }

                adminPassword = GetPassword("password: ");
                Console.WriteLine();
                verifyPassword = GetPassword("verify password: ");

                if (adminPassword == verifyPassword)
                    break;

                Console.WriteLine("\n\npasswords does not match");
            }

            return new UserCredentials
            {
                Login = adminLogin,
                Password = adminPassword
            };
        }

        private static string GetPassword(string prompt)
        {
            string password = string.Empty;
            Console.Write(prompt);

            while (true)
            {
                ConsoleKeyInfo input = System.Console.ReadKey(true);

                if (input.Key == ConsoleKey.Enter && password.Length >= 8)
                {
                    return password;
                }
                else if (input.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1]; // same as password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (IsValidChar(input.Key))
                {
                    password += input.KeyChar;
                    Console.Write('*');
                }
            }
        }

        private static bool IsValidChar(ConsoleKey key)
        {
            if (key >= ConsoleKey.D0 && key <= ConsoleKey.Z)
                return true;
            else if (key >= ConsoleKey.Oem1 && key <= ConsoleKey.Oem7)
                return true;

            return false;
        }
    }
}
