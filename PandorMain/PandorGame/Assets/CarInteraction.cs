using System;
using Pandor;

public class CarInteraction : ScriptComponent
{
    Player player;
    GameObject camera;
    bool playerInCar = false;

    public float distance = 10.0f;
    public float height = 2.5f;
    public float transitionDuration = 0.5f;
    float ts = 0;

    void OnCreate()
    {
        var PlayerGameObject = FindObjectByName("Player");
        if (PlayerGameObject != null)
        {
            player = PlayerGameObject.As<Player>();
        }
    }
    void OnUpdate(float dt)
    {
        if(playerInCar && camera != null)
        {
            Vector3 playerForward = transform.forward;
            playerForward = new Vector3(playerForward.x, 0, playerForward.z).GetNormalized();

            // Transition
            if (ts < transitionDuration)
            {
                ts += dt;

                Vector3 currentPos = camera.transform.position;
                Vector3 targetPos = transform.position - playerForward * distance + Vector3.up * height;
                camera.transform.position = PMath.SmoothLerp(currentPos, targetPos, ts);

                Quaternion currentRot = camera.transform.rotation;
                Quaternion targetRot = Quaternion.LookRotation(playerForward, Vector3.up);
                camera.transform.rotation = Quaternion.SLerp(currentRot, targetRot, ts);
            }
            // Set Camera Transfrom
            else
            {
                camera.transform.position = transform.position - playerForward * distance + Vector3.up * height;
                camera.transform.rotation = Quaternion.LookRotation(playerForward, Vector3.up);
            }
            
        }
    }
    public override void OnTriggerStay(Collider collider)
    {
        if (playerInCar)
            return;
        if (collider.gameObject.name == "Collider" && Input.IsKeyPressed(Key.Key_E))
        {
            camera = player.TakeCar(ref gameObject);
            playerInCar = true;
        }
    }
}
