using Microsoft.EntityFrameworkCore;
using PFM.Api.Formatters;
using PFM.Infrastructure.DependencyInjection;
using PFM.Infrastructure.Persistence.DbContexts;
using PFM.Application.UseCases.Catagories.Commands.Import;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PFMDbContext>();
builder.Services.AddInfrastructureServices();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ImportCategoriesCommandHandler).Assembly);

});
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new CsvInputFormatter());
})
    .AddJsonOptions(o => { /* ... */ })
    .AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();                         
    c.OperationFilter<PFM.Api.Swagger.CsvSingleSchemaFilter>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PFMDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errApp =>
{
    errApp.Run(async ctx =>
    {
      
        ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        ctx.Response.ContentType = "application/json";

        await ctx.Response.WriteAsJsonAsync(new
        {
            message = "The service is not available, please try again later."
        });
    });
});



app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();