using dotenv.net;
using JsonPatchSample;
using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Post>("Post");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var environment = builder.Environment;



builder.Host.ConfigureAppConfiguration((configBuilder) =>
{
    configBuilder.Sources.Clear();

   if( builder.Environment.IsDevelopment()){
        DotEnvOptions options = new DotEnvOptions(true,new List<string>() { ".env.development" });
        DotEnv.Load(options);
    } else
    {
        DotEnvOptions options = new DotEnvOptions(true, new List<string>() { ".env.prod" });
        DotEnv.Load(options);
    }
    configBuilder.AddEnvironmentVariables();
});



var AUTH0_DOMAIN = builder.Configuration.GetValue<string>("AUTH0_DOMAIN");


builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(20)
        .Count()
        .Expand()
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudentservice, StudentService>();

//builder.Services.AddDbContext<WeddingContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program).Assembly);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var audience =
              builder.Configuration.GetValue<string>("AUTH0_AUDIENCE");

        options.Authority =
              $"https://{AUTH0_DOMAIN}/";
        options.Audience = audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader(); // Can add client_origin_url here.
}));


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:users", policy =>
        policy.Requirements.Add(new HasScopeRequirement("read:users", $"https://{AUTH0_DOMAIN}/")));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors("MyPolicy");
//app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{ 
   app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}

app.Run();
