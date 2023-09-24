using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class PiCarXClientService : IPiCarXClientService
    {        
        private int SpeedIncrement = 10;
        private int DirectionAngleIncrement = 15;
        private int HeadAngleIncrement = 15;

        private int CurrentSpeed;        
        private int CurrentDirectionAngle;
        private int CurrentHeadAngle;

        private readonly IMqttClientService _mqttClientService;

        /// <summary>
        /// Creates a new instance of the <see cref="PiCarXClientService"/> class.
        /// </summary>
        /// <param name="mqttClientService">An instance of <see cref="IMqttClientService"/>.</param>
        public PiCarXClientService(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }

        /// <inheritdoc/>
        public async Task Connect()
        {
            if (!_mqttClientService.IsConnected())
            {
                await _mqttClientService.Connect();
            }
        }

        /// <inheritdoc/>
        public void SetDefaultIncrements(int speed, int direction, int headAngle)
        {
            SpeedIncrement = speed;
            DirectionAngleIncrement = direction;
            HeadAngleIncrement = headAngle;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> GoBackward()
        {
            var commandResponses = new List<CommandResponse>();
            var speed = CurrentSpeed > 0 ? 0 : Math.Max(CurrentSpeed - SpeedIncrement, -45);
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetSpeed(speed), () => CurrentSpeed = speed));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> GoForward()
        {
            var commandResponses = new List<CommandResponse>();

            var speed = CurrentSpeed < 0 ? 0 : Math.Min(CurrentSpeed + SpeedIncrement, 45);

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetSpeed(speed), () => CurrentSpeed = speed));
            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(0), () => CurrentDirectionAngle = 0));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> GoLeft()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(-DirectionAngleIncrement), () => CurrentDirectionAngle = -DirectionAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> GoRight()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetDirection(DirectionAngleIncrement), () => CurrentDirectionAngle = DirectionAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> TurnHeadLeft()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetHeadRotate(-HeadAngleIncrement), () => CurrentHeadAngle = -HeadAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> TurnHeadRight()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.SetHeadRotate(HeadAngleIncrement), () => CurrentHeadAngle = HeadAngleIncrement));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <inheritdoc/>
        public async Task<MovementChangedResponse> Stop()
        {
            var commandResponses = new List<CommandResponse>();

            commandResponses.Add(await SendCommandAsync(MqttCommandFactory.Stop(), () => CurrentSpeed = 0));

            return new MovementChangedResponse()
                .WithCurrentValues(CurrentSpeed, CurrentDirectionAngle, CurrentHeadAngle)
                .WithCommandResponses(commandResponses);
        }

        /// <summary>
        /// Helper method which handles sending commands to the robot.
        /// </summary>
        /// <param name="mqttCommand">The command to send.</param>
        /// <param name="callback">Callback function, invoked in case of success.</param>
        /// <returns>A response indicating the result of the command.</returns>
        private async Task<CommandResponse> SendCommandAsync(MqttCommand mqttCommand, Action? callback = null)
        {
            try
            {
                // Ensures the connection.
                await Connect();

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
