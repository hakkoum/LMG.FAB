using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LMG.Fab.Data;
using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.Fab.Web.Models;
using LMG.Fab.Web.Services;
using LMG.FAB.Services;
using LMG.FAB.Services.Business;
using LMG.FAB.Services.FAB;
using LMG.FAB.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LMG.Fab.Web
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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<ReflmgContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ReflmgConnectionString"), opt => opt.UseRowNumberForPaging()),ServiceLifetime.Transient);
            services.AddDbContext<LMG_FABContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("FabConnectionString"), opt => opt.UseRowNumberForPaging()), ServiceLifetime.Transient);


            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITiersManager, TiersManager>();
            services.AddTransient<ILotManager, LotManager>();
            services.AddTransient<IAuditManager, AuditManager>();
            services.AddTransient<ISharedDataManager, SharedDataManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ILockManager, LockManager>();
            services.AddSingleton<IClaimsTransformation, ClaimsTransformerService>();
            services.AddSingleton<IDateFormater, DateFormater>();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("FAB_ADMIN", policy => policy.RequireClaim(CustomClaimTypes.FAB_ADMIN, "true"));
                options.AddPolicy("FAB_DEVIS", policy => policy.RequireClaim(CustomClaimTypes.FAB_DEVIS, "true"));
                options.AddPolicy("FAB_DEVIS_EDIT", policy => policy.RequireClaim(CustomClaimTypes.FAB_DEVIS_EDIT, "true"));
            });

            services.AddMvc()
           .AddJsonOptions(options =>
           {
               options.SerializerSettings.ContractResolver = new CustomJsonResolver();
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 100000000;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = false
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseExceptionHandler(
                     builder =>
                     {
                         builder.Run(
                         async context =>
                         {
                             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                             context.Response.ContentType = "application/json";
                             var ex = context.Features.Get<IExceptionHandlerFeature>();
                             if (ex != null)
                             {
                                 var err = JsonConvert.SerializeObject(new Error()
                                 {
                                     Stacktrace = ex.Error.StackTrace,
                                     Message = ex.Error.Message +Environment.NewLine + ex.Error.InnerException?.Message
                                 });
                                 await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                             }
                         });
                     }
                );

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });


            if (env.IsDevelopment())
            {

            }
        }
    }


}
