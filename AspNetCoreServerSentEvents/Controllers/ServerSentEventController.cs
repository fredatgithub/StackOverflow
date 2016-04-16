using Microsoft.AspNetCore.Mvc;

namespace ServerSentEventSample
{
    [Route("/api/sse")]
    public class ServerSentEventController
    {
        [HttpGet]
        public IActionResult Get()
        {
            // in progress. :)
            var result = new ContentResult();
            result.Content = "data: Well hello there.\r\r";
            result.ContentType = "text/event-stream";
            result.StatusCode = 200;
            return result;
        }    
    }
} 