# copyright by fnwinter 2021

import cv2
import tkinter as tk

from PIL import ImageTk, Image

class virtualCVFrame(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        self.master = master
        self.pack()
        self.create_widgets()
        self.callback = None
        self.capture = None
        self.start()

    def create_widgets(self):
        self.hi_there = tk.Button(self)
        self.hi_there["text"] = "Hello World\n(click me)"
        self.hi_there.pack(side="top")

        self.quit = tk.Button(self, text="QUIT", fg="red",
                              command=self.master.destroy)
        self.quit.pack(side="bottom")

        self.canvas = tk.Canvas(self, width=640, height=480)
        self.canvas.pack()

    def start(self):
        self.capture = cv2.VideoCapture("udp://127.0.0.1:1234")
        self.capture.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
        self.capture.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
        self.run()

    def end(self):
        self.capture.release()
        cv2.destroyAllWindows()

    def run(self):
        if self.capture:
            ret, frame = self.capture.read()
            cv2image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGBA)
            img = Image.fromarray(cv2image)
            self.imgtk = ImageTk.PhotoImage(image=img)
            self.canvas.create_image(0, 0, image = self.imgtk, anchor = tk.NW)

            if self.callback:
                self.callback(frame)

        self.after(5, self.run)

    def setFrameCallback(self, callback):
        self.callback = callback

class virtualCVWindow():
    def __init__(self, callback):
        self.root = tk.Tk()
        self.root.geometry("640x480+0+0")
        self.root.resizable(False, False)
        self.app = virtualCVFrame(master=self.root)
        self.app.setFrameCallback(callback)

    def run(self):
        self.root.mainloop()