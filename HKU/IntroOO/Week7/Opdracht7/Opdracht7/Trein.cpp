#include <iostream>
#include "SnelTrein.h"
#include "Vervoermidel.h"
#include "Trein.h"

Trein::Trein() {}

void Trein::PrijsBereken() {
	return (prijs*km) * aantalPassagiers;
}