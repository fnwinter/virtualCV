# copyright by fnwinter 2021

import cv2
import tkinter as tk

from .virtualCVSocket import virtualCVSocket

from PIL import ImageTk, Image

class virtualCVFrame(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        self.callback = None
        self.capture = None

        self.master = master
        self.pack()
        self.create_widgets()

        self.startCV()

    def create_widgets(self):
        # create menu
        self.menubar = tk.Menu(self)
        self.menu = tk.Menu(self.menubar)
        self.menu.add_command(label="Always top", command=self.alwaysTop)
        self.menu.add_command(label="Detach window", command=self.detachWindow)
        self.menu.add_command(label="Exit", command=self.endCV)
        self.menubar.add_cascade(label="Menu", menu=self.menu)
        self.master.config(menu=self.menubar)
        # create canvas
        self.canvas = tk.Canvas(self, width=640, height=480, bd=2, bg='yellow', highlightbackground="black")
        self.canvas.pack(fill=tk.BOTH, expand=True)

    def startCV(self):
        self.capture = cv2.VideoCapture("udp://127.0.0.1:1234")
        self.capture.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
        self.capture.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
        self.updateFrame()

    def endCV(self):
        self.capture.release()
        cv2.destroyAllWindows()
        self.master.destroy()

    def alwaysTop(self):
        self.master.attributes('-topmost', True)
        self.master.update()

    def detachWindow(self):
        pass

    def updateFrame(self):
        if self.capture and self.callback:
            ret, frame = self.capture.read()
            if ret:
                updated_frame = self.callback(frame)

            img = Image.fromarray(cv2.cvtColor(updated_frame, cv2.COLOR_BGR2RGBA))
            self.imgtk = ImageTk.PhotoImage(image=img)
            self.canvas.create_image(0, 0, image = self.imgtk, anchor="nw")
        self.after(5, self.updateFrame)

    def setFrameCallback(self, callback):
        self.callback = callback

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