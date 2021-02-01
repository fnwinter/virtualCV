/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

namespace VirtualCV
{
    public class VirtualCVWebSocket
    {
        const string URL = "ws://127.0.0.1";
        static int Port = 8090;

        private Thread thread = null;
        private static WebSocketServer webSocketServer = null;
        private static Data data = null;

        public class Data : WebSocketBehavior
        {
            public Data()
            {
                VirtualCVWebSocket.data = this;
            }

            protected override void OnMessage(MessageEventArgs e)
            {

            }
        }

        public static void WebSocketProc()
        {
            VirtualCVLog.Log("Start WebSocket Server");
 
            string serverURL = $"{URL}:{Port}";
            webSocketServer = new WebSocketServer(serverURL);
            webSocketServer.AddWebSocketService<Data>("/Data");
            webSocketServer.Start();
        }

        public static WebSocketServer getWebsocket()
        {
            return webSocketServer;
        }

        public void Initialize()
        {
            thread = new Thread(new ThreadStart(WebSocketProc));
            thread.Start();
        }
    }
}