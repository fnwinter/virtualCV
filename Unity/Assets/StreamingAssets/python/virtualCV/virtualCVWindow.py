import tkinter as tk
from .virtualCV import virtualCVFrame, virtualCVSocket

class virtualCVWindow():
    def __init__(self, frameCallback, dataCallback):
        self.root = tk.Tk()
        self.root.title("virtualCV")
        self.root.geometry("650x490+0+0")
        self.root.resizable(False, False)
        self.root.protocol("WM_DELETE_WINDOW", self.onClose)
        self.app = virtualCVFrame(master=self.root)
        self.app.setFrameCallback(frameCallback)

        self.socket = virtualCVSocket()
        self.socket.start()
        self.socket.setDataCallback(dataCallback)

    def sendData(self, data):
        self.socket.sendData(data)

    def run(self):
        self.root.mainloop()

    def onClose(self):
        print("window closed")
        self.socket.stop()