#include "dll_instant_fail.h"
#include <stdlib.h>

extern "C"
{
    __declspec(dllexport) bool Instant_Fail()
    {
        return false;
    }
}