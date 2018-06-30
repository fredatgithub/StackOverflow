using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace AspNetCoreUriToAssoc.Controllers
{
    public class PropertiesController : Controller
    {
        public IActionResult Search([FromPath]BedsEtCetera model)
        {
            return Json(model);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromPath : Attribute, IBindingSourceMetadata, IModelNameProvider
    {
        /// <inheritdoc />
        public BindingSource BindingSource => BindingSource.Custom;

        /// <inheritdoc />
        public string Name { get; set; }
    }

    public class PathValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            var provider = new PathValueProvider(
                BindingSource.Custom, 
                context.ActionContext.RouteData.Values);

            context.ValueProviders.Add(provider);

            return Task.CompletedTask;
        }
    }

    public class PathValueProvider : IValueProvider
    {
        public Dictionary<string, string> _values { get; }

        public PathValueProvider(BindingSource bindingSource, RouteValueDictionary values)
        {
            if(!values.TryGetValue("path", out var path)) 
                throw new InvalidOperationException("Route value 'path' was not present in the route.");

            _values = ToDictionaryFromUriPath(path as string);
        }

        public bool ContainsPrefix(string prefix) => _values.ContainsKey(prefix);

        public ValueProviderResult GetValue(string key)
        {
            key = key.ToLower(); // case insensitive model binding
            if(!_values.TryGetValue(key, out var value)) {
                return ValueProviderResult.None;
            }

            return new ValueProviderResult(value);
        }

        Dictionary<string, string> ToDictionaryFromUriPath(string path) {
            var parts = path.Split('/');
            var dictionary = new Dictionary<string, string>();
            for(var i = 0; i < parts.Length; i++)
            {
                if(i % 2 != 0) continue;
                var key = parts[i].ToLower(); // case insensitive model binding
                var value = parts[i + 1];
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }

    public class BedsEtCetera 
    {
        public int Beds { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
    }
}
