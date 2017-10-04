#include <iostream>
#include "Broodmandje.h"

void geefMandjeDoor(Broodmandje* mandje) {
	mandje->voegToe("taart");
}

int main() {
	Broodmandje* mandje = new Broodmandje();
	mandje->toon();
	
	mandje->voegToe("taart2");
	mandje->toon();

	geefMandjeDoor(mandje);
	mandje->toon();

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}