namespace Gah.HC.Spa
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus.Configuration;
    using Gah.HC.Commands;
    using Gah.HC.Commands.Handlers;
    using Gah.HC.Domain;
    using Gah.HC.Events;
    using Gah.HC.Events.Handlers;
    using Gah.HC.Queries;
    using Gah.HC.Queries.Handlers;
    using Gah.HC.Repository;
    using Gah.HC.Repository.Sql;
    using Gah.HC.Repository.Sql.Data;
    using Gah.HC.Spa.Authorization;
    using Gah.HC.Spa.Authorization.Handler;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital Capacity API");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddDbContext<HospitalCapacityContext>(
                options => options.UseSqlServer(
                    this.Configuration.GetConnectionString("Database"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(HospitalCapacityContext).Assembly.FullName);
                        builder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<HospitalCapacityContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (
                        ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) &&
                        ctx.Response.StatusCode == StatusCodes.Status200OK)
                        {
                            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }

                        return Task.CompletedTask;
                    },
                };
                options.AccessDeniedPath = "/acessDenied";
                options.Cookie.Name = "app-hospital-capacity";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/manage/login";
                //// ReturnUrlParameter requires
                ////using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "AddHospitalPolicy",
                    policy => policy.Requirements.Add(new CreateHospitalRequirement()));

                options.AddPolicy(
                    "RapidHospitalUpdatePolicy",
                    policy => policy.Requirements.Add(new RapidHospitalUpdateRequirement()));

                options.AddPolicy(
                    "UpdateHospitalPolicy",
                    policy => policy.Requirements.Add(new UpdateHospitalRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, CreateHospitalRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, RapidHospitalUpdateRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UpdateHospitalRequirementHandler>();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ClaimsPrincipalFactory>();

            services.AddScoped<IHospitalCapacityUow, HospitalCapacityUow>();

            services.AddDomainBus()

                // Events
                .AddEvent<HospitalChangedEvent, HospitalChangedEventHandler>()

                // Queryies
                .AddQuery<MatchRegionByNameQuery, List<Region>, MatchRegionByNameQueryHandler>()
                .AddQuery<FindHospitalBySlugOrIdQuery, Hospital, FindHospitalBySlugOrIdQueryHandler>()
                .AddQuery<FindHospitalsQuery, List<HospitalView>, FindHospitalsQueryHandler>()
                .AddQuery<GetLastHospitalCapacityQuery, List<HospitalCapacity>, GetLastHospitalCapacityQueryHandler>()
                .AddQuery<FindUserByEmailQuery, AppUser, FindUserByEmailQueryHandler>()

                // Commands
                .AddCommand<RegisterSuperUserCommand, RegisterSuperUserCommandHandler>()
                .AddCommand<RegisterHospitalUserCommand, RegisterHospitalUserCommandHandler>()
                .AddCommand<UpdateHospitalCommand, UpdateHospitalCommandHandler>()
                .AddCommand<RegisterRegionUserCommand, RegisterRegionUserCommandHandler>()
                .AddCommand<RegisterHospitalUserCommand, RegisterHospitalUserCommandHandler>()
                .AddCommand<RapidHospitalUpdateCommand, RapidHospitalUpdateCommandHandler>()
                .AddCommand<CreateHospitalCommand, CreateHospitalCommandHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Hospital Capacity API",
                        Version = "v1",
                    });
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
    }
}
