# Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
# Licensed under the MIT License.

import select
import websocket

from websocket import create_connection
from threading import Thread

class virtualCVSocket(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.callback = None
        self.websocket = None
        self.running = True

    def run(self):
        self.websocket = create_connection("ws://127.0.0.1:8090/Data")
        if not self.websocket: return
        if not self.websocket.connected: return
        if not self.callback: return

        timeout = 10 # 10 seconds
        while self.running:
            r, _, _ = select.select([self.websocket.sock], [], [], timeout)

            if len(r) == 0: break
            data = self.websocket.recv()
            if data:
                self.callback(data)
        self.websocket.close()

    def stop(self):
        self.running = False
        self.websocket.send("VirtualCVSocket closed")

    def setDataCallback(self, dataCallback):
        self.callback = dataCallback

    def sendData(self, data):
        if self.websocket and self.websocket.connected:
            self.websocket.send(data)