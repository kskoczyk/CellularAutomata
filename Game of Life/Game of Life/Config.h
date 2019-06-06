#pragma once

#include <string>

#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/info_parser.hpp>


using namespace std;
using namespace boost::property_tree;

static string configPath = "config.info"; // leave static or make normal?
class Config
{
	ptree properties;
	bool cylindrical;
	bool topBottom;
	bool history;
	bool rleHistory;

public:
	Config();

	void save();
	void load();

	bool isCylindrical();
	bool isTopBottom();
	bool isHistory();
	bool isRleHistory();
	void setCylindrical(bool state);
	void setTopBottom(bool state);
	void setHistory(bool state);
	void setRleHistory(bool state);
};