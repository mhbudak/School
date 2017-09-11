#include "Kalender.h"
#include "Vakken.h"
#include "Docent.h"
#include "Lokaal.h"
#include "Richting.h"
#include "Database.h"
#include <list>
#include <string>

class LesRooster
{
public:
	LesRooster();
	~LesRooster();

	std::list<std::string> klas(std::string richting, std::string vakken);
	std::list<std::string> listLokaal(int lokaal, std::string docent, std::list<std::string, std::string> klas(std::string richting, std::string vakken), bool lokaalCheck);
	bool lokaalCheck(int lokaal, int tijd);
};