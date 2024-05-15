using dotenv.net;
using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Post>("Post");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Host.ConfigureAppConfiguration((configBuilder) =>
{
    configBuilder.Sources.Clear();
    DotEnv.Load();
    configBuilder.AddEnvironmentVariables();
});

var domain = builder.Configuration.GetValue<string>("AUTH0_DOMAIN");

builder.Services.AddControllers()
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
              $"https://{builder.Configuration.GetValue<string>("AUTH0_DOMAIN")}/";
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
    policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
});

var app = builder.Build();

app.UseCors("MyPolicy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
