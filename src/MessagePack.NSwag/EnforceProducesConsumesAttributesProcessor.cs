using System;
using System.Collections.Generic;
using System.Linq;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace MessagePack.NSwag
{
    public class EnforceProducesConsumesAttributesProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            dynamic consumesAttribute = context.MethodInfo
                .GetCustomAttributes(true)
                //.OfType<Microsoft.AspNetCore.Mvc.ConsumesAttribute>() // dynamic to avoid ref to asp.net core
                .SingleOrDefault(p => p.GetType().Name == "ConsumesAttribute");

            if (consumesAttribute == null)
            {
                consumesAttribute = context.MethodInfo.DeclaringType
                   .GetCustomAttributes(true)
                   //.OfType<Microsoft.AspNetCore.Mvc.ConsumesAttribute>()
                   .SingleOrDefault(p => p.GetType().Name == "ConsumesAttribute");
            }

            if (consumesAttribute != null && consumesAttribute.ContentTypes != null)
            {
                if (context.OperationDescription.Operation.Consumes == null)
                    context.OperationDescription.Operation.Consumes = new List<string>(consumesAttribute.ContentTypes);
                else
                    context.OperationDescription.Operation.Consumes.AddRange(consumesAttribute.ContentTypes);
            }

            dynamic producesAttribute = context.MethodInfo
                .GetCustomAttributes(true)
                //.OfType<Microsoft.AspNetCore.Mvc.ProducesAttribute>()
                .SingleOrDefault(p => p.GetType().Name == "ProducesAttribute");

            if (producesAttribute == null)
            {
                producesAttribute = context.MethodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    //.OfType<Microsoft.AspNetCore.Mvc.ProducesAttribute>()
                    .SingleOrDefault(p => p.GetType().Name == "ProducesAttribute");
                ;
            }

            if (producesAttribute != null && producesAttribute.ContentTypes != null)
            {
                if (context.OperationDescription.Operation.Produces == null)
                    context.OperationDescription.Operation.Produces = new List<string>(producesAttribute.ContentTypes);
                else
                    context.OperationDescription.Operation.Produces.AddRange(producesAttribute.ContentTypes);
            }

            return true;
        }
    }
}