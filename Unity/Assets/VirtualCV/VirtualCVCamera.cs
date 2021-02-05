/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VirtualCV
{
    public class VirtualCVCamera : MonoBehaviour
    {
        private Camera cam = null;
        private Texture2D cameraImage = null;

        private int screenshotIndex = 0;
        private string screenshotPath = "";

        private FFMPEGExecutor ffmpegExecutor = null;

        void Start()
        {
            bool useStereo = VirtualCVSettings.GetParam().useStereoCamera;
            bool isRightCamera = (name == "VirtualCVCameraRight");
            if (isRightCamera && !useStereo)
            {
                return;
            }

            VirtualCVLog.Log("VirtualCVCamera starts");
            setCamera();
            setTexture();

            screenshotPath = Path.Combine(Application.dataPath, "..", "Screenshot");
            Directory.CreateDirectory(screenshotPath);

            int port = isRightCamera ? 9091 : 9090;
            ffmpegExecutor = new FFMPEGExecutor(port);
            ffmpegExecutor.Initialze();
            ffmpegExecutor.ExecuteFFMPEG();
        }

        void setCamera()
        {
            cam = GetComponent<Camera>();

            cam.fieldOfView = 60;
            cam.usePhysicalProperties = false;
            cam.focalLength = 50;
        }

        void setTexture()
        {
            // 640x480
            int width = VirtualCVSettings.GetParam().textureWidth;
            int height = VirtualCVSettings.GetParam().textureHeight;
            cam.targetTexture.texelSize.Set(width, height);
            cameraImage = new Texture2D(width, height, TextureFormat.RGB24, false);
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
            VirtualCVLog.Log($"Screenshot saved : path - {screenshotFileName}");

            File.WriteAllBytes(screenshotFileName, cameraImage.EncodeToJPG());
        }
    }
}