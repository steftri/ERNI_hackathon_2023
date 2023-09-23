using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Extensions
{
    /// <summary>
    /// Provides extension methods to convert a <see cref="RemoteCommand"/> to a <see cref="MqttCommand"/>
    /// </summary>
    internal static class RemoteCommandEstensions
    {
        /// <summary>
        /// Converts the given command to a <see cref="MqttCommand"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static MqttCommand ToMqttCommand(this RemoteCommand command)
        {
            return command.CommandType switch
            {
                RemoteCommandType.None => throw new NotImplementedException(),
                RemoteCommandType.Forward => new MqttCommand { Topic = command.CommandType.ToString(), Payload = command.Value.ToString() },
                RemoteCommandType.Backward => new MqttCommand { Topic = command.CommandType.ToString(), Payload = command.Value.ToString() },
                RemoteCommandType.Left => new MqttCommand { Topic = command.CommandType.ToString(), Payload = command.Value.ToString() },
                RemoteCommandType.Right => new MqttCommand { Topic = command.CommandType.ToString(), Payload = command.Value.ToString() },
                RemoteCommandType.Set => new MqttCommand { Topic = "/set", Payload = null },
                _ => throw new NotImplementedException(),
            };
        }
    }
}
