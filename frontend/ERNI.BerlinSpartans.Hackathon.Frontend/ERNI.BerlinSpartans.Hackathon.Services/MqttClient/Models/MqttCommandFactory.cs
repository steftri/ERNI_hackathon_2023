using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Extensions;

namespace ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;

/// <summary>
/// Represnts a remote control command sent to the robot.
/// </summary>
public static class MqttCommandFactory
{
    const int SpeedMaxValue = 50;
    const int SpeedMinvalue = -50;
    const int AngleMaxValue = 45;
    const int AngleMinValue = -45;

    public static MqttCommand SetSpeed(decimal value)
    {
        if (value < SpeedMinvalue || value > SpeedMaxValue)
        {
            throw new InvalidOperationException($"Speed must be a value between {SpeedMinvalue} and {SpeedMaxValue}.");
        };

        var payload = new[]
        {
            new {
                Operation = "set_speed",
                Speed = value
            }
        }.ToJson();
        return GetCommand(payload);
    }

    public static MqttCommand Stop(decimal value)
    {
        var payload = new[]
        {
            new {
                Operation = "stop"
            }
        }.ToJson();
        return GetCommand(payload);
    }

    public static MqttCommand SetDirection(decimal value)
    {
        if (value < AngleMinValue || value > AngleMaxValue)
        {
            throw new InvalidOperationException($"Direction must be a value between {AngleMinValue} and {AngleMaxValue}.");
        };
        var payload = new[]
        {
            new {
                Operation = "set_direction",
                Angle = value
            }
        }.ToJson();
        return GetCommand(payload);
    }

    public static MqttCommand SetHeadRotate(decimal value)
    {
        if (value < AngleMinValue || value > AngleMaxValue)
        {
            throw new InvalidOperationException($"Rotate Angle must be a value between {AngleMinValue} and {AngleMaxValue}.");
        };
        var payload = new[]
        {
            new {
                Operation = "set_head_rotate",
                Angle = value
            }
        }.ToJson();
        return GetCommand(payload);
    }

    public static MqttCommand SetHeadTilt(decimal value)
    {
        if (value < AngleMinValue || value > AngleMaxValue)
        {
            throw new InvalidOperationException($"Tilt Angle must be a value between {AngleMinValue} and {AngleMaxValue}.");
        };
        var payload = new[]
        {
            new {
                Operation = "set_head_tilt",
                Angle = value
            }
        }.ToJson();
        return GetCommand(payload);
    }

    public static MqttCommand Say(string text)
    {
        var payload = new[]
        {
            new {
                Operation = "say",
                text
            }
        }.ToJson();
        return GetCommand(payload);
    }

    private static MqttCommand GetCommand(string payload)
    {
        return new MqttCommand
        {
            Topic = "command",
            Payload = payload
        };
    }

}
