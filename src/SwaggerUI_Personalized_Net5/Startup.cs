using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SwaggerUI_Personalized.Modules.Swagger;  // Refactored extension method 
using System;

namespace SwaggerUI_Personalized
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

            services.AddControllers();

            services.AddSwagger();  // Step 7 to adding SWAGGER
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var vPersonalised  = Convert.ToBoolean(Configuration["CustomSwaggerUi:Personalised"]);
            var vCustomHeader  = Configuration["CustomSwaggerUi:HeaderImg"];
            var vCustomTitle   = Configuration["CustomSwaggerUi:DocTitle"];
            var vCustomPathCss = Configuration["CustomSwaggerUi:PathCss"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(); // for static files

            // adding the enabling the middleware to generate the swagger endpoints 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Modified V.1");
                c.RoutePrefix = string.Empty;

                
                if (vPersonalised) { // True If we want to use a custom swagger ui
                    c.DocumentTitle = vCustomTitle; // Can be the company name.
                    c.HeadContent = vCustomHeader; // we add a custom image for the header                                              
                    c.InjectStylesheet(vCustomPathCss); // We add this custom css styles to Swagger.ui
                };

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
