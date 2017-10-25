#include "AnimalDetector.h"
#include <iostream>

void AnimalDetector::detect(Animal a) {
	std::cout << "same sort of animal" << std::endl;
}

void AnimalDetector::detect(Cat c) {
	std::cout << "its fluffy" << std::endl;
}

void AnimalDetector::detect(Dog d) {
	std::cout << "it's wagging" << std::endl;
}
