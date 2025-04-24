//https://ai.google.dev/gemini-api/docs/quickstart?lang=rest
//https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

using System.Runtime.ExceptionServices;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
DotNetEnv.Env.Load();
var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


app.MapGet("/", () => "Hello World!");

app.MapGet("/ai",async (string prompt)=>{

System.Console.WriteLine(prompt);


string key = Environment.GetEnvironmentVariable("GEMINI_KEY");
if(key == null){
    throw new Exception("GEmini_key not set in enviornment");
}

HttpClient client = new HttpClient();
string url=$"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={key}";
var body = new ContentData(
    [
        new ContentsItem(
            [
                new Dictionary<string,string>()
                {
                    {"text",prompt}
                }
            ]
        )
    ]
);
var responce = await client.PostAsJsonAsync(url,body);
var jsonData = await responce.Content.ReadAsStringAsync();
//Console.WriteLine(jsonData);
var structuredResponse = JsonSerializer.Deserialize<ResponseData>(jsonData,new JsonSerializerOptions(JsonSerializerDefaults.Web));

//Console.WriteLine(JsonSerializer.Serialize(structuredResponse));
//Console.WriteLine(structuredResponse.candidates[0].content.parts[0]["text"]);
return structuredResponse.candidates[0].content.parts[0]["text"];
});

app.Run();
record ContentsItem(List<Dictionary<string,string>>Parts);
record ContentData(List<ContentsItem>Contents);
record ResponseData(List<ResponceContentData>candidates);

record ResponseContentsItem(List<Dictionary<string,string>>parts);
record ResponceContentData(ResponseContentsItem content);