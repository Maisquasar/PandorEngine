using System;
using Pandor;

public class AxeSlice : ScriptComponent
{
	bool canSlice = true;

	float firstRot = -15;
	float secondRot = 80;
	float thirdRot = 0;

	readonly float duration1 = 0.2f;
	readonly float duration2 = 0.15f;
	readonly float duration3 = 0.4f;

	float currentTime = 0;
	int stepsDone = 0;

	public bool hitActive = false;

    Player player;
    // Called once at the beginning of the Play Mode
    void OnCreate()
    {
		var PlayerGameObject = FindObjectByName("Player");
        if (PlayerGameObject != null)
        {
            player = PlayerGameObject.As<Player>();
        }
    }

    // Called at each frame in Play Mode
    void OnUpdate(float dt)
    {
        if (player == null || !player.hasAxe || Application.timeScale <= 0)
            return;
        if (canSlice)
        {
			if (Input.IsKeyPressed(Key.Key_MouseLeft))
				canSlice = false;
		}
		else
        {
			currentTime += dt;
			if (stepsDone == 0)
            {
				if(currentTime >= duration1)
                {
					transform.localRotation = Quaternion.Euler(firstRot, 0, 0);
					stepsDone++;
					currentTime = 0;
					return;
				}
				transform.localRotation = Quaternion.Euler(PMath.SmoothLerp(thirdRot, firstRot, currentTime / duration1), 0, 0);
			}
			else if (stepsDone == 1)
			{
				if (currentTime >= duration2)
				{
					transform.localRotation = Quaternion.Euler(secondRot, 0, 0);
					stepsDone++;
					currentTime = 0;
					return;
				}
				if (!hitActive && currentTime >= duration2 * 0.75f)
                {
					AudioManager.PlaySoundByName("Sounds/Attack.wav");
					hitActive = true;
				}
				transform.localRotation = Quaternion.Euler(PMath.EaseInLerp(firstRot, secondRot, currentTime / duration2), 0, 0);
			}
			else
			{
				if (currentTime >= duration3)
				{
					transform.localRotation = Quaternion.Euler(thirdRot, 0, 0);
					currentTime = 0;
					stepsDone = 0;
					canSlice = true;
					return;
				}
				if (hitActive && currentTime >= duration3 * 0.25f)
					hitActive = false;
				transform.localRotation = Quaternion.Euler(PMath.EaseInLerp(secondRot, thirdRot, currentTime / duration3), 0, 0);
			}
        }
	}

	public void SetRot()
    {
		float rot = transform.localEulerAngles.x;
		firstRot += rot;
		secondRot += rot;
		thirdRot += rot;
	}
}
