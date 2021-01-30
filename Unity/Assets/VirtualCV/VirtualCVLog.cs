/*!
 * Copyright (c) 2021 fnwinter@gmail.com All rights reserved.
 * Licensed under the MIT License.
 */

using UnityEngine;

namespace VirtualCV
{
    class VirtualCVLog : Debug
    {
        internal static string TAG = "[VirtualCV] ";

        public static void Log(string msg)
        {
            Debug.Log($"{TAG} {msg}");
        }

        public static void LogE(string msg)
        {
            LogError($"{TAG} {msg}");
        }
    }
}