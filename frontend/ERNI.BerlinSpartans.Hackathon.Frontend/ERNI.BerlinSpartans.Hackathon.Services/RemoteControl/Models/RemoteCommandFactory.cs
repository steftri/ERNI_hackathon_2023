using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Extensions;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;

/// <summary>
/// Represnts a remote control command sent to the robot.
/// </summary>
public static class RemoteCommandFactory
{
          
    public static string SetSpeed(decimal value)
    {
        return new 
        {
            Operation = "set_speed",
            Speed = value
        }.ToJson();
    }

    public static string Stop(decimal value)
    {
        return new
        {
            Operation = "stop"
        }.ToJson();
    }

    public static string SetDirection(decimal value)
    {
        return new
        {
            Operation = "set_speed",
            Angle = value
        }.ToJson();
    }

    public static string SetHeadRotate(decimal value)
    {
        return new
        {
            Operation = "set_head_rotate",
            Angle = value
        }.ToJson();
    }

    public static string SetHeadTilt(decimal value)
    {
        return new
        {
            Operation = "set_head_tilt",
            Angle = value
        }.ToJson();
    }

    public static string Say(string text)
    {
        return new
        {
            Operation = "say",
            text = text
        }.ToJson();
    }

}
