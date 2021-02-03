/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace VirtualCV
{
    public class VirtualCVDialog : EditorWindow
    {
        private static string[] pythonFiles = GetPythonScripts();

        static private VirtualCVCameraParams param = VirtualCVSettings.LoadSettings();

        [MenuItem("virtualCV/Setup")]
        static void Init()
        {
            param = VirtualCVSettings.LoadSettings();

            VirtualCVDialog window = (VirtualCVDialog)EditorWindow.GetWindow(typeof(VirtualCVDialog));
            window.titleContent.text = "virtualCV";
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Camera settings", EditorStyles.boldLabel);

            param.usePhysicalCamera = EditorGUILayout.Toggle("Use physical camera", param.usePhysicalCamera);
            param.useDepthCameara = EditorGUILayout.Toggle("Use depth camera", param.useDepthCameara);

            EditorGUILayout.Space();

            param.textureWidth = EditorGUILayout.IntField("Texture width", param.textureWidth);
            param.textureHeight = EditorGUILayout.IntField("Texture height", param.textureHeight);

            EditorGUILayout.Space();

            param.fov = EditorGUILayout.IntField("FOV", param.fov);
            param.fps = EditorGUILayout.IntField("FPS", param.fps);
            param.focal_length = EditorGUILayout.FloatField("Focal length", param.focal_length);

            EditorGUILayout.Space();

            param.useStereoCamera = EditorGUILayout.Toggle("Use stereo camera", param.useStereoCamera);
            param.ipd = EditorGUILayout.FloatField("Interpupillary distance", param.ipd);

            EditorGUILayout.Space();

            if (GUILayout.Button("Save settings"))
            {
                VirtualCVLog.Log("Settings saved");
                VirtualCVSettings.SaveSettings(param);
            }
            if (GUILayout.Button("Apply to camera"))
            {
                ApplyCamera();
            }

            EditorGUILayout.Space();

            int selectedPythonScript = GetPythonScriptIndex(param.python_script);
            selectedPythonScript = EditorGUILayout.Popup("Python script", selectedPythonScript, pythonFiles);
            param.python_script = pythonFiles[selectedPythonScript];
            if (GUILayout.Button("Launch the script"))
            {
                PythonExecutor.getInstance().ExecutePython(param.python_script);
            }
        }

        /// <summary>
        /// Attach virtualCV camera to Unity main camera
        /// </summary>
        private void ApplyCamera()
        {
            GameObject prefabVirtualCV = Resources.Load("VirtualCVRig") as GameObject;
            GameObject virtualCVRig = Instantiate(prefabVirtualCV);
            virtualCVRig.transform.SetParent(Camera.main.transform);
            virtualCVRig.transform.localPosition = Vector3.zero;
            virtualCVRig.transform.localRotation = Quaternion.identity;
            virtualCVRig.transform.localScale = Vector3.one;
            virtualCVRig.name = "VirtualCVRig";
        }

        #region Python script
        /// <summary>
        /// Get python script names
        /// </summary>
        /// <returns>python scripts in StreamingAssets</returns>
        private static string[] GetPythonScripts()
        {
            string pythonFolderPath = PythonExecutor.getInstance().GetPythonFolderPath();

            string[] pythonFiles = Directory.GetFiles(pythonFolderPath);

            List<string> pythonFileList = new List<string>();
            foreach (string fileName in pythonFiles)
            {
                if (fileName.EndsWith(".py"))
                {
                    string onlyFileName = Path.GetFileName(fileName);
                    pythonFileList.Add(onlyFileName);
                }
            }

            return pythonFileList.ToArray();
        }

        private static int GetPythonScriptIndex(string script)
        {
            for(int i = 0; i < pythonFiles.Length; i++)
            {
                if (pythonFiles[i] == script) return i;
            }
            return 0;
        }
        #endregion
    }
}