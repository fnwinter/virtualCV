# Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
# Licensed under the MIT License.

import cv2
import sys

from virtualCV.virtualCVWindow import virtualCVWindow

class openCVExample(virtualCVWindow):
    def __init__(self, argv):
        super().__init__(argv, self.updateFrame, self.recvData)

    def updateFrame(self, frame):
        feature = cv2.ORB_create()
        kp1 = feature.detect(frame)
        dst1 = cv2.drawKeypoints(frame, kp1, None,
						 flags=cv2.DRAW_MATCHES_FLAGS_DRAW_RICH_KEYPOINTS)
        return dst1

    def recvData(self, data):
        self.sendData("send data from python code")

if __name__ == '__main__':
    example = openCVExample(sys.argv)
    example.run()
