/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
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
        string[] options = new string[]
        {
            "Option1", "Option2", "Option3",
        };
        selected = EditorGUILayout.Popup("Python script", selected, options);
        GUILayout.Button("Launch the script");
    }
}