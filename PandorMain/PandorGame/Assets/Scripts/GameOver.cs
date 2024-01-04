using System;
using Pandor;

public class GameOver : ScriptComponent
{
	string sceneName = "LevelScene";
	// Called once at the beginning of the Play Mode
	void OnCreate()
	{

	}

	// Called at each frame in Play Mode
	void OnUpdate(float dt)
	{

	}

	public void NewGame()
    {
		SceneManager.LoadScene(sceneName);

	}

	public void Quit()
    {
		Application.QuitRequest();
    }
}
