/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System;
using System.IO;
using UnityEngine;

namespace VirtualCV
{
    public class VirtualCVSettings
    {
        const string settingFileName = "virtualCVSetting.ini";

        private static string settingFilePath = Path.Combine(Application.streamingAssetsPath, settingFileName);

        private static VirtualCVCameraParams param = new VirtualCVCameraParams();

        public static VirtualCVCameraParams GetParam()
        {
            return param;
        }

        public static VirtualCVCameraParams LoadSettings()
        {            
            try
            {
                string line;
                using (var sr = new StreamReader(settingFilePath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("usePhysicalCamera")) param.usePhysicalCamera = GetValue<bool>(line);
                        if (line.Contains("useDepthCameara")) param.useDepthCameara = GetValue<bool>(line);
                        if (line.Contains("useStereoCamera")) param.useStereoCamera = GetValue<bool>(line);

                        if (line.Contains("textureWidth")) param.textureWidth = GetValue<int>(line);
                        if (line.Contains("textureHeight")) param.textureHeight = GetValue<int>(line);

                        if (line.Contains("fov")) param.fov = GetValue<int>(line);
                        if (line.Contains("fps")) param.fps = GetValue<int>(line);
                        if (line.Contains("focal_length")) param.focal_length = GetValue<float>(line);
                        if (line.Contains("ipd")) param.ipd = GetValue<float>(line);

                        if (line.Contains("python_script")) param.python_script = GetValue<string>(line);
                    }
                }
            }
            catch (Exception e)
            {
                VirtualCVLog.LogE($"fail to read settings file : {e}");

                // return default parameters
                param.usePhysicalCamera = false;
                param.useDepthCameara = false;
                param.useStereoCamera = false;
                param.textureWidth = 640;
                param.textureHeight = 480;
                param.fov = 60;
                param.fps = 20;
                param.focal_length = 50;
                param.ipd = 1.0f;
                param.python_script = "opencv.py";
            }

            return param;
        }

        public static void SaveSettings(VirtualCVCameraParams _param)
        {
            param = _param;

            try
            {
                using (StreamWriter outputFile = new StreamWriter(settingFilePath))
                {
                    outputFile.WriteLine($"usePhysicalCamera={param.usePhysicalCamera}");
                    outputFile.WriteLine($"useDepthCameara={param.useDepthCameara}");
                    outputFile.WriteLine($"useStereoCamera={param.useStereoCamera}");
                    outputFile.WriteLine($"textureWidth={param.textureWidth}");
                    outputFile.WriteLine($"textureHeight={param.textureHeight}");
                    outputFile.WriteLine($"fov={param.fov}");
                    outputFile.WriteLine($"fps={param.fps}");
                    outputFile.WriteLine($"focal_length={param.focal_length}");
                    outputFile.WriteLine($"ipd={param.ipd}");
                    outputFile.WriteLine($"python_script={param.python_script}");
                }
            }
            catch(Exception e)
            {
                VirtualCVLog.LogE($"fail to save settings : {e}");
            }
        }

        /// <summary>
        /// read value from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="line">string from setting file</param>
        /// <returns>value</returns>
        private static T GetValue<T>(string line)
        {
            int delimeter = line.IndexOf('=');
            string value = line.Substring(delimeter + 1);

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
