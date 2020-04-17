using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestWebApi.Models
{
    public class PatchHelper
    {
        public static T Update<T>(string raw, object src, T dst)
        {
            JsonElement element = JsonSerializer.Deserialize<JsonElement>(raw);
            return Update(element, src, dst);
        }

        public static T Update<T>(JsonElement element, object src, T dst)
        {
            PropertyInfo[] dstProperties = dst.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CustomAttributes.All(a => a.AttributeType != typeof(JsonIgnoreAttribute))).ToArray();

            PropertyInfo[] srcProperties = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CustomAttributes.All(a => a.AttributeType != typeof(JsonIgnoreAttribute))).ToArray();

            foreach (PropertyInfo srcProp in srcProperties)
            {
                var ch = srcProp.Name.ToCharArray();
                ch[0] = char.ToLower(ch[0]);
                var jsonPropertyName = new string(ch);
                if (element.TryGetProperty(jsonPropertyName, out var value))
                {
                    var dstProp = dstProperties.FirstOrDefault(x => x.Name == srcProp.Name);
                    if (dstProp == null || dstProp.PropertyType != srcProp.PropertyType)
                    {
                        continue;
                    }

                    if (value.ValueKind == JsonValueKind.Object)
                    {
                         Update(value, srcProp.GetValue(src), dstProp.GetValue(dst));
                         continue;
                    } 

                    dstProp.SetValue(dst, srcProp.GetValue(src));
                }
            }

            return dst;
        }
    }
}