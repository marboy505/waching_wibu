using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// **1. Tambahkan koneksi ke MySQL**
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 33)))); // Versi MySQL

// **2. Tambahkan layanan pengguna**
builder.Services.AddScoped<UserService>();

// Swagger setup
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Izinkan semua domain
              .AllowAnyMethod() // Izinkan semua metode (GET, POST, dll.)
              .AllowAnyHeader(); // Izinkan semua header
    });
});

var app = builder.Build();

// Swagger UI untuk development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

// **3. Endpoint untuk registrasi**
app.MapPost("/api/register", async ([FromServices] UserService userService, [FromBody] RegisterRequest request) =>
{
    if (request.Password != request.ConfirmPassword)
    {
        return Results.BadRequest(new { message = "Passwords do not match" });
    }

    var result = await userService.RegisterUserAsync(request);
    return result
        ? Results.Ok(new { message = "Registration successful" })
        : Results.BadRequest(new { message = "User already exists" });
})
.WithName("Register");

// **4. Endpoint untuk login**
app.MapPost("/api/login", async ([FromServices] UserService userService, [FromBody] LoginRequest request) =>
{
    var user = await userService.AuthenticateUserAsync(request);
    if (user != null)
    {
        return Results.Ok(new { message = "Login successful", user });
    }
    return Results.Problem("Invalid email or password", statusCode: 401);
})
.WithName("Login");

// **5. Endpoint untuk mendapatkan semua user**
app.MapGet("/api/users", async ([FromServices] AppDbContext dbContext) =>
{
    var users = await dbContext.Users!.ToListAsync();
    return Results.Ok(users);
})
.WithName("GetUsers");

// **6. Endpoint untuk mendapatkan user berdasarkan ID**
app.MapGet("/api/users/{id}", async ([FromServices] AppDbContext dbContext, int id) =>
{
    var user = await dbContext.Users!.FindAsync(id);
    return user != null
        ? Results.Ok(user)
        : Results.NotFound(new { message = "User not found" });
})
.WithName("GetUserById");

// **7. Endpoint untuk mengupdate user**
app.MapPut("/api/users/{id}", async ([FromServices] AppDbContext dbContext, int id, [FromBody] User updatedUser) =>
{
    var user = await dbContext.Users!.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound(new { message = "User not found" });
    }

    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;

    if (!string.IsNullOrEmpty(updatedUser.Password))
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
    }

    if (!string.IsNullOrEmpty(updatedUser.Avatar))
    {
        user.Avatar = updatedUser.Avatar;
    }

    await dbContext.SaveChangesAsync();
    return Results.Ok(new { message = "User updated successfully", user });
});

// **8. Endpoint untuk menghapus user**
app.MapDelete("/api/users/{id}", async ([FromServices] AppDbContext dbContext, int id) =>
{
    var user = await dbContext.Users!.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound(new { message = "User not found" });
    }

    dbContext.Users.Remove(user);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new { message = "User deleted successfully" });
})
.WithName("DeleteUser");

app.MapPost("/api/pictures", async ([FromServices] AppDbContext dbContext, [FromBody] Picture newPicture) =>
{
    var user = await dbContext.Users!.FindAsync(newPicture.UserId);
    if (user == null)
    {
        return Results.NotFound(new { message = "User not found" });
    }

    if (string.IsNullOrWhiteSpace(newPicture.Url))
    {
        return Results.BadRequest(new { message = "Invalid URL" });
    }

    dbContext.Pictures!.Add(newPicture);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new { message = "Picture uploaded successfully", picture = newPicture });
});

app.MapGet("/api/users/{id}/pictures", async ([FromServices] AppDbContext dbContext, int id) =>
{
    var pictures = await dbContext.Pictures!
        .Where(p => p.UserId == id)
        .ToListAsync();

    if (!pictures.Any())
    {
        return Results.NotFound(new { message = "No pictures found for this user" });
    }

    return Results.Ok(pictures);
});


app.MapDelete("/api/pictures/{id}", async ([FromServices] AppDbContext dbContext, int id) =>
{
    var picture = await dbContext.Pictures!.FindAsync(id);
    if (picture == null)
    {
        return Results.NotFound(new { message = "Picture not found" });
    }

    dbContext.Pictures.Remove(picture);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new { message = "Picture deleted successfully" });
});


app.MapPost("/api/upload", async ([FromServices] IWebHostEnvironment env, [FromForm] IFormFile file) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest(new { message = "No file uploaded" });
    }

    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var extension = Path.GetExtension(file.FileName).ToLower();

    if (!allowedExtensions.Contains(extension))
    {
        return Results.BadRequest(new { message = "Only image files are allowed" });
    }

    var uploadsFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    var fileName = Guid.NewGuid() + extension;
    var filePath = Path.Combine(uploadsFolder, fileName);

    try
    {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }
    catch (Exception ex)
    {
      return Results.Json(new { message = "File upload failed", error = ex.Message }, statusCode: 500);
    }

    var fileUrl = $"/uploads/{fileName}";
    return Results.Ok(new { message = "File uploaded successfully", url = fileUrl });
});



// Endpoint batas

// **9. Buat database secara otomatis jika belum ada**
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();


// **11. Request Models**
public record RegisterRequest(string Name, string Email, string Password, string ConfirmPassword);
public record LoginRequest(string Email, string Password);

// **12. DbContext**
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User>? Users { get; set; }
    public DbSet<Picture>? Pictures { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>(entity =>
    {
        entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Email).HasMaxLength(150).IsRequired();
        entity.Property(e => e.Avatar).HasMaxLength(250);
    });

    modelBuilder.Entity<Picture>(entity =>
    {
        entity.Property(e => e.Url).HasMaxLength(250).IsRequired();
        entity.HasOne(p => p.User)
              .WithMany(u => u.Pictures)
              .HasForeignKey(p => p.UserId);
    });
}
}



// **13. User Service**
public class UserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> RegisterUserAsync(RegisterRequest request)
    {
        if (await _dbContext.Users!.AnyAsync(u => u.Email == request.Email))
        {
            return false; // Email sudah ada
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password) // Hash password
        };

        _dbContext.Users!.Add(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<User?> AuthenticateUserAsync(LoginRequest request)
    {
        var user = await _dbContext.Users!.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return user;
        }
        return null;
    }
}

// **10. Model User**
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Avatar { get; set; }

    public ICollection<Picture>? Pictures { get; set; }
}

public class Picture
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Url { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }
}

