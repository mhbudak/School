#include <iostream>
#include <fstream>
#include <string>
#include "SnelTrein.h"
#include "Trein.h"

int main() {

	SnelTrein sneltrein = SnelTrein(300, 3.0f, 5);
	Trein trein = Trein(300, 2.5f);


	std::cout << sneltrein.PrijsBereken() << std::endl;
	std::cout << trein.PrijsBereken() << std::endl;

	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}