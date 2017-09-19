#pragma once
#include <string>
class Persoon{
public:
	Persoon();
	~Persoon();
	int age;
	std::string name;
	
	std::string getName();
	int getAge();
};