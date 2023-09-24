using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

using Microsoft.AspNetCore.SignalR;

namespace ERNI.BerlinSpartans.Hackathon.Frontend.Hubs
{
    public class RobotCommandHub : Hub
    {
        private readonly IPiCarXClient _picarClient;

        public RobotCommandHub(IPiCarXClient picarClient, ILogger<RobotCommandHub> logger)
        {
            _picarClient = picarClient;
        }

        public async Task MovementChanged(Movement movement)
        {
            if (movement.Accelerate)
            {
                await _picarClient.Accelerate();
            }

            if (movement.Decelerate)
            {
                await _picarClient.Decelerate();
            }

            if (movement.TurnHeadLeft)
            {
                await _picarClient.TurnHeadLeft();
            }

            if (movement.TurnHeadRight)
            {
                await _picarClient.TurnHeadRight();
            }

            if (movement.Forward)
            {
                await _picarClient.GoForward();
            }

            if (movement.Backward)
            {
                await _picarClient.GoBackward();
            }

            if (movement.Left)
            {
                await _picarClient.GoLeft();
            }

            if (movement.Right)
            {
                await _picarClient.GoRight();
            }

            await Task.CompletedTask;
        }
    }
}
