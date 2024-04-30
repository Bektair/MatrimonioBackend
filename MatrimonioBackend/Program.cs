using dotenv.net;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(); //To enable JsonPatch
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
DotEnv.Load();


//builder.Services.AddDbContext<WeddingContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

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
