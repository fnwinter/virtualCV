/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using SysDiagnostics = System.Diagnostics;

public class PythonExecutor
{
    private SysDiagnostics.Process pythonProcess = new SysDiagnostics.Process();
    private string pythonScriptPath = Path.Combine(Application.streamingAssetsPath, "python");
    private string pythonScriptFile = "";

    private static PythonExecutor executor = null;

    /// <summary>
    /// Get python executor instance
    /// </summary>
    /// <returns></returns>
    public static PythonExecutor getInstance()
    {
        return (executor ?? new PythonExecutor());
    }

    /// <summary>
    /// Return python folder path
    /// </summary>
    public string GetPythonFolderPath()
    {
        return pythonScriptPath;
    }

    /// <summary>
    /// Execute python script
    /// </summary>
    /// <param name="scriptFile">python script file name, default is opencv.py</param>
    public void ExecutePython(string pythonScriptFile)
    {
        Debug.Log("[virtualCV] Execute python script");
        pythonProcess.StartInfo.FileName = "python.exe";
        pythonProcess.StartInfo.WorkingDirectory = pythonScriptPath;
        pythonProcess.StartInfo.Arguments = pythonScriptFile;
        pythonProcess.StartInfo.UseShellExecute = false;
        pythonProcess.StartInfo.RedirectStandardOutput = true;
        pythonProcess.StartInfo.CreateNoWindow = true;

        pythonProcess.Start();
    }
}
