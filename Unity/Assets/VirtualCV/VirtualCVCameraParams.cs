using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCV
{
    public class VirtualCVCameraParams
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
}
