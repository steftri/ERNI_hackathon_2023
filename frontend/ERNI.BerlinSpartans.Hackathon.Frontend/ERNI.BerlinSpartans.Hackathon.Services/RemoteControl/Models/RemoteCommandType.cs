namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models
{
    /// <summary>
    /// Enumerates the available command types that can be sent.
    /// </summary>
    public enum RemoteCommandType
    {
        None = 0,

        /// <summary>
        /// Command used to move forward.
        /// </summary>
        Forward = 1,

        /// <summary>
        /// Command used to move forward.
        /// </summary>
        Backward,

        /// <summary>
        /// Command used to turn left.
        /// </summary>
        Left,

        /// <summary>
        /// Command used to turn right.
        /// </summary>
        Right,

        /// <summary>
        /// Test Command
        /// </summary>
        Set = 1000
    }
}