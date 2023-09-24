using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using MQTTnet.Client;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient
{
    public interface IMqttClientService: IDisposable
    {
        /// <summary>
        /// Event raised when receiving a message from the broker.
        /// </summary>
        event Func<MqttApplicationMessageReceivedEventArgs, Task>? ApplicationMessageReceived;

        /// <summary>
        /// Connects the client to the broker.
        /// </summary>
        /// <returns></returns>
        Task<MqttClientConnectResult?> Connect();

        /// <summary>
        /// Disconnects the client.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Returns true if the client is connected.
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// Sends a command to the client.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<MqttClientPublishResult>? SendCommandAsync(MqttCommand command);
    }
}