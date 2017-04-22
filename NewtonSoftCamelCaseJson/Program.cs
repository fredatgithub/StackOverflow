using System;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace temp
{
    class Program
    {
static void Main(string[] args)
{
    if (args.Length != 1)
    {
        Console.WriteLine("Please pass a file name e.g. 'dotnet run -- C:/path/to/some/file.json'.");
        return;
    }

    var path = args[0];
    var json = File.ReadAllText(path); // maybe-someday: add error handling
    var obj = JsonConvert.DeserializeObject<ExpandoObject>(json);
    var camel = JsonConvert.SerializeObject(
        obj,
        Formatting.Indented,
        new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

    File.WriteAllText(path, camel);
}
    }
}
