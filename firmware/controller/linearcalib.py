
class LinearCalib(object):

    def __init__(self, m: float = 1.0, n: float = 0.0):
        self.m = m
        self.n = n

    def setParams(self, m: float, n: float):
        self.m = m
        self.n = n 

    def setCalcParams(self, InMin: float, InMax: float, OutMin: float, OutMax: float):
        if(0.0 == InMax-InMin):
            self.m = 1.0
            self.n = OutMin-InMin
        else:
            self.m = (OutMax-OutMin)/(InMax-InMin)
            self.n = OutMin-self.m*InMin
        
    def calc(self, Value: float):
        return self.m*Value+self.n

