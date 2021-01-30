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
        private string ffmpegPath = "";

        public StreamWriter ffmpegStreamWriter = null;
        private int FPS = 60;

        public void Initialze()
        {
            Debug.Log("Start FFMPEGExecutor");
            ffmpegPath = Path.Combine(Application.streamingAssetsPath, "ffmpeg", "bin");
            Debug.Log("ffmpeg path : " + ffmpegPath);
        }

        public void ExecuteFFMPEG()
        {
            Debug.Log("Execute ffmpeg");
            string fps = string.Format("\"fps={0}\"", FPS);
            // libx264 : mpegts
            // jpg : mjpeg
            string[] ffmpegOptions =
            {
            "-re",
            "-stream_loop", "-1",
            "-i", "pipe:",
            "-c:v", "jpg",
            "-vf", fps,
            "-c:v", "mjpeg",
            "-preset", "veryfast",
            "-f", "mjpeg",
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
}