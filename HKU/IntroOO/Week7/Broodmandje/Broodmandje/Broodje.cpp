#include "Broodje.h"
#include <iostream>



Broodje::Broodje(std::string type) {
	_type = type;
}

Broodje::~Broodje() {
	std::cout << "Broodje: desstructor" << std::endl;
}

Broodje::Broodje() {
	std::cout << "Broodje: ctor" << std::endl;
}