from vilib import Vilib
from picarx import Picarx
from robot_hat import TTS
import time
import paho.mqtt.client as mqtt
import json
from threading import Lock

from controller.pi_controller import PiController
from controller.lane import Lane
from controller.linearcalib import LinearCalib



px = Picarx()
px_lock = Lock()
tts_robot = TTS()
Vilib.camera_start(vflip=False,hflip=False)
Vilib.display(local=False,web=True)

la_max_speed = 50
p = 20.0
i = 0.2
broker = "broker.hivemq.com"
port = 1883


myDirController = PiController(p, i, -40, 40)
myDirController.setIntegralLimits(-100, 100)
myLane = Lane(32, 255-32)

sensorCalibLeft = LinearCalib()
sensorCalibMiddle = LinearCalib()
sensorCalibRight = LinearCalib()

sensorCalibLeft.setCalcParams(28, 175, 32, 255-48)
sensorCalibMiddle.setCalcParams(36, 226, 32, 255-48)
sensorCalibRight.setCalcParams(29, 176, 32, 255-48)

# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

    # Subscribing in on_connect() means that if we lose the connection and
    # reconnect then subscriptions will be renewed.
    client.subscribe("command")

# The callback for when a PUBLISH message is received from the server.
def on_message(client, userdata, msg):
    message_text = str(msg.payload.decode('utf-8'));
    print( msg.topic + " " + message_text)
    commands = json.loads( msg.payload.decode('utf-8'))

    with px_lock:
        for command in commands:
            operation = command['operation']

            if  operation == 'set_speed':
                cmd_set_speed( command)
            elif operation == 'stop':
                cmd_stop( command)
            elif operation == 'set_direction':
                cmd_set_direction( command)
            elif operation == 'set_head_rotate':
                cmd_set_head_rotate( command)
            elif operation == 'set_head_tilt':
                cmd_set_head_tilt( command)
            elif operation == 'set_max_speed':
                cmd_set_max_speed( command)
            elif operation == 'set_grayscale_config':
                cmd_set_grayscale_config( command)
            elif operation == 'set_controller_config':
                cmd_set_controller_config( command)                
            elif operation == 'start_lane_assist':
                cmd_start_lane_asssist( command)
            elif operation == 'say':
                cmd_say( command)
            else:
                print('Unknown command')



def cmd_say( command):
    text = command['text']

    tts_robot.say( text)


def cmd_set_head_tilt( cmd):
    angle = cmd['angle']

    if( -45 < angle & angle < 45):
        px.set_camera_tilt_angle( angle)

def cmd_set_head_rotate( cmd):
    angle = cmd['angle']

    if( -45 < angle & angle < 45):
        px.set_cam_pan_angle( angle)

def cmd_set_speed( cmd):
    speed = cmd['speed']

    if( speed > 0 & speed < 50):
        px.forward( speed)
    elif( speed < 0 & speed > -50):
        px.backward( -speed)
    else:
        px.stop()

def cmd_stop( cmd):
    px.stop()

def cmd_set_direction( cmd):
    angle = cmd['angle']

    if( -45 < angle & angle < 45):
        px.set_dir_servo_angle( angle)


def cmd_set_max_speed( cmd):
    la_max_speed = cmd['max_speed']
    print("max_speed: ", la_max_speed)



def cmd_set_grayscale_config( cmd):
    black = cmd['black']
    white = cmd['white']
    print("grayscale black: ", black, "; white: ", white)
    myLane.blackValue = black
    myLane.whiteValue = white


def cmd_set_controller_config( cmd):
    p = cmd['p']
    i = cmd['i']
    print("controller p: ", p, "; i: ", i)
    myDirController.setParams(p, i)


def cmd_start_lane_asssist( cmd):
    print("starting lane assist")
    invalid_count = 0
    last_tick = time.time()

    try:
        px.forward(la_max_speed)

        while invalid_count<40:
            sensor_value_list = px.get_grayscale_data()
            sensor_value_list[0] = sensorCalibLeft.calc(sensor_value_list[0])
            sensor_value_list[1] = sensorCalibMiddle.calc(sensor_value_list[1])
            sensor_value_list[2] = sensorCalibRight.calc(sensor_value_list[2])
        
            if(myLane.isValid(sensor_value_list)):
                linePos = myLane.getPos(sensor_value_list)
                direction = myDirController.calc(linePos)
                print("sensor: ", sensor_value_list, "; linepos: ", linePos, " dir: ", direction)
                px.set_dir_servo_angle(direction)
                px.forward(la_max_speed)  
                invalid_count = 0
            else:
                print("sensor: ", sensor_value_list, " (invalid)")
                px.set_dir_servo_angle(0.0)
                px.forward(la_max_speed)
                invalid_count += 1
            print("Exec-Time: ", time.time()-last_tick)
            last_tick =  time.time();
            time.sleep(0.005)

    finally:
        px.set_dir_servo_angle(0)
        px.stop()
        print("stop and exit")
        time.sleep(0.1)  







client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

# separate mqtt client to send events and avoid threading issues
#client2 = mqtt.Client()









def main():
    # Blocking call that processes network traffic, dispatches callbacks and
    # handles reconnecting.
    # Other loop*() functions are available that give a threaded interface and a
    # manual interface.
    client.connect(broker, port, 60)
    client.loop_start()

    #client2.connect(broker, port, 60)

    while True:
        sensor_value_list = px.get_grayscale_data()
        print("Time: ", time.time(), "; raw sensor: ", sensor_value_list)

        sensor_value_list[0] = sensorCalibLeft.calc(sensor_value_list[0])
        sensor_value_list[1] = sensorCalibMiddle.calc(sensor_value_list[1])
        sensor_value_list[2] = sensorCalibRight.calc(sensor_value_list[2])
        if(myLane.isValid(sensor_value_list)):
            linePos = myLane.getPos(sensor_value_list)        
            print("Calibrated sensor: ", sensor_value_list, "; linepos: ", linePos)
        else:
            print("Calibrated sensor: ", sensor_value_list, " (invalid)")
        time.sleep(3);


if __name__ == "__main__":
    main()