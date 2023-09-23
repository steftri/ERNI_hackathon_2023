using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Extensions;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;


/// <summary>
/// Provides methods to send commands to the device.
/// </summary>
public class RemoteControlService : IRemoteControlService
{
    private readonly IMqttClientService mqttClientService;

    /// <summary>
    /// Creates a new instance of the <see cref="RemoteControlService"/> class.
    /// </summary>
    /// <param name="mqttClientService">An instance of <see cref="MqttClientService"/> used to communicate with the device.</param>
    public RemoteControlService(IMqttClientService mqttClientService)
    {
        this.mqttClientService = mqttClientService;
    }

    /// <summary>
    /// Sends the given command to the device and returns the result.
    /// </summary>
    /// <param name="remoteCommand"></param>
    /// <returns></returns>
    public async Task<RemoteResponse> SendAsync(RemoteCommand remoteCommand)
    {
        var response = new RemoteResponse();
        try
        {
            var success = await mqttClientService.SendCommandAsync(remoteCommand.ToMqttCommand());
            if (!success)
            {
                response.WithError($"The command {remoteCommand.CommandType} was not processed within the expected timeout. The connection to the device could be lost.");
            }
        }
        catch (Exception ex)
        {
            return response.WithError($"The command {remoteCommand.CommandType} returned the unexpected exception: {ex.Message}.");
        }

        return response.AsSuccess();

    }
}
