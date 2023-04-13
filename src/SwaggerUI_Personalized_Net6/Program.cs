using SwaggerUI_Personalized_Net6.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwagger(); // adding SWAGGER

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    
    app.UseSwagger(); // adding the enabling the middleware to generate the swagger endpoints 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Modified V.2");
        c.RoutePrefix = string.Empty;

        var vPersonalised = Convert.ToBoolean(builder.Configuration["CustomSwaggerUi:Personalised"]);
        if (vPersonalised)
        { // True If we want to use a custom swagger ui
            c.DocumentTitle = builder.Configuration["CustomSwaggerUi:DocTitle"]; // Can be the company name.
            c.HeadContent = builder.Configuration["CustomSwaggerUi:HeaderImg"]; // we add a custom image for the header                                              
            c.InjectStylesheet(builder.Configuration["CustomSwaggerUi:PathCss"]); // We add this custom css styles to Swagger.ui
        };

    });

}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();      // for static files in wwwroot

app.UseAuthorization();

app.MapControllers();

app.Run();
