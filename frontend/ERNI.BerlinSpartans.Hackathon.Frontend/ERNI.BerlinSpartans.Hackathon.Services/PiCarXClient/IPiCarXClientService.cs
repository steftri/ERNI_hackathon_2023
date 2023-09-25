using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    /// <summary>
    /// This is used to encapsulate the PiCar X implementation.
    /// </summary>
    public interface IPiCarXClientService
    {
        /// <summary>
        /// Sets the default increment values applied when sending commands.
        /// </summary>
        /// <param name="speed">The speed increment applied on the <see cref="GoForward"/> and <see cref="GoBackward"/> commands.</param>
        /// <param name="direction">The angle increment applied to the <see cref="GoLeft"/> and <see cref="GoRight"/> commands.</param>
        /// <param name="headAngle">The angle increment applied to the <see cref="TurnHeadLeft"/> and <see cref="TurnHeadRight"/> commands.</param>        
        void SetDefaultIncrements(int speed, int direction, int headAngle);

        /// <summary>
        /// Connects the MQTT service to the robot.
        /// </summary>
        Task Connect();

        /// <summary>
        /// Tells the car to go backward.
        /// </summary>
        Task<MovementChangedResponse> GoBackward();
        
        /// <summary>
        /// Tells the car to go forward.
        /// </summary>
        Task<MovementChangedResponse> GoForward();
        
        /// <summary>
        /// Tells the car to steer left.
        /// </summary>
        Task<MovementChangedResponse> GoLeft();
        
        /// <summary>
        /// Tells the car to steer right.
        /// </summary>
        Task<MovementChangedResponse> GoRight();        

        /// <summary>
        /// Tells the car to move its head to the left.
        /// </summary>
        Task<MovementChangedResponse> TurnHeadLeft();
        
        /// <summary>
        /// Tells the car to move its head to the left.
        /// </summary>
        Task<MovementChangedResponse> TurnHeadRight();

        /// <summary>
        /// Resets all the values of the robot to zero. Affects the spped, the direction and the head rotation angle.
        /// </summary>
        /// <remarks>
        /// This command should be immediatly sent after connecting to the robot, to get a clean start.
        /// </remarks>
        Task<MovementChangedResponse> Reset();

        /// <summary>
        /// Sets the speed of the robot to zero.
        /// </summary>
        /// <returns></returns>
        Task<MovementChangedResponse> Stop();

        /// <summary>
        /// Starts the lane assisted run.
        /// </summary>
        /// <returns></returns>
        Task<MovementChangedResponse> StartLane();
    }
}
