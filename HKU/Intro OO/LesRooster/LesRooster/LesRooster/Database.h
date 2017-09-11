#pragma once
#include "LesRooster.h"
class Database
{
public:
	Database();
	~Database();

	void convertToSQL(std::list<std::string> listLokaal());
	void convertToC();
};

