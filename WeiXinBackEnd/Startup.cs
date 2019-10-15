using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeiXinBackEnd.Application.ImgSec;
using WeiXinBackEnd.Application.UserAccount;
using WeiXinBackEnd.Core.Extension;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Extensions;

namespace WeiXinBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        private const string DefaultCorsPolicyName = "localhost";


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure CORS for UI
            services.AddCors(options =>
                options.AddPolicy(
                    DefaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            Configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                ));

            services.AddWeChatService(new WeChatClientOptions(Configuration["App:AppId"], Configuration["App:AppSecret"]),
                config => { config.RefreshTimeSpan = 80; });

            #region DI

            services.AddTransient<IImgSecApp, ImgSecApp>();
            services.AddTransient<IUserAccountApp, UserAccountApp>();
            #endregion


            services.AddSwaggerDocument(document =>
            {
                document.DocumentName = "NSwag";

                #region OAuth2

                #endregion

                // Post process the generated document
                document.PostProcess = d =>
                {
                    d.Info.Title = "WeiXin";
                    d.Info.Description = "Api文档";
                };
            });
            // Register a OpenAPI v3.0 document generator
            services.AddOpenApiDocument(
                document => document.DocumentName = "OpenAPI");
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger Middleware
            //// Add OpenAPI/Swagger middleware to serve documents and web UIs

            // URLs:
            // - http://localhost:5000/swagger/nswag/swagger.json
            // - http://localhost:5000/swagger/openapi/swagger.json
            // - http://localhost:5000/swagger

            #region Location Route

            // Add Swagger 2.0 document serving middleware
            app.UseOpenApi();

            // Add web UIs to interact with the document
            app.UseSwaggerUi3(options =>
            {
                #region IIS Virtual Host

                options.TransformToExternalPath = (internalUiRoute, request) =>
                {
                    if (internalUiRoute.StartsWith("/") &&
                        internalUiRoute.StartsWith(request.PathBase) == false)
                    {
                        return request.PathBase + internalUiRoute;
                    }
                    return internalUiRoute;
                };
                #endregion

            });

            #endregion

            #endregion
        }
    }
}
