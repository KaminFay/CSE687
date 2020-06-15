#include "dll_instant_pass.h"
#include <stdlib.h>

extern "C"
{
    __declspec(dllexport) bool Instant_Pass()
    {
        return true;
    }
}