using MQTTnet.Client;
using MQTTnet;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient;

/// <summary>
/// The Mqtt Client used to connect to the broker served by the device.
/// </summary>
public class MqttClientService : IMqttClientService, IDisposable
{
    private MqttClientConnectionOptions options;
    private readonly MqttFactory mqttFactory = new();
    private readonly ILogger<MqttClientService> logger;
    private readonly IMqttClient mqttClient;

    public event Func<MqttApplicationMessageReceivedEventArgs, Task>? ApplicationMessageReceived;


    /// <summary>
    /// Creates a new instance of the <see cref="MqttClientService"/> class.
    /// </summary>
    /// <param name="options">
    /// The options used to configure the client.
    /// </param>
    public MqttClientService(IOptions<MqttClientConnectionOptions> options,
        ILogger<MqttClientService> logger) : this(options.Value, logger)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="MqttClientService"/> class.
    /// </summary>
    /// <param name="options">
    /// The options used to configure the client.
    /// </param>
    public MqttClientService(MqttClientConnectionOptions options,
        ILogger<MqttClientService> logger)
    {
        this.options = options;
        this.logger = logger;

        this.mqttClient = mqttFactory.CreateMqttClient();
        this.mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
    }

    private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<MqttClientPublishResult>? SendCommandAsync(MqttCommand command)
    {
        logger.LogTrace("Sending Command: {command.Topic}:{command.Payload}", command.Topic, command.Payload ?? "- no payload -");

        var connectionresult = await mqttClient.ConnectAsync(GetOptions(), CancellationToken.None);        

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(command.Topic)
            .WithPayload("[" + command.Payload + "]")
            .Build();

        var response = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

        await mqttClient.DisconnectAsync();

        return response;

    }

    private MqttClientOptions GetOptions()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(this.options.BrokerAddress, this.options.Port)
                .WithTimeout(TimeSpan.FromSeconds(5))
                .Build();

        return mqttClientOptions;
    }

    public void Dispose()
    {
        this.mqttClient.DisconnectAsync().Wait();
        this.mqttClient.Dispose();
    }
}