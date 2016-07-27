using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

public class Startup
{
    private const string CrLf = "\r\n";

    public void Configure(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var response = context.Response;
            response.Headers.Add("Content-Type", "multipart/byteranges; boundary=THIS_STRING_SEPARATES");

            if(!File.Exists("./goats01.jpg"))
            {
                throw new Exception();
            }

            var bytes = File.ReadAllBytes("./goats01.jpg");
            var file = new FileContentResult(bytes, "image/jpg");

            context.Response.ContentType = file.ContentType.ToString();

            await context.Response.Body.WriteAsync(
                file.FileContents, 
                offset:0, 
                count: file.FileContents.Length);

            response.Body.Flush();
        });
    }
}
