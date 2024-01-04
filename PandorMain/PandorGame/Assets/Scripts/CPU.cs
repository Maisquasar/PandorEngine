using System;
using Pandor;

public class CPU : ScriptComponent
{
    public GameObject Target;
    public float Speed = 5.0f;
    public float colliderSize = 5.0f;
    Player playerScript;

    void OnCreate()
    {
        var PlayerGameObject = FindObjectByName("Player");
        if (PlayerGameObject != null)
        {
            playerScript = PlayerGameObject.As<Player>();
        }
    }

    void OnUpdate(float dt)
    {
        if (!playerScript.hasSkull || !playerScript.hasRadio || !playerScript.hasAxe)
            return;
        else if (!gameObject.GetChild(0).active)
        {
            gameObject.GetChild(0).active = true;
        }
        Vector3 target = Target.transform.position;
        target.y -= 0f;

        Vector3 distance = target - gameObject.transform.position;

        if (distance.Length() < colliderSize)
        {
            SceneManager.LoadScene("GameOver");
            enable = false;
        }


        Vector3 direction = distance.GetNormalized();
        direction *= Speed;

        Vector3 translation = gameObject.transform.position;
        translation += direction * dt;
        gameObject.transform.position = translation;
    }
}

