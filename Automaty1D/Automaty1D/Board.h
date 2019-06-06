#pragma once
#include <vector>

#include <SFML/Graphics.hpp>

using namespace std;

namespace board
{
	extern vector<vector<int>> timeline;
	extern sf::VertexArray vertexBoard;
	extern bool drawn;
	extern bool alwaysSquare;
}