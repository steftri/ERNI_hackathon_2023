namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model
{
    public enum MovementChangedResponseCodes
    {
        Success,
        NotConnected,
        GenericError,
        InvalidCommand
    }

    /// <summary>
    /// Response object which contains the result of the command.
    /// </summary>
    public class MovementChangedResponse
    {
        public MovementChangedResponseCodes ResponseCode { get; set; }

        public string Message { get; set; }
    }
}
