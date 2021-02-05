/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.IO;
using SysDiagnostics = System.Diagnostics;
using UnityEngine;

namespace VirtualCV
{
    public class FFMPEGExecutor
    {
        private SysDiagnostics.Process ffmpegProcess = new SysDiagnostics.Process();
        private string ffmpegPath = Path.Combine(Application.streamingAssetsPath, "ffmpeg", "bin");

        public StreamWriter ffmpegStreamWriter = null;

        const string URL = "udp://127.0.0.1";
        private int Port = 9090;
        private int FPS = 60;

        public FFMPEGExecutor(int _Port)
        {
            Port = _Port;
        }

        public void Initialze()
        {
            VirtualCVLog.Log("ffmpeg path : " + ffmpegPath);
        }

        public void ExecuteFFMPEG()
        {
            VirtualCVLog.Log("Execute ffmpeg");

            // libx264 : mpegts
            // jpg : mjpeg
            string[] ffmpegOptions =
            {
                "-re",
                "-stream_loop", "-1",
                "-i", "pipe:",
                "-c:v", "jpg",
                "-vf", $"fps={FPS}",
                "-c:v", "mjpeg",
                "-preset", "veryfast",
                "-f", "mjpeg",
                $"{URL}:{Port}"
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

        public void Terminate()
        {
            if (ffmpegProcess != null) ffmpegProcess.Close();
        }
    }
}