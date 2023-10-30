import unittest   # The test framework

from controller.pi_controller import *

class Test_PiController(unittest.TestCase):
    def test_constructor(self):
        myPiController = PiController(1.0, 0.0, -45, 45)
        self.assertEqual(myPiController.getCtrlVal(), 0)

    def test_controller_direction(self):
        myPiController = PiController(1.0, 0.0, -45, 45)
        myPiController.calc(1)
        self.assertTrue(myPiController.getCtrlVal() > 0)

    def test_controller_proportional_only(self):
        myPiController = PiController(1.0, 0.0, -45, 45)
        myPiController.calc(1)
        self.assertTrue(myPiController.getCtrlVal() > 0)
        myPiController.calc(0)
        self.assertTrue(myPiController.getCtrlVal() == 0)

    def test_controller_integral(self):
        myPiController = PiController(1.0, 0.0, -45, 45)
        myPiController.setParams(1, 1)
        myPiController.calc(1)
        self.assertTrue(myPiController.getCtrlVal() > 0)
        myPiController.calc(0)
        self.assertTrue(myPiController.getCtrlVal() > 0)




if __name__ == '__main__':
    unittest.main()