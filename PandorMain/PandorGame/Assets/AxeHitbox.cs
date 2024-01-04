using System;
using Pandor;

public class AxeHitbox : ScriptComponent
{
	Player player;
	AxeSlice axe;
	void OnCreate()
	{
		var PlayerGameObject = FindObjectByName("Player");
		if (PlayerGameObject != null)
		{
			player = PlayerGameObject.As<Player>();
		}

		var AxeGameObject = FindObjectByName("Axe");
		if (AxeGameObject != null)
		{
			axe = AxeGameObject.As<AxeSlice>();
		}
	}
	public override void OnTriggerStay(Collider collider)
	{
		if (player.hasAxe && axe.hitActive)
        {
			if (collider.gameObject.name == "Destructable")
			{
				AudioManager.PlaySoundByName("Sounds/Dive.wav");
				collider.gameObject.transform.position = new Vector3(0, -1000, 0);
				enable = false;
			}
		}
	}
}
