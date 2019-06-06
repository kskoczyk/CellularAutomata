#include "GameMap.h"

namespace gameMap
{
	int iterations = 0; // @x in the history file
	int maxIteration = 0; // prevent multiple entries in history
	vector< vector<Cell>> prev;
	vector< vector<Cell>> current;
	string mapPath = "Map.txt"; // same directory as solution
	string historyPath = "Map_history.txt";
}

void gameMap::iterate(Config config)
{
	for (int i = 0; i < prev.size(); i++)
	{
		for (int j = 0; j < prev[i].size(); j++)
		{
			// COUNT
			int aliveCount = 0;

			if (i - 1 >= 0)
			{
				if (j - 1 >= 0)
				{
					if (prev[i - 1][j - 1].getState()) aliveCount++;
					if (prev[i][j - 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[i - 1][prev[i - 1].size() - 1].getState()) aliveCount++;
					if (prev[i][prev[i].size() - 1].getState()) aliveCount++;
				}
				if (prev[i - 1][j].getState()) aliveCount++;
				if (j + 1 <= prev[i].size() - 1)
				{
					if (prev[i - 1][j + 1].getState()) aliveCount++;
					if (prev[i][j + 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[i - 1][0].getState()) aliveCount++;
					if (prev[i][0].getState()) aliveCount++;
				}
			}
			else if (config.isTopBottom())
			{
				if (j - 1 >= 0)
				{
					if (prev[prev.size() - 1][j - 1].getState()) aliveCount++;
					if (prev[i][j - 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[prev.size() - 1][prev[prev.size() - 1].size() - 1].getState()) aliveCount++;
					if (prev[i][prev[i].size() - 1].getState()) aliveCount++;
				}
				if (prev[prev.size() - 1][j].getState()) aliveCount++;
				if (j + 1 <= prev[i].size() - 1)
				{
					if (prev[prev.size() - 1][j + 1].getState()) aliveCount++;
					if (prev[i][j + 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[prev.size() - 1][0].getState()) aliveCount++;
					if (prev[i][0].getState()) aliveCount++;
				}
			}

			if (i + 1 <= prev.size() - 1)
			{
				if (j - 1 >= 0)
				{
					if (prev[i + 1][j - 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[i + 1][prev[i + 1].size() - 1].getState()) aliveCount++;
				}
				if (prev[i + 1][j].getState()) aliveCount++;
				if (j + 1 <= prev[i].size() - 1)
				{
					if (prev[i + 1][j + 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[i + 1][0].getState()) aliveCount++;
				}
			}
			else if (config.isTopBottom())
			{
				if (j - 1 >= 0)
				{
					if (prev[0][j - 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[0][prev[0].size() - 1].getState()) aliveCount++;
				}
				if (prev[0][j].getState()) aliveCount++;
				if (j + 1 <= prev[i].size() - 1)
				{
					if (prev[0][j + 1].getState()) aliveCount++;
				}
				else if (config.isCylindrical())
				{
					if (prev[0][0].getState()) aliveCount++;
				}
			}

			// STATE
			if (prev[i][j].getState())
			{
				if (aliveCount < 2)	current[i][j].setState(false);
				else if(aliveCount > 3) current[i][j].setState(false);
				// else still alive
			}
			else
			{
				if (aliveCount == 3) current[i][j].setState(true);
			}
		}
	}

	prev = current;
	maxIteration = max(maxIteration, iterations);
	iterations++;
	if (config.isHistory() && iterations > maxIteration) saveRLE(encodeRLE(iterations), historyPath);
	// TODO: plain text for isRLEHistory();
}

void gameMap::randomize()
{
	for (int i = 0; i < current.size(); ++i) 
	{
		for (int j = 0; j < current[i].size(); j++)
		{
			current[i][j].setState(vp::dist(vp::mt));
		}
	}
	prev = current;
}

void gameMap::clear()
{
	for (int i = 0; i < current.size(); ++i)
	{
		for (int j = 0; j < current[i].size(); j++)
		{
			current[i][j].setState(false);
		}
	}
	prev = current;
}

void gameMap::changeSize(int sizeX, int sizeY)
{
	current.resize(sizeY);
	for (int i = 0; i < current.size(); i++)
	{
		current[i].resize(sizeX, Cell(false));
	}
	prev = current;
}

void gameMap::updatePrev()
{
	prev = current;
}

void gameMap::init(bool &&random)
{
	ofstream history(historyPath, ios::trunc); // TODO: make global
	if (random)
	{
		// TBA
	}
	else
	{
		string line;
		ifstream map(mapPath);
		if (map)
		{
			while (getline(map, line))
			{
				current.push_back(vector<Cell>());
				auto end_pos = remove(line.begin(), line.end(), ' '); // std::string::iterator
				line.erase(end_pos, line.end());
				for (int i = 0; i < line.size(); i++)
				{
					// TODO: consider using boost:lexical<cast>
					if (line[i] == '0')
					{
						current[current.size() - 1].push_back(Cell(false));
					}
					else
					{
						current[current.size() - 1].push_back(Cell(true));
					}
				}
			}
			prev = current;
		}
		if (vp::config.isHistory()) saveRLE(encodeRLE(iterations), historyPath);
		map.close();
	}
}

void gameMap::print(bool toFile, bool rle)
{
	string visualize = "";
	stringstream ss;
	for (int i = 0; i < current.size(); i++)
	{
		for (int j = 0; j < current[i].size(); j++)
		{
			ss << current[i][j].getState() << " ";
		}
		ss << endl;
	}
	ss << endl;
	visualize = ss.str();
	cout << visualize;

	if (toFile)
	{
		ofstream history(historyPath, ios::app);
		if (history)
		{
			history << visualize;
		}
		else
		{
			cerr << "Could not open/create the history file.";
		}
		history.close();
	}
}

void gameMap::create()
{
	cout << "User-created new map" << endl;
	cout << "Set number of rows" << endl;
	int rows;
	cin >> rows;
	if (rows == 0)
	{
		cout << "Aborted" << endl;
		return;
	}

	cout << "Set number of columns" << endl;
	int columns;
	cin >> columns;

	vector<vector<Cell>>().swap(current);
	current.resize(rows);
	for (int i = 0; i < rows; i++)
	{
		current[i].resize(columns);
		for (int j = 0; j < columns; j++)
		{
			bool isAlive;
			cin >> isAlive;
			Cell cell = Cell(move(isAlive)); // TODO: USE SMART POINTERS with ?emplace_back?
			current[i][j] = cell;
		}
	}
	prev = current;
}

stringstream gameMap::loadRLE(string path, int iteration)
{
	if (path == "" || path == historyPath)
	{
		path = historyPath;
		if (iteration == -1) iteration = iterations - 1;
	}
	ifstream rleFile(path);
	string line;
	stringstream rleStream;

	bool loading;
	if (iteration == -1) loading = true;
	else loading = false;
	if (rleFile)
	{
		while (getline(rleFile, line))
		{
			if (line[0] == '#')	continue; // skip comments
			else if (line[0] == '@' && path == historyPath)
			{
				int fileIteration = stoi(line.substr(1)); // omit '@'
				if (loading) break; // reached the next iteration
				else if (fileIteration == iteration) loading = true;
			}
			else if (loading) rleStream << line << endl; // TODO: something that works as a separator
		}
	}
	rleFile.close();
	return rleStream;
}

void gameMap::decodeRLE(stringstream RLE, int startX, int startY)
{
	string line;

	int x = 0;
	int y = 0;
	int row = startY;
	int column = startX;
	string number = "";

	while (getline(RLE, line))
	{
		if (line[0] == '#' || line[0] == '@') continue; // skip comments
		else if (line[0] == 'x' || line[0] == 'y') // get figure's size
		{
			auto end_pos = remove(line.begin(), line.end(), ' ');
			line.erase(end_pos, line.end());
			for (int i = 0; i < line.size(); i++)
			{
				if (line[i] == '=')
				{
					string number = "";
					for (i = i + 1; i < line.size(); i++)
					{
						if (isdigit(line[i])) number += line[i];
						else break;
					}

					if (x == 0) x = stoi(number);
					else y = stoi(number);
				}

				if (y != 0 && x != 0) break;
			}
		}
		else
		{
			if (current.size() < y + startY) current.resize(y + startY);
			for (int i = 0; i < current.size(); i++)
			{
				if (current[i].size() < x + startX) current[i].resize(max(x + startX, static_cast<int>(current[0].size())), Cell(false));
			}
			// QUICKFIX - could be handled more efficiently
			for (int i = 0; i < current.size(); i++)
			{
				fill(current[i].begin(), current[i].end(), Cell(false));
			}
			//

			int i = 0;
			for (row; row < current.size() && i < line.size(); )
			{
				for (i; column <= current[row].size() && i < line.size(); i++)
				{
					if (isdigit(line[i])) number += line[i];
					else if (line[i] == '$')
					{
						int numberInt;
						if (number.size() > 0) numberInt = stoi(number);
						else numberInt = 1;
						number = "";

						for (int emptyLines = 0; emptyLines < numberInt; emptyLines++) // obsolete?
						{
							for_each(begin(current[row]) + column, begin(current[row]) + x + startX, [](Cell& c) { c.setState(false); });
							column = startX;
							row++;
						}
						i++;
						break;
					}
					else
					{
						int numberInt;
						if (number.size() > 0) numberInt = stoi(number);
						else numberInt = 1;
						number = "";

						if (line[i] == 'b') for_each(begin(current[row]) + column, begin(current[row]) + column + numberInt, [](Cell& c) { c.setState(false); }); // set multiple dead cells in a row
						else if (line[i] == 'o') for_each(begin(current[row]) + column, begin(current[row]) + column + numberInt, [](Cell& c) { c.setState(true); }); // multiple alive
						else // ! - end of figure
						{
							// obsolete?
							if (column > startX) // prevent overwriting the next line outside the scope when file ends with an empty line ($)
							{
								for_each(begin(current[row]) + column, begin(current[row]) + x + startX, [](Cell& c) { c.setState(false); });
							}
							i++;
							break;
						}

						column += numberInt;
					}
				}
			}
		}
	}
	prev = current;
}

string gameMap::encodeRLE(int iteration)
{
	if (current.size() == 0)
	{
		cerr << "Map doesn't exist!" << endl;
		return "";
	}

	string encodedRLE = "";
	// OPENING
	if (iteration != -1) encodedRLE += "@" + to_string(iteration) + "\n";
	encodedRLE += "#N Automatic file save\n";
	// TODO: user comment?

	int y = current.size();
	int x = current[0].size();
	encodedRLE += "x = " + to_string(x) + ", y = " + to_string(y) + ", rule = ???\n";

	string figure = "";
	int consecutiveLines = -1;
	int consecutiveDead = 0;
	int consecutiveAlive = 0;
	string prevLine = "";
	for (int i = 0; i < current.size(); i++)
	{
		string line = "";
		consecutiveAlive = 0;
		consecutiveDead = 0;
		for (int j = 0; j < current[i].size(); j++)
		{
			if (current[i][j].getState() == 0)
			{
				consecutiveDead++;
				if (consecutiveAlive > 0)
				{
					if (consecutiveAlive == 1) line += "o";
					else line += to_string(consecutiveAlive) + "o";
					consecutiveAlive = 0;
				}
			}
			else
			{
				consecutiveAlive++;
				if (consecutiveDead > 0)
				{
					if (consecutiveDead == 1) line += "b";
					else line += to_string(consecutiveDead) + "b";
					consecutiveDead = 0;
				}
			}
		}

		// fill in with the residual alive - dead can be omitted
		if (consecutiveAlive > 0)
		{
			if (consecutiveAlive == 1) line += "o";
			else line += to_string(consecutiveAlive) + "o";
			consecutiveAlive = 0;
		}

		consecutiveLines++;
		if (consecutiveLines == 0) // beginning
		{
			figure += line;
		}
		else if (consecutiveDead == x) // full-dead line
		{
			continue;
		}
		else // broke the consecutive dead lines chain
		{
			if (consecutiveLines == 1) figure += "$" + line;
			else figure += to_string(consecutiveLines) + "$" + line;
			consecutiveLines = 0;
		}
	}
	figure += "!";

	// TODO: 70 as MAX_SIZE
	if (figure.size() > 70)
	{
		// TODO: fix the problem with insert and for's end condition
		for (int i = 0; (i + 1) * 70 < figure.size(); i++) // take inserted endlines into account
		{
			//figure.insert((i + 1) * 70 + i, "\n");
		}
	}
	encodedRLE += figure;
	return encodedRLE;
}

void gameMap::saveRLE(string figure, string path)
{
	/*ofstream saveFile;
	if (path == "" || path == historyPath)
	{
		path = historyPath;
		saveFile.open(path, ios::app); // do not erase history
	}
	else saveFile.open(path);
	if (saveFile) saveFile << figure << endl;
	saveFile.close();*/

	if (vp::saveFile) vp::saveFile << figure << endl;
	vp::saveFile.flush();
}
