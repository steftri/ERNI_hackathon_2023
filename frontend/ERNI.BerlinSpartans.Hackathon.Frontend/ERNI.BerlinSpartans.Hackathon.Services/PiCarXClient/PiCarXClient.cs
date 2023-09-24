using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    public class PiCarXClient : IPiCarXClient
    {
        private readonly IMqttClientService _mqttClientService;

        public PiCarXClient(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            if (!_mqttClientService.IsConnected())
            {
                _mqttClientService.Connect().Wait();
            }
        }

        public async Task Accelerate()
        {
            _ = await _mqttClientService.SendCommandAsync(MqttCommandFactory.SetSpeed(50));
            await Task.CompletedTask;
        }

        public async Task Decelerate()
        {
            _ = await _mqttClientService.SendCommandAsync(MqttCommandFactory.SetSpeed(-50));
            await Task.CompletedTask;
        }

        public async Task GoBackward()
        {
            await Task.CompletedTask;
        }

        public async Task GoForward()
        {
            await Task.CompletedTask;
        }

        public async Task GoLeft()
        {
            await Task.CompletedTask;
        }

        public async Task GoRight()
        {
            await Task.CompletedTask;
        }

        public async Task TurnHeadLeft()
        {
            await Task.CompletedTask;
        }

        public async Task TurnHeadRight()
        {
            await Task.CompletedTask;
        }

    }
}
