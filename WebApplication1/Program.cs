using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;// Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAuthService, Auth>();
builder.Services.AddScoped<ISkillsService, SkillsService>();
builder.Services.AddScoped<IEmailService, EmailServicer>();
builder.Services.AddScoped<IProfileServicer,ProfileServicer>();
builder.Services.AddScoped<IContactServicer,ContactServicer>();
builder.Services.AddScoped<ISubSkillsServicer,SubSkillsServicer>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

//Addign session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

builder.Services.AddSession(option =>
{
    option.Cookie.HttpOnly = true;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    option.Cookie.SameSite = SameSiteMode.None;
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Register, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(x=>{
    x.SwaggerDoc("v1",new OpenApiInfo{Title="My Portfolio", Version="v1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAllOrigins");

app.UseSwagger();

app.UseSwaggerUI(x=>{
    x.SwaggerEndpoint("/swagger/v1/swagger.json","My Portfolio v1");
    x.RoutePrefix=string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.UseStaticFiles();

app.MapControllers();

app.Run();
