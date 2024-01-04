using System;
using Pandor;


class FanBoy : ScriptComponent
{
    public GameObject player;

    void OnUpdate(float dt)
    {
        if(player == null)
        {
            Console.WriteLine($"HUUUHHHHHHHHH?????");
        }
        gameObject.transform.rotation = Quaternion.LookRotation(player.transform.position - gameObject.transform.position, Vector3.up);

    }

    public void Test()
    {
        Console.WriteLine($"HUUUHHHHHHHHH?????");
    }
}

