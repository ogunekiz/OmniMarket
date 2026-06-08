var builder = WebApplication.CreateBuilder(args);

// YARP servislerini IoC Container'a ekliyoruz ve ayarlarý appsettings'ten okuyoruz
builder.Services.AddReverseProxy()
		.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


app.UseHttpsRedirection();

// YARP ara yazýlýmýný (Middleware) aktif ediyoruz
app.MapReverseProxy();

app.Run();

