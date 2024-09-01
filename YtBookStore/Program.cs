using Microsoft.EntityFrameworkCore;
using YtBookStore.Models.Domain;
using YtBookStore.Repositories.Abstract;
using YtBookStore.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Update the DbContext to use MySQL
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("conn"),
        new MySqlServerVersion(new Version(8, 0, 32)))); 

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Genre}/{action=Add}/{id?}");

app.Run();
