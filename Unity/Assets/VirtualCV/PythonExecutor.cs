using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using SysDiagnostics = System.Diagnostics;

public class PythonExecutor
{
    private SysDiagnostics.Process pythonProcess = new SysDiagnostics.Process();
    private string pythonScriptPath = "";
    private string pythonScriptFile = "";

    public void Initialze(string scriptFile = "opencv.py")
    {
        pythonScriptPath = Path.Combine(Application.streamingAssetsPath, "python");
        Debug.Log("Python script path : " + pythonScriptPath);
        pythonScriptFile = scriptFile;
    }

    public void ExecutePython()
    {
        Debug.Log("Execute python script");
        pythonProcess.StartInfo.FileName = "python.exe";
        pythonProcess.StartInfo.WorkingDirectory = pythonScriptPath;
        pythonProcess.StartInfo.Arguments = "opencv.py";
        pythonProcess.StartInfo.UseShellExecute = false;
        pythonProcess.StartInfo.RedirectStandardOutput = true;
        pythonProcess.StartInfo.CreateNoWindow = true;

        pythonProcess.Start();
    }
}
