#pragma once
#include "Boekje.h"

class Bibliotheek {
	public:
		Bibliotheek();
		void toon();
		void voegToe(std::string type);
	private:
		Boekje* boekje = new Boekje();
};