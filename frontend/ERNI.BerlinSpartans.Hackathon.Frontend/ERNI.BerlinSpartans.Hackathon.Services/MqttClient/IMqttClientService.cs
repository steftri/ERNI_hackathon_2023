using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using MQTTnet.Client;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient
{
    public interface IMqttClientService: IDisposable
    {
        event Func<MqttApplicationMessageReceivedEventArgs, Task>? ApplicationMessageReceived;

        Task<MqttClientPublishResult>? SendCommandAsync(MqttCommand command);
    }
}