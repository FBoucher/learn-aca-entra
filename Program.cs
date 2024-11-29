using learn_aca_entra.Components;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//add Microsoft Entra ID authentication
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseAuthorization();

//app.MapGroup("/authentication").MapLoginAndLogout();

app.Run();
