#pragma once
#include <string>
class Login
{
public:
	Login();
	~Login();

	int ID;
	std::string naam;
	std::string wachtwoord;
	std::string school;

	void login(int ID, std::string naam, std::string wachtwoord, std::string school);
};

