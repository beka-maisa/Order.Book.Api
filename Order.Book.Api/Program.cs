using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Book.Api.Middleware;
using Order.Book.Api.NotificationHandlers;
using Order.Book.Api.OrderHub;
using Order.Book.Application.Abstracts;
using Order.Book.Application.Handlers;
using Order.Book.Application.Notifications;
using Order.Book.Application.Validators;
using Order.Book.Infrastructure.Concretes;
using Order.Book.Infrastructure.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["apiVersion"] = typeof(ApiVersionRouteConstraint);
});


builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<UpdateOrderDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<OrderValidator>();
});

builder.Services.AddTransient<INotificationHandler<OrderAddedNotification>, OrderAddedNotificationHandler>();
builder.Services.AddTransient<INotificationHandler<OrderDeletedNotification>, OrderDeletedNotificationHandler>();
builder.Services.AddTransient<INotificationHandler<OrderUpdatedNotification>, OrderUpdatedNotificationHandler>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Book API",
        Version = "v1",
        Description = "We will consider an implementation of the Order Book," +
        " where the user can add/edit/delete orders, " +
        "as well as retrieve the complete Order Book."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateOrderHandler).Assembly));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add Razor Pages services
builder.Services.AddRazorPages();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Book API v1");
});

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<OrderBookHub>("/orderBookHub");
    endpoints.MapRazorPages();
});

app.Run();