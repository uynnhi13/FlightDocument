using FlightDocument.Service;
using FlightDocument.Service.IService;
using FlightDocument.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IDocumentService,IDocumentService>();
SD.DocumentAPIBase = builder.Configuration["ServiceUrls:DocumentAPI"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

// ??ng ký interface I... và th?c hi?n các ch?c n?ng c?a nó trong file
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
