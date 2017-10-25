#pragma once
class Animal {
	public:
		Animal(const std::string& name);
		virtual void makeNoise() = 0;
	private:
		std::string name;
};