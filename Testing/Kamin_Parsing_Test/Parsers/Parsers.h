#pragma once
#include <iostream>
#include <fstream>
#include <iomanip>
#include <regex>
#include "json.hpp"

namespace Parsers {

	struct dllFunction {
		std::string functionName;
		std::string returnType;
		std::string parameters;
	};

	struct dllInfo {
		std::string name;
		std::string location;
		std::vector<dllFunction> functions;
	};

	class JSONParser {
	public:
		JSONParser();
		JSONParser(std::string, std::string);
		void testPrintingJSON();
		void setJsonFileName(std::string);
		void setJsonFileLocation(std::string);
		std::string parseDllFunctions();
		std::string getJsonFileName();
		std::string getJsonFileLocation();
		std::string parseStringFromJSON(std::string, std::string);
		void openJsonFile();
		void setJsonObject(nlohmann::json);
		dllInfo getSingleDll(std::string dllName);
		std::vector<dllInfo> getAllDlls();

	private:
		nlohmann::json j;
		std::string jsonFileName;
		std::string jsonFileLocation;
	};

	class SourceParser {
	public:
		SourceParser();
		SourceParser(std::string, std::string);
		void setSourceFileName(std::string);
		void setSourceFileLocation(std::string);
		std::string getSourceFileName();
		std::string getSourceFileLocation();
		void getFunctionsFromSource();
		void printFunctionNames();
		void getFunctionSections();
		nlohmann::json sourceToJSON();
		
	private:
		enum partials {
			RETURN_TYPE,
			FUNCTION_NAME,
			INPUT_PARAMS
		};

		nlohmann::json sourceJSON;
		std::regex funcPrecurse;
		std::regex funcReplace;
		std::vector<std::string> functionNames;
		std::vector<std::vector<std::string>> parsedFunctions;
		std::string sourceFileName;
		std::string sourceFileLocation;
	};

	void to_json(nlohmann::json& j, const dllInfo& d);
	void from_json(const nlohmann::json& j, dllInfo& d);
}

