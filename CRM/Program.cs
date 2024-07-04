using CRM.Components;
using CRM.Data;
using CRM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Npgsql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .WriteTo.Console());
#endregion

#region EF Core
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration["ConnectionStrings:NpgsqlConnectionString"]);
dataSourceBuilder.MapEnum<CRM.Models.Enums.AbsenceReason>("AbsenceReason");
dataSourceBuilder.MapEnum<CRM.Models.Enums.Names>("Names");
dataSourceBuilder.MapEnum<CRM.Models.Enums.Positions>("Positions");
dataSourceBuilder.MapEnum<CRM.Models.Enums.ProjectType>("ProjectType");
dataSourceBuilder.MapEnum<CRM.Models.Enums.RequestStatus>("RequestStatus");
dataSourceBuilder.MapEnum<CRM.Models.Enums.Status>("Status");
dataSourceBuilder.MapEnum<CRM.Models.Enums.Subdivision>("Subd");
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContextFactory<CrmContext>((s, opt) =>
    opt.UseNpgsql(dataSource)
.UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));
#endregion

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
