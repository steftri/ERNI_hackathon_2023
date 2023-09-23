namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

/// <summary>
/// Represents a command sent to the device.
/// </summary>
public class MqttCommand
{
    /// <summary>
    /// A unique identiier of the command, used for logging and tracing purposes.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>    
    /// Gets or sets the topic of the command.
    /// </summary>
    public string Topic { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional payload.
    /// </summary>
    public string? Payload { get; set; }
}
