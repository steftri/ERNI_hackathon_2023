using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    public class PiCarXClient : IPiCarXClient
    {
        private readonly IMqttClientService _mqttClientService;

        public PiCarXClient(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }

        public async Task Accelerate()
        {
            await Task.CompletedTask;
        }

        public async Task Decelerate()
        {
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
