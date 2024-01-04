#include <iostream>
#include <Core/App.h>
#include <Core/ProjectManager.h>

//#define _CRTDBG_MAP_ALLOC
//#include <crtdbg.h>
#include <stdlib.h>

#include <Math/Maths.h>

using namespace Core;

void Main()
{
	ProjectManager::Create();
	std::string projectPath = ProjectManager::Get().Update();
	ProjectManager::Get().Clear();

	if (projectPath.empty())
		return;

	App::CreateInstance();

	AppInit init =
	{
		1600, 900,
		4,5,
		"Pandor",
		true,
	};

	App::Get().Init(init, projectPath);
	App::Get().Update();
	App::Get().Clear();
}

int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, char*, int nShowCmd)
{
	Main();
}


int main()
{
	//_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
	//  TODO: Remove Comments To Break on leaks
	// |
	// V
	//_CrtSetBreakAlloc(1381);
	Main();
	return 0;
}
