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
	std::string line;
	
	{
	std::getline(file1, line);
	combined_file << line << '\n';
	std::getline(file2, line);
	combined_file << line << '\n';
	std::getline(file1, line);
	combined_file << line << '\n';
	std::getline(file2, line);
	combined_file << line << '\n';
	std::getline(file1, line);
	combined_file << line << '\n';
	std::getline(file2, line);
	combined_file << line << '\n';
	std::getline(file1, line);
	combined_file << line << '\n';
	std::getline(file2, line);
	combined_file << line << '\n';
	std::getline(file1, line);
	combined_file << line << '\n';
	std::getline(file2, line);
	combined_file << line << '\n';
	}

	/*
	int count = 1;

	while (std::getline(file1, line)){
		combined_file << line << '\n';
		++count;
		while (std::getline(file2, line)) {
			combined_file << line << '\n';
			++count;
			while (std::getline(file1, line)) {
				combined_file << line << '\n';
				++count;
				while (std::getline(file2, line)) {
					combined_file << line << '\n';
					++count;
					while (std::getline(file1, line)) {
						combined_file << line << '\n';
						++count;
						while (std::getline(file2, line)) {
							combined_file << line << '\n';
							++count;
							while (std::getline(file1, line)) {
								combined_file << line << '\n';
								++count;
								while (std::getline(file2, line)) {
									combined_file << line << '\n';
									++count;
									while (std::getline(file1, line)) {
										combined_file << line << '\n';
										++count;
									}
								}
							}
						}
					}
				}
			}
		}
	}
	*/
	std::cout << "Your File has been reversed to ..\\Muhammed.txt" << std::endl;
	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}