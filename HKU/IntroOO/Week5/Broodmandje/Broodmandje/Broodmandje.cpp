#include "Broodmandje.h"
#include <iostream>
#include <string>


Broodmandje::Broodmandje() {
	std::cout << "Broodje: constructor" << std::endl;
}

void Broodmandje::toon() {
	std::cout << "Broodmandje: " << broodje->_type << std::endl;
}

void Broodmandje::voegToe(std::string type) {
	delete broodje;
	broodje = new Broodje(type);
}

Broodmandje::~Broodmandje() {
	std::cout << "Broodmandje: desstructor" << std::endl;
	delete broodje;
}

Broodmandje::Broodmandje(const Broodmandje & anderMandje) {
	broodje = new Broodje();
	broodje->_type = anderMandje.broodje->_type;
}

Broodmandje & Broodmandje::operator=(const Broodmandje& anderMandje) {
	broodje = new Broodje;
	broodje->_type = anderMandje.broodje->_type;
}
