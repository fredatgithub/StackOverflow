using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CustomControllerNames 
{
    public class MyControllerFeatureProvider : ControllerFeatureProvider 
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);
            if (!isController)
            {
                isController = 
                    typeInfo.Name.EndsWith("Controller`1", StringComparison.OrdinalIgnoreCase) ||
                    typeInfo.Name.EndsWith("Foobar", StringComparison.OrdinalIgnoreCase);
            }
            
            Console.WriteLine($"{typeInfo.Name} IsController: {isController}.");
            
            return isController;
        }
    }
}
    