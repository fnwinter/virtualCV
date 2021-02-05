# Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
# Licensed under the MIT License.

import cv2
import sys

from virtualCV.virtualCVWindow import virtualCVWindow

class openCVExample(virtualCVWindow):
    def __init__(self, argv):
        super().__init__(argv, self.updateFrame, self.recvData)

    def updateFrame(self, frame):
        return frame

    def recvData(self, data):
        self.sendData("send data")

if __name__ == '__main__':
    example = openCVExample(sys.argv)
    example.run()
