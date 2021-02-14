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
        private FFMPEGExecutor ffmpegExecutor = null;

        private int screenshotIndex = 0;
        private string screenshotPath = "";

        private const int leftCameraPort = 9090;
        private const int rightCameraPort = 9091;

        private int textureWidth = 640;
        private int textureHeight = 480;

        private bool isRightCamera = false;

        void Start()
        {
            VirtualCVLog.Log("VirtualCVCamera starts");

            bool useStereo = VirtualCVSettings.GetParam().useStereoCamera;
            isRightCamera = (name == "VirtualCVCameraRight");
            if (isRightCamera && !useStereo)
            {
                return;
            }

            SetCamera();
            SetTexture();

            screenshotPath = Path.Combine(Application.dataPath, "..", "Screenshot");
            Directory.CreateDirectory(screenshotPath);

            int port = isRightCamera ? rightCameraPort : leftCameraPort;
            ffmpegExecutor = new FFMPEGExecutor(port);
            ffmpegExecutor.Initialze();
            ffmpegExecutor.ExecuteFFMPEG();
        }

        void SetCamera()
        {
            cam = GetComponent<Camera>();

            cam.fieldOfView = VirtualCVSettings.GetParam().fov;
            cam.usePhysicalProperties = VirtualCVSettings.GetParam().usePhysicalCamera;
            cam.focalLength = VirtualCVSettings.GetParam().focal_length;

            VirtualCVLog.Log($"Camera info : {name} - {cam.fieldOfView} - {cam.usePhysicalProperties} - {cam.focalLength}");
        }

        void SetTexture()
        {
            // 640x480
            textureWidth = VirtualCVSettings.GetParam().textureWidth;
            textureHeight = VirtualCVSettings.GetParam().textureHeight;
            if (textureWidth > 0 && textureHeight > 0)
            {
                cam.targetTexture.texelSize.Set(textureWidth, textureHeight);
                cameraImage = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB24, false);
            }
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
            if (cam == null || cameraImage == null || textureWidth == 0 || textureHeight == 0) return;

            RenderTexture rendText = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;
            cam.Render();

            cameraImage.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
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
            string isRight = (isRightCamera ? "right" : "left");
            string screenshotFileName = Path.Combine(screenshotPath, string.Format($"Screenshot_{isRight}_{screenshotIndex}.jpg"));
            VirtualCVLog.Log($"Screenshot saved : path - {screenshotFileName}");

            File.WriteAllBytes(screenshotFileName, cameraImage.EncodeToJPG());
        }
    }
}