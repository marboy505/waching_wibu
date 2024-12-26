using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    // Model
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    // Database Context
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }

    // Repository
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User?> Login(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public async Task<User> Register(User user)
        {
            user.Password = HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Login(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);
        }
    }

    // Service
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User?> Login(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(User user)
        {
            return await _userRepository.Register(user);
        }

        public async Task<User?> Login(string email, string password)
        {
            return await _userRepository.Login(email, password);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<UserContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapPost("/api/auth/register", async (User user, IUserService userService) =>
            {
                if (!Validator.TryValidateObject(user, new ValidationContext(user), null, true))
                {
                    return Results.BadRequest("Invalid user data.");
                }

                var createdUser = await userService.Register(user);
                return Results.Created($"/api/auth/register/{createdUser.Id}", createdUser);
            });

            app.MapPost("/api/auth/login", async (string email, string password, IUserService userService) =>
            {
                var user = await userService.Login(email, password);
                if (user == null)
                {
                    return Results.Unauthorized();
                }
                return Results.Ok(user);
            });

            app.Run();
        }
    }
}
