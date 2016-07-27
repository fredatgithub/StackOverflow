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

            // TODO Softcode the 'Content-length' header.            
            response.ContentLength = 13646;
            var contentLength = response.ContentLength.Value;

            await response.WriteAsync(Boundary + CrLf);

            var blue = new FileInfo("./blue.jpg");
            var red = new FileInfo("./red.jpg");
            var green = new FileInfo("./green.jpg");

            long start = 0;
            long end = blue.Length;
            await AddImage(response, blue, start, end, contentLength);

            start = end + 1;
            end = start + red.Length;
            await AddImage(response, red, start, end, contentLength);

            start = end + 1;
            end = start + green.Length;
            await AddImage(response, green, start, end, contentLength);

            response.Body.Flush();
        });
    }

    private async Task AddImage(HttpResponse response, FileInfo fileInfo,
        long start, long end, long total)
    {
        var bytes = File.ReadAllBytes(fileInfo.FullName);
        var file = new FileContentResult(bytes, "image/jpg");

        await response
            .WriteAsync($"Content-type: {file.ContentType.ToString()}" + CrLf);

        await response
            .WriteAsync($"Content-disposition: attachment; filename={fileInfo.Name}" + CrLf);

        await response
            .WriteAsync($"Content-range: bytes {start}-{end}/{total}" + CrLf);

        await response.WriteAsync(CrLf);
        await response.Body.WriteAsync(
            file.FileContents,
            offset: 0,
            count: file.FileContents.Length);
        await response.WriteAsync(CrLf);

        await response.WriteAsync(Boundary + CrLf);
    }
}
