using System;
using Pandor;

public class MainMenu : ScriptComponent
{
    public Text Title;
    Vector4 startColor;
    Vector4 endColor;
    float time = 0;
    string sceneName = "LevelScene";
    // Called once at the beginning of the Play Mode
    void OnCreate()
    {
        startColor = Vector4.one;
        endColor = new Vector4(0.775f, 0.224f, 0.224f, 1f);
    }

    // Called at each frame in Play Mode
    bool ping = false;
    void OnUpdate(float dt)
    {
        Title.color = LerpPingPong(startColor, endColor, time);
        if (time >= 1f)
        {
            ping = true;
        }
        else if (time <= 0)
        {
            ping = false;
        }
        time += ping ? -dt : dt;
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.QuitRequest();
    }



    private Vector4 Lerp(Vector4 color1, Vector4 color2, float t)
    {
        float r = LerpFloat(color1.x, color2.x, t);
        float g = LerpFloat(color1.y, color2.y, t);
        float b = LerpFloat(color1.z, color2.z, t);
        float a = LerpFloat(color1.w, color2.w, t);

        return new Vector4(r, g, b, a);
    }

    private float LerpFloat(float value1, float value2, float t)
    {
        return value1 + (value2 - value1) * t;
    }

    private Vector4 LerpPingPong(Vector4 color1, Vector4 color2, float t)
    {
        float pingPongT = PMath.PingPong(t, 1.0f);
        return Lerp(color1, color2, t);
    }
}
