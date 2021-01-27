/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class VirtualCVSetUp : EditorWindow
{
    bool usePhysicalCamera = false;
    bool useDepthCameara = false;
    bool useStereoCamera = false;

    int textureWidth = 640;
    int textureHeight = 480;
    int fov = 90;
    int fps = 30;
    float focal_length = 10.0f;
    float ipd = 0.1f;

    private static string[] pythonFiles = VirtualCVSetUp.GetPythonScripts();

    // Add menu named "My Window" to the Window menu
    [MenuItem("virtualCV/Setup")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        VirtualCVSetUp window = (VirtualCVSetUp)EditorWindow.GetWindow(typeof(VirtualCVSetUp));

        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Camera settings", EditorStyles.boldLabel);

        usePhysicalCamera = EditorGUILayout.Toggle("Use physical camera", usePhysicalCamera);
        useDepthCameara = EditorGUILayout.Toggle("Use depth camera", useDepthCameara);

        EditorGUILayout.Space();

        textureWidth = EditorGUILayout.IntField("Texture width", textureWidth);
        textureHeight = EditorGUILayout.IntField("Texture height", textureHeight);

        EditorGUILayout.Space();

        fov = EditorGUILayout.IntField("FOV", fov);
        fps = EditorGUILayout.IntField("FPS", fps);
        focal_length = EditorGUILayout.FloatField("Focal length", focal_length);

        EditorGUILayout.Space();

        useStereoCamera = EditorGUILayout.Toggle("Use stereo camera", useStereoCamera);
        ipd = EditorGUILayout.FloatField("Interpupillary distance", ipd);

        EditorGUILayout.Space();

        GUILayout.Button("Save settings");
        GUILayout.Button("Apply to camera");

        EditorGUILayout.Space();

        int selected = 0;
        selected = EditorGUILayout.Popup("Python script", selected, pythonFiles);
        GUILayout.Button("Launch the script");
    }

    /// <summary>
    /// Get python script names
    /// </summary>
    /// <returns></returns>
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
}