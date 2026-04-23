using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YogaStudioAttendanceAPI.Data;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// postgres service
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

//JWT validation - validate tokens issued by the Yoga studio app
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),            
                ValidateIssuer = false,
                ValidateAudience = false
        };
    });

//CORS - only the yoga studio main MVC app is allowed to cal this api
builder.Services.AddCors(options =>
{
    options.AddPolicy("MVCApp", policy =>
    {
        policy.WithOrigins(
            "https://localhost:7209",
            "http://localhost:5130",
             "https://yogastudiolramanagementsystem-main.onrender.com"
            ).AllowAnyMethod().AllowAnyHeader();  //Yoga Studio MVC app https and http
            
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.7
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("MVCApp"); //must be before authentication
app.UseAuthentication(); //must be before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
