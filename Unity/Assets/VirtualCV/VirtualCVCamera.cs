/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VirtualCVCamera : MonoBehaviour
{
    private Camera cam = null;
    private Texture2D cameraImage = null;

    private int screenshotIndex = 0;
    private string screenshotPath = "";

    private FFMPEGExecutor ffmpegExecutor = null;
    private PythonExecutor pythonExecutor = null;
    private VirtualCVWebSocket socket = null;

    void Start()
    {
        Debug.Log("VirtualCVCamera starts");
        cam = GetComponent<Camera>();
        cameraImage= new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        screenshotPath = Path.Combine(Application.dataPath, "..", "Screenshot");
        Directory.CreateDirectory(screenshotPath);

        ffmpegExecutor = new FFMPEGExecutor();
        ffmpegExecutor.Initialze();
        ffmpegExecutor.ExecuteFFMPEG();

        pythonExecutor = new PythonExecutor();
        pythonExecutor.Initialze();
        pythonExecutor.ExecutePython();

        socket = new VirtualCVWebSocket();
        socket.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRenderTexture();
        UpdateToFFMPEG();

        if (Input.GetKeyUp(KeyCode.F12))
        {
            TakeScreenshot();
        }
    }

    void UpdateRenderTexture()
    {
        if (cam == null || cameraImage == null) return;

        RenderTexture rendText = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();

        cameraImage.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        cameraImage.Apply();
        RenderTexture.active = rendText;
    }

    void UpdateToFFMPEG()
    {
        if (ffmpegExecutor == null || cameraImage == null) return;

        byte[] imageData = cameraImage.EncodeToJPG();
        if (ffmpegExecutor.ffmpegStreamWriter != null)
        {
            ffmpegExecutor.ffmpegStreamWriter.BaseStream.Write(imageData, 0, imageData.Length);
        }

    }

    void TakeScreenshot()
    {
        screenshotIndex++;
        string screenshotFileName = Path.Combine(screenshotPath, string.Format("Screenshot_{0}.jpg", screenshotIndex));
        Debug.Log("Screenshot saved : path - " + screenshotFileName);
        File.WriteAllBytes(screenshotFileName, cameraImage.EncodeToJPG());
    }
}
