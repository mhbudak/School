#include <iostream>
#include "Animal.h"
#include "Cat.h"
#include "Dog.h"
#include "AnimalDetector.h"


void saySomething(Animal& animal) {
	animal.makeNoise();
}

int main() {

	Animal animal1 = Animal();
	Dog animal2 = Dog();
	Cat animal3 = Cat();

	//animal1.makeNoise();
	//animal2.makeNoise();
	//animal3.makeNoise();

	saySomething(animal1);
	saySomething(animal2);
	saySomething(animal3);

	AnimalDetector dectector = AnimalDetector();
	dectector.detect(animal1);
	dectector.detect(animal2);
	dectector.detect(animal3);

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;

	return 0;
}