using CentralService.Admin.Orchestration;
using CentralService.Helper;
using CentralServiceAPI.web.Notification.Mappings;
using CentralServiceAPI.web.Notification.Orchestrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace PaymentsServiceAPI
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
            ConfigureInjectedOrchestrations(services);
            ConfigureInjectedServices(services);
            ConfigureInjectedMappings(services);

            services.AddCors(options1 =>
            {
                options1.AddPolicy("MyCorsPolicy", builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyOrigin()
                    .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<JWTSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Payments Service API",
                    Description = "A simple payments Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Mkhululi Mtshemla",
                        Email = "201402344@student.uj.ac.za",
                        Url = "https://github.com/mkay47/PaymentsApp"
                    }
                });
            });
        }

        public void ConfigureInjectedServices(IServiceCollection services)
        {
        }

        public void ConfigureInjectedMappings(IServiceCollection services)
        {
            services.AddSingleton<INotificationMapping, NotificationMapping>();
        }

        public void ConfigureInjectedOrchestrations(IServiceCollection services)
        {
            services.AddSingleton<INotificationOrchestration, NotificationOrchestration>();
            services.AddSingleton<IMarketOrchestration, MarketOrchestration>();
            services.AddSingleton<IUserOrchestration, UserOrchestration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO uncomment when we go live and add our teams computers IPs the the white list
            //app.UseMiddleware<IpRestrictionMiddleware>();

            app.UseSwagger();
            app.UseCors("MyCorsPolicy");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Payment Service API v1");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}