using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projet.Context;
using Projet.DAL;
using Projet.DAL.Contracts;
using Projet.BLL;
using Projet.BLL.Contract;

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
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("HMS_DB")); 
            // or UseSqlServer(...)

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ILabReportRepository, LabReportRepository>();

            // BLL Managers
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IMedicalHistoryManager, MedicalHistoryManager>();
            services.AddScoped<IAppointmentManager, AppointmentManager>();
            services.AddScoped<IMessageManager, MessageManager>();
            services.AddScoped<ILabReportManager, LabReportManager>();

            // Controllers
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}