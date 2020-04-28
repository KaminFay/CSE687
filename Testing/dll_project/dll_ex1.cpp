#include "dll_ex1.h"

extern "C"
{
    __declspec(dllexport) std::string Test1(const int x)
    {
        std::string result = "NULL";
        if (x % 2 == 0)
        {
            result = "even";
        }
        else
        {
            result = "odd";
        }

        return result;
    }
}