using CemSys.Business;
using CemSys.Data;
using CemSys.Data.NichosDB;
using CemSys.Interface;
using CemSys.Interface.Fosas;
using CemSys.Interface.Nichos;
using CemSys.Interface.SeccionesNichos;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar el DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

//contenedor de capa de datos
builder.Services.AddScoped(typeof(IRepositoryDB<>), typeof(ServiceGenericDB<>));
builder.Services.AddScoped<INichosDB, NichosDB>();
builder.Services.AddScoped<IFosaDB, FosaBD>();

//contenedor de capa de negocio
builder.Services.AddScoped(typeof(IRepositoryBusiness<>), typeof(ServiceGenericBusiness<>));
builder.Services.AddScoped<ISeccionesNichosBusiness, SeccionesNichosBusiness>();
builder.Services.AddScoped<INichosBusiness, NichosBusiness>();
builder.Services.AddScoped<IFosasBusiness, FosaBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Login}/{action=Index}/{id?}");
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
