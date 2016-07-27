using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    private const string CrLf = "\r\n";
    private const string Boundary = "--THIS_STRING_SEPARATES";
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var response = context.Response;
            response.ContentType = $"multipart/byteranges; boundary={Boundary}";

            await response.WriteAsync(Boundary + CrLf);
            await AddImage(response, "./blue.jpg");
            await AddImage(response, "./red.jpg");
            await AddImage(response, "./green.jpg");
            response.Body.Flush();
        });
    }

    private async Task AddImage(HttpResponse response, string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception();
        }

        var bytes = File.ReadAllBytes(path);
        var file = new FileContentResult(bytes, "image/jpg");

        await response.WriteAsync("Content-type: " + file.ContentType.ToString() + CrLf);

        // TODO Add Content-range header. This will take some thought. :)

        await response.WriteAsync(CrLf);
        await response.Body.WriteAsync(
            file.FileContents, 
            offset: 0, 
            count: file.FileContents.Length);
        await response.WriteAsync(CrLf);
        await response.WriteAsync(Boundary + CrLf);
    }
}
