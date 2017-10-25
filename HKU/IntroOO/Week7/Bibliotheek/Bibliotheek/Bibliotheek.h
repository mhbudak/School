#pragma once
#include "Boekje.h"

class Bibliotheek {
	public:
		Bibliotheek();
		void toon();
		void voegToe(std::string type);
		virtual ~Bibliotheek();
		Bibliotheek(const Bibliotheek& anderBoekje);
		Bibliotheek& operator=(const Bibliotheek&);

	private:
		Boekje* boekje = new Boekje();
};