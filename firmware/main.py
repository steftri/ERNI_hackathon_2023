from vilib import Vilib
from picarx import Picarx
from robot_hat import TTS
import time
import paho.mqtt.client as mqtt
import json
from threading import Lock

from controller.pi_controller import PiController
from controller.lane import Lane




px = Picarx()
px_lock = Lock()
tts_robot = TTS()
Vilib.camera_start(vflip=False,hflip=False)
Vilib.display(local=False,web=True)

speed = 20
p = 20.0
i = 0.5
broker = "broker.hivemq.com"
port = 1883


myDirController = PiController(p, i, -40, 40)
myDirController.setIntegralLimits(-100, 100)
myLane = Lane(40, 160)




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
        px.forward(speed)

        while invalid_count<30:
            sensor_value_list = px.get_grayscale_data()
        
            if(myLane.isValid(sensor_value_list)):
                linePos = myLane.getPos(sensor_value_list)
                direction = myDirController.calc(linePos)
                print("sensor: ", sensor_value_list, "; linepos: ", linePos, " dir: ", direction)
                px.set_dir_servo_angle(direction)
                invalid_count = 0
            else:
                print("sensor: ", sensor_value_list, " (invalid)")
                px.set_dir_servo_angle(0.0)
                invalid_count += 1
            print("Exec-Time: ", time.time()-last_tick)
            last_tick =  time.time();
            time.sleep(0.01)

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
        print("Time: ", time.time());
        sensor_value_list = px.get_grayscale_data()
        if(myLane.isValid(sensor_value_list)):
            linePos = myLane.getPos(sensor_value_list)        
            print("sensor: ", sensor_value_list, "; linepos: ", linePos)
        else:
            print("sensor: ", sensor_value_list, " (invalid)")
        time.sleep(5);      



if __name__ == "__main__":
    main()