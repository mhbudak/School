#include "Student.h"



Student::Student(int age, std::string name, std::string className, std::string education){
	this->age = age;
	this->name = name;
	this->className = className;
	this->education = education;
}


Student::~Student(){
}
