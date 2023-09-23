namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

/// <summary>
/// Represnts a remote control command sent to the robot.
/// </summary>
public class RemoteCommand
{
    /// <summary>
    /// Gets or sets the type of command.
    /// </summary>
    public RemoteCommandType CommandType { get; set; }

    /// <summary>
    /// A value associated to the command.
    /// </summary>
    public decimal? Value { get; set; }


    #region Factory Methods
    
    /// <summary>
    /// Creates a new <see cref="RemoteCommand"/> of type <see cref="RemoteCommandType.Forward"/>  with the  given value.
    /// </summary>
    /// <param name="value">The value associated to the command.</param>
    /// <returns>
    /// An instance of <see cref="RemoteCommand"/>.
    /// </returns>
    public static RemoteCommand ForwardCommand(decimal value)
    {
        return new RemoteCommand
        {
            CommandType = RemoteCommandType.Forward,
            Value = value
        };
    }

    /// <summary>
    /// Creates a new <see cref="RemoteCommand"/> of type <see cref="RemoteCommandType.Backward"/>  with the  given value.
    /// </summary>
    /// <param name="value">The value associated to the command.</param>
    /// <returns>
    /// An instance of <see cref="RemoteCommand"/>.
    /// </returns>
    public static RemoteCommand BackwardCommand(decimal value)
    {
        return new RemoteCommand
        {
            CommandType = RemoteCommandType.Backward,
            Value = value
        };
    }

    /// <summary>
    /// Creates a new <see cref="RemoteCommand"/> of type <see cref="RemoteCommandType.Left"/>  with the  given value.
    /// </summary>
    /// <param name="value">The value associated to the command.</param>
    /// <returns>
    /// An instance of <see cref="RemoteCommand"/>.
    /// </returns>
    public static RemoteCommand LeftCommand(decimal value)
    {
        return new RemoteCommand
        {
            CommandType = RemoteCommandType.Left,
            Value = value
        };
    }

    /// <summary>
    /// Creates a new <see cref="RemoteCommand"/> of type <see cref="RemoteCommandType.Right"/>  with the  given value.
    /// </summary>
    /// <param name="value">The value associated to the command.</param>
    /// <returns>
    /// An instance of <see cref="RemoteCommand"/>.
    /// </returns>
    public static RemoteCommand RightCommand(decimal value)
    {
        return new RemoteCommand
        {
            CommandType = RemoteCommandType.Right,
            Value = value
        };
    }

    #endregion


}
