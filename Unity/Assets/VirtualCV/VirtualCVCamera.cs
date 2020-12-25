using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class VirtualCVCamera : MonoBehaviour
{
    Camera cam = null;
    // Start is called before the first frame update
    string screenshotPath = "";
    Texture2D cameraImage = null;

    int screenshotIndex = 0;
    void Start()
    {
        Debug.Log("Start VirtualCVCamera");
        cam = GetComponent<Camera>();
        cameraImage= new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        screenshotPath = Path.Combine(Application.dataPath, "..", "Screenshot");
        UnityEngine.Windows.Directory.CreateDirectory(screenshotPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            takeScreenshot();
        }
    }

    void takeScreenshot()
    {
        if (cam == null || cameraImage == null) return;

        RenderTexture rendText = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();

        cameraImage.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        cameraImage.Apply();
        RenderTexture.active = rendText;

        byte[] bytes = cameraImage.EncodeToJPG();

        screenshotIndex++;
        string screenshotFileName = Path.Combine(screenshotPath, string.Format("Screenshot_{0}.jpg", screenshotIndex));
        System.IO.File.WriteAllBytes(screenshotFileName, bytes);
    }
}
