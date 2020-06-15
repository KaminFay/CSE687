#include "auto_pass_delayed.h"
#include <Windows.h>

extern "C"
{
    __declspec(dllexport) bool auto_pass_delayed()
    {
        Sleep(2000);
        return true;
    }
}