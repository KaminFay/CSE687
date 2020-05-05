#include "throw_exception.h"
#include <stdlib.h>

extern "C"
{
    __declspec(dllexport) bool throw_exception()
    {
        int exception = rand() % 4;
        
        switch (exception)
        {
            case 0:
                throw "received exception 0";
            case 1:
                throw "received exception 1";
            case 2:
                throw "recieved exception 2";
            case 3:
                throw "received exception 3";
            default:
                throw "receieved an impossible exception";
        }

        return true;
    }
}