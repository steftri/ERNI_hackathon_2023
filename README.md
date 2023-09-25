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



## Requirements


#### StR01 - Solving a parcours from remote
With the robot, it shall be possible to perform a drive through a parcours with the operator not beeing physically present at the location as quickly as possible. 



### System Requirements


#### SysR02 - Remote Control
The system shall be able to control the movement of the robot remotely.



### Frontend Software Requirements

#### UiSW01 - Web application
The software shall run on a web server outside of the local network 
### FR02 Keyboard controls
The user should be able to control the robot with WASD (driving/steering) and EQ (turning the head). 
The keyboard controls should be responsive and not laggy.

### FR03 On-screen keyboard
The user should be able to control the robot with an on-screen keyboard/gamepad.
The on-scren keyboard should give feedback to the user if it's being pressed.

### FR04 Video feed
The user should see the robot's video feed so they know where they are going.

### Firmware SW Requirements

The software shall run on a Raspberry Pi 4 with a Robot HAT board.


#### SW03 - Steering
The software shall be able to control the direction of the robot's movement.

The software shall be able to provide a real-time video from the robot's camera.

The software shall be able to receive and process remote control commands.


#### SW07 - Minimum Turn radius
The software shall be able to follow a track with a radius of down to 30 cm.

### Traceabilitiy and Test Coverage

### Stakeholder Traceability 

| Stakeholder Requiremet     | Traces to |
|----------------------------|-----------|
| StR01 - Solving a parcours | SysR01 - Video<br>SysR02 - Remote Control |

### System Traceability

| System Requirements        | Traces to |
|----------------------------|-----------|



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
 * EzBlock OS
 * mosquitto, apt-get, Version v3.2/v3.1.1
 * vilib, apt-get, latest version 

libzbar0: https://pypi.org/project/pyzbar/, version 0.1.9
git python3-pip python3-setuptools python3-smbus libzbar0

https://github.com/sunfounder/robot-hat.git
https://github.com/sunfounder/vilib.git


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
