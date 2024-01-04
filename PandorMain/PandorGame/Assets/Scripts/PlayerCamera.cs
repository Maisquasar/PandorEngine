using System;
using Pandor;

public class PlayerCamera : ScriptComponent
{
    public GameObject player;
    public float distance = 10;
    public float height = 2;

    // Called at each frame in Play Mode
    void OnUpdate(float dt)
    {
        if (player == null || Application.timeScale <= 0)
            return;

        Vector3 playerForward = player.transform.forward;
        playerForward = new Vector3(playerForward.x, 0, playerForward.z).GetNormalized();
        Vector3 pos = player.transform.position - playerForward * distance + Vector3.up * height;
        gameObject.transform.position = pos;

        gameObject.transform.rotation = Quaternion.LookRotation(playerForward * distance, Vector3.up);
    }
}
