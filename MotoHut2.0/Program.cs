using Business;
using Business.Interface;
using Dal;
using MotoHut2._0;
using MotoHut2._0.Collections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMotorCollection, MotorCollection>();
builder.Services.AddSingleton<IMotorDal, MotorDal>();
builder.Services.AddSingleton<IMotorCollectionDal, MotorCollectionDal>();
builder.Services.AddSingleton<IMotor, Motor>();
builder.Services.AddSingleton<IHuurderMotorCollection, HuurderMotorCollection>();
builder.Services.AddSingleton<IHuurderMotor, HuurderMotor>();
builder.Services.AddSingleton<IHuurderMotorDal, HuurderMotorDal>();



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
