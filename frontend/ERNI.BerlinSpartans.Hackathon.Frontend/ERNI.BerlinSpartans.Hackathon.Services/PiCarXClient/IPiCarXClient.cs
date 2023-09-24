using ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model;

namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    /// <summary>
    /// This is used to encapsulate the PiCar X implementation.
    /// </summary>
    public interface IPiCarXClient
    {
        /// <summary>
        /// Tells the car to go backward. The speed will be incremented by 10% for each call.
        /// </summary>
        Task<MovementChangedResponse> GoBackward();
        /// <summary>
        /// Tells the car to go forward. The speed will be incremented by 10% for each call.
        /// </summary>
        Task<MovementChangedResponse> GoForward();
        /// <summary>
        /// Tells the car to steer left. The steering angle will increase by 10° for each call.
        /// </summary>
        Task<MovementChangedResponse> GoLeft();
        /// <summary>
        /// Tells the car to steer right. The steering angle will increase by 10° for each call.
        /// </summary>
        Task<MovementChangedResponse> GoRight();
        /// <summary>
        /// Tells the car to move it's head to the left. The steering angle will increase by 10° for each call.
        /// </summary>
        Task<MovementChangedResponse> TurnHeadLeft();
        /// <summary>
        /// Tells the car to move it's head to the left. The steering angle will increase by 10° for each call.
        /// </summary>
        Task<MovementChangedResponse> TurnHeadRight();
    }
}
