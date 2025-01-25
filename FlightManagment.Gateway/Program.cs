using FlightManagement.BusinessLogic;
using FlightManagement.DataAccessLayer;
using FlightManagment.Gateway.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(CancellationToken), serviceProvider =>
{
    IHttpContextAccessor httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    return httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
});
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
});
builder.Services.AddSwaggerGen();

builder.Services
    .AddBusinessLogicDependencies()
    .AddDataAccessLayerDependencies();

builder.Services.AddResponseCaching();

var app = builder.Build();


app.MapOpenApi();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
    c.EnableFilter();
    c.EnablePersistAuthorization();
    c.EnableTryItOutByDefault();
    c.DefaultModelsExpandDepth(-1);
});
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
