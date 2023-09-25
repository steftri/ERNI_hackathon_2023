namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model
{
    /// <summary>
    /// Enumerates the available response codes for a command sent to the robot.
    /// </summary>
    public enum MovementChangedResponseCodes
    {
        /// <summary>
        /// The command was executed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// The command could not be executed because the client is not connected to the robot.
        /// </summary>
        NotConnected,

        /// <summary>
        /// An unexpected error occurred.
        /// </summary>
        GenericError,

        /// <summary>
        /// The command was not recognized.
        /// </summary>
        InvalidCommand
    }
}
