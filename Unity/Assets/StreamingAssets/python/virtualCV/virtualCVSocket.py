import asyncio
import websockets

class virtualCVSocket:
    def __init__(self):
        self.callback = None
        self.websocket = None

    async def connect(self):
        uri = "ws://127.0.0.1:8090/Data"
        async with websockets.connect(uri) as self.websocket:
            while True:
                data = await self.websocket.recv()
                if self.callback:
                    self.callback(data)
                    print(f"< {data}")

    def start(self):
        self.loop = asyncio.get_event_loop()
        self.loop.run_until_complete(self.connect())

    def getSocket(self):
        return self.websocket

    def setDataCallback(self, dataCallback):
        self.callback = dataCallback

    async def sendData(self, data):
        if self.websocket:
            await self.websocket.send(data)