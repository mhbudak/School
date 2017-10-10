#include <iostream>
#include "Bibliotheek.h"


void geefBoekjeDoor(Bibliotheek* boekje) {
	boekje->voegToe("Lord of The Rings");
}

int main() {
	Bibliotheek* boekje = new Bibliotheek();
	boekje->toon();

	boekje->voegToe("Tales of Demons & Gods");
	boekje->toon();

	geefBoekjeDoor(boekje);
	boekje->toon();

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}