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
        public int CurrentSpeed { get; set; }
        public int CurrentDirectionAngle { get; set; }
        public int CurrentHeadAngle { get; set; }

        public List<CommandResponse> CommandResponses { get; set; } = new();

        public MovementChangedResponse WithCurrentValues(int speed, int direction, int cameraAngle) 
        { 
            this.CurrentSpeed = speed;
            this.CurrentDirectionAngle = direction;
            this.CurrentHeadAngle = cameraAngle;
            return this;
        }

        public MovementChangedResponse WithCommandResponses(List<CommandResponse> commandResponses)
        {
            this.CommandResponses = commandResponses;
            return this;
        }
    }
}
