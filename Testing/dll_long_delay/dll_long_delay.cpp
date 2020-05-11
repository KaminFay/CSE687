#include "dll_long_delay.h"
#include <stdlib.h>
#include <Windows.h>

extern "C"
{
    __declspec(dllexport) bool Run_Test()
    {
        int pass_fail = rand() % 2;

        Sleep(5000);

        switch (pass_fail)
        {
        case 0:
            return true;
        case 1:
            return false;
        default:
            throw "receieved an impossible exception";
        }
    }
}