#include <string>
#include <iostream>
#include "String_Manip.h"


std::string String_Manip::Get_Path_Ending(std::string in_string)
{
    std::string out_string;

    for (int i = 0; i < in_string.size(); i++)
    {
        out_string.push_back(in_string[i]);
        if (in_string[i] == '\\')
        {
            out_string.clear();
        }
    }

    return out_string;
}


std::string String_Manip::Remove_Filetype(std::string in_string)
{
    std::string out_string;

    for (int i = 0; i < in_string.size(); i++)
    {
        if (in_string[i] == '.')
        {
            return out_string;
        }

        out_string.push_back(in_string[i]);
    }


    return out_string;

}

int main()
{
    std::string dll_file_path = "C:\\cygwin64\\home\\Austin\\grad_school\\github\\CS687\\CSE687\\Testing\\dll_files\\dll_instant_pass.dll";
    std::cout << String_Manip::Remove_Filetype(String_Manip::Get_Path_Ending(dll_file_path));


    return 0;
}