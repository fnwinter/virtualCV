using System.IO;
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
        string[] ffmpegOptions =
        {
            "-re",
            "-stream_loop", "-1",
            "-i", "pipe:",
            "-c:v", "png",
            "-vf", "\"fps=60\"",
            "-c:v", "libx264",
            "-preset", "veryfast",
            "-f", "mpegts",
            "udp://127.0.0.1:1234"
        };

        ffmpegProcess.StartInfo.FileName = Path.Combine(ffmpegPath, "ffmpeg.exe");
        ffmpegProcess.StartInfo.Arguments = string.Join(" ", ffmpegOptions);
        ffmpegProcess.StartInfo.WorkingDirectory = ffmpegPath;
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardInput = true;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
        ffmpegProcess.StartInfo.CreateNoWindow = true;

        ffmpegProcess.Start();

        ffmpegStreamWriter = ffmpegProcess.StandardInput;
    }
}
