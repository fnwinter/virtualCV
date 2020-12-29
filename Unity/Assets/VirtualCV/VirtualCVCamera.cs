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

    private FFMPEGExecutor ffmpeg = null;
    private PythonExecutor python = null;

    void Start()
    {
        Debug.Log("Start VirtualCVCamera");
        cam = GetComponent<Camera>();
        cameraImage= new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        screenshotPath = Path.Combine(Application.dataPath, "..", "Screenshot");
        Directory.CreateDirectory(screenshotPath);
        Debug.Log("Screenshot path : " + screenshotPath);

        ffmpeg = new FFMPEGExecutor();
        ffmpeg.Initialze();
        ffmpeg.ExecuteFFMPEG();

        python = new PythonExecutor();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.F12))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        if (cam == null || cameraImage == null) return;

        RenderTexture rendText = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();

        cameraImage.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        cameraImage.Apply();
        RenderTexture.active = rendText;

        byte[] bytes = cameraImage.EncodeToJPG();
        //char[] cArray = System.Text.Encoding.ASCII.GetString(bytes).ToCharArray();
        if (ffmpeg != null && ffmpeg.myStreamWriter != null) ffmpeg.myStreamWriter.BaseStream.Write(bytes, 0, bytes.Length);

        //screenshotIndex++;
        //string screenshotFileName = Path.Combine(screenshotPath, string.Format("Screenshot_{0}.jpg", screenshotIndex));
        //System.IO.File.WriteAllBytes(screenshotFileName, bytes);
    }
}
