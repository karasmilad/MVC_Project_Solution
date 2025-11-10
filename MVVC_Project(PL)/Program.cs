using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Project_BLL_;
using MVC_Project_BLL_.Services.Attachment_Service;
using MVC_Project_BLL_.Services.Classes;
using MVC_Project_BLL_.Services.EmailSender;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Data.DBContext;
using MVC_Project_DAL_.Models.IdentityModels;
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
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
        #endregion
            #region UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            #region AttachmentService
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            #endregion
            #region DepartmentControllerService
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            #endregion
            #region EmployeeControllerService
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            #endregion
            #region IdentitdyDBContext
            builder.Services
                   .AddIdentity<ApplicationUser, IdentityRole>
                   (
                    options =>
                    {
                        //options.Password.RequireNonAlphanumeric = false;
                        options.User.RequireUniqueEmail = true;
                        options.Password.RequiredLength = 8;
                    }

                   )
                   .AddEntityFrameworkStores<ApplicationDBContext>()
                   .AddDefaultTokenProviders();
            #endregion
            #region AutoMapper
            builder.Services.AddAutoMapper(E => E.AddProfile(new MappingProfiles()));
            #endregion
            #region EmailSenderService
            builder.Services.AddScoped<IEmailSender, EmailSender>();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
 