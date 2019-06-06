#pragma once
#include <random>
#include <fstream>
#include "Config.h"

namespace vp
{
	extern bool initialized;
	extern bool paused;
	extern bool loadedFromFile;
	extern bool mouseOnBoard;
	extern bool userChange;
	extern bool alwaysSquare; // add to config
	extern Config config;

	//FILESTREAMS
	extern ofstream saveFile;

	//RANDOM
	extern std::random_device rd;
	extern std::mt19937 mt;
	extern std::uniform_int_distribution<> dist;
	//extern std::_Binder<std::_Unforced, std::uniform_int_distribution<>, std::default_random_engine> boolGen;
	//extern auto boolGen;
}