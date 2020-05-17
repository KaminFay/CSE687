#ifndef DLL_CONTROL_H_
#define DLL_CONTROL_H_

#include <string>

typedef bool (*FNPTR)(void);

class dll_control
{
public:

    static FNPTR load_dll(std::string dll_path, std::string dll_function);
    static bool  unload_dll(std::string dll_path);
};

#endif
