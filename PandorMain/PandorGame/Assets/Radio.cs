using System;
using Pandor;

public class Radio : ScriptComponent
{
	public SoundEmitter soundRef;
    Player player;
    public Text pickUpText;

    // Called once at the beginning of the Play Mode
    void OnCreate()
	{
		soundRef.Play();
        var PlayerGameObject = FindObjectByName("Player");
        if (PlayerGameObject != null)
        {
            player = PlayerGameObject.As<Player>();
        }
        pickUpText.enable = false;
    }

	// Called at each frame in Play Mode
	void OnUpdate(float dt)
	{
	}

    public override void OnTriggerEnter(Collider collider)
    {
        if (player.hasRadio)
            return;
        if (collider.gameObject.name == "Collider")
        {
            pickUpText.enable = true;
        }
    }


    public override void OnTriggerStay(Collider collider)
    {
        if (player.hasRadio)
            return;
        if (collider.gameObject.name == "Collider" && Input.IsKeyPressed(Key.Key_E))
        {
            AudioManager.PlaySoundByName("Sounds/FindSomething.wav");
            var radio = this;
            player.PickupRadio(ref radio);
            pickUpText.enable = false;
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (player.hasRadio)
            return;
        if (collider.gameObject.name == "Collider")
        {
            pickUpText.enable = false;
        }
    }
}
