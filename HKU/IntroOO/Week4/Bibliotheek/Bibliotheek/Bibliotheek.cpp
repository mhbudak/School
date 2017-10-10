#include "Bibliotheek.h"
#include <iostream>
#include <string>


Bibliotheek::Bibliotheek() {
}

void Bibliotheek::toon() {
	std::cout << "Bibliotheek " << boekje->_type << std::endl;
}

void Bibliotheek::voegToe(std::string type) {
	boekje = new Boekje(type);
}

