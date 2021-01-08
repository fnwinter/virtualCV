using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

public class VirtualCVWebSocket
{
    public class Data : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                      ? "I've been balused already..."
                      : "I'm not available now.";

            Send(msg);
        }
    }

    public static void ThreadProc()
    {
        Debug.Log("ThreadProc");
        var wssv = new WebSocketServer("ws://127.0.0.1:8090");
        wssv.AddWebSocketService<Data>("/Data");
        wssv.Start();
        
    }

    public void Initialize()
    {
        Thread t = new Thread(new ThreadStart(ThreadProc));
        t.Start();
    }
}
