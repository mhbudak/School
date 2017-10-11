#pragma once
#include "Broodje.h"

class Broodmandje {
	public:
		Broodmandje();
		void toon();
		void voegToe(std::string type);
		virtual ~Broodmandje();
		Broodmandje(const Broodmandje& anderMandje);
		Broodmandje& operator = (const Broodmandje&);

	private:
		Broodje* broodje = new Broodje();
};