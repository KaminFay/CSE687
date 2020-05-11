// Parsers.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include "Parsers.h"

namespace Parsers {
	
	/*
	* Basic constructor with no information provided. Set's everything to NULL or empty strings
	*/
	JSONParser::JSONParser() {
		j = NULL;
		jsonFileName = "";
		jsonFileLocation = "";
	}

	/*
	* Constructor with a file location and file name string passed in. The JSON object will still need to be parsed
	* So that will remain NULL for the time being.
	*/
	JSONParser::JSONParser(std::string location, std::string name)
	{
		j = NULL;
		jsonFileName = name;
		jsonFileLocation = location;
	}

	/*
	*
	*/
	void JSONParser::setJsonFileName(std::string name) {
		jsonFileName = name;
	}

	std::string JSONParser::getJsonFileName() {
		return jsonFileName;
	}

	void JSONParser::setJsonFileLocation(std::string location) {
		jsonFileLocation = location;
	}

	std::string JSONParser::getJsonFileLocation() {
		return jsonFileLocation;
	}

	void JSONParser::setJsonObject(nlohmann::json passedInJSON) {
		j = passedInJSON;
	}

	std::string JSONParser::parseStringFromJSON(std::string fromDLL, std::string keyString) {
		if (j.find(fromDLL) != j.end()) {
			std::string valueString = j[fromDLL][keyString].get<std::string>();
			return valueString;
		}
		else {
			return "NULL";
		}

	}

	//std::vector<dllFunction> JSONParser::getDllFunctions() {

	//}

	std::vector<dllInfo> JSONParser::getAllDlls() {
		std::vector<dllInfo> allDLLVec;

		std::cout << "Testing the print for the high level dll names. -----" << std::endl << std::endl;

		for (auto& item : j.items()) {
			std::cout << item.key() << std::endl;
			allDLLVec.push_back(getSingleDll(item.key()));
		}

		return allDLLVec;
	}

	//dllFunction JSONParser::getSingleDllFunction() {

	//}

	/*
	Given a dll name that is within the JSON we are parsing we can parse the functions from only that dll
	This will return a dllInfo object that includes dll name, location, and a vector of functions.
	*/
	dllInfo JSONParser::getSingleDll(std::string dllName) {
		dllInfo newDll;
		if (j.find(dllName) != j.end()) {
			nlohmann::json dllCurJSON = j[dllName];

			if (dllCurJSON.find("functions") != dllCurJSON.end() && dllCurJSON.find("dllName") != dllCurJSON.end()
				&& dllCurJSON.find("dllLocation") != dllCurJSON.end()) {

				newDll.location = dllCurJSON["dllLocation"].get<std::string>();
				newDll.name = dllCurJSON["dllName"].get<std::string>();

				std::vector<dllFunction> dllFunctions;

				for (auto it: dllCurJSON["functions"].items()) {
					dllFunction func;
					if (it.value().find("name") != it.value().end() && it.value().find("passedIn") != it.value().end()
						&& it.value().find("returnType") != it.value().end()) {
						func.functionName = it.value()["name"];
						func.parameters = it.value()["passedIn"];
						func.returnType = it.value()["returnType"];
						dllFunctions.push_back(func);
					}
				}

				newDll.functions = dllFunctions;
			}
		}
		return newDll;
	}

	std::string JSONParser::parseDllFunctions() {
		if (j.find("functions") != j.end()) {
			std::vector<std::string> functions = j["functions"];
			std::cout << "Here is the vector of functions: " << std::endl;
			for (int i = 0; i < functions.size(); i++) {
				std::cout << functions[i] << std::endl;
			}
		}
		return "None";
	}

	void JSONParser::openJsonFile() {
		if (jsonFileName == "" || jsonFileLocation == "") {
			std::cout << "Filename or location empty" << std::endl;
			exit(EXIT_FAILURE);
		}
		else {
			std::ifstream inFile;
			inFile.open(jsonFileLocation + jsonFileName, std::ifstream::in);

			if (!inFile.is_open()) {
				std::cout << "File did not open correctly, this is the file: " << jsonFileLocation + jsonFileName << std::endl;
				exit(EXIT_FAILURE);
			}
			else {
				inFile >> j;
			}
		}
	}

	void JSONParser::testPrintingJSON() {
		if (j != NULL) {
			std::cout << std::setw(4) << j << std::endl;
		}
		else {
			std::cout << "Failed to print as the json variable is NULL." << std::endl;
		}
	}

	void XMLParser::testPrintingXML() {
		std::cout << "Testing Printing From The XML" << std::endl;
	}

	SourceParser::SourceParser()
	{
		funcPrecurse = "(.*)(__declspec)(.)(dllexport)(.*)";
		funcReplace = "(.*)(__declspec)(.)(dllexport)(.)(.)";
		sourceJSON = NULL;
		sourceFileLocation = "";
		sourceFileName = "";
	}

	SourceParser::SourceParser(std::string location, std::string name)
	{
		funcPrecurse = "(.*)(__declspec)(.)(dllexport)(.*)";
		funcReplace = "(.*)(__declspec)(.)(dllexport)(.)(.)";
		sourceJSON = NULL;
		sourceFileLocation = location;
		sourceFileName = name;
	}

	void SourceParser::setSourceFileName(std::string name)
	{
		sourceFileName = name;
	}

	void SourceParser::setSourceFileLocation(std::string location)
	{
		sourceFileLocation = location;
	}

	std::string SourceParser::getSourceFileName()
	{
		return sourceFileName;
	}

	std::string SourceParser::getSourceFileLocation()
	{
		return sourceFileLocation;
	}

	void SourceParser::getFunctionsFromSource()
	{
		std::string line;
		//std::regex e("(.*)(string)(.*)");
		if (sourceFileName == "" || sourceFileLocation == "") {
			std::cout << "Filename or location empty" << std::endl;
			exit(EXIT_FAILURE);
		}
		else {
			std::ifstream inFile;
			inFile.open(sourceFileLocation + sourceFileName, std::ifstream::in);

			if (!inFile.is_open()) {
				std::cout << "File did not open correctly, this is the file: " << sourceFileLocation + sourceFileName << std::endl;
				exit(EXIT_FAILURE);
			}
			else {
				while (std::getline(inFile, line)) {
					std::cout << line << std::endl;
					if (std::regex_match(line, funcPrecurse)) {
						functionNames.push_back(line);
					}
				}
			}
		}
	}

	void SourceParser::printFunctionNames()
	{
		std::cout << "Functions from source: " << std::endl;
		for (int i = 0; i < parsedFunctions.size(); i++) {
			std::cout << "R: " << parsedFunctions.at(i).at(RETURN_TYPE) << " - ";
			std::cout << "FN: " << parsedFunctions.at(i).at(FUNCTION_NAME) << " - ";
			std::cout << "IP: " << parsedFunctions.at(i).at(INPUT_PARAMS);
			std::cout << std::endl;
		}
	}

	void SourceParser::getFunctionSections() {
		for (int i = 0; i < functionNames.size(); i++) {
			std::string tempString = functionNames.at(i);
			tempString = std::regex_replace(tempString, funcReplace, "");
			std::cout << tempString << std::endl;
			int spaceLoc = tempString.find_first_of(" ");
			int leftPar = tempString.find_first_of("(");
			int rightPar = tempString.find_first_of(")");
			std::string returnType = tempString.substr(0, spaceLoc);
			std::string functionName = tempString.substr(spaceLoc, leftPar - spaceLoc);
			functionName = std::regex_replace(functionName, std::regex(" "), ""); // Trim off white space
			std::string inputParams = tempString.substr(leftPar + 1, rightPar - leftPar - 1);

			std::vector<std::string> tempLineVec;

			tempLineVec.push_back(returnType);
			tempLineVec.push_back(functionName);
			tempLineVec.push_back(inputParams);
			parsedFunctions.push_back(tempLineVec);
			tempLineVec.clear();
		}
	}

	nlohmann::json SourceParser::sourceToJSON() {
		dllInfo info;
		info.name = getSourceFileName();
		info.location = getSourceFileLocation();
		for (int i = 0; i < parsedFunctions.size(); i++) {
			dllFunction func = { parsedFunctions.at(i).at(FUNCTION_NAME), parsedFunctions.at(i).at(RETURN_TYPE), parsedFunctions.at(i).at(INPUT_PARAMS) };
			info.functions.push_back(func);
		}

		nlohmann::json testingJSON = info;

		if (testingJSON != NULL) {
			std::cout << std::setw(4) << testingJSON << std::endl;
			return testingJSON;
		}
		else {
			std::cout << "Failed to print as the json variable is NULL." << std::endl;
			return NULL;
		}
	}
	void to_json(nlohmann::json& j, const dllInfo& d)
	{
		nlohmann::json tempJ;

		for (int i = 0; i < d.functions.size(); i++) {
			nlohmann::json tt = nlohmann::json{ {"name", d.functions.at(i).functionName}, {"returnType", d.functions.at(i).returnType}, {"passedIn", d.functions.at(i).parameters} };
			tempJ.push_back(tt);
		}


		//j = nlohmann::json{{"dll", {{"dllName", d.name},{"dllLocation", d.location},{"functions", d.functions}}}};
		j = nlohmann::json{ {"dll", {{"dllName", d.name}, {"dllLocation", d.location}, {"functions", tempJ}}} };

	}

	void from_json(const nlohmann::json& j, dllFunction& f) {
		j.at("name").get_to(f.functionName);
		j.at("passedIn").get_to(f.parameters);
		j.at("returnType").get_to(f.returnType);
	}

	void from_json(const nlohmann::json& j, dllInfo& d)
	{
		j.at("dll").get_to(d);
	}
}