using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Internal;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using NSubstitute;
using Microsoft.Extensions.Options;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.Tests.Mocks;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests;

[Trait("Category", "Services")]
[Trait("Category", "RemoteControl")]
public class RemoteControlServiceTests
{
    [Fact]
    public async Task SendAsync_ForwardCommand_Should_Succeed()
    {
        // Arrange
        var server = new MockBroker();
        await server.StartAsync();

        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            SpinTimeout = 100,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();
        var client = new MqttClientService(clientOptions, logger);

        var remoteService = new RemoteControlService(client);

        var command = RemoteCommand.ForwardCommand(1);

        // Act
        await remoteService.SendAsync(command);
        
        // Clean-up
        await server.StopAsync();        

        // Assert
        Assert.Equal(1, server.YPosition);
        Assert.Equal(0, server.XPosition);
    }

    [Fact]
    public async Task SendAsync_BackwardsCommand_Should_Succeed()
    {
        // Arrange
        var server = new MockBroker();
        await server.StartAsync();

        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            SpinTimeout = 100,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();
        var client = new MqttClientService(clientOptions, logger);

        var remoteService = new RemoteControlService(client);

        var command = RemoteCommand.BackwardCommand(1);

        // Act
        await remoteService.SendAsync(command);

        // Clean-up
        await server.StopAsync();

        // Assert
        Assert.Equal(-1, server.YPosition);
        Assert.Equal(0, server.XPosition);
    }

    [Fact]
    public async Task SendAsync_LeftCommand_Should_Succeed()
    {
        // Arrange
        var server = new MockBroker();
        await server.StartAsync();

        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            SpinTimeout = 100,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();
        var client = new MqttClientService(clientOptions, logger);

        var remoteService = new RemoteControlService(client);

        var command = RemoteCommand.LeftCommand(1);

        // Act
        await remoteService.SendAsync(command);

        // Clean-up
        await server.StopAsync();

        // Assert
        Assert.Equal(0, server.YPosition);
        Assert.Equal(-1, server.XPosition);
    }


    [Fact]
    public async Task SendAsync_RightCommand_Should_Succeed()
    {
        // Arrange
        var server = new MockBroker();
        await server.StartAsync();

        var options = new MqttClientConnectionOptions
        {
            BrokerAddress = "127.0.0.1",
            SpinTimeout = 100,
        };

        var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
        clientOptions.Value.Returns(options);

        var logger = Substitute.For<ILogger<MqttClientService>>();
        var client = new MqttClientService(clientOptions, logger);

        var remoteService = new RemoteControlService(client);

        var command = RemoteCommand.RightCommand(1);

        // Act
        await remoteService.SendAsync(command);

        // Clean-up
        await server.StopAsync();

        // Assert
        Assert.Equal(0, server.YPosition);
        Assert.Equal(1, server.XPosition);
    }


}