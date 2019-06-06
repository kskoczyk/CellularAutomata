#include "Config.h"

Config::Config()
{
	load();
}

void Config::save()
{
	properties.put("map.cylindrical", cylindrical);
	properties.put("map.topBottom", topBottom);
	properties.put("history", history);
	properties.put("history.rleHistory", rleHistory);
	write_info(configPath, properties);
}

void Config::load()
{
	read_info(configPath, properties);
	cylindrical = properties.get("map.cylindrical", false);
	topBottom = properties.get("map.topBottom", false);
	history = properties.get("history", false);
	rleHistory = properties.get("history.rleHistory", true);
}

bool Config::isCylindrical()
{
	return cylindrical;
}

bool Config::isTopBottom()
{
	return topBottom;
}

bool Config::isHistory()
{
	return history;
}

bool Config::isRleHistory()
{
	return rleHistory;
}

void Config::setCylindrical(bool state)
{
	cylindrical = state;
}

void Config::setTopBottom(bool state)
{
	topBottom = state;
}

void Config::setHistory(bool state)
{
	history = state;
}

void Config::setRleHistory(bool state)
{
	rleHistory = state;
}
