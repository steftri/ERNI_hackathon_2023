
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
            if(middleValue+(outerValue-otherouterValue) == 0):
              return 0.0 
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






#myLane = Lane(180, 20)
#myLane.getPos([180, 20, 180])