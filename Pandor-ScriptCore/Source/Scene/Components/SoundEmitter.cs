using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class SoundEmitter : BaseComponent 
    {
        public void Play()
        {
            InternalCalls.SoundEmitter_Play(gameObject.ID, ID);
        }
        public void Stop()
        {
            InternalCalls.SoundEmitter_Stop(gameObject.ID, ID);
        }
        public void Pause()
        {
            InternalCalls.SoundEmitter_Pause(gameObject.ID, ID);
        }
        public void FadeIn(int millisecondsFading)
        {
            InternalCalls.SoundEmitter_FadeIn(gameObject.ID, ID, millisecondsFading);
        }
        public void SetLooping(bool value)
        {
            InternalCalls.SoundEmitter_SetLooping(gameObject.ID, ID, value);
        }
    }
}