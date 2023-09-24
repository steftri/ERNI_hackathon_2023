using MQTTnet.Client;
using MQTTnet;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using System.Diagnostics;

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
    private readonly System.Timers.Timer timer = new();

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

        this.mqttClient.ConnectedAsync += e =>
        {            
            return this.mqttClient.SubscribeAsync("state");
        };

        this.mqttClient.DisconnectedAsync += e =>
        {
            return this.mqttClient.UnsubscribeAsync("state");
        };

        this.mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            return this.HandleApplicationMessage(e);
        };

        
        this.timer.Elapsed += (sender, e) =>
        {
            this.Disconnect();
        };
    }

    private Task HandleApplicationMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        Debug.WriteLine(e.ApplicationMessage.ConvertPayloadToString());
        return this.ApplicationMessageReceived?.Invoke(e) ?? Task.CompletedTask;
    }

    public async Task<MqttClientConnectResult?> Connect()
    {
        if (this.mqttClient.IsConnected)
        {
            throw new InvalidOperationException("The client is already connected.");
        }
        
        var connectionResult = await mqttClient.ConnectAsync(GetOptions(), CancellationToken.None);
        this.timer.Interval = this.options.IdleTimeoutInMinutes * 60 * 1000;
        this.timer.Start();
        return connectionResult;
    }

    public void Disconnect()
    {
        this.timer.Stop();
        mqttClient.DisconnectAsync().Wait();
    }

    /// <inheritdoc/>
    public async Task<MqttClientPublishResult>? SendCommandAsync(MqttCommand command)
    {
        logger.LogTrace("Sending Command: {command.Topic}:{command.Payload}", command.Topic, command.Payload ?? "- no payload -");
        
        this.ResetTimer();

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(command.Topic)
            .WithPayload(command.Payload)
            .Build();

        var response = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);        

        return response;
    }

    private void ResetTimer()
    {
        this.timer.Stop();
        this.timer.Start();
    }

    private MqttClientOptions GetOptions()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(this.options.BrokerAddress, this.options.Port)
                .WithTimeout(TimeSpan.FromSeconds(10))
                .Build();

        return mqttClientOptions;
    }

    public void Dispose()
    {
        this.mqttClient.DisconnectAsync().Wait();
        this.mqttClient.Dispose();
    }
}