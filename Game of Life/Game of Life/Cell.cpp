#include "Cell.h"

void Cell::setState(bool &&newState)
{
	isAlive = newState;
	if (isAlive) this->setFillColor(sf::Color::Green);
	else this->setFillColor(sf::Color::Red);
}

void Cell::invertState()
{
	isAlive = !isAlive;
}

Cell::Cell()
{

}

Cell::Cell(bool &&state)
{
	isAlive = state;
	if (state) this->setFillColor(sf::Color::Green);
	else this->setFillColor(sf::Color::Red);
}

bool Cell::getState()
{
	return isAlive;
}
