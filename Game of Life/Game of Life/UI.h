#pragma once
#include <iostream>
#include <string>

#include <boost/chrono.hpp>
#include <boost/thread.hpp>

#include "Cell.h"
#include "Config.h"
#include "GameMap.h"

using namespace std;

namespace UI
{
	void waitForInput(boost::thread &t1, boost::chrono::milliseconds delay, Config &config = vp::config);
};
