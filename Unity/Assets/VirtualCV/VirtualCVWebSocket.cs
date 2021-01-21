﻿/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

public class VirtualCVWebSocket
{
    private Thread thread = null;
    private static WebSocketServer webSocketServer = new WebSocketServer("ws://127.0.0.1:8090");
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
        Debug.Log("Start WebSocket Server");
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
