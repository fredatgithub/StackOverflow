using Microsoft.AspNetCore.Mvc;

namespace MyXmlSample
{
    [Route("xml")]
    public class MyXmlController
    {
        public static string xmlString = 
        
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<sample>
  Hello World.
</sample>";
                     
        [HttpGet]
        public ContentResult Get()
        {
            return new ContentResult
            {
                ContentType = "application/xml",
                Content = xmlString,
                StatusCode = 200
            };
        }
    }
}