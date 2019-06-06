#include "UI.h"

namespace UI
{
	string command;
}

void UI::waitForInput(boost::thread &t1, boost::chrono::milliseconds delay, Config &config)
{
	if (t1.try_join_for(delay))
	{
		if (command == "pause")
		{
			cout << "Paused. Awaiting input. . ." << endl;
			cin >> command; //effective stop
		}

		if (command == "cylindrical")
		{
			config.setCylindrical(!config.isCylindrical());
			cout << "Toggled Cylindrical to: " << config.isCylindrical() << endl;
			Sleep(500);
		}
		else if (command == "topbottom")
		{
			config.setTopBottom(!config.isTopBottom());
			cout << "Toggled TopBottom to: " << config.isTopBottom() << endl;
			Sleep(500);
		}
		else if (command == "enclosed")
		{
			config.setCylindrical(false);
			config.setTopBottom(false);
			cout << "Cylindrical and TopBottom set to false" << endl;
			Sleep(500);
		}
		else if (command == "infinite")
		{
			config.setCylindrical(true);
			config.setTopBottom(true);
			cout << "Cylindrical and TopBottom set to true" << endl;
			Sleep(500);
		}
		else if (command == "history")
		{
			config.setHistory(!config.isHistory());
			cout << "Toggled History to: " << config.isHistory() << endl;
			Sleep(500);
		}
		else if (command == "load")
		{
			config.load();
			cout << "Loaded the latest config file" << endl;
		}
		else if (command == "save")
		{
			config.save();
			cout << "Saved the latest configuration" << endl;
		}
		else if (command == "newmap")
		{
			gameMap::create();
			Sleep(500);
		}
		else if (command == "" || command == "continue") 
		{
			// do nothing
		} 
		else
		{
			cout << command << ": " << "Unknown command" << endl;
			Sleep(500);
		}

		t1 = boost::thread([] {cin >> command; });
	}
}
