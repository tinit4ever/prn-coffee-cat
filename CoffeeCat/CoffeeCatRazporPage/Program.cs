using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Admin;
using Repositories.Auth;
using Repositories.Extension;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//DI
builder.Services.AddDatabase();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<CoffeeCatContext>();
builder.Services.AddTransient(typeof(ICoffeeShopManagerRepository<>), typeof(CoffeeShopManagerRepository<>));
builder.Services.AddTransient(typeof(ICustomerRepository<>), typeof(CustomerRepository<>));
builder.Services.AddTransient(typeof(ISignInRepository), typeof(SignInRepository));
builder.Services.AddTransient(typeof(IRegisterRepository), typeof(RegisterRepository));
builder.Services.AddTransient(typeof(IAdminRepository), typeof(AdminRepository));
builder.Services.AddTransient(typeof(ISessionRepository), typeof(SessionRepository));
builder.Services.AddTransient(typeof(IProfileRepository), typeof(ProfileRepository));
builder.Services.AddTransient(typeof(ICoffeeShopStaffRepository), typeof(CoffeeShopStaffRepository));

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
