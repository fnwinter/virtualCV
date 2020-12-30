﻿using System.IO;
using SysDiagnostics = System.Diagnostics;
using UnityEngine;

public class FFMPEGExecutor
{
    private SysDiagnostics.Process ffmpegProcess = new SysDiagnostics.Process();
    private string ffmpegPath = "";

    public StreamWriter ffmpegStreamWriter = null;

    public void Initialze()
    {
        Debug.Log("Start FFMPEGExecutor");
        ffmpegPath = Path.Combine(Application.streamingAssetsPath, "ffmpeg", "bin");
        Debug.Log("ffmpeg path : " + ffmpegPath);
    }

    public void ExecuteFFMPEG()
    {
        Debug.Log("Execute ffmpeg");
        ffmpegProcess.StartInfo.FileName = Path.Combine(ffmpegPath, "ffmpeg.exe");
        ffmpegProcess.StartInfo.Arguments = "-re -stream_loop -1 -i pipe: -c:v libx264 -vf \"fps = 60\" -c:v libx264 -f mpegts udp://127.0.0.1:1234";
        ffmpegProcess.StartInfo.WorkingDirectory = ffmpegPath;
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardInput = true;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
        ffmpegProcess.StartInfo.CreateNoWindow = true;

        ffmpegProcess.Start();

        ffmpegStreamWriter = ffmpegProcess.StandardInput;
    }
}
