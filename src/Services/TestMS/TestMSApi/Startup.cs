using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Infrastructure;
using TestMSApi.Infrastructure.Filters;
using TestMSApi.Seed;

namespace TestMSApi
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
            var connectionString = Configuration.GetConnectionString("MySQL");
            //options.UseMySql(@"userid=root;pwd=TMN!passw0rd;database=sample;server=localhost;"
            connectionString = @"userid=docker_user;pwd=docker_pass;database=docker_db;server=localhost; ";
            connectionString = Configuration["SqlConnectionString"];
            services.AddDbContext<TodoContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services
                .AddControllers().AddDapr();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eShopOnDapr - TestMSApi", Version = "v1" });

                //identityUrl
                var identityUrlExternal = Configuration.GetValue<string>("IdentityUrlExternal");
                //AddSecurityDefinition
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            //IdentityUrl Authorization
                            AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                            //IdentityUrl Token
                            TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                            //Scope
                            Scopes = new Dictionary<string, string>()
                            {
                                //Scope名   任意の説明
                                { "testms", "TestMS API" }
                            }
                        }
                    }
                });
                //OperationFilter
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            // Prevent mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    //ApiResource名
                    options.Audience = "testmsapi";
                    //検証先（IdentityUrl）
                    options.Authority = Configuration.GetValue<string>("IdentityUrl");
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    //claimType=scope scope名=testms
                    policy.RequireClaim("scope", "testms");
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoContext todoContext)
        {
#if DEBUG
            //デプロイ環境には別途コマンド発行の方が良いかな
            todoContext.Database.Migrate();
            ContextInizializer.Seed(todoContext);
#endif


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestMSApi v1");
                    //IdentityServerのClientID
                    c.OAuthClientId("testmsswaggerui");
                    //IdentityServerのClientName
                    c.OAuthAppName("TestMS Swagger UI");
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
