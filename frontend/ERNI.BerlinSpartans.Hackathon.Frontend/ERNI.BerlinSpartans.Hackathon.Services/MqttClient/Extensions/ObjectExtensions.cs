using System.Text.Json;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
