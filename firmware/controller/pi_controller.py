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
        if(self.i<self.iMin):
           self.i=self.iMin
        elif(self.i>self.iMax):
           self.i=self.iMax
        print("i: ", self.i)
        self.ctrlVal = self.kp*diff +self.ki*self.i
        if self.ctrlVal<self.ctrlMin:
            self.ctrlVal=self.ctrlMin
        elif self.ctrlVal>self.ctrlMax:
            self.ctrlVal=self.ctrlMax
        return self.ctrlVal

    def getCtrlVal(self):
        return self.ctrlVal