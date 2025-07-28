using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PFM.Api.Formatters;
using PFM.Api.Swagger;
using PFM.Application.Mapping;
using PFM.Application.UseCases.Catagories.Commands.Import;
using PFM.Application.UseCases.Transaction.Commands.Import;
using PFM.Infrastructure.DependencyInjection;
using PFM.Infrastructure.Persistence.DbContexts;
using SixLabors.ImageSharp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PFMDbContext>();
builder.Services.AddInfrastructureServices();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(TransactionMappingProfile).Assembly);
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ImportCategoriesCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(ImportTransactionsCommandHandler).Assembly);


});
builder.Services
    .AddControllers(options =>
    {
        options.InputFormatters.Insert(0, new CsvInputFormatter());
    })
    .ConfigureApiBehaviorOptions(opts =>
    {
        opts.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(o => { /* ... */ })
    .AddXmlSerializerFormatters();
builder.Services.AddCors(options => options.AddPolicy( "cors", policy => policy.WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL")??"http://localhost:4200")
                                                                                                        .AllowAnyHeader()
                                                                                                        .AllowAnyMethod()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();                         
    c.OperationFilter<CsvSingleSchemaFilter>();
    c.OperationFilter<CsvTransactionSchemaFilter>();

});
builder.Configuration.AddJsonFile("Config/rules.json", optional: false);

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

app.UseCors("cors");



app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();