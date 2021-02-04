using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualCV
{
    public class virtualCVRig : MonoBehaviour
    {
        GameObject left = null;
        GameObject right = null;

        Camera leftCamera = null;
        Camera rightCamera = null;

        private VirtualCVWebSocket socket = null;

        // Start is called before the first frame update
        void Start()
        {
            socket = new VirtualCVWebSocket();
            socket.Initialize();

            left = this.gameObject.transform.GetChild(0).gameObject;
            leftCamera = left.GetComponent<Camera>();

            if (VirtualCVSettings.GetParam().useStereoCamera)
            {
                right = this.gameObject.transform.GetChild(1).gameObject;
                rightCamera = right.GetComponent<Camera>();
            }
            else
            {

            }

            string pythonScript = VirtualCVSettings.GetParam().python_script;
            PythonExecutor.getInstance().ExecutePython(pythonScript);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}