#pragma once
#include "Animal.h"
#include <iostream>

Dog::Animal(const std::string & name) {
}

class Dog : public Animal{
	public:
		void makeNoise();
};

