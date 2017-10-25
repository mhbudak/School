#include <iostream>
#include <fstream>
#include <string>

#define FILE "..\\test.txt"

int main() {
	char ch;

	std::ifstream in_stream;
	in_stream.open(FILE);
	if (!in_stream) {
		std::cout << "probleem bij openen File" << std::endl;
		exit(1);
	}

	std::ofstream out_stream;
	out_stream.open("..\\Muhammed.txt");
	if (!out_stream) {
		std::cout << "probleem bij openen out File" << std::endl;
		exit(1);
	}

	bool skip = true;

	in_stream >> std::noskipws;
	while (!in_stream.eof()) {
		in_stream >> ch;

		skip = !skip;
		if (!skip) {
			out_stream << ch;
		}
	}
	out_stream.close();
	in_stream.close();

	/*	//in_stream.get(ch);
	//in_stream >> std::noskipws;
	//in_stream >> ch;
	//std::cout << ch;
	std::string data;
	getline(in_stream, data);
	std::cout << data;
	while (!in_stream.eof()) {
		//in_stream >> ch;
		//in_stream.get(ch);

		getline(in_stream, data);
		std::cout << data << std::endl;;
	}
	in_stream.close();

	*/
	std::cout << "Press key to quit" << std::endl;
	char c;
	std::cin >> c;
	return 0;
}