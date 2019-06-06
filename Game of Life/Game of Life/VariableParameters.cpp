#include "VariableParameters.h"

namespace vp
{
	bool initialized = false;
	bool paused = false;
	bool loadedFromFile = false;
	bool mouseOnBoard = false;
	bool userChange = false;
	bool alwaysSquare = true;
	Config config;

	//FILESTREAMS
	ofstream saveFile("Map_history.txt", ios::app);

	//RANDOM
	std::random_device rd;
	std::mt19937 mt(rd());
	std::uniform_int_distribution<> dist(0, 1);
	//std::_Binder<std::_Unforced, std::uniform_int_distribution<>, std::default_random_engine> boolGen = std::bind(std::uniform_int_distribution<int>(0, 1), std::mt19937());
	//auto boolGen = std::bind(std::uniform_int_distribution<>(0, 1), std::default_random_engine());
}