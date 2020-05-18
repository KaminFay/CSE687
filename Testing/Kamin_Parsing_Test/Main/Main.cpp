// Main.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <Parsers.h>
#include <json.hpp>


static void glfw_error_callback(int error, const char* description)
{
    fprintf(stderr, "Glfw Error %d: %s\n", error, description);
}

int main()
{
    Parsers::JSONParser testJSON = Parsers::JSONParser("..\\Parsers\\", "TestingJSONTwo.json");
    Parsers::XMLParser testXML;
    Parsers::SourceParser testSource;

    testJSON.openJsonFile();

    std::cout << "source Location: " << testJSON.parseStringFromJSON("dll", "sourceLocation") << std::endl;
    std::cout << "source name: " << testJSON.parseStringFromJSON("dll", "sourceName") << std::endl;

    Parsers::dllInfo dllData = testJSON.getSingleDll("dllTwo");


    std::cout << "Dll Name: " << dllData.name << std::endl;
    std::cout << "DLL LOcation: " << dllData.location << std::endl;
    for (int i = 0; i < dllData.functions.size(); i++) {
        std::cout << "Func Name: " << dllData.functions.at(i).functionName << std::endl;
    }

    std::vector<Parsers::dllInfo> testingVec = testJSON.getAllDlls();

    std::cout << "------------Testing JSON File--------------" << std::endl;
    for (int i = 0; i < testingVec.size(); i++) {
        std::cout << "-------DLL Location---------" << std::endl;
        std::cout << testingVec.at(i).location << std::endl;
        std::cout << "-------Function--------" << std::endl;
        for (int j = 0; j < testingVec.at(i).functions.size(); j++) {
            std::cout << testingVec.at(i).functions.at(j).functionName << std::endl;
        }
    }
    std::cout << std::endl;
    std::cout << std::endl;
    //testJSON.testPrintingJSON();

    testSource.setSourceFileLocation("..\\Parsers\\");
    testSource.setSourceFileName("dll_ex1.txt");
    testSource.getFunctionsFromSource();
    testSource.getFunctionSections();
    //testSource.printFunctionNames();
    nlohmann::json testingTwo = testSource.sourceToJSON();

    testJSON.setJsonObject(testingTwo);
    
    std::vector<Parsers::dllInfo> testingSource = testJSON.getAllDlls();

    std::cout << "-----------Testing Source---------------" << std::endl;
    for (int i = 0; i < testingSource.size(); i++) {
        std::cout << "-------DLL Location---------" << std::endl;
        std::cout << testingSource.at(i).location << std::endl;
        std::cout << "-------Function--------" << std::endl;
        for (int j = 0; j < testingSource.at(i).functions.size(); j++) {
            std::cout << testingSource.at(i).functions.at(j).functionName << std::endl;
        }
    }
    //te

    return 0;
}