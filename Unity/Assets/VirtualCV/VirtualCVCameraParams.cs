/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

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

        public string python_script;
    }
}
