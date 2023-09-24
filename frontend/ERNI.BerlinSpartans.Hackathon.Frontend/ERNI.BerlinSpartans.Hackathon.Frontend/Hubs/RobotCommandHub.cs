﻿using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
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
        private readonly IPiCarXClient _picarClient;

        public RobotCommandHub(IPiCarXClient picarClient, ILogger<RobotCommandHub> logger)
        {
            _picarClient = picarClient;
        }

        /// <summary>
        /// This is called by the SignalR JS library and tells the robot to make a move.
        /// </summary>
        /// <param name="movement">Contains all the commands the robot should take.</param>
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
        }
    }
}
