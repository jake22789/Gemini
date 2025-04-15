var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//app.Run();

HttpClient client = new HttpClient();
string url="";
var responce = await client.GetAsync(url);
Console.WriteLine(responce.Content.ToString());
