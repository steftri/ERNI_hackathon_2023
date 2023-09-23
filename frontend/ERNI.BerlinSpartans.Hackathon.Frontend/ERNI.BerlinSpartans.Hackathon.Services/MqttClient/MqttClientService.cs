using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient;

/// <summary>
/// The Mqtt Client used to connect to the broker served by the device.
/// </summary>
public class MqttClientService : IMqttClientService
{
    private MqttClientConnectionOptions options;
    private readonly MqttFactory mqttFactory = new();
    private readonly ILogger<MqttClientService> logger;

    /// <summary>
    /// Creates a new instance of the <see cref="MqttClientService"/> class.
    /// </summary>
    /// <param name="options">
    /// The options used to configure the client.
    /// </param>
    public MqttClientService(IOptions<MqttClientConnectionOptions> options,
        ILogger<MqttClientService> logger)
    {
        this.options = options.Value;
        this.logger = logger;
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
    }

    /// <inheritdoc/>
    public async Task<bool> SendCommandAsync(MqttCommand command)
    {
        logger.LogTrace("Command received: {command.Topic}:{command.Payload}", command.Topic, command.Payload ?? "- no payload -");

        using var managedMqttClient = mqttFactory.CreateManagedMqttClient();

        var clientOptions = GetOptions();
        await managedMqttClient.StartAsync(clientOptions);

        // The application message is not sent. It is stored in an internal queue and
        // will be sent when the client is connected.
        await managedMqttClient.EnqueueAsync(command.Topic, command.Payload);

        // Wait until the queue is fully processed.
        return SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, options.SpinTimeout);
    }

    private ManagedMqttClientOptions GetOptions()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(this.options.BrokerAddress)
                .Build();

        var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
            .WithClientOptions(mqttClientOptions)
            .Build();

        return managedMqttClientOptions;
    }
}