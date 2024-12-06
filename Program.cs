using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using BlazorSample.Helpers;
using Microsoft.AspNetCore.Mvc;
using learn_aca_entra.Components;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthHelperService>();


// var tenantId = builder.Configuration.GetValue<string>("AzureAd:TenantId")!;
// var vaultUri = builder.Configuration.GetValue<string>("AzureAd:VaultUri")!;
// var secretName = builder.Configuration.GetValue<string>("AzureAd:SecretName")!;
// var clientSecret = string.Empty;

// if (!builder.Environment.IsDevelopment())
// {
//     clientSecret = AzureHelper.GetKeyVaultSecret(tenantId, vaultUri, secretName);
// }
// else{
//     clientSecret = builder.Configuration["AzureAd:ClientSecret"];
// }

//     builder.Services.Configure<MicrosoftIdentityOptions>(
//         OpenIdConnectDefaults.AuthenticationScheme,
//         options =>
//         {
//             options.ClientSecret = clientSecret;
//         });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();




app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
   context.Request.Scheme = "https";
   await next.Invoke();
});

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapGroup("/authentication").MapLoginAndLogout();

app.Run();
