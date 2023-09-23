using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Internal;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.Tests.Mocks
{
    internal class MockBroker : IDisposable
    {
        private readonly MqttServer mqttServer;

        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public MockBroker()
        {
            var mqttFactory = new MqttFactory();
            var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

            mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
            mqttServer.InterceptingPublishAsync += args =>
            {
                var topic = args.ApplicationMessage.Topic;
                var payload = args.ApplicationMessage.ConvertPayloadToString();

                var remoteCommandType = Enum.Parse<RemoteCommandType>(topic);
                switch (remoteCommandType)
                {
                    case RemoteCommandType.None:
                        break;
                    case RemoteCommandType.Forward:
                        YPosition += Convert.ToInt32(payload);
                        break;
                    case RemoteCommandType.Backward:
                        YPosition -= Convert.ToInt32(payload);
                        break;
                    case RemoteCommandType.Left:
                        XPosition -= Convert.ToInt32(payload);
                        break;
                    case RemoteCommandType.Right:
                        XPosition += Convert.ToInt32(payload);
                        break;
                    default:
                        break;
                }
                return CompletedTask.Instance;
            };
        }

        public Task StartAsync()
        {
            return this.mqttServer.StartAsync();
        }

        public Task StopAsync()
        {
            return this.mqttServer.StopAsync();
        }

        public void Dispose()
        {
            this.mqttServer.Dispose();
        }
    }
}
