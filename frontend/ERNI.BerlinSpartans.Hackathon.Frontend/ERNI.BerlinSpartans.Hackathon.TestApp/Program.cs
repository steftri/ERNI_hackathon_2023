// See https://aka.ms/new-console-template for more information
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Implementations;
using System.Diagnostics;

var options = new MqttClientConnectionOptions
{
    BrokerAddress = "10.213.90.68",
    Port = 1883,
    IdleTimeoutInMinutes= 1
};
var loggerFactory = LoggerFactory.Create(builder => { });
var logger = loggerFactory.CreateLogger<MqttClientService>();

var mqttClient = new MqttClientService(options, logger);

mqttClient.ApplicationMessageReceived += e =>
{
    Debug.WriteLine(e.ApplicationMessage.ConvertPayloadToString());
    return Task.CompletedTask;
};

Console.WriteLine("##################### PiCar-X Control #####################");
Console.WriteLine();
Console.WriteLine("List of Commands:");
Console.WriteLine();
Console.WriteLine("C: Connect");
Console.WriteLine("X: Disconnect");
Console.WriteLine();
Console.WriteLine("W: Speed + 1 (fw)");
Console.WriteLine("S: Speed - 1 (bw)");
Console.WriteLine("A: Angle - 1 (left)");
Console.WriteLine("D: Angle + 1 (right)");
Console.WriteLine();
Console.WriteLine("Press CTRL-C to Exit");

var speedMaxValue = 50;
var speedMinvalue = -50;
var angleMaxValue = 45;
var angleMinValue = -45;

var currentSpeed = 0;
var currentAngle = 0;

while (true)
{
    Console.Write("Enter a Command: ");
    var key = Console.ReadKey();
    Console.WriteLine();

    try
    {
        switch (key.Key)
        {
            case ConsoleKey.C:
                Console.Write("Connecting... ");
                await mqttClient.Connect();
                Console.WriteLine("Done");
                break;
            case ConsoleKey.X:
                mqttClient.Disconnect();
                break;

            case ConsoleKey.W:
                {
                    currentSpeed = Math.Min(currentSpeed + 1, speedMaxValue);
                    var command = MqttCommandFactory.SetSpeed(currentSpeed);
                    await mqttClient.SendCommandAsync(command)!;
                }                
                break;

            case ConsoleKey.S:
                {
                    currentSpeed = Math.Max(currentSpeed - 1, speedMinvalue);
                    var command = MqttCommandFactory.SetSpeed(currentSpeed);
                    await mqttClient.SendCommandAsync(command)!;
                }
                break;

            case ConsoleKey.A:
                {
                    currentAngle = Math.Max(currentAngle - 1, angleMinValue);
                    var command = MqttCommandFactory.SetDirection(currentAngle);
                    await mqttClient.SendCommandAsync(command)!;

                    var cameraCommand = MqttCommandFactory.SetHeadRotate(currentAngle);
                    await mqttClient.SendCommandAsync(cameraCommand)!;
                }
                break;
            case ConsoleKey.D:
                {
                    currentAngle = Math.Min(currentAngle + 1, angleMaxValue);
                    var command = MqttCommandFactory.SetDirection(currentAngle);
                    await mqttClient.SendCommandAsync(command)!;

                    var cameraCommand = MqttCommandFactory.SetHeadRotate(currentAngle);
                    await mqttClient.SendCommandAsync(cameraCommand)!;
                }
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}


