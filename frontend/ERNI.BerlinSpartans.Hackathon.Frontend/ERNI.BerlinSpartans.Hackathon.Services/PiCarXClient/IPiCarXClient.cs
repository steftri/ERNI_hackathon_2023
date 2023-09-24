namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient
{
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
