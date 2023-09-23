using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Internal;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using NSubstitute;
using Microsoft.Extensions.Options;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests;

[Trait("Category", "Services")]
[Trait("Category", "MwttClient")]
public class MqttClientServiceTests
{
    /*
     * These tests contain a Thread.Sleep() command because apparently the mock broker
     * needs some time to free the listening port. Even when the tests are not running in parallel,
     * it seems that the time needed to free the resoources (ip:port) is slower than the time
     * needed to run the tests. 
     */

    [Fact]
    public async Task SendCommand_Should_Succeed()
    {
        // Arrange
        var server = GetServer();
        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
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
        
        server.InterceptingPublishAsync += args =>
        {

            capturedTopic = args.ApplicationMessage.Topic;
            capturedPayload = args.ApplicationMessage.ConvertPayloadToString();
            return CompletedTask.Instance;
        };
        //Thread.Sleep(100);
        await server.StartAsync();

        // Act
        await client.SendCommandAsync(command);
        
        // Clean-up
        await server.StopAsync();        
        server.Dispose();
        
        // Assert
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

        // Act
        //Thread.Sleep(100);
        var result  = await client.SendCommandAsync(command);

        // Assert
        Assert.False(result);
    }

    private MqttServer GetServer()
    {
        var mqttFactory = new MqttFactory();
        var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

        var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);            

        return mqttServer;               
    }

}