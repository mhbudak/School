#include "Boekje.h"
#include <iostream>


Boekje::Boekje(std::string type) {
	_type = type;
}

Boekje::~Boekje() {
	std::cout << "Boekje: desstructor" << std::endl;
}

Boekje::Boekje() {
	std::cout << "Boekje: ctor" << std::endl;
}
