import unittest   # The test framework

from controller.lane import *


myLane = Lane(20, 180)


class Test_Lane(unittest.TestCase):
    def test_constructor(self):
        self.assertEqual(myLane.calibrate(20), 0)
        self.assertEqual(myLane.calibrate(180), 1.0)

    def test_valid(self):
        self.assertTrue(myLane.isValid([180,21,180]))
        self.assertTrue(myLane.isValid([179,20,180]))
        self.assertTrue(myLane.isValid([180,20,179]))
        self.assertFalse(myLane.isValid([19,19,19]))
                         

    def test_getPosition_in_center(self):
        self.assertEqual(myLane.getPos([180,20,180]), 0.0)

    def test_getPosition_in_range(self):
        self.assertEqual(myLane.getPos([100,100,180]), -0.5)
        self.assertEqual(myLane.getPos([180,100,100]), 0.5)

    def test_getPosition_at_range_limit(self):
        self.assertEqual(myLane.getPos([20,180,180]), -1.0)
        self.assertEqual(myLane.getPos([20,180,20]), 1.0)


    def test_getPosition_outside_of_range(self):
        self.assertTrue(myLane.getPos([180,180,100]) > 1.0)
        self.assertTrue(myLane.getPos([100,180,180]) < -1.0)




if __name__ == '__main__':
    unittest.main()