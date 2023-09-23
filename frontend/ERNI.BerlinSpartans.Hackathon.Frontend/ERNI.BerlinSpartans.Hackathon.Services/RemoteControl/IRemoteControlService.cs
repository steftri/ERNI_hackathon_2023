using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl
{
    public interface IRemoteControlService
    {
        Task<RemoteResponse> SendAsync(RemoteCommand remoteCommand);
    }
}