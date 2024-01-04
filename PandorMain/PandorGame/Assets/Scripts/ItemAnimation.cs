using System;
using Pandor;

public class ItemAnimation : ScriptComponent
{
	private float startValue = 0.05f;
	private float endValue = -0.05f;
	private float startHeight = -0.1f;
	private float endHeight = 0.0f;

	private const float duration = 0.4f; 
	private const float midDuration = duration * 0.5f;

	private Vector2 currentValue;
	private float timeElapsed = midDuration;

	private bool increasing = true;
	private bool movingUp = true;
	public bool playerMoving = false;

	void OnCreate()
	{
		Vector3 pos = gameObject.transform.localPosition;
		startValue += pos.x;
		endValue += pos.x;
		startHeight += pos.y;
		endHeight += pos.y;
	}

	void OnUpdate(float dt)
	{
		if (!playerMoving)
        {
			timeElapsed = PMath.Lerp(timeElapsed, midDuration, dt * 2 / midDuration);

			float ts = timeElapsed / duration;
			currentValue.x = increasing ? PMath.SmoothLerp(startValue, endValue, ts) : PMath.SmoothLerp(endValue, startValue, ts);
			currentValue.y = movingUp ? PMath.SmoothLerp(startHeight, endHeight, ts * 2) : PMath.SmoothLerp(endHeight, startHeight, (ts * 2) - 1);

			Vector3 position = gameObject.transform.localPosition;
			position.x = currentValue.x;
			position.y = currentValue.y;
			gameObject.transform.localPosition = position;

			return;
		}
			

		timeElapsed += dt;

		if (timeElapsed >= duration)
		{
			timeElapsed = 0f;
			increasing = !increasing;
			Random random = new Random();
			int randomSetp = random.Next(1, 5);
			AudioManager.PlaySoundByName($"Sounds/Step{randomSetp}.wav");
		}

		if (timeElapsed < midDuration)
			movingUp = true;
		else
			movingUp = false;

		float t = timeElapsed / duration;
		currentValue.x = increasing ? PMath.SmoothLerp(startValue, endValue, t) : PMath.SmoothLerp(endValue, startValue, t);
		currentValue.y = movingUp ? PMath.SmoothLerp(startHeight, endHeight, t * 2) : PMath.SmoothLerp(endHeight, startHeight, (t * 2) - 1);

		Vector3 postion = gameObject.transform.localPosition;
		postion.x = currentValue.x;
		postion.y = currentValue.y;
		gameObject.transform.localPosition = postion;
	}
}
