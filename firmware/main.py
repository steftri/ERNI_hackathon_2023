from picarx import Picarx
from time import sleep

px = Picarx()



#from controller.pi_controller import *


class PiController(object):

    def __init__(self, kp: float, ki: float, ctrlMin: float, ctrlMax: float):
        self.kp = kp
        self.ki = ki
        self.i = 0        
        self.iMin = -10
        self.iMax = 10
        self.ctrlMin = ctrlMin
        self.ctrlMax = ctrlMax
        self.setPoint = 0
        self.ctrlVal = 0

    def setParams(self, kp: float, ki: float):
        self.kp = kp
        self.ki = ki        

    def setIntegralLimits(self, iMin: float, iMax: float):
        self.iMin = iMin
        self.iMax = iMax     

    def reset(self):
        self.i = 0
        self.setPoint = 0
        self.ctrlVal = 0

    def calc(self, processVar):
        diff = processVar - self.setPoint
        self.i += diff
        self.ctrlVal = self.kp*diff +self.ki*self.i
        if self.ctrlVal<self.ctrlMin:
            self.ctrlVal=self.ctrlMin
        elif self.ctrlVal>self.ctrlMax:
            self.ctrlVal=self.ctrlMax
        return self.ctrlVal

    def getCtrlVal(self):
        return self.ctrlVal
    



class Lane(object):

    def __init__(self, blackValue: int, whiteValue: int):
        self.blackValue = blackValue
        self.whiteValue = whiteValue


    def calibrate(self, sensorValue): 
        calibratedValue = (sensorValue-self.blackValue)/(self.whiteValue-self.blackValue)
        if(calibratedValue<=0): 
            calibratedValue=0.0
        elif(calibratedValue>1):
            calibratedValue=1.0     
        return calibratedValue 

    def calcPos(self, middleValue, outerValue, otherouterValue):
        if(middleValue<outerValue and middleValue<0.1): # the lane is outside the range
            if(outerValue<0.1):
                return 0.0         # no lane is detectable at all
            return 1/outerValue;
        else:                      # the middle of the lane is in the range
            return (outerValue-otherouterValue)/(middleValue+(outerValue-otherouterValue));

    def isValid(self, sensorValues):
        if(sensorValues[0] < self.blackValue
           and sensorValues[1] < self.blackValue
           and sensorValues[2] < self.blackValue):
            return False
        elif(sensorValues[0] > self.whiteValue
           and sensorValues[1] > self.whiteValue
           and sensorValues[2] > self.whiteValue):
            return False
        else:
            return True

    def getPos(self, sensorValues):
        left = 1.0-self.calibrate(sensorValues[0])
        middle = 1.0-self.calibrate(sensorValues[1])
        right = 1.0-self.calibrate(sensorValues[2])
        if(left<=right):
            pos = self.calcPos(middle, right, left)
        else:
            pos = -self.calcPos(middle, left, right)
        print("Val: ", left, "-", middle, "-", right, "-> ", pos)
        return pos







myDirController = PiController(20.0, 0.0, -40, 40)
myLane = Lane(20, 180)



def get_line_status(sensor_value_list):
    states = []
    for value in sensor_value_list:
        if(value>100): 
            state = 1
        else:
            state = 0
        states.append(state)
    return states        


def calc_line_pos(sensor_state_list):  # [bool, bool, bool], 0 means black, 1 means white
    if sensor_state_list == [1, 0, 1]:
        return [True, 0]
    elif sensor_state_list == [0, 0, 1]:
        return [True, -0.5]  
    elif sensor_state_list == [0, 1, 1]:
        return [True, -1]  
    elif sensor_state_list == [1, 0, 0]:
        return [True, 0.5]  
    elif sensor_state_list == [1, 1, 0]:
        return [True, 1]  
    return [False, 0]







def main():
    print("starting lane assist")
    invalid_count = 0

    try:
        px.forward(5)

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