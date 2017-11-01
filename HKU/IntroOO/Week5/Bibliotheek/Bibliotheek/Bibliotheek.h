#pragma once
#include "Boekje.h"
#include <string>

class Bibliotheek {
	public:
		Bibliotheek();
		void toon();
		void voegToe(std::string type);
		virtual ~Bibliotheek();
		std::string filiaalNaam;
		Bibliotheek(const Bibliotheek& anderBoekje);
		Bibliotheek& operator=(const Bibliotheek&);

	private:
		Boekje* boekje = new Boekje();
};