class PiController(object):

    def __init__(self, kp: float, ki: float):
        self.kp = kp
        self.ki = ki
        self.i = 0        
        self.iMin = -100
        self.iMax = -100
        self.setPoint = 0
        self.ctrlVal = 0

    def setParams(self, kp: float, ki: float):
        self.kp = kp
        self.ki = ki        

    def setLimits(self, iMin: float, iMax: float):
        self.iMin = iMin
        self.iMax = iMax     

    def reset(self):
        self.i = 0
        self.setPoint = 0
        self.ctrlVal = 0

    def calc(self, processVar):
        diff = self.setPoint - processVar
        self.i += diff
        self.ctrlVal = self.kp*diff +self.ki*self.i

    def getCtrlVal(self):
        return self.ctrlVal
    