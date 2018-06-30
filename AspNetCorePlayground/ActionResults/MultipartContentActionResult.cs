using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace AspNetCorePlayground
{
    // https://www.w3.org/Protocols/rfc1341/7_2_Multipart.html
    public class MyFirstActionResult : ActionResult
    {
        public override Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }

}