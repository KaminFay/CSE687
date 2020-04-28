#include <iostream>
#include "../pugixml-1.10/pugixml-1.10/src/pugixml.hpp"

int main()
{
    pugi::xml_document doc;

    pugi::xml_parse_result result = doc.load_file("XMLFile1.xml");

    pugi::xml_node root = doc.child("mesh");

    std::cout << "Load result: " << result.description() << ", mesh name: "<<doc.child("mesh").child("node").attribute("attr1").value()<<std::endl;
    std::cout << "Load result: " << result.description() << ", mesh name: " << doc.child("mesh").child("node").attribute("attr1").value() << std::endl;
    
    for (pugi::xml_node Node = root.child("node"); Node; Node = Node.next_sibling("node"))
    {
        std::cout << "node attribute 1 = " << Node.attribute("attr1").value() << std::endl;
    }
    return 0;
}