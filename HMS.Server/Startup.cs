using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Projet.Context;
using Projet.DAL;
using Projet.DAL.Contracts;
using Projet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Projet.Middleware;
using Projet.BLL.Contract;
using Projet.BLL;
using System.Security.Claims;

namespace HMS.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services)
{
    // Add DbContext
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    // Register Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAdminRepository, AdminRepository>();
    services.AddScoped<IDoctorRepository, DoctorRepository>();
    services.AddScoped<IPatientRepository, PatientRepository>();
    services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    services.AddScoped<ILabReportRepository, LabReportRepository>();
    services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
    services.AddScoped<IMessageRepository, MessageRepository>();

    // Register Services
    services.AddScoped<UserService>();
    services.AddScoped<AdminService>();
    services.AddScoped<DoctorService>();
    services.AddScoped<PatientService>();
    services.AddScoped<AppointmentService>();
    

    // Register Managers
    services.AddScoped<IUserManager, UserManager>();

    services.AddControllers();
    
    // Add Authentication and Authorization
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secret = Configuration["Jwt:Secret"] ?? 
            throw new InvalidOperationException("JWT Secret not configured");
            
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => 
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => 
                (c.Type == ClaimTypes.Role && c.Value == "1"))));
                
    options.AddPolicy("RequireDoctorRole", policy => 
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => 
                (c.Type == ClaimTypes.Role && c.Value == "2"))));
                
    options.AddPolicy("RequirePatientRole", policy => 
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => 
                (c.Type == ClaimTypes.Role && c.Value == "3"))));
});

   

    // Add Swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HMS API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMS API v1"));
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });}
}}