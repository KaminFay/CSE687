#include <iostream>
#include <Windows.h>
#include <string>

#include "dll_control.h"

FNPTR dll_control::load_dll(std::string dll_path, std::string dll_function)
{
    FNPTR fp;

    //CONVERT FROM C_STR TO WSTR
    size_t origsize1 = strlen(dll_path.c_str()) + 1;  //get length of original string
    const size_t newsize = 120;                                                                     //max size of wstr
    size_t convertedChars = 0;                                                                      //return value for conversion
    wchar_t wstr_lib_path_1[newsize];                                                               //wstr variable for library
    mbstowcs_s(&convertedChars, wstr_lib_path_1, origsize1, dll_path.c_str(), _TRUNCATE);         //conversion function for library

    //Load library and check it worked
    HINSTANCE hInst1 = LoadLibrary(wstr_lib_path_1);
    if (!hInst1)
    {
        throw "Could not load the dll library";
    }

    //Get the address of the function we want to run and check it worked
    fp = (FNPTR)(GetProcAddress(hInst1, dll_function.c_str()));
    if (!fp)
    {
        throw "Could not locate the dll function";
    }

    return fp;
}

//In order to unload the dll, we will have to reload the file to get its pointer
bool dll_control::unload_dll(std::string dll_path)
{
    FNPTR fp;

    //CONVERT FROM C_STR TO WSTR
    size_t origsize1 = strlen(dll_path.c_str()) + 1;                                        //get length of original string
    const size_t newsize = 120;                                                             //max size of wstr
    size_t convertedChars = 0;                                                              //return value for conversion
    wchar_t wstr_lib_path_1[newsize];                                                       //wstr variable for library
    mbstowcs_s(&convertedChars, wstr_lib_path_1, origsize1, dll_path.c_str(), _TRUNCATE);   //conversion function for library

    //Load library and check it worked
    HINSTANCE hInst1 = LoadLibrary(wstr_lib_path_1);
    if (!hInst1)
    {
        return false;
    }

    //we need to keep calling FreeLibrary until its reference count reaches zero
    //we will technically call one extra time to make sure we have surpassed zero and it has terminated
    bool references_exist = true;
    int free_count = 0;
    while (references_exist == true)
    {
        references_exist = FreeLibrary(hInst1);
        if (references_exist)
        {
            free_count++;
        }

    }

    //If we did not sucessfully free at least twice, then there was an error
    if (free_count < 2)
    {
        return true;
    }
    else
    {
        return false;
    }
}