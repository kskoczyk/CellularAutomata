#pragma once
#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>
#include <string>
#include <sstream>
#include <random>

#include "Cell.h"
#include "Config.h"
#include "VariableParameters.h"

using namespace std;

namespace gameMap
{
	//TODO: global history stream
	extern vector< vector<Cell>> current;
	extern int iterations;

	void iterate(Config config = vp::config);
	void randomize();
	void clear();
	void changeSize(int sizeX = -1, int sizeY = -1);
	void updatePrev();
	void init(bool &&random = false);
	void print(bool toFile = vp::config.isHistory(), bool rle = vp::config.isRleHistory()); // TODO: unnecessary arguments?
	void create();
	stringstream loadRLE(string path = "", int iteration = -1);
	void decodeRLE(stringstream RLE, int startX = 0, int startY = 0);
	void saveRLE(string figure, string path = "");
	string encodeRLE(int iteration = -1);
}
