#pragma once
#include <string>

class Docent
{
public:
	Docent();
	~Docent();
	std::string docentNaam;
	std::string docentAchternaam;

	std::string afkorting(std::string naam, std::string achterNaam);

};

