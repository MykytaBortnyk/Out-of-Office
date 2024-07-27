using CRM.Components;
using CRM.Components.Account;
using CRM.Data;
using CRM.Models.Identity;
using CRM.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();

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

#region DI
builder.Services.AddScoped<IRepository, Repository>();
#endregion

#region Identity
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddDbContextFactory<IdentityContext>((s, opt) =>
    opt.UseNpgsql(dataSource)
.UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
#endregion

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
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
