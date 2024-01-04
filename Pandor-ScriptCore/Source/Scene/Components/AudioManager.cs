using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class AudioManager : BaseComponent
    {
        public static void PlaySoundByName(string soundName)
        {
            InternalCalls.AudioManager_PlaySoundByName(soundName);
        }
    }
}
