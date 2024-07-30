using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mappers;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;
using NLayer.Web;
using NLayer.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IProductRepýository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddDbContext<AppDbContext>(
    x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
        {
            option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
        });
    }
    );

builder.Services.AddHttpClient<ProductApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});


builder.Services.AddHttpClient<CategoryApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

var app = builder.Build();
app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
