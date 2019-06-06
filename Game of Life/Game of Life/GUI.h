#pragma once
#include <iostream>
#include <string>

#include <SFML/System/String.hpp>

#include <TGUI/TGUI.hpp>

#include "GameMap.h"
#include "VariableParameters.h"
#include "FileManagement.h"

using namespace std;

class GUI
{
	// TODO: move to fileManagement
	vector<filesystem::directory_entry> files; // TODO: get rid of static - use function returnin paths for example
	const string figuresPath = "./Figures";
	vector<string> getPaths();
	//

	const string guiPath = "./TGUI/Test.txt";
	sf::VertexArray vertexBoard;
	bool mouseOnBoard = false;
	bool mouseClicked = false;
	bool mouseReleased = false;
	sf::Vector2i cursorPos;
public:
	const double marginLeft = 0.725;
	const double marginTop = 0.882;

	GUI();
	GUI(tgui::Gui &gui);
	map<sf::String, tgui::Widget::Ptr> initGUI(tgui::Gui &gui, string path = ""); // TODO: change back to void?

	static void handlePlayButtonPressed(tgui::Gui *const gui);
	static void handlePauseButtonPressed(tgui::Gui *const gui);
	static void handleNextButtonPressed();
	static void handlePreviousButtonPressed();
	static void handleLoadButtonPressed(tgui::Gui *const gui);
	static void handleResizeButtonPressed(tgui::Gui *const gui);
	static void handleRandomizeButtonPressed();
	static void handleClearButtonPressed();
	static void handleSaveButtonPressed(tgui::Gui *const gui);

	void prepareBoard(sf::RenderWindow &window);
	void highlightCell(sf::RenderWindow &window);

	void setMouseOnBoard(bool state);
	void setMouseClicked(bool state);
	void setMouseReleased(bool state);
	void setCursorPos(sf::Vector2i pos);
	sf::VertexArray getVertexBoard();
};