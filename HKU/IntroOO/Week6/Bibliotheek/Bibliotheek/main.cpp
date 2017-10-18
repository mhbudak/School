#include <iostream>
#include "Bibliotheek.h"
#include "main.h"

int main() {
	Bibliotheek boekje1 = Bibliotheek();
	boekje1.toon();

	std::cout << "---Boek1" << std::endl;
	boekje1.voegToe("Tales of Demons & Gods");
	boekje1.toon();

	Bibliotheek boekje2;
	boekje2 = boekje1;

	std::cout << "---Boek2" << std::endl;
	boekje2.toon();

	boekje1.voegToe("Re:Monster");
	std::cout << "---Boek1" << std::endl;
	boekje1.toon();
	std::cout << "---Boek2" << std::endl;
	boekje2.toon();

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}