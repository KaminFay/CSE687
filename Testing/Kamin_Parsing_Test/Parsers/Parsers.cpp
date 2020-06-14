// Parsers.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include "Parsers.h"
#include <ctime>
#include <sstream>
#include <locale>
#include <codecvt>

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

	/**
	*
	* This funciton sets the name of the JSON file being parsed.
	*
	* @param name -- passed in name
	* @return 
	*/
	void JSONParser::setJsonFileName(std::string name) {
		jsonFileName = name;
	}


	/**
	*
	* This funciton getss the name of the JSON file being parsed.
	*
	* @param 
	* @return -- jsonFileName -- String containing the file name
	*/
	std::string JSONParser::getJsonFileName() {
		return jsonFileName;
	}

	/**
	*
	* This funciton sets the path of the JSON file being parsed.
	*
	* @param -- location -- string containing the windows path not including the file name
	* @return
	*/
	void JSONParser::setJsonFileLocation(std::string location) {
		jsonFileLocation = location;
	}

	/**
	*
	* This funciton gets the path of the JSON file being parsed.
	*
	* @param 
	* @return -- jsonFileLocation -- string containing the windows path not including the file name
	*/
	std::string JSONParser::getJsonFileLocation() {
		return jsonFileLocation;
	}

	/**
	*
	* This funciton will set the local JSON object if there is already one provided.
	*
	* @param -- passedInJSON -- JSON object containing all of the required dll info
	* @return 
	*/
	void JSONParser::setJsonObject(nlohmann::json passedInJSON) {
		j = passedInJSON;
	}

	/**
	*
	* Get a single string from the JSON based on the higher level name and the inner key.
	* If it fails it will return a NULL value.
	* Ex. The below json and function call will result in a return of "..\\parsers\\"
	*
	* "dllTwo": {
    *	"sourceLocation": "..\\Parsers\\",
    *	"sourceName": "dll_ex1.txt",
	*
	* parseStringFromJSON("dllTwo", "sourceLocation")
	*
	*
	* @param -- fromDLL -- string containing the higher level key needed (in case of this project the dllName)
	* @param -- keyString -- string containing the lower level key needed (in this case what we are looking to find the value of)
	* @return
	*/
	std::string JSONParser::parseStringFromJSON(std::string fromDLL, std::string keyString) {
		if (j.find(fromDLL) != j.end()) {
			std::string valueString = j[fromDLL][keyString].get<std::string>();
			return valueString;
		}
		else {
			return "NULL";
		}

	}

	/**
	*
	* Getting all of the data from the JSON object. This will loop through each of the dll's that are within
	* the JSON object parse that data using the getSingleDll function and store all of the data into a vector of 
	* dllInfo objects.
	*
	* @param 
	* @return -- allDllVec -- A Vector containing dllInfo objects that hold all of the data for each dll
	*/
	std::vector<dllInfo> JSONParser::getAllDlls() {
		std::vector<dllInfo> allDLLVec;

		std::cout << "Testing the print for the high level dll names. -----" << std::endl << std::endl;

		for (auto& item : j.items()) {
			std::cout << item.key() << std::endl;
			allDLLVec.push_back(getSingleDll(item.key()));
		}

		return allDLLVec;
	}


	
	/**
	*
	* Given a single dllName this function will parse the data from that dll section of the JSON into a dllInfo
	* object. This object will then be returned.
	* 
	* @param -- dllName -- string containing the name of the dll to be found within the JSON object
	* @return -- newDll -- dllInfo object that contains the demarshalled info from the JSON object for the given dll
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

	/**
	*
	* Once jsonFileName and jsonFileLocation have been assigned we can open the JSON file in question
	* this data will then be stored into the JSON object (j).
	*
	* @param
	* @return
	*/
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

	/**
	*
	* Once the JSON file has been marshalled into the JSON object this function can be used to test print the 
	* JSON into the terminal in a "pretty" format
	*
	* @param
	* @return
	*/
	void JSONParser::testPrintingJSON() {
		if (j != NULL) {
			std::cout << std::setw(4) << j << std::endl;
		}
		else {
			std::cout << "Failed to print as the json variable is NULL." << std::endl;
		}
	}

	std::string JSONParser::resultObjectToJSONString(result_log result) {
		nlohmann::json jsonObject;
		jsonObject["ID"] = result.databaseID;
		jsonObject["FuncName"] = result.function;
		jsonObject["DllName"] = result.file;
		jsonObject["DllPath"] = result.file;
		jsonObject["Result"] = result.pass;
		jsonObject["Exception"] = result.exception;
		jsonObject["StartTime"] = result.start_time;
		jsonObject["EndTime"] = result.completion_time;

		std::cout << "Object being sent: " << std::endl;
		std::cout << jsonObject.dump(4) << std::endl;

		return jsonObject.dump();
	}

	std::vector<dll_info> JSONParser::jsonStringToFunctionObject(std::string jsonString) {
		auto jsonObject = nlohmann::json::parse(jsonString);
		std::vector<dll_info> passedInFunctions;

		for (auto& jsonFunc : jsonObject) {
			dll_info func;
			func.databaseID = jsonFunc["ID"].get<int>();
			func.dll_file = jsonFunc["DllPath"].get<std::string>();
			func.dll_function = jsonFunc["FuncName"].get<std::string>();
			passedInFunctions.push_back(func);
		}

		return passedInFunctions;
	}

	/**
	*
	* Base intilization of the SourceParser Class, 
	*
	* @param
	* @return
	*/
	SourceParser::SourceParser()
	{
		funcPrecurse = "(.*)(__declspec)(.)(dllexport)(.*)";
		funcReplace = "(.*)(__declspec)(.)(dllexport)(.)(.)";
		sourceJSON = NULL;
		sourceFileLocation = "";
		sourceFileName = "";
	}

	/**
	*
	* Customized intilization of the SourceParser Class, in this instance the path and name of the c++ source 
	* file is included to intiliaze that data with fewer function calls
	*
	* @param -- location -- Windows path to the location of the c++ source file (file name not included)
	* @param -- name -- Name of the physical c++ source file
	* @return
	*/
	SourceParser::SourceParser(std::string location, std::string name)
	{
		funcPrecurse = "(.*)(__declspec)(.)(dllexport)(.*)";
		funcReplace = "(.*)(__declspec)(.)(dllexport)(.)(.)";
		sourceJSON = NULL;
		sourceFileLocation = location;
		sourceFileName = name;
	}

	/**
	*
	* This funciton sets the name of the C++ source file being parsed.
	*
	* @param name -- passed in name
	* @return
	*/
	void SourceParser::setSourceFileName(std::string name)
	{
		sourceFileName = name;
	}

	/**
	*
	* This funciton sets the file location of the C++ source file being parsed.
	*
	* @param name -- Windows path of the c++ source file (file name not included)
	* @return
	*/
	void SourceParser::setSourceFileLocation(std::string location)
	{
		sourceFileLocation = location;
	}

	/**
	*
	* This funciton gets the name of the C++ source file being parsed.
	*
	* @param 
	* @return sourceFileName -- name of the currently being parsed source file
	*/
	std::string SourceParser::getSourceFileName()
	{
		return sourceFileName;
	}

	/**
	*
	* This funciton gets the location of the C++ source file being parsed.
	*
	* @param
	* @return sourceFileLocation -- Windows path of the currently being parsed source file (file name not included)
	*/
	std::string SourceParser::getSourceFileLocation()
	{
		return sourceFileLocation;
	}

	/**
	*
	* Once the source file is known this will read in the file and push each line onto a local vector of strings.
	* Each string will be matched with the a regex that will locate this precurser on the function tags "__declspec(dllexport)"
	*
	* @param
	* @return
	*/
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

	/**
	*
	* Sample functions to print the data parsed from the C++ source file once that is completed.
	* !WARNING -- This is just for testing, no practical function at this time.
	*
	* @param
	* @return
	*/
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

	/**
	*
	* Given the vector of function tags this will parse those tags for the three main portions (return type, function name, input params)
	* These three portions will be pushed into a 2d vector containing each line and their corresponding parts.
	* 
	* @param
	* @return
	*/
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

	/**
	*
	* The parsedFunction vector will be iterated over and it's data marshalled into a JSON object that is passed back to be used
	* for later purposes.
	* @param
	* @return -- testingJSON -- JSON object containg the function info from the C++ source file being parsed.
	*/
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

	/**
	*
	* Overloaded function from the nlohmann::json library
	* This function will convert from a dllInfo object into a standard JSON object
	*
	* @param -- &j -- reference to the JSON object that needs to be written into
	* @param -- &d -- reference to the dllInfo object that is to be read from
	* @return
	*/
	void to_json(nlohmann::json& j, const dllInfo& d)
	{
		nlohmann::json tempJ;

		for (int i = 0; i < d.functions.size(); i++) {
			nlohmann::json tt = nlohmann::json{ {"name", d.functions.at(i).functionName}, {"returnType", d.functions.at(i).returnType}, {"passedIn", d.functions.at(i).parameters} };
			tempJ.push_back(tt);
		}

		j = nlohmann::json{ {"dll", {{"dllName", d.name}, {"dllLocation", d.location}, {"functions", tempJ}}} };

	}

	/**
	*
	* TODO -- This needs to be looked into as it is not working as intended.
	*
	* Overloaded function from the nlohmann::json library
	* This should convert from JSON to the dllInfo object type
	*
	* @param -- &j -- constant reference to the json that needs to be translated
	* @param -- &d -- reference to the dllFunction object the data should be marshalled into
	* @return
	*/
	void from_json(const nlohmann::json& j, dllFunction& f) {
		j.at("name").get_to(f.functionName);
		j.at("passedIn").get_to(f.parameters);
		j.at("returnType").get_to(f.returnType);
	}


	/**
	*
	* TODO -- This needs to be looked into as it is not working as intended.
	*
	* Overloaded function from the nlohmann::json library
	* This should convert from JSON to the dllInfo object type
	*
	* @param -- &j -- constant pointer to the json that needs to be translated
	* @param -- &d -- pointer to the dllInfo object the data should be marshalled into
	* @return
	*/
	void from_json(const nlohmann::json& j, dllInfo& d)
	{
		j.at("dll").get_to(d);
	}
}