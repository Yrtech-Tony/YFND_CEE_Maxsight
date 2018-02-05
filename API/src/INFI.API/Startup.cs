using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using INFI.API.Context;
using INFI.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace INFI.API
{
    public class Startup
    {
        private static readonly string secretKey = "infisupersecret_secretkey!123";
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            DapperContext.Current.Configuration = builder.Build();

            //System.Diagnostics.Process.Start(
            //   "net.exe",
            //   "use \\\\CNVFCUSTIF01.home.e-kmall.com\\uploads \"y*Ta7i!u\" /user:\"HOME\\cheng.cuicui\""
            //);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            #region Services
            services.AddSingleton<IAppInfoService, AppInfoService>();
            services.AddSingleton<IUsersService, UsersService>();
            services.AddSingleton<INotifiMngService, NotifiMngService>();
            services.AddSingleton<INotifiFeedBService, NotifiFeedBService>();
            services.AddSingleton<INotifiApprovalService, NotifiApprovalService>();
            services.AddSingleton<IAttachmentMngService, AttachmentMngService>();
            services.AddSingleton<IUploadFileService, UploadFileService>();
            services.AddSingleton<ITourService, TourService>();
            services.AddSingleton<IImprovementService, ImprovementService>();
            services.AddSingleton<IPlanTaskMngService, PlanTaskMngService>();
            services.AddSingleton<ICasesInfoService, CasesInfoService>();
            services.AddSingleton<ICalenderMngService, CalenderMngService>();
            services.AddSingleton<IHomeMngService, HomeMngService>();
            services.AddSingleton<IStatisticService, StatisticService>();
            services.AddSingleton<IReportService, ReportService>();
            services.AddSingleton<IAppealMngService, AppealMngService>();
            services.AddSingleton<IRecordService, RecordService>();
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.Configure<MvcOptions>(options => options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(DapperContext.Current.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = false,
                ValidIssuer = "INFIIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = false,
                ValidAudience = "INFIAudience",

                // Validate the token expiry
                ValidateLifetime = false
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = false,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc();
            app.UseCors("AllowAll");
        }
    }
}
