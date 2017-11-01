#include <iostream>
#include <string>
#include <fstream>
#include <algorithm>

#define FILE "..\\test.txt"


int main() {
	const char* const file_name = FILE;

	std::string text;
	{
		std::ifstream file(FILE); 
		char c;
		while (file.get(c)) 
			text += c;
	}
	std::reverse(text.begin(), text.end());
	{
		std::ofstream file("..\\Muhammed.txt");
		file << text;
	}
	
	std::ifstream file1("..\\nummer1.txt");
	std::ifstream file2("..\\nummer2.txt");
	std::ofstream combined_file("..\\nummer3.txt");
	//combined_file << file1.rdbuf() << file2.rdbuf();


	char ch;
	file1.open("..\\nummer1.txt");
	file2.open("..\\nummer2.txt");
	if (!file1) {
		std::cout << "probleem bij openen File" << std::endl;
		
	}

	combined_file.open("..\\nummer3.txt");
	if (!combined_file) {
		std::cout << "probleem bij openen out File" << std::endl;
		
	}

	bool skip = true;

	file1 >> std::noskipws;
	while (!file1.eof()) {
		file1 >> ch;

		skip = !skip;
		if (!skip) {
			combined_file << ch;
		}
	}
	combined_file.close();
	file1.close();
	file2.close();




	std::cout << "Your File has been reversed to ..\\Muhammed.txt" << std::endl;
	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}