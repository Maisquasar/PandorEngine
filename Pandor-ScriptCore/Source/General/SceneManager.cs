using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class SceneManager
    {
        public static void LoadScene(string name)
        {
            if (!InternalCalls.SceneManager_LoadScene(name))
                Debug.PrintError($"Could not load th scene: {name}");
        }
    }
}
