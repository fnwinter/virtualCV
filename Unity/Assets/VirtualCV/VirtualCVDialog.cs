/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


public struct VirtualCVParams
{
    public bool usePhysicalCamera;
    public bool useDepthCameara;
    public bool useStereoCamera;

    public int textureWidth;
    public int textureHeight;
    public int fov;
    public int fps;
    public float focal_length;
    public float ipd;
}

public class VirtualCVDialog : EditorWindow
{
    private VirtualCVParams param = (new VirtualCVSettings()).LoadSettings();

    private static string[] pythonFiles = GetPythonScripts();

    // Add menu named "My Window" to the Window menu
    [MenuItem("virtualCV/Setup")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        VirtualCVDialog window = (VirtualCVDialog)EditorWindow.GetWindow(typeof(VirtualCVDialog));

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
            Debug.Log("[Saved]");
            new VirtualCVSettings().SaveSettings(param);
        }
        GUILayout.Button("Apply to camera");

        EditorGUILayout.Space();

        int selected = 0;
        selected = EditorGUILayout.Popup("Python script", selected, pythonFiles);
        if (GUILayout.Button("Launch the script"))
        {
            string fileName = pythonFiles[selected];
            PythonExecutor.getInstance().ExecutePython(fileName);
        }
    }

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
}