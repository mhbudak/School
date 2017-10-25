#include <iostream>

#include "Cat.h"
#include "Dog.h"
#include "AnimalDetector.h"


int main() {

//	Animal animal = Animal("Beast");
//	animal.makeNoise();

	Cat cat = Cat("Felix");
	cat.makeNoise();

	Dog dog = Cat("Bro");
	dog.makeNoise();

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;

	return 0;
}