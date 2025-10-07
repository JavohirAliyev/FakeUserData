var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapBlazorHub();
app.UseStaticFiles();
app.MapFallbackToPage("/_Host");

app.Run();
