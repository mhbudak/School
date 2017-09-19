#include <iostream>
#include "Student.h"
#include "Docent.h"

Student student = Student(17, "piet", "H1D", "HAVO");

Docent docent = Docent(45, "jan", "Wiskunde", 15000);



int main(){
	std::cout << student.getAge() << " " << student.getName()   << std::endl;
	std::cout << docent.getAge() << " " << docent.getName() << std::endl;

	char key;
	std::cin >> key;

	return 0;
}