using System.Text.Json;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Extensions
{
    internal static class ObjectExtensions
    {

        public static string ToJson(this object obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return System.Text.Json.JsonSerializer.Serialize(obj, options);
        }
    }
}
