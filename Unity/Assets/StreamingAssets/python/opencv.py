# copyright by fnwinter 2021

from virtualCV.virtualCV import virtualCVWindow

class openCVExample(virtualCVWindow):
    def __init__(self):
        super().__init__(self.updateFrame, self.recvData)

    def updateFrame(self, frame):
        self.sendData("test")
        return frame

    async def recvData(self, data):
        await self.sendData("test")

if __name__ == '__main__':
    example = openCVExample()
    example.run()
