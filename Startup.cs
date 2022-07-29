using IstimAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using IstimAPI.Data.IRepositories;
using IstimAPI.Data.Repositories;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using IstimAPI.Shared;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Identity;
using IstimAPI.Models;
using System.Threading.Tasks;
using IstimAPI.Services;

namespace IstimAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(
                                "http://localhost:4200"
                            )
                            .AllowCredentials();
                    });
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes
                    .Concat(new[] { "application/json" });
            });

            services.AddControllers();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
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

            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<TokenService, TokenService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IAgeRangeRepository, AgeRangeRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IUserGameRepository, UserGameRepository>();

            services.AddDefaultIdentity<ApplicationUser>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            services.AddDbContext<DataContext>(opt =>
               opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IstimAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IstimAPI v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateDefaultUserAsync(serviceProvider).Wait();
        }

        private async Task CreateDefaultUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("admin@mail.com");

            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    Role = "ADMIN"
                };

                await userManager.CreateAsync(newUser, "1qazZAQ!");
            }
        }
    }
}
