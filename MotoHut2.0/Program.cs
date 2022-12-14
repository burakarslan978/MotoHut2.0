using Business;
using Business.Interface;
using Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using MotoHut2._0;
using MotoHut2._0.Collections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMotorCollection, MotorCollection>();
builder.Services.AddSingleton<IMotorDal, MotorDal>();
builder.Services.AddSingleton<IMotorCollectionDal, MotorCollectionDal>();
builder.Services.AddSingleton<IMotor, Motor>();
builder.Services.AddSingleton<IUser, User>();
builder.Services.AddSingleton<IUserCollection, UserCollection>();
builder.Services.AddSingleton<IUserCollectionDal, UserCollectionDal>();
builder.Services.AddSingleton<IUserDal, UserDal>();
builder.Services.AddSingleton<IHuurderMotorCollection, HuurderMotorCollection>();
builder.Services.AddSingleton<IHuurderMotorCollectionDal, HuurderMotorCollectionDal>();
builder.Services.AddSingleton<IHuurderMotor, HuurderMotor>();
builder.Services.AddSingleton<IHuurderMotorDal, HuurderMotorDal>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
