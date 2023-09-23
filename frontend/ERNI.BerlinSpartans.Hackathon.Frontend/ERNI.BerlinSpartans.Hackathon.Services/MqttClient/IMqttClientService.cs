using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient;

/// <summary>
/// Interface for the Mqtt Client used to connect to the broker served by the device.
/// </summary>
public interface IMqttClientService
{
    /// <summary>
    /// Sends a command to the broker.
    /// </summary>
    /// <param name="command">The command to be sent.</param>
    /// <returns>
    /// True if the command was processed within the timeout, otherwise false.
    /// </returns>
    Task<bool> SendCommandAsync(MqttCommand command);
}