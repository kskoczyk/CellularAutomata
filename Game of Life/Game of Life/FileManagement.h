#pragma once
#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <filesystem>

using namespace std;
using namespace std::experimental;

namespace fileManagement
{
	extern vector<filesystem::directory_entry> figureFiles;
	vector<filesystem::directory_entry> findFilesByExtension(string extension, string path = "");
}
