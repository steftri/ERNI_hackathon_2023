using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

using Microsoft.AspNetCore.SignalR;

namespace ERNI.BerlinSpartans.Hackathon.Frontend.Hubs
{
    /// <summary>
    /// This SignalR hub handles commands from the UI.
    /// It accepts movement struct which tells it what action the robot should take.
    /// </summary>
    public class RobotCommandHub : Hub
    {
        /// <summary>
        /// The concrete implementation of the PiCar X robot.
        /// </summary>
        private readonly IPiCarXClientService _picarClient;

        public RobotCommandHub(IPiCarXClientService picarClient, ILogger<RobotCommandHub> logger)
        {
            _picarClient = picarClient;
        }

        /// <summary>
        /// This is called by the SignalR JS library and tells the robot to make a move.
        /// </summary>
        /// <param name="movement">Contains all the commands the robot should take.</param>
        public async Task<MovementChangedResponse> MovementChanged(Movement movement)
        {
            if (movement.TurnHeadLeft)
            {
                return await _picarClient.TurnHeadLeft();
            }

            if (movement.TurnHeadRight)
            {
                return await _picarClient.TurnHeadRight();
            }

            if (movement.Forward)
            {
                return await _picarClient.GoForward();
            }

            if (movement.Backward)
            {
                return await _picarClient.GoBackward();
            }

            if (movement.Left)
            {
                return await _picarClient.GoLeft();
            }

            if (movement.Right)
            {
                return await _picarClient.GoRight();
            }

            if (movement.StartLane)
            {
                return await _picarClient.StartLane();
            }

            throw new NotImplementedException();
        }
    }
}
