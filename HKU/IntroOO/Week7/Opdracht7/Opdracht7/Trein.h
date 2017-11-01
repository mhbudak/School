#pragma once
#include <iostream>
#include "SnelTrein.h"
#include "Vervoermidel.h"

class Trein: Vervoermidel {
public:

	Trein(int _aantalPassagiers, float _prijsPerKm) {
		aantalPassagiers = _aantalPassagiers;
		prijs = _prijsPerKm;
	}

	void PrijsBereken() {}

	~Trein();
	Trein();
};
