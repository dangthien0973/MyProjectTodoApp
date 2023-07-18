using APIToDoListV1.Authorization;
using APIToDoListV1.Data;
using APIToDoListV1.Entities;
using APIToDoListV1.Extentions;
using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using AppChat_API.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
// key authen token 


// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false,
                       ValidateIssuer = false,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["JwtIssuer"],
                       ValidAudience = builder.Configuration["JwtAudience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
                   };
               });
// this is add Role in identity 
builder.Services.AddAuthorization(options =>
    options.AddPolicy("admin", policy => policy.RequireRole("admin"))
);
builder.Services.AddAuthorization();
builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EmployeeAppCon")));

builder.Services.AddTransient<IdotoRepository, TodoReponsitory>();
builder.Services.AddTransient<IUserRepository, UserReponsitory>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.update-database
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

// tự thêm data khi database trống
/*app.MigrateDbContext<PostgresDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<TodoListDbContextSeed>>();
                new TodoListDbContextSeed().SeedAsync(context, logger).Wait();
            });*/
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
