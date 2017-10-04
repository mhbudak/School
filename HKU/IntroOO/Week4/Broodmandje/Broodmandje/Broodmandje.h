#pragma once
#include "Broodje.h"

class Broodmandje {
	public:
		Broodmandje();
		void toon();
		void voegToe(std::string type);
	private:
		Broodje* broodje = new Broodje();;
};