using NSubstitute;
using Microsoft.Extensions.Options;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.Tests.Mocks;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests;

//[Trait("Category", "Services")]
//[Trait("Category", "RemoteControl")]
//public class RemoteControlServiceTests: IClassFixture<MockBroker>
//{
    //private MockBroker server;

    //public RemoteControlServiceTests(MockBroker fixture) 
    //{
    //    this.server = fixture;
    //}    

    //[Fact]    
    //public async Task SendAsync_ForwardCommand_Should_Succeed()
    //{
    //    // Arrange
    //    await server.StartAsync();

    //    var options = new MqttClientConnectionOptions
    //    {
    //        BrokerAddress = "127.0.0.1",
    //        SpinTimeout = 100,
    //    };

    //    var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
    //    clientOptions.Value.Returns(options);

    //    var logger = Substitute.For<ILogger<MqttClientService>>();
    //    var client = new MqttClientService(clientOptions, logger);

    //    var remoteService = new RemoteControlService(client);

    //    var command = RemoteCommandFactory.SetSpeed;

    //    // Act
    //    await remoteService.SendAsync(command);
    //    Thread.Sleep(1000);

    //    // Assert
    //    Assert.Equal(1, server.YPosition);
    //    Assert.Equal(0, server.XPosition);
    //}

    //[Fact]
    //public async Task SendAsync_BackwardsCommand_Should_Succeed()
    //{
    //    // Arrange
    //    await server.StartAsync();

    //    var options = new MqttClientConnectionOptions
    //    {
    //        BrokerAddress = "127.0.0.1",            
    //        SpinTimeout = 100,
    //    };

    //    var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
    //    clientOptions.Value.Returns(options);

    //    var logger = Substitute.For<ILogger<MqttClientService>>();
    //    var client = new MqttClientService(clientOptions, logger);

    //    var remoteService = new RemoteControlService(client);

    //    var command = RemoteCommand.BackwardCommand(1);

    //    // Act
    //    await remoteService.SendAsync(command);
    //    Thread.Sleep(1000);

    //    // Assert
    //    Assert.Equal(-1, server.YPosition);
    //    Assert.Equal(0, server.XPosition);
    //}

    //[Fact]
    //public async Task SendAsync_LeftCommand_Should_Succeed()
    //{
    //    // Arrange        
    //    await server.StartAsync();

    //    var options = new MqttClientConnectionOptions
    //    {
    //        BrokerAddress = "127.0.0.1",
    //        SpinTimeout = 100,
    //    };

    //    var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
    //    clientOptions.Value.Returns(options);

    //    var logger = Substitute.For<ILogger<MqttClientService>>();
    //    var client = new MqttClientService(clientOptions, logger);

    //    var remoteService = new RemoteControlService(client);

    //    var command = RemoteCommand.LeftCommand(1);

    //    // Act
    //    await remoteService.SendAsync(command);
    //    Thread.Sleep(1000);

    //    // Assert
    //    Assert.Equal(0, server.YPosition);
    //    Assert.Equal(-1, server.XPosition);
    //}


    //[Fact]
    //public async Task SendAsync_RightCommand_Should_Succeed()
    //{
    //    // Arrange        
    //    await server.StartAsync();

    //    var options = new MqttClientConnectionOptions
    //    {
    //        BrokerAddress = "127.0.0.1",
    //        SpinTimeout = 100,
    //    };

    //    var clientOptions = Substitute.For<IOptions<MqttClientConnectionOptions>>();
    //    clientOptions.Value.Returns(options);

    //    var logger = Substitute.For<ILogger<MqttClientService>>();
    //    var client = new MqttClientService(clientOptions, logger);
    //    var remoteService = new RemoteControlService(client);

    //    var command = RemoteCommand.RightCommand(1);

    //    // Act
    //    await remoteService.SendAsync(command);        

    //    // Assert
    //    Assert.Equal(0, server.YPosition);
    //    Assert.Equal(1, server.XPosition);
    //}


//}