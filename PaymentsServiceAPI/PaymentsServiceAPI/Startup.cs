using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
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
                        //Url = "https://twitter.com/spboyer"
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