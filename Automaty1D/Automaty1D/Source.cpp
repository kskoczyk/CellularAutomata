// TODO: class Cell

#include <iostream>
#include <vector>
#include <bitset>

#include <SFML/Graphics.hpp>

#include <TGUI/TGUI.hpp>

#include "Rule.h"
#include "GUI.h"
#include "Board.h"

using namespace std;

int main()
{
	sf::RenderWindow window(sf::VideoMode(800, 600), "Automata 1D");
	tgui::Gui gui(window);
	GUI userInterface = GUI(gui, window);

	window.setFramerateLimit(60);
	window.setVerticalSyncEnabled(true);
	while (window.isOpen())
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				window.close();
				return 0;
			}		
			else if (event.type == sf::Event::Resized)
			{
				window.setView(sf::View(sf::FloatRect(0.f, 0.f, static_cast<float>(event.size.width), static_cast<float>(event.size.height))));
				gui.setView(window.getView());
				userInterface.prepareBoard(&window);
			}
			gui.handleEvent(event);
		}
		window.clear();
		gui.draw();
		window.draw(board::vertexBoard);
		window.display();
	}

	system("pause");
	return 0;
}


//vector<vector<int>> timeline;
//
//// SPACE
//cout << "Set the length: " << endl;
//int length;
//cin >> length;
//cout << "Set the time: " << endl;
//int t;
//cin >> t;
//
//timeline.resize(t);
//for (int i = 0; i < timeline.size(); i++)
//{
//	timeline[i].resize(length);
//}
//
//// NEIGHBORHOOD
//cout << "Set the initial values: " << endl;
//for (int j = 0; j < timeline[0].size(); j++)
//{
//	cin >> timeline[0][j];
//}
//
//cout << "Set the rule: " << endl;
//int rule;
//cin >> rule;
//
//// TRANSITION RULES
//for (int i = 1; i < timeline.size(); i++)
//{
//	enforceRule(timeline[i - 1], timeline[i], rule);
//}
//
//cout << endl;
//for (int i = 0; i < timeline.size(); i++)
//{
//	for (int j = 0; j < timeline[i].size(); j++)
//	{
//		cout << timeline[i][j] << "\t";
//	}
//	cout << endl;
//}