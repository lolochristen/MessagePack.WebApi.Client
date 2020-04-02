using System;
using System.Collections.Generic;
using System.Linq;
using Namotion.Reflection;
using NJsonSchema.Generation;

namespace MessagePack.NSwag
{
    public class MessagePackAttributesSchemaProcessor : NJsonSchema.Generation.ISchemaProcessor
    {
        public void Process(SchemaProcessorContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var msgPackAttr = context.Type.GetCustomAttributes(true).OfType<MessagePack.MessagePackObjectAttribute>().SingleOrDefault();
            if (msgPackAttr != null)
            {
                if (context.Schema.ExtensionData == null)
                    context.Schema.ExtensionData = new Dictionary<string, object>();

                context.Schema.ExtensionData.Add("x-msgpack", true);

                // loop through types
                foreach (var properties in context.Type.GetProperties())
                {
                    var name = context.Generator.GetPropertyName(null, properties.ToContextualMember());

                    if (!context.Schema.Properties.ContainsKey(name))
                        continue;

                    var schemaProp = context.Schema.Properties[name];

                    var keyAttr = properties.GetCustomAttributes(true).OfType<MessagePack.KeyAttribute>().SingleOrDefault();
                    if (keyAttr != null && context.Schema.Properties.ContainsKey(name))
                    {
                        if (schemaProp.ExtensionData == null)
                            schemaProp.ExtensionData = new Dictionary<string, object>();
                        schemaProp.ExtensionData.Add("x-msgpack-key", keyAttr.IntKey);
                    }

                    var ignoreAttr = properties.GetCustomAttributes(true).OfType<MessagePack.IgnoreMemberAttribute>().SingleOrDefault();
                    if (ignoreAttr != null)
                    {
                        if (schemaProp.ExtensionData == null)
                            schemaProp.ExtensionData = new Dictionary<string, object>();
                        schemaProp.ExtensionData.Add("x-msgpack-ignore", true);
                    }
                }
            }
        }
    }
}