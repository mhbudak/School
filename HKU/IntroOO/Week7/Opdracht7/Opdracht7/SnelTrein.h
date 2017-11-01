#pragma once
#include "SnelTrein.h"
#include "Vervoermidel.h"
#include "Trein.h"

class SnelTrein : public Vervoermidel {
public:
	SnelTrein(int _aantalPassagiers, float _prijsPerKm, float _toeslag) {
		aantalPassagiers = _aantalPassagiers;
		prijs = _prijsPerKm;
		heeftToeslag = _toeslag;
		toeslag = _toeslag;
	}

	~SnelTrein();
	SnelTrein();
};

