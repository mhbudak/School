#pragma once
#include <string>
class Dino {
	public:
		Dino(std::string name);
		std::string getName();

	private:
		std::string name;
};

