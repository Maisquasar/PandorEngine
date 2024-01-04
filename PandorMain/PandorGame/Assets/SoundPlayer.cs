using System;
using Pandor;

public class SoundPlayer : ScriptComponent
{
    void OnUpdate(float dt)
    {
        if (Input.IsKeyPressed(Key.Key_K))
            AudioManager.PlaySoundByName("Sounds/What.wav");
    }
}
