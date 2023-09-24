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

        private const int SpeedIncrement = 10;
        private const int DirectionAngleIncrement = 45;
        private const int HeadAngleIncrement = 45;

        public int CurrentSpeed { get; set; }
        public int CurrentDirectionAngle { get; set; }
        public int CurrentHeadAngle { get; set; }

        public PiCarXClient(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }

        /// <summary>
        /// Connects the MQTT service to the robot.
        /// </summary>
        public async Task Connect()
        {
            if (!_mqttClientService.IsConnected())
            {
                await _mqttClientService.Connect();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> Reset()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(0), () => CurrentDirectionAngle = 0));
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetSpeed(0), () => CurrentSpeed = 0));
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetHeadRotate(0), () => CurrentHeadAngle = 0));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoBackward()
        {
            var commandResponses = new List<CommandResponse>();
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetSpeed(0), () => CurrentSpeed = 0));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoForward()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetSpeed(SpeedIncrement), () => CurrentSpeed = SpeedIncrement));
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(0), () => CurrentDirectionAngle = 0));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoLeft()
        {
            var commandResponses = new List<CommandResponse>();
            
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(-DirectionAngleIncrement), () => CurrentDirectionAngle = -DirectionAngleIncrement));
            
            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> GoRight()
        {
            var commandResponses = new List<CommandResponse>();            
            
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(DirectionAngleIncrement), ()=> CurrentDirectionAngle = DirectionAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> TurnHeadLeft()
        {
            var commandResponses = new List<CommandResponse>();
            
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetHeadRotate(-HeadAngleIncrement), ()=>CurrentHeadAngle = -HeadAngleIncrement));
            
            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<MovementChangedResponse> TurnHeadRight()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetHeadRotate(HeadAngleIncrement), () => CurrentHeadAngle = HeadAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// Helper method which handles sending commands to the robot.
        /// </summary>
        /// <param name="mqttCommand">The command to send.</param>
        /// <param name="callback">Funciton invoked in case of success.</param>
        /// <returns>A response indicating the result of the command.</returns>
        private async Task<CommandResponse> SendCommandAsync(MqttCommand mqttCommand, Action? callback = null)
        {
            try
            {
                if (!_mqttClientService.IsConnected())
                {
                    return new CommandResponse()
                        .WithError(MovementChangedResponseCodes.NotConnected, "The application could not connect to the robot.");
                }

                var result = await _mqttClientService.SendCommandAsync(mqttCommand)!;

                if (result.IsSuccess)
                {
                    callback?.Invoke();
                    return new CommandResponse()
                        .WithError(MovementChangedResponseCodes.Success, "The command was successfully sent to the robot.");
                }
                else
                {
                    return new CommandResponse()
                        .WithError(MovementChangedResponseCodes.GenericError, result.ReasonString);
                }
            }
            catch (Exception ex)
            {
                return new CommandResponse()
                    .WithError(MovementChangedResponseCodes.GenericError, ex.Message);
            }
        }
    }
}
