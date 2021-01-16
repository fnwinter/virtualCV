# copyright by fnwinter 2021

from virtualCV.virtualCV import virtualCVWindow

class openCVExample(virtualCVWindow):
    def __init__(self):
        super().__init__(self.updateFrame, self.recvData)

    def updateFrame(self, frame):
        return frame

    def recvData(self, data):
        self.sendData("send data")

if __name__ == '__main__':
    example = openCVExample()
    example.run()
