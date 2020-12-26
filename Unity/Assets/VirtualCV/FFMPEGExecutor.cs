using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FFMPEGExecutor
{
    // Start is called before the first frame update
    public void Initialze()
    {
        Debug.Log("Start FFMPEGExecutor");
        string ffmpegPath= Path.Combine(Application.streamingAssetsPath, "ffmpeg");
        Debug.Log("FFMPEG path : " + ffmpegPath);
    }

    // Update is called once per frame
    public void Update()
    {

    }
}
