using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using MQTTnet.Extensions.ManagedClient;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl
{
    public interface IRemoteControlService
    {

        event Func<ApplicationMessageProcessedEventArgs, Task>? ApplicationMessageProcessed;
        Task<RemoteResponse> SendAsync(RemoteCommand remoteCommand);
    }
}