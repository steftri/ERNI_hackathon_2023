using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Internal;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using NSubstitute;
using Microsoft.Extensions.Options;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Extensions;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests;

[Trait("Category", "Services")]
[Trait("Category", "MqttClientService")]
public class MqttClientServiceTests
{    

    [Fact]
    public async Task SendCommand_Should_Succeed()
    {
        // Arrange        
        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            Port = 18835,
            IdleTimeoutInMinutes = 5,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();

        var client = new MqttClientService(clientOptions, logger);
        var command = new MqttCommand
        {
            Topic = "MyTopic",
            Payload =  "MyPayload".ToJson()
        };

        string capturedTopic = "";
        string capturedPayload = "";

        var server = GetServer();

        server.InterceptingPublishAsync += args =>
        {
            capturedTopic = args.ApplicationMessage.Topic;
            capturedPayload = args.ApplicationMessage.ConvertPayloadToString();
            return CompletedTask.Instance;
        };        
        await server.StartAsync();
        

        // Act
        await client.Connect();
        var response = await client.SendCommandAsync(command)!;
        Thread.Sleep(1000);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal(command.Topic, capturedTopic);
        Assert.Equal(command.Payload, capturedPayload);            
    }

    [Fact]
    public async Task SendCommand_With_No_Sever_Should_Fail()
    {
        // Arrange            
        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            Port = 18835,
            IdleTimeoutInMinutes = 1,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();

        var client = new MqttClientService(clientOptions, logger);
        var command = new MqttCommand
        {
            Topic = "MyTopic",
            Payload = "MyPayload"
        };

        // Act && Assert
        await Assert.ThrowsAsync<MQTTnet.Exceptions.MqttCommunicationException>( () =>  client.SendCommandAsync(command)!);
    }

    private static MqttServer GetServer()
    {
        var mqttFactory = new MqttFactory();
        var mqttServerOptions = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(18835)
            .Build();

        var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);            

        return mqttServer;               
    }

}