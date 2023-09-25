namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model
{

    /// <summary>
    /// Response object that contains the new status of the robot and the results of the command executed.
    /// </summary>
    public class MovementChangedResponse
    {
        /// <summary>
        /// Gets the current speed of the robot.
        /// </summary>
        public int CurrentSpeed { get; private set; }

        /// <summary>
        /// Gets the current direction angle of the robot.
        /// </summary>
        public int CurrentDirectionAngle { get; private set; }

        /// <summary>
        /// Gets the current rotation angle of the robot camera.
        /// </summary>
        public int CurrentHeadAngle { get; private set; }

        /// <summary>
        /// Gets the list of responses of the commands sent to the robot.
        /// </summary>
        public List<CommandResponse> CommandResponses { get; set; } = new();

        /// <summary>
        /// Fluent method that sets the given values and returns the object itself.
        /// </summary>
        /// <param name="speed">The current Speed.</param>
        /// <param name="direction">The current Direction Angle.</param>
        /// <param name="cameraAngle">The current camera rotation angle.</param>
        /// <returns>
        /// The object instance.
        /// </returns>
        public MovementChangedResponse WithCurrentValues(int speed, int direction, int cameraAngle) 
        { 
            this.CurrentSpeed = speed;
            this.CurrentDirectionAngle = direction;
            this.CurrentHeadAngle = cameraAngle;
            return this;
        }

        /// <summary>
        /// Fluent method that sets the command responses and returns the object itself.
        /// </summary>
        /// <param name="commandResponses">The list of command responses.</param>
        /// <returns>
        /// The object instance.
        /// </returns>
        public MovementChangedResponse WithCommandResponses(List<CommandResponse> commandResponses)
        {
            this.CommandResponses = commandResponses;
            return this;
        }
    }
}
