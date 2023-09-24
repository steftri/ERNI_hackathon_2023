namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
    /// <summary>
    /// This is used to encapsulate the PiCar X implementation.
    /// </summary>
    public interface IPiCarXClient
    {
        Task Accelerate();
        Task Decelerate();
        Task GoBackward();
        Task GoForward();
        Task GoLeft();
        Task GoRight();
        Task TurnHeadLeft();
        Task TurnHeadRight();
    }
}
