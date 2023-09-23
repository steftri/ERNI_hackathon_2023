using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Extensions;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using MQTTnet.Extensions.ManagedClient;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;


/// <summary>
/// Provides methods to send commands to the device.
/// </summary>
public class RemoteControlService : IRemoteControlService, IDisposable
{
    private readonly IMqttClientService mqttClientService;

    public event Func<ApplicationMessageProcessedEventArgs, Task>? ApplicationMessageProcessed;

    /// <summary>
    /// Creates a new instance of the <see cref="RemoteControlService"/> class.
    /// </summary>
    /// <param name="mqttClientService">An instance of <see cref="ManagedMqttClientService"/> used to communicate with the device.</param>
    public RemoteControlService(IMqttClientService mqttClientService)
    {
        this.mqttClientService = mqttClientService;
        //this.mqttClientService.ApplicationMessageProcessed += MqttClientService_ApplicationMessageProcessed; ;  
    }

    private Task MqttClientService_ApplicationMessageProcessed(ApplicationMessageProcessedEventArgs arg)
    {
        return this.ApplicationMessageProcessed?.Invoke(arg) ?? Task.CompletedTask;
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
            var sendResponse = await mqttClientService!.SendCommandAsync(remoteCommand.ToMqttCommand())!;            
        }
        catch (Exception ex)
        {
            return response.WithError($"The command {remoteCommand.CommandType} returned the unexpected exception: {ex.Message}.");
        }

        return response.AsSuccess();

    }

    public void Dispose()
    {
        this.mqttClientService.Dispose();
    }
}
