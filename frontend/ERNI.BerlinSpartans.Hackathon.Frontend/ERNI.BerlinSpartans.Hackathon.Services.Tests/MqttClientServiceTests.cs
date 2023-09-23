using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Internal;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using NSubstitute;
using Microsoft.Extensions.Options;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.Tests.Mocks;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests;

[Trait("Category", "Services")]
[Trait("Category", "MqttClient")]
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
            SpinTimeout = 100,
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
        Thread.Sleep(1000);

        // Act
        var response = await client.SendCommandAsync(command)!;       
                
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
            SpinTimeout = 100,
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

    private MqttServer GetServer()
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