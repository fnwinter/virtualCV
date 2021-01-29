/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using System.IO;
using UnityEngine;

public class VirtualCVSettings
{
    internal string settingFilePath = Path.Combine(Application.streamingAssetsPath, "virtualCVSetting.ini");

    public VirtualCVParams LoadSettings()
    {
        VirtualCVParams param = new VirtualCVParams();

        return param;
    }

    public void SaveSettings(VirtualCVParams param)
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
        }
    }
}
