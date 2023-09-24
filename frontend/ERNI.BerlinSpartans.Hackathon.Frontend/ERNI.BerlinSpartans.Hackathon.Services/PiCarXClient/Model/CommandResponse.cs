namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model
{
    public class CommandResponse
    {
        public string CommandType { get; set; } = null!;

        public MovementChangedResponseCodes ResponseCode { get; set; }

        public string? Message { get; set; }

        public bool IsSuccess => this.ResponseCode == MovementChangedResponseCodes.Success;

        public CommandResponse WithError(MovementChangedResponseCodes code, string message)
        {
            this.ResponseCode = code;
            this.Message = message;
            return this;
        }

    }
}
