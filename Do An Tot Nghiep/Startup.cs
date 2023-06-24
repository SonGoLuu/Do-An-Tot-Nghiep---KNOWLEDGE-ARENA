using Do_An_Tot_Nghiep.Hubs;
using Do_An_Tot_Nghiep.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Do_An_Tot_Nghiep
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication()
                    .AddGoogle(googleOptions =>
             {
                 // Đọc thông tin Authentication:Google từ appsettings.json
                 IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

                 // Thiết lập ClientID và ClientSecret để truy cập API google
                 googleOptions.ClientId = googleAuthNSection["ClientId"];
                 googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                 // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
                 //googleOptions.CallbackPath = "/dang-nhap-tu-google";

             })
                .AddFacebook(facebookOptions =>
                {
                    // Đọc cấu hình
                    IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
                    facebookOptions.AppId = facebookAuthNSection["AppId"];
                    facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
                    // Thiết lập đường dẫn Facebook chuyển hướng đến
                    facebookOptions.CallbackPath = "/signin-facebook";
                });
            services.AddSignalR();
            services.AddDbContext<dbKA>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("dbKA")));
            services.AddDefaultIdentity<IdentityUser>(config =>
            { 
                config.SignIn.RequireConfirmedAccount = true;
                config.User.RequireUniqueEmail = true; 
            })
                .AddEntityFrameworkStores<dbKA>()
               .AddDefaultTokenProviders(); 
            ;
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "XepHang",
                    pattern: "xephang",
                    defaults: new { controller = "XepHang", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "BacXepHang",
                    pattern: "bacxephang",
                    defaults: new { controller = "BacXepHang", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "PhongCho",
                    pattern: "phong-cho",
                    defaults: new { controller = "PhongCho", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "CauHoi",
                    pattern: "cauhoi",
                    defaults: new { controller = "CauHoi", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "ChuongNgaiVat",
                    pattern: "chuongngaivat",
                    defaults: new { controller = "ChuongNgaiVat", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<GameHub>("/gameHub");
                
            });

            
        }
    }
}
