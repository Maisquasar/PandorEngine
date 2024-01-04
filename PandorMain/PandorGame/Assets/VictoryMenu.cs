using System;
using Pandor;

public class VictoryMenu : ScriptComponent
{
	// Called once at the beginning of the Play Mode
	void OnCreate()
	{

	}

	// Called at each frame in Play Mode
	void OnUpdate(float dt)
	{

	}

	public void MainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }

	public void Quit()
    {
		Application.QuitRequest();
    }
}
