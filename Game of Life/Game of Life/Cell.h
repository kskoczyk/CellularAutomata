#pragma once
#include <SFML/Graphics.hpp>

class Cell : public sf::RectangleShape
{
	bool isAlive;

public:
	Cell();
	Cell(bool &&state);
	bool getState();
	void setState(bool &&newState);
	void invertState();
};
