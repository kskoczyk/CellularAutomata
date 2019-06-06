#include "FileManagement.h"

namespace fileManagement
{
	vector<filesystem::directory_entry> figureFiles;
	string rleFigures = "./Figures";
}

vector<filesystem::directory_entry> fileManagement::findFilesByExtension(string extension, string path)
{
	if (path == "") path = rleFigures;
	vector<filesystem::directory_entry> files;
	for (auto& p : filesystem::recursive_directory_iterator(path))
	{
		if (p.path().extension() == extension) files.push_back(p);
	}
	figureFiles = files;
	return figureFiles;
}
