import websocket
from websocket import create_connection
import select
from threading import Thread

class virtualCVSocket(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.callback = None
        self.websocket = None
        self.running = True

    def run(self):
        self.websocket = create_connection("ws://127.0.0.1:8090/Data")
        while self.running and self.websocket.connected:
            r, _, e = select.select([self.websocket.sock], [], [])
            data =  self.websocket.recv()
            if data:
                self.callback(data)
        self.websocket.close()

    def stop(self):
        self.running = False
        self.websocket.send("closed")

    def setDataCallback(self, dataCallback):
        self.callback = dataCallback

    def sendData(self, data):
        if self.websocket and self.websocket.connected:
            self.websocket.send(data)