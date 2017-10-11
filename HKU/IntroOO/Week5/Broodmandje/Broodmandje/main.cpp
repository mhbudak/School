#include <iostream>
#include "Broodmandje.h"

int main() {
	Broodmandje mandje1 = Broodmandje();
	mandje1.toon();
	
	std::cout << "---mandje1" << std::endl;
	mandje1.voegToe("cibatta");
	mandje1.toon();

	Broodmandje mandje2;
	mandje2 = mandje1;
	std::cout << "---mandje2" << std::endl;
	mandje2.toon();

	mandje1.voegToe("croissant");
	std::cout << "---mandje1" << std::endl;
	mandje1.toon();
	std::cout << "---mandje2" << std::endl;
	mandje2.toon();

	//delete mandje;
	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}