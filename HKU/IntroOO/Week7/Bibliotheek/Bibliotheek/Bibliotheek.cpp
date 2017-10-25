#include "Bibliotheek.h"
#include <iostream>
#include <string>


Bibliotheek::Bibliotheek() {
	std::cout << "Boekje: constructor" << std::endl;
}

void Bibliotheek::toon() {
	std::cout << "Bibliotheek: " << boekje->_type << std::endl;
}

void Bibliotheek::voegToe(std::string type) {
	delete boekje;
	boekje = new Boekje(type);
}

Bibliotheek::~Bibliotheek() {
	std::cout << "Broodmandje: desstructor" << std::endl;
	delete boekje;
}

Bibliotheek::Bibliotheek(const Bibliotheek & anderMandje) {
	boekje = new Boekje();
	boekje->_type = anderMandje.boekje->_type;
}

Bibliotheek& Bibliotheek::operator=(const Bibliotheek& anderMandje) {
	if (this != &anderMandje) {
		delete boekje;
		boekje = new Boekje;
		boekje->_type = anderMandje.boekje->_type;
	}
	return *this;
}
