// TODO:
// keep file streams open and global throughout the whole execution of the program
// FPS limiter
// history as plain text
// user changes affecting continuity of history
// saveHistory() function that uses config.rleHistory and config.plainHistory
// move some functions to fileManagement
//?? handle non-existing config file - pretty much handled I'd say, needs testing
//done? handle non-existing history/map file
//?? non-square map? X
//?? shared pointers for map

//# xout_of_range string - located and isolated, still not fully fixed

#include <iostream>
#include <vector>

#include <boost/thread.hpp>

#include <SFML/Graphics.hpp>

#include <TGUI/TGUI.hpp>

#include "Cell.h"
#include "GameMap.h"
#include "UI.h"
#include "Config.h"
#include "FileManagement.h"
#include "GUI.h"
#include "VariableParameters.h"

using namespace std;

int main()
{
	sf::RenderWindow window(sf::VideoMode(800, 600), "TGUI window test");
	tgui::Gui gui(window);
	GUI userInterface = GUI(gui);

	sf::Vector2i cursorPos;
	while (window.isOpen())
	{
		window.clear();
		// TODO: function to handle manual user changes
		if (vp::initialized && !vp::paused)
		{
			gameMap::iterate();
			//gameMap::print(false);
		}
		userInterface.prepareBoard(window);
		window.draw(userInterface.getVertexBoard());

		// TODO: event handling function
		sf::Event event;
		if (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				window.close();
				vp::saveFile.close();
			}
			else if (event.type == sf::Event::Resized)
			{
				window.setView(sf::View(sf::FloatRect(0.f, 0.f, static_cast<float>(event.size.width), static_cast<float>(event.size.height))));
				gui.setView(window.getView());
				userInterface.prepareBoard(window);
			}
			else if (event.type == sf::Event::MouseMoved)
			{
				// TODO: cellSize global?
				// function for drawing outside the event
				// don't draw when not paused
				cursorPos = sf::Mouse::getPosition(window);
				userInterface.setCursorPos(cursorPos);
				if ((cursorPos.x < userInterface.marginLeft * window.getSize().x) && (cursorPos.y < userInterface.marginTop * window.getSize().y))
				{
					userInterface.setMouseOnBoard(true);
				}
				else userInterface.setMouseOnBoard(false);
			}
			else if (event.type == sf::Event::MouseButtonPressed)
			{
				if (event.mouseButton.button == sf::Mouse::Left)
				{
					userInterface.setMouseClicked(true);
					userInterface.setMouseReleased(false);
				}
			}
			else if (event.type == sf::Event::MouseLeft)
			{
				userInterface.setMouseOnBoard(false);
			}
			else if (event.type == sf::Event::MouseButtonReleased)
			{
				if (event.mouseButton.button == sf::Mouse::Left)
				{
					userInterface.setMouseClicked(false);
					userInterface.setMouseReleased(true);
					// TODO: another gui function to handle release more efficiently
				}
			}

			gui.handleEvent(event); // Pass the event to all the widgets
		}

		userInterface.highlightCell(window);
		userInterface.setMouseReleased(false);
		gui.draw();
		window.display();
	}

	// TODO: game::run()

	/*boost::thread t1([]{});
	for ( ; ; )
	{
		//gameMap::print(vp::config.isHistory());
		gameMap::iterate(vp::config);

		UI::waitForInput(t1, boost::chrono::milliseconds(300), vp::config);
	}*/

	system("pause");
	return 0;
}