#include "SnelTrein.h"
#include "Trein.h"
#include "Vervoermidel.h"

SnelTrein::SnelTrein() {}

void SnelTrein::PrijsBereken(int km) {
	return ((prijs + toeslag) * km) * aantalPassagiers;
}