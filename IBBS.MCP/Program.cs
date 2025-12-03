using Azure.Identity;
using IBBS.MCP.IOC;
using IBBS.MCP.Middleware;
using Microsoft.OpenApi;
using static IBBS.MCP.Helpers.MCPConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(ConfigurationConstants.LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

var credentials = builder.Environment.IsDevelopment()
    ? new DefaultAzureCredential()
    : new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = builder.Configuration[ConfigurationConstants.ManagedIdentityClientIdConstant] });

builder.Services.AddEndpointsApiExplorer();

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
builder.ConfigureMcpServices();

builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMcpServer().WithHttpTransport().WithToolsFromAssembly();

var app = builder.Build();

app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapMcp("/ibbs").RequireAuthorization();

app.Run();