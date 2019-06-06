#include "GUI.h"

GUI::GUI()
{

}

GUI::GUI(tgui::Gui &gui, sf::RenderWindow &window)
{
	initGUI(gui, window);
}

void GUI::initGUI(tgui::Gui &gui, sf::RenderWindow &window, string path)
{
	if (path == "") path = guiPath;

	gui.loadWidgetsFromFile(path);
	auto widgets = gui.getWidgets();
	auto widgetNames = gui.getWidgetNames();

	for (int i = 0; i < widgets.size(); i++)
	{
		widgetMap.insert(make_pair(widgetNames[i], widgets[i]));
	}

	gui.get("ButtonGo")->cast<tgui::Button>()->connect("Pressed", GUI::handleGoButtonPressed, &gui, this, &window);
	editBoxLength = gui.get("EditBoxLength")->cast<tgui::EditBox>();
	editBoxPeriods = gui.get("EditBoxPeriods")->cast<tgui::EditBox>();
	editBoxRule = gui.get("EditBoxRule")->cast<tgui::EditBox>();
	editBoxInput = gui.get("EditBoxInput")->cast<tgui::EditBox>();
}

void GUI::handleGoButtonPressed(tgui::Gui *const gui, GUI *const GUIclass, sf::RenderWindow *const window)
{
	// TODO: calculate function
	// SPACE
	board::timeline.clear();
	board::vertexBoard.clear();

	int length;
	if (GUIclass->editBoxLength->getText().toAnsiString() == "") length = 1;
	else length = stoi(GUIclass->editBoxLength->getText().toAnsiString());

	int t;
	if (GUIclass->editBoxPeriods->getText().toAnsiString() == "") t = 1;
	else t = stoi(GUIclass->editBoxPeriods->getText().toAnsiString());

	int rule;
	if (GUIclass->editBoxRule->getText().toAnsiString() == "") rule = 90;
	else rule = stoi(GUIclass->editBoxRule->getText().toAnsiString());

	board::timeline.resize(t);
	for (int i = 0; i < board::timeline.size(); i++)
	{
		board::timeline[i].resize(length, 0);
	}

	// NEIGHBORHOOD
	string userInput = GUIclass->editBoxInput->getText().toAnsiString();
	if (userInput == "" || userInput.size() < length)
	{
		board::timeline[0][length / 2] = 1;
	}
	else
	{
		for (int i = 0; i < length; i++)
		{
			board::timeline[0][i] = static_cast<int>(userInput[i] - '0');
		}
	}

	// TRANSITION RULES
	for (int i = 1; i < board::timeline.size(); i++)
	{
		enforceRule(board::timeline[i - 1], board::timeline[i], rule);
	}

	// TODO: draw function
	// DRAW
	board::drawn = true;
	GUIclass->prepareBoard(window);
}


void GUI::prepareBoard(sf::RenderWindow *const window)
{
	if (!board::drawn) return; // nothing to draw

	int length = board::timeline[0].size();
	int height = board::timeline.size();
	board::vertexBoard.resize(height * length * 4);

	const double marginLeft = 0.725;
	const double marginTop = 0.882; // TODO: make GUIclass global
	double xMax = window->getSize().x * marginLeft;
	double yMax = window->getSize().y * marginTop;

	double singleSizeY = yMax / height;
	double singleSizeX = xMax / length;

	if (board::alwaysSquare)
	{
		if (singleSizeY > singleSizeX) singleSizeY = singleSizeX;
		else singleSizeX = singleSizeY;
	}
	sf::Vector2f cellSize = sf::Vector2f(singleSizeX, singleSizeY);

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < length; j++)
		{
			board::vertexBoard[(i * length * 4) + (j * 4) + 0].position = sf::Vector2f(j * cellSize.x, i * cellSize.y);
			board::vertexBoard[(i * length * 4) + (j * 4) + 1].position = sf::Vector2f(j * cellSize.x + cellSize.x, i * cellSize.y);
			board::vertexBoard[(i * length * 4) + (j * 4) + 2].position = sf::Vector2f(j * cellSize.x + cellSize.x, i * cellSize.y + cellSize.y);
			board::vertexBoard[(i * length * 4) + (j * 4) + 3].position = sf::Vector2f(j * cellSize.x, i * cellSize.y + cellSize.y);

			if (board::timeline[i][j] == 1)
			{
				board::vertexBoard[(i * length * 4) + (j * 4) + 0].color = sf::Color::Green;
				board::vertexBoard[(i * length * 4) + (j * 4) + 1].color = sf::Color::Green;
				board::vertexBoard[(i * length * 4) + (j * 4) + 2].color = sf::Color::Green;
				board::vertexBoard[(i * length * 4) + (j * 4) + 3].color = sf::Color::Green;
			}
			else
			{
				board::vertexBoard[(i * length * 4) + (j * 4) + 0].color = sf::Color::Red;
				board::vertexBoard[(i * length * 4) + (j * 4) + 1].color = sf::Color::Red;
				board::vertexBoard[(i * length * 4) + (j * 4) + 2].color = sf::Color::Red;
				board::vertexBoard[(i * length * 4) + (j * 4) + 3].color = sf::Color::Red;
			}
		}
	}
}
