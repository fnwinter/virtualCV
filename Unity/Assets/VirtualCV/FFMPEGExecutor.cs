using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using UnityEngine;

public class FFMPEGExecutor
{
    public StreamWriter myStreamWriter = null;
    public System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
    public string ffmpegPath;

    // Start is called before the first frame update
    public void Initialze()
    {
        Debug.Log("Start FFMPEGExecutor");
        ffmpegPath= Path.Combine(Application.streamingAssetsPath, "ffmpeg", "bin");
        Debug.Log("FFMPEG path : " + ffmpegPath);
    }

    public void ExecuteFFMPEG()
    {
        Debug.Log("Execute ffmpeg");
        myProcess.StartInfo.FileName = ffmpegPath + "\\ffmpeg.exe";
        myProcess.StartInfo.Arguments = "-re -stream_loop -1 -i pipe: -c:v libx264 -vf \"fps = 60\" -c:v libx264 -f mpegts udp://127.0.0.1:1234";
        myProcess.StartInfo.WorkingDirectory = ffmpegPath;
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.RedirectStandardInput = true;
        //myProcess.StartInfo.RedirectStandardOutput = true;

        myProcess.Start();

        myStreamWriter = myProcess.StandardInput;
    }

    // Update is called once per frame
    public void Update()
    {

    }
}
