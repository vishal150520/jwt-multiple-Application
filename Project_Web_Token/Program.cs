using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Web_Token;
using Project_Web_Token.Database;
using Project_Web_Token.Repository;
using System.Security.Cryptography.Xml;
using System.Text;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StamensProjectContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


// Add Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<StamensProjectContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Auidence"],
        
        ValidateAudience =false,
        ValidateIssuer=false,
        ValidateLifetime = true
        

    };

});
//builder.Services.AddCors(options => options.AddPolicy("corsapp", builder =>
//{
//    builder.WithOrigins("http://localhost:5121").AllowAnyMethod().AllowAnyHeader();
//}));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
    builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();

builder.Services.AddTransient<JwtMangerRepocs, JwtManagerRepoClass>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
//app.UseCors("corsapp");
app.MapControllers();


app.Run();
