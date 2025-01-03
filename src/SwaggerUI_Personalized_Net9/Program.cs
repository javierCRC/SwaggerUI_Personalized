using SwaggerUI_Personalized_Net9.Modules.Features;
using SwaggerUI_Personalized_Net9.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwagger(); // Step 2: Adding a Swagger configuration extension method
builder.Services.AddFeature(builder.Configuration); //  Step 6: Adding CORS and MVC features to the API

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Step 3: adding and enabling the middleware to generate the swagger documentation
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Modified V.3");
        c.RoutePrefix = string.Empty;

        // Step 5: adding custom swagger personalized styles
        var vPersonalised = Convert.ToBoolean(builder.Configuration["CustomSwaggerUi:Personalised"]);
        if (vPersonalised) // True If we want to use a custom swagger ui
        {
            c.DocumentTitle = builder.Configuration["CustomSwaggerUi:DocTitle"]; // Can be the company name.
            c.HeadContent = builder.Configuration["CustomSwaggerUi:HeaderImg"];  // we add a custom image for the header                                              
            c.InjectStylesheet(builder.Configuration["CustomSwaggerUi:PathCss"]); // We add this custom css styles to Swagger.ui
        };
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Step 4: enabling the use of static files

app.UseAuthorization();

app.MapControllers();

app.Run();
