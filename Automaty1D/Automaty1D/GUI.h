#pragma once
#include <iostream>
#include <string>

#include <SFML/System/String.hpp>

#include <TGUI/TGUI.hpp>

#include "Board.h"
#include "Rule.h"

using namespace std;

class GUI
{
	const string guiPath = "./TGUI/Automata";
	map<sf::String, tgui::Widget::Ptr> widgetMap;
	tgui::Gui *gui;

	shared_ptr <tgui::EditBox> editBoxLength;
	shared_ptr <tgui::EditBox> editBoxPeriods;
	shared_ptr <tgui::EditBox> editBoxRule;
	shared_ptr <tgui::EditBox> editBoxInput;
public:
	GUI();
	GUI(tgui::Gui &gui, sf::RenderWindow &window);
	void initGUI(tgui::Gui &gui, sf::RenderWindow &window, string path = "");

	static void handleGoButtonPressed(tgui::Gui *const gui, GUI *const GUIclass, sf::RenderWindow *const window);
	void prepareBoard(sf::RenderWindow *const window);
};