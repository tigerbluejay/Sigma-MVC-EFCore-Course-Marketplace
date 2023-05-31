//using Sigma.DataAccess.Repository;
//using Sigma.DataAccess.Repository.IRepository;
//using Sigma.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Sigma.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// ENABLE TO CONNECT TO THE DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ENABLE FOR STRIPE
// builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// ENABLE FOR IDENTITY
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

// ENABLE FOR UNIT OF WORK
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ENABLE FOR EMAIL SENDER
//builder.Services.AddSingleton<IEmailSender, EmailSender>();

// ENABLE FOR FACEBOOK LOGIN - AND DEFINE VALUES
//builder.Services.AddAuthentication().AddFacebook(options =>
//{
//      options.AppId = "";
//      options.AppSecret = "";
//});

// ENABLE TO USE COOKIES in LOGIN
//builder.Services.ConfigureApplicationCookie(options =>
//{
//      options.LoginPath = $"/Identity/Account/Login";
//      options.LogoutPath = $"/Identity/Account/Logout";
//      options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});

// ENABLE TO USE SESSIONS
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

// app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// TO ENABLE AREAS - WHEN IMPLEMENTED
// app.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");



app.Run();
