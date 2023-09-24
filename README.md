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

A robot shall be able to drive thorugh a pre-defined parcours, while the operator is not physically present at the location of the parcours. 


## Requirements


### Stakeholder Requirements

#### StR01 - Solving a parcours
With the robot, it shall be possible to perform a drive through a parcours with the operator not beeing physically present at the location. 


### System Requirements


#### SysR01 - Video
The system shall be able to display a video captured by the robot in real-time on a remote device

#### SysR02 - Remote Control
The system shall be able to controll the movement of the robot remotely



### Frontend Software Requirements





### Firmware SW Requirements

#### SW01 Environment
The software shall run on a Raspberry Pi 4 with a Robot HAT board.

#### SW02 Motion
The software shall be able to control the forward and backward motion with adjustable speed.

#### SW03 Steering
The software shall be able to control the direction of the robot's movement.

#### SW04 Video
The software shall be able to provide a real-time video from the robot's camera.

#### SW05 RemoteControl
The software shall be able to receive and process remote control commands.



### Traceabilitiy and Test Coverage

### Stakeholder Traceability 

| Stakeholder Requiremet     | Traces to |
|----------------------------|-----------|
| StR01 - Solving a parcours | SysR01 - Video<br>SysR02 - Remote Control |

### System Traceability

| System Requirements        | Traces to |
|----------------------------|-----------|
| SysR01 - Video             |           |
| SysR02 - Remote Control    |           |



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





## Additional Information

### Getting started with the Raspberry PI

Youtube video: [The PiCar-X. A Raspberry Pi powered robot car. Supplied by SunFounder](https://www.youtube.com/watch?v=pVayQiLgPK0)


### Environment: 

WLAN: Hackathon
