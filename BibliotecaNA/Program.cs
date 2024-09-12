using Microsoft.EntityFrameworkCore;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;
using BibliotecaNA.Repositories.Implementation;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Usuario/Login"; // Caminho para a página de login
        options.LogoutPath = "/Usuario/Logout"; // Caminho para a página de logout
        options.AccessDeniedPath = "/Usuario/AcessoNegado"; // Caminho para acesso negado (opcional)
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Update the DbContext to use MySQL
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("conn"),
        new MySqlServerVersion(new Version(8, 0, 32)))); 

// Register services
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Create the upload folder if it does not exist
var uploadPath = Path.Combine(app.Environment.WebRootPath, "upload");
if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Ensure the upload folder is served as static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadPath),
    RequestPath = "/upload"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}");

app.Run();
