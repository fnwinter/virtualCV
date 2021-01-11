# copyright by fnwinter 2021

from virtualCV.virtualCV import virtualCVWindow

class openCVExample(virtualCVWindow):
    def __init__(self):
        super().__init__(self.updateFrame)

    def updateFrame(self, frame):
        #print("called")
        #print(frame)
        pass

if __name__ == '__main__':
    example = openCVExample()
    example.run()
