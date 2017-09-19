#pragma once
#include "Persoon.h"
#include <string>

class Docent : public Persoon{
public:
	Docent(int, std::string, std::string, int);
	~Docent();
	std::string subject;
	int income;
};