#include <Windows.h>
#include <iostream>

typedef std::string (*FNPTR)(int);

int main()
{
    HINSTANCE hInst = LoadLibrary(L"C:\\Users\\Austin\\source\\repos\\dll_project.dll");

    if (!hInst)
    {
        std::cout << "\nCould not load the library";
        return 1;
    }

    //Resolve the function address
    FNPTR fn = (FNPTR)(GetProcAddress(hInst, "Test1"));
    if (!fn)
    {
        std::cout << "\nCould not locate the function";
        return 1;
    }

    int test_value = 0;
    std::cout << "\nPlease enter a number to test: ";
    std::cin>>test_value;
    std::string result = fn(test_value);
    std::cout << "\nThe DLL result was: " << result;

    return 0;
}