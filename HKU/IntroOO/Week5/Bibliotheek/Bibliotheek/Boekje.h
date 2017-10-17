#pragma once
#include <iostream>
#include <string>

class Boekje {
	public:
		Boekje(std::string type);
		std::string _type = "onbekend";
		Boekje();
		~Boekje();
};
