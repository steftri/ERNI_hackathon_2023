namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models
{

    /// <summary>
    /// Represnts the options used to configure the MttClient.
    /// </summary>
    public class MqttClientConnectionOptions
    {
        /// <summary>
        /// The configuration key.
        /// </summary>
        public const string Position = "MqttClient";

        /// <summary>
        /// Gets or sets the address of the Broker that the client should connect to.
        /// </summary>
        public string BrokerAddress { get; set; } = null!;

        /// <summary>
        /// Gets or sets the address of the Broker that the client should connect to.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the number of milliseconds that are waited before a command is discarded.
        /// </summary>
        public int SpinTimeout { get; set; }

    }
}
