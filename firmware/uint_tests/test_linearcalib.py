import unittest   # The test framework

from controller.linearcalib import *


myCalib = LinearCalib()


class Test_LinearCalib(unittest.TestCase):
    def test_constructor(self):
        self.assertEqual(myCalib.calc(0.0), 0.0)
        self.assertEqual(myCalib.calc(0.5), 0.5)
        self.assertEqual(myCalib.calc(1.0), 1.0)  
        self.assertEqual(myCalib.calc(128.0), 128.0)
        self.assertEqual(myCalib.calc(-128.0), -128.0)

    def test_setParams(self):
        myCalib.setParams(2.0, 1.0)
        self.assertEqual(myCalib.calc(0.0), 1.0)

    def test_setCalcParams(self):
        myCalib.setCalcParams(0.5, 1.3, 0.0, 1.0)
        self.assertEqual(myCalib.calc(0.5), 0.0)
        self.assertEqual(myCalib.calc(0.9), 0.5)
        self.assertEqual(myCalib.calc(1.3), 1.0)



if __name__ == '__main__':
    unittest.main()