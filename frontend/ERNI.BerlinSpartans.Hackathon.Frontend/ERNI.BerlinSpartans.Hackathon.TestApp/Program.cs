// See https://aka.ms/new-console-template for more information
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;
using Microsoft.Extensions.Logging;


var options = new MqttClientConnectionOptions
{
    BrokerAddress = "10.213.90.68",
    Port = 1883,
    SpinTimeout = 10000
};
var loggerFactory = LoggerFactory.Create(builder => { });

var remoteControlService = new RemoteControlService(new MqttClientService(options, loggerFactory.CreateLogger<MqttClientService>()));

remoteControlService.ApplicationMessageProcessed += e =>
{
    return Task.CompletedTask;
};

//10.230.90.68: 1883
var sendResult = await remoteControlService.SendAsync(new RemoteCommand
{
    CommandType = RemoteCommandType.Set
});
