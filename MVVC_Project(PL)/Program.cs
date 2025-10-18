using Microsoft.EntityFrameworkCore;
using MVC_Project_BLL_;
using MVC_Project_BLL_.Services.Classes;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Data.DBContext;
using MVC_Project_DAL_.Repositories.Classes;
using MVC_Project_DAL_.Repositories.Interfaces;

namespace MVVC_Project_PL_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #region Configure Services
            #region DBContext Service
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            #region DepartmentRepositoryService
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            #endregion
            #region EmployeeRepositoryService
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            #endregion
            #region AutoMapper
            builder.Services.AddAutoMapper(E => E.AddProfile(new MappingProfiles()));
            #endregion
            #region DepartmentControllerService
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            #endregion
            #endregion

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
        }
    }
}
