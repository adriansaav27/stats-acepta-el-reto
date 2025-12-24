using Microsoft.OpenApi;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Servicios.
builder.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "CrawlerAceptaElReto",
		Version = "latest"
	});

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
	if (File.Exists(xmlPath))
		options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowCors", policy =>
		policy.SetIsOriginAllowed(_ => true)
			  .AllowAnyMethod()
			  .AllowAnyHeader()
			  .AllowCredentials());
});

var app = builder.Build();

// Middlewares.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseSwagger(c =>
	{
		c.PreSerializeFilters.Add((swaggerDoc, _) =>
		{
			swaggerDoc.Servers = [new OpenApiServer { Url = "/acepta-el-reto" }];
		});
	});
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("AllowCors");
app.MapControllers();
app.Run();
