using Azure.Identity;
using IBBS.API.IOC;
using IBBS.API.Middleware;
using Microsoft.OpenApi.Models;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(ConfigurationConstants.LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

var credentials = builder.Environment.IsDevelopment()
    ? new DefaultAzureCredential()
    : new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = builder.Configuration[ConfigurationConstants.ManagedIdentityClientIdConstant] });

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(SwaggerUIConstants.ApiVersion, new OpenApiInfo
    {
        Title = SwaggerUIConstants.ApplicationAPIName,
        Version = SwaggerUIConstants.ApiVersion,
        Description = SwaggerUIConstants.SwaggerDescription,
        Contact = new OpenApiContact
        {
            Name = SwaggerUIConstants.AuthorDetails.Name,
            Email = SwaggerUIConstants.AuthorDetails.Email
        }

    });
    options.EnableAnnotations();
});

builder.ConfigureAzureAppConfiguration(credentials);
builder.ConfigureApiServices();

builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(SwaggerUIConstants.SwaggerEndpointUrl, $"{SwaggerUIConstants.ApplicationAPIName}.{SwaggerUIConstants.ApiVersion}");
        c.RoutePrefix = SwaggerUIConstants.SwaggerUiPrefix;
    });
}

app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
