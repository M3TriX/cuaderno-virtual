using Cuaderno_virtual.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//aquí se realiza una inyección de dependencias para hacer uso de la base datos
builder.Services.AddDbContext<CuadernowebContext>(
options =>
{
    //hago uso de la cadena de conexión que he establecido para conectar a mysql
    //la cadena de conexión se establece en el archivo appsetting.json
    options.UseMySql(builder.Configuration.GetConnectionString("default"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
});

var app = builder.Build();

//este scope ha sido declarado con el objetivo que cuando se inicialice el proyecto cree la base de dato si no existe
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CuadernowebContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
