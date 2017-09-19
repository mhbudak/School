#pragma once
#include "Persoon.h"
#include <string>

class Student : public Persoon{
public:
	Student(int, std::string, std::string, std::string);
	~Student();
	std::string className;
	std::string education;
};