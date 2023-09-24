using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class PiCarXClient : IPiCarXClient
    {
        private readonly IMqttClientService _mqttClientService;

        public PiCarXClient(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }

        /// <summary>
        /// Connects the MQTT service to the robot.
        /// </summary>
        public void Connect()
        {
            if (!_mqttClientService.IsConnected())
            {
                _mqttClientService.Connect().Wait();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoBackward()
        {
            return await SendCommandAsync(MqttCommandFactory.SetSpeed(0));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoForward()
        {
            return await SendCommandAsync(MqttCommandFactory.SetDirection(0));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoLeft()
        {
            return await SendCommandAsync(MqttCommandFactory.SetDirection(-45));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoRight()
        {
            return await SendCommandAsync(MqttCommandFactory.SetDirection(45));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> TurnHeadLeft()
        {
            return await SendCommandAsync(MqttCommandFactory.SetHeadTilt(-45));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> TurnHeadRight()
        {
            return await SendCommandAsync(MqttCommandFactory.SetHeadTilt(45));
        }

        /// <summary>
        /// Helper method which handles sending commands to the robot.
        /// </summary>
        /// <param name="mqttCommand">The command to send.</param>
        /// <returns>A response indicating the result of the command.</returns>
        private async Task<MovementChangedResponse> SendCommandAsync(MqttCommand mqttCommand)
        {
            try
            {
                if (!_mqttClientService.IsConnected())
                {
                    return new MovementChangedResponse { ResponseCode = MovementChangedResponseCodes.NotConnected, Message = "The application could not connect to the robot." };
                }

                var result = await _mqttClientService.SendCommandAsync(mqttCommand);

                if (result.IsSuccess)
                {
                    return new MovementChangedResponse { ResponseCode = MovementChangedResponseCodes.Success, Message = "The command was successfully sent to the robot." };
                }
                else
                {
                    return new MovementChangedResponse { ResponseCode = MovementChangedResponseCodes.GenericError, Message = result.ReasonString };
                }
            }
            catch (Exception ex)
            {
                return new MovementChangedResponse { ResponseCode = MovementChangedResponseCodes.GenericError, Message = ex.Message };
            }
        }
    }
}
