﻿/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.IO;
using UnityEngine;
using SysDiagnostics = System.Diagnostics;

namespace VirtualCV
{
    public class PythonExecutor
    {
        private SysDiagnostics.Process pythonProcess = new SysDiagnostics.Process();

        private const string pythonExe = "python.exe";
        private const string pythonPath = "python";
        private readonly string pythonScriptPath = Path.Combine(Application.streamingAssetsPath, pythonPath);

        private static PythonExecutor executor = null;

        /// <summary>
        /// Get python executor instance
        /// </summary>
        /// <returns>Python Executor instance</returns>
        public static PythonExecutor getInstance()
        {
            return executor ?? (executor = new PythonExecutor());
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
            VirtualCVLog.Log("Execute python script : " + pythonScriptFile);

            string useStereo = VirtualCVSettings.GetParam().useStereoCamera ? "stereo" : "";

            pythonProcess.StartInfo.FileName = pythonExe;
            pythonProcess.StartInfo.WorkingDirectory = pythonScriptPath;
            pythonProcess.StartInfo.Arguments = $"{pythonScriptFile} {useStereo}";
            pythonProcess.StartInfo.UseShellExecute = false;
            pythonProcess.StartInfo.RedirectStandardOutput = true;
            pythonProcess.StartInfo.CreateNoWindow = true;

            pythonProcess.Start();
        }

        public void Terminate()
        {
            if (pythonProcess != null) pythonProcess.Close();
        }
    }
}