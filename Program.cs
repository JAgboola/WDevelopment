using RateMyProfessors.Models;
using RateMyProfessors.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddTransient<JsonFileProfessorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

//search feature
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// not the best way to show Json
//app.UseEndpoints(endpoints =>
//  {
//      endpoints.MapGet("/professors", (context) =>
//      {
//          app.MapRazorPages();
//          var professors = app.ApplicationServices.GetService<JsonFileProfessorService>().GetProfessors();
//          var json = JsonSerializer.Serialize<IEnumerable<Professor>>(professors);
//          return context.Response.WriteAsync(json);

//      });

//  });


app.Run();
