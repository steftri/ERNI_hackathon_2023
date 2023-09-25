using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using NSubstitute;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient;
using MQTTnet.Client;
using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests
{
    [Trait("Category", "Services")]
    [Trait("Category", "PiCarXClientService")]
    public class PiCarXClientServiceTests
    {
        [Fact]
        public async Task GoForward_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);
            
            // Act
            var response = await piCarXClient.GoForward();

            // Assert
            Assert.Equal(1, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);

            await mqttCLientServiceMock.Received(2).SendCommandAsync(Arg.Any<MqttCommand>())!;
        }

        [Fact]
        public async Task GoBackward_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Act
            var response = await piCarXClient.GoBackward();

            // Assert
            Assert.Equal(-1, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task GoLeft_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Act
            var response = await piCarXClient.GoLeft();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(-1, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task GoRight_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Act
            var response = await piCarXClient.GoRight();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(1, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task TurnHeadLeft_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Act
            var response = await piCarXClient.TurnHeadLeft();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(-1, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task TurnHeadRight_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Act
            var response = await piCarXClient.TurnHeadRight();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(1, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task Reset_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(1, 1, 1);

            // Send commands to change the current values
            await piCarXClient.GoForward();
            await piCarXClient.GoRight();
            var lastResult = await piCarXClient.TurnHeadRight();

            Assert.Equal(1, lastResult.CurrentSpeed);
            Assert.Equal(1, lastResult.CurrentHeadAngle);
            Assert.Equal(1, lastResult.CurrentDirectionAngle);

            // Act
            var response = await piCarXClient.Reset();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task Stop_Should_Succeed()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock();
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);
            piCarXClient.SetDefaultIncrements(10, 1, 1);

            // Send commands to change the current values
            var lastResult = await piCarXClient.GoForward();                        

            Assert.Equal(10, lastResult.CurrentSpeed);
            Assert.Equal(0, lastResult.CurrentHeadAngle);
            Assert.Equal(0, lastResult.CurrentDirectionAngle);

            // Act
            var response = await piCarXClient.Stop();

            // Assert
            Assert.Equal(0, response.CurrentSpeed);
            Assert.Equal(0, response.CurrentHeadAngle);
            Assert.Equal(0, response.CurrentDirectionAngle);
            Assert.NotEmpty(response.CommandResponses);
        }

        [Fact]
        public async Task SendCommand_With_Disconnected_Client_Should_Fail()
        {
            // Arrange
            var mqttCLientServiceMock = GetMqttClientServiceMock(isConnected: false);
            var piCarXClient = new PiCarXClientService(mqttCLientServiceMock);            

            // Act
            var response = await piCarXClient.GoLeft();

            // Assert
            Assert.Equal(MovementChangedResponseCodes.NotConnected, response.CommandResponses.Single().ResponseCode);
        }


        private static IMqttClientService GetMqttClientServiceMock(
            bool isConnected = true, 
            MqttClientPublishReasonCode reasonCode = MqttClientPublishReasonCode.Success)
        {
            var client = Substitute.For<IMqttClientService>();
            client.IsConnected().Returns(isConnected);

            var clientresponse = new MqttClientPublishResult(1, reasonCode, string.Empty, null);

            client.SendCommandAsync(Arg.Any<MqttCommand>()).Returns(Task.FromResult(clientresponse));

            return client;
        }
    }
}
