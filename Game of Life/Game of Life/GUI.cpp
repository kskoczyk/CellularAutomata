#include "GUI.h"

void test(string param)
{
	cout << param << endl;
}

vector<string> GUI::getPaths()
{
	vector<string> paths(files.size());
	for (int i = 0; i < paths.size(); i++)
	{
		paths[i] = files[i].path().string();
	}
	return paths;
}

GUI::GUI() // TODO: necessary? for separate call of initGUI?
{
	vertexBoard = sf::VertexArray(sf::Quads);
}

GUI::GUI(tgui::Gui &gui)
{
	initGUI(gui);
	vertexBoard = sf::VertexArray(sf::Quads);
}

map<sf::String, tgui::Widget::Ptr> GUI::initGUI(tgui::Gui &gui, string path)
{
	if (path == "") path = guiPath;

	map<sf::String, tgui::Widget::Ptr> widgetMap;
	gui.loadWidgetsFromFile(path);
	auto widgets = gui.getWidgets();
	auto widgetNames = gui.getWidgetNames();

	for (int i = 0; i < widgets.size(); i++)
	{
		widgetMap.insert(make_pair(widgetNames[i], widgets[i]));
	}

	files = fileManagement::findFilesByExtension(".rle", figuresPath);
	auto filesComboBox = gui.get("ComboBoxFiles")->cast<tgui::ComboBox>();
	for (int i = 0; i < files.size(); i++)
	{
		filesComboBox->addItem(sf::String(files[i].path().filename()));
	}

	gui.get("ButtonPlay")->cast<tgui::Button>()->connect("Pressed", GUI::handlePlayButtonPressed, &gui);
	gui.get("ButtonPause")->cast<tgui::Button>()->connect("Pressed", GUI::handlePauseButtonPressed, &gui);
	gui.get("ButtonNext")->cast<tgui::Button>()->connect("Pressed", GUI::handleNextButtonPressed);
	gui.get("ButtonPrevious")->cast<tgui::Button>()->connect("Pressed", GUI::handlePreviousButtonPressed);
	gui.get("ButtonLoad")->cast<tgui::Button>()->connect("Pressed", GUI::handleLoadButtonPressed, &gui);
	gui.get("ButtonResize")->cast<tgui::Button>()->connect("Pressed", GUI::handleResizeButtonPressed, &gui);
	gui.get("ButtonRandomize")->cast<tgui::Button>()->connect("Pressed", GUI::handleRandomizeButtonPressed);
	gui.get("ButtonClear")->cast<tgui::Button>()->connect("Pressed", GUI::handleClearButtonPressed);
	gui.get("ButtonSave")->cast<tgui::Button>()->connect("Pressed", GUI::handleSaveButtonPressed, &gui);
	return widgetMap;
}

sf::VertexArray GUI::getVertexBoard()
{
	return vertexBoard;
}

void GUI::handlePlayButtonPressed(tgui::Gui *const gui)
{
	vp::paused = false;
	if (vp::userChange)
	{
		gameMap::updatePrev();
		vp::userChange = false;
	}
	if (vp::loadedFromFile)
	{
		//gameMap::print();
		gui->get("ButtonPause")->cast<tgui::Button>()->setEnabled(true);
		gui->get("ButtonNext")->cast<tgui::Button>()->setEnabled(true);
		gui->get("ButtonPrevious")->cast<tgui::Button>()->setEnabled(true);
		gui->get("ButtonPlay")->cast<tgui::Button>()->setEnabled(false);
		vp::initialized = true;
	}
	else if (vp::initialized == false)
	{
		gameMap::init();
		//gameMap::print(false);
		gui->get("ButtonPause")->cast<tgui::Button>()->setEnabled(true);
		gui->get("ButtonPlay")->cast<tgui::Button>()->setEnabled(false);
		vp::initialized = true;
	}
	else
	{
		gui->get("ButtonPlay")->cast<tgui::Button>()->setEnabled(false);
		gui->get("ButtonPause")->cast<tgui::Button>()->setEnabled(true);
	}
}

void GUI::handlePauseButtonPressed(tgui::Gui * const gui)
{
	vp::paused = true;
	gui->get("ButtonPlay")->cast<tgui::Button>()->setEnabled(true);
	gui->get("ButtonPause")->cast<tgui::Button>()->setEnabled(false);
	gui->get("ButtonNext")->cast<tgui::Button>()->setEnabled(true);
	gui->get("ButtonPrevious")->cast<tgui::Button>()->setEnabled(true);

	//auto editBox = gui->get("EditBoxSave")->cast<tgui::EditBox>();
	//cout << static_cast<string>(editBox->getText()) << endl;
}

void GUI::handleNextButtonPressed() // TODO: next should read from history if possible
{
	if (vp::userChange)
	{
		gameMap::updatePrev();
		vp::userChange = false;
	}
	gameMap::iterate();
	//gameMap::print(false);
}

// TODO: use ifstream::ignore() for reading history lines
// TODO: max history?
void GUI::handlePreviousButtonPressed()
{
	if (gameMap::iterations == 0) return;
	//vp::saveFile.flush();
	vp::saveFile.close();
	gameMap::decodeRLE(gameMap::loadRLE());
	gameMap::iterations--;
	vp::saveFile.open("Map_history.txt", ios::app);
	//gameMap::print(false);
}

void GUI::handleLoadButtonPressed(tgui::Gui *const gui)
{
	// TODO:shouldn't clear the whole board
	int index = gui->get("ComboBoxFiles")->cast<tgui::ComboBox>()->getSelectedItemIndex();
	string path = fileManagement::figureFiles[index].path().string();
	// TODO: handle loaded flag
	gameMap::decodeRLE(gameMap::loadRLE(path));
	vp::userChange = true;
}

void GUI::handleResizeButtonPressed(tgui::Gui * const gui)
{
	int sizeX = stoi(gui->get("EditBoxX")->cast<tgui::EditBox>()->getText().toAnsiString());
	int sizeY = stoi(gui->get("EditBoxY")->cast<tgui::EditBox>()->getText().toAnsiString());

	if (sizeX > 0 && sizeY > 0)
	{
		gameMap::changeSize(sizeX, sizeY);
	}
}

void GUI::handleClearButtonPressed()
{
	gameMap::clear();
}

void GUI::handleRandomizeButtonPressed()
{
	gameMap::randomize();
}

void GUI::handleSaveButtonPressed(tgui::Gui * const gui)
{
	int sizeX = stoi(gui->get("EditBoxX")->cast<tgui::EditBox>()->getText().toAnsiString());
	int sizeY = stoi(gui->get("EditBoxY")->cast<tgui::EditBox>()->getText().toAnsiString());

	if(sizeX > 0 && sizeY > 0)
	{
		gameMap::changeSize(sizeX, sizeY);
	}
}

void GUI::prepareBoard(sf::RenderWindow &window)
{
	// TODO: use legacy code and the fact that Cell inherits from RectangleShape - maybe?
	//double singleSizeY = yMax / gameMap::current.size();
	//double singleSizeX = xMax / gameMap::current[0].size();
	//sf::Vector2f cellSize = sf::Vector2f(singleSizeX, singleSizeY); // TODO: time measurements with move and without
	//for (int i = 0; i < gameMap::current.size(); i++)
	//{
	//	for (int j = 0; j < gameMap::current[i].size(); j++)
	//	{
	//		gameMap::current[i][j].setSize(move(cellSize));
	//		gameMap::current[i][j].setPosition(sf::Vector2f(cellSize.x * j, cellSize.y * i));
	//	}
	//}

	if (!vp::initialized) return; // nothing to draw

	int length = gameMap::current[0].size();
	int height = gameMap::current.size();
	vertexBoard.resize(height * length * 4);

	double xMax = window.getSize().x * marginLeft;
	double yMax = window.getSize().y * marginTop;
	double singleSizeY = yMax / height;
	double singleSizeX = xMax / length;

	if (vp::alwaysSquare) // TODO: alwaysSquare config
	{
		if (singleSizeY > singleSizeX) singleSizeY = singleSizeX;
		else singleSizeX = singleSizeY;
	}
	sf::Vector2f cellSize = sf::Vector2f(singleSizeX, singleSizeY);

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < length; j++)
		{
			vertexBoard[(i * length * 4) + (j * 4) + 0].position = sf::Vector2f(j * cellSize.x, i * cellSize.y);
			vertexBoard[(i * length * 4) + (j * 4) + 1].position = sf::Vector2f(j * cellSize.x + cellSize.x, i * cellSize.y);
			vertexBoard[(i * length * 4) + (j * 4) + 2].position = sf::Vector2f(j * cellSize.x + cellSize.x, i * cellSize.y + cellSize.y);
			vertexBoard[(i * length * 4) + (j * 4) + 3].position = sf::Vector2f(j * cellSize.x, i * cellSize.y + cellSize.y);

			gameMap::current[i][j].setSize(cellSize); // TODO: only if size changes
			gameMap::current[i][j].setPosition(sf::Vector2f(cellSize.x * j, cellSize.y * i));
			if (gameMap::current[i][j].getState() == true)
			{
				vertexBoard[(i * length * 4) + (j * 4) + 0].color = sf::Color::Green;
				vertexBoard[(i * length * 4) + (j * 4) + 1].color = sf::Color::Green;
				vertexBoard[(i * length * 4) + (j * 4) + 2].color = sf::Color::Green;
				vertexBoard[(i * length * 4) + (j * 4) + 3].color = sf::Color::Green;
			}
			else
			{
				vertexBoard[(i * length * 4) + (j * 4) + 0].color = sf::Color::Red;
				vertexBoard[(i * length * 4) + (j * 4) + 1].color = sf::Color::Red;
				vertexBoard[(i * length * 4) + (j * 4) + 2].color = sf::Color::Red;
				vertexBoard[(i * length * 4) + (j * 4) + 3].color = sf::Color::Red;
			}
		}
	}
}

void GUI::highlightCell(sf::RenderWindow &window)
{
	int rowIndex = -1;
	int colIndex = -1;

	if (vp::initialized && mouseOnBoard)
	{
		double squareSizeY = gameMap::current[0][0].getSize().y;
		double squareSizeX = gameMap::current[0][0].getSize().x;
		rowIndex = floor(cursorPos.y / squareSizeY);
		rowIndex = min(static_cast<unsigned long long>(max(0, rowIndex)), gameMap::current.size() - 1);
		colIndex = floor(cursorPos.x / squareSizeX);
		colIndex = min(static_cast<unsigned long long>(max(0, colIndex)), gameMap::current[0].size() - 1);
	}

	if (rowIndex > -1 && colIndex > -1)
	{
		if (mouseReleased)
		{
			gameMap::current[rowIndex][colIndex].invertState();
			vp::userChange = true;
		}

		if(mouseClicked) gameMap::current[rowIndex][colIndex].setFillColor(sf::Color::Blue);
		else gameMap::current[rowIndex][colIndex].setFillColor(sf::Color::Yellow);
		window.draw(gameMap::current[rowIndex][colIndex]);
	}
}

void GUI::setMouseOnBoard(bool state)
{
	mouseOnBoard = state;
}

void GUI::setMouseClicked(bool state)
{
	mouseClicked = state;
}

void GUI::setMouseReleased(bool state)
{
	mouseReleased = state;
}

void GUI::setCursorPos(sf::Vector2i pos)
{
	cursorPos = pos;
}
