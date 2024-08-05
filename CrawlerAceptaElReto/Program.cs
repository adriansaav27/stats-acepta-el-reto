using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
 options =>
 {
     options.AddPolicy(
     "AllowCors",
     builder =>
     {
         builder.SetIsOriginAllowed((x) => true); // Permite hacer llamadas AJAX.
         builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
     });
 });

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CrawlerAceptaElReto", Version = "latest" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers =
        [
            new() { Url = $"/acepta-el-reto"},
        ]);
    });
}

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("AllowCors");
app.MapControllers();
app.Run();