from picarx import Picarx
from time import sleep

px = Picarx()

speed = 20
p = 20.0
i = 0.5


from controller.pi_controller import PiController
from controller.lane import Lane


myDirController = PiController(p, i, -40, 40)
myLane = Lane(20, 160)



def main():
    print("starting lane assist")
    invalid_count = 0

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
                invalid_count += 1 
            sleep(0.05)

    finally:
        px.set_dir_servo_angle(0)
        px.stop()
        print("stop and exit")
        sleep(0.1)        





if __name__ == "__main__":
    main()