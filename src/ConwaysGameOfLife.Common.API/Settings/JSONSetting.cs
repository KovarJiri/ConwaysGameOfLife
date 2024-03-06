using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Common.API.Settings
{
    public static class JSONSerializeOptions
    {
        public static void SetSerializationOptions(JsonSerializerOptions jsonSerializerOptions)
        {
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jsonSerializerOptions.IncludeFields = true;
            jsonSerializerOptions.AllowTrailingCommas = true;
        }
    }
}
