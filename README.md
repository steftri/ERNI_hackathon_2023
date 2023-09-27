# ERNI_hackathon_2023

This is the main development repository for the 030 Berlin Spartans team for the oneERNI global hackathon 2023. 

Development branch: [develop](https://github.com/steftri/ERNI_hackathon_2023/tree/develop)

Link to main repository: [oneERNIglobalhackaton](https://github.com/dani72/oneERNIglobalHackathon)

Our members are: 
 * Stephan Finner [Stefan F.](https://github.com/StepBerlin)
 * Max Cocco [max-cocco](https://github.com/max-cocco)
 * Martin Teichler [MTeichlerRche](https://github.com/MTeichlerRche)
 * Stefan Trippler [steftri](https://github.com/steftri)

The base for the project is the PiCar-X by SunFounder with a Rapberry Pi 4. 


## Intended Use

A robot shall be able to drive thorugh a pre-defined parcours as fast as possible, while the operator is not physically present at the location of the parcours. 


## Requirements

### Stakeholder Requirements

#### StR01 - Solving a parcours from remote
With the robot, it shall be possible to perform a drive through a parcours with the operator not beeing physically present at the location as quickly as possible. 


### System Requirements

#### SysR02 - Remote Control
The system shall be able to control the movement of the robot remotely.

#### SysR03 - Lane Assist
The system shall provide a lane assist.


### Frontend Software Requirements

#### FR02 - Keyboard controls
The user should be able to control the robot with WASD (driving/steering) and EQ (turning the head). 
The keyboard controls should be responsive and not laggy.

#### FR03 - On-screen keyboard
The user should be able to control the robot with an on-screen keyboard/gamepad.
The on-scren keyboard should give feedback to the user if it's being pressed.

#### FR04 - Video feed
The user should see the robot's video feed so they know where they are going.

#### FR05 - Line Assist support
The software shall provide the possibility to engage the lane assist on the robot.


### Firmware SW Requirements

#### SW01 - Environment
The software shall run on a Raspberry Pi 4 with a Robot HAT board.

#### SW02 - Motion
The software shall be able to control the forward and backward motion.

#### SW03 - Steering
The software shall be able to control the direction of the robot's movement.

#### SW04 - Video
The software shall be able to provide a real-time video from the robot's camera.

#### SW05 - RemoteControl
The software shall be able to receive and process remote control commands.

#### SW06 - Lane Assist
The software shall be able to follow a given track.

#### SW07 - Minimum Turn radius
The software shall be able to follow a track with a radius of down to 30 cm.

#### SW08 - Gaps in the track
The software shall be able to be tolerant against gaps in the track marking of up to 25 cm.


### Traceabilitiy and Test Coverage

### Stakeholder Traceability 

| Stakeholder Requiremet     | Traces to |
|----------------------------|-----------|
| StR01 - Solving a parcours | SysR01 - Video<br>SysR02 - Remote Control |

### System Traceability

| System Requirements        | Traces to |
|----------------------------|-----------|
| SysR02 - Remote Control    | FR02 - Keyboard controls<br>FR03 - On-screen keyboard<br>FR04 - Video feed<br>SW01 - Environment<br>SW02 - Motion<br>SW03 - Steering<br>SW04 - Video<br>SW05 - RemoteControl   |
| SysR03 - Lane Assist       | SW06 - Lane Assist<br>SW07 - Minimum Turn radius<br>SW08 - Gaps in the track   |


## Software Architecture


The firmware is composed of different modules
 * The Lane Controller is responsible for calculating the position relatively to the track. Input data are the three grey values from the grey scale sensor. Output data is a value in float, while 0 means the car is exactly above the track, negative values means it is left of the track, positive values right.
 * the PiControlle is responsible for calculating a direction command as the result of the calculated position above the track. 


## Software Development

### Tools used for Firmware development

 * [Raspberry Pi imager](https://www.raspberrypi.com/software/), version 1.7.5
 * [EzBlock Studio online](http://ezblock.cc/ezblock-studio)
 * EzBlock App, AppStore, Version 3.2.140
 * Git Bash
 * Visual Studio Code 
 * VSCode extension Python, Version v2023.6.1

### Firmware SOUP 
 * Raspberry Pi OS Lite (32-bit) with git, python3-pip, python3-setuptools, python3-smbus and 
 * Robot-Hat v2.0
 * vilib v0.0.6
 * PiCar-X v2.0


### Tools used for Frontend development
 * Visual Studio 2022

### Nuget Packages used for the Frontend
| Package                                  | Version   |                  
| ---------------------------------------- | ----------|
| Microsoft.Extensions.Logging.Abstractions| 7.0.1     |
| Microsoft.Extensions.Options             | 7.0.1     |
| MQTTnet                                  | 4.3.1.873 |
| MQTTnet.Extensions.ManagedClient         | 4.3.1.873 |
| Microsoft.NET.Test.Sdk                   | 17.5.0    |
| NSubstitute                              | 5.1.0     |
| xunit                                    | 2.4.2     |
| xunit.runner.visualstudio                | 2.4.5     |
| coverlet.collector                       | 3.2.0     |
| Microsoft.Extensions.Logging             | 7.0.0     |



## Additional Information

### Getting started with the Raspberry PI

Youtube video: [The PiCar-X. A Raspberry Pi powered robot car. Supplied by SunFounder](https://www.youtube.com/watch?v=pVayQiLgPK0)


### Environment: 

WLAN: Hackathon
