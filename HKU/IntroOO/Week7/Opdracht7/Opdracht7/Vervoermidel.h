#pragma once
class Vervoermidel {
public:
	Vervoermidel();
	int aantalPassagiers;
	float prijs;
	float toeslag;
	bool heeftToeslag;

	virtual void PrijsBereken();
	void getaantalPassagiers();
};