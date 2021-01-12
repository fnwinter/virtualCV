import asyncio
import websockets

websocket = None

class virtualCVSocket:
    @staticmethod
    async def connect():
        uri = "ws://127.0.0.1:8090/Data"
        async with websockets.connect(uri) as websocket:
            name = input("What's your name? ")

            await websocket.send(name)
            print(f"> {name}")

            greeting = await websocket.recv()
            print(f"< {greeting}")

    @staticmethod
    def start():
        asyncio.get_event_loop().run_until_complete(virtualCVSocket.connect())

    def getSocket():
        return websocket
