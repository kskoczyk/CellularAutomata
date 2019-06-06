#include "Rule.h"

void enforceRule(vector<int> prev, vector<int> &present, int rule)
{
	if ((prev.size() < 3) || (present.size() != prev.size()))
	{
		cout << "Incorrect vector lengths!" << endl;
		return;
	}

	string binaryRule = bitset<8>(rule).to_string();
	//cout << binaryRule << endl;
	stringstream chain;
	string s;
	int position;

	// TODO: consider using another function for this
	chain << prev[prev.size() - 1] << prev[0] << prev[1];
	s = chain.str();
	//cout << s << endl;
	position = bitset<3>(s).to_ulong();
	present[0] = binaryRule[7 - position] - '0';
	chain.str(std::string());
	for (int i = 1; i < prev.size() - 1; i++)
	{
		chain << prev[i - 1] << prev[i] << prev[i + 1];
		s = chain.str();
		//cout << s << endl;
		position = bitset<3>(s).to_ulong();
		present[i] = binaryRule[7 - position] - '0'; // convert char to int
		chain.str(std::string()); // clear the chain
	}
	chain << prev[prev.size() - 2] << prev[prev.size() - 1] << prev[0];
	s = chain.str();
	//cout << s << endl;
	position = bitset<3>(s).to_ulong();
	present[present.size() - 1] = binaryRule[7 - position] - '0';
	chain.str(std::string());

	for (int i = 0; i < prev.size(); i++)
	{
		//cout << present[i] << " ";
	}
	//cout << endl;
}
