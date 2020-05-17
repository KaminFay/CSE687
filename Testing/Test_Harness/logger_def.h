#ifndef LOGGER_DEF_H_
#define LOGGER_DEF_H_

#include <string>
#include <time.h>

class result_log
{
public:
    bool            pass;
    std::string     exception;
    time_t          start_time;
    time_t          completion_time;
    std::string     file;
    std::string     function;
    int             thread_id;

    result_log()
    {
        this->clear_log();
    }

    void clear_log()
    {
        pass = false;
        exception.clear();
        file.clear();
        function.clear();
        start_time = 0;
        completion_time = 0;
        thread_id = -1;
    }

};

class dll_info
{
public:
    std::string     dll_file;
    std::string     dll_function;

    dll_info()
    {
        dll_file.clear();
        dll_function.clear();
    }

    dll_info(std::string file_name, std::string function_name)
    {
        dll_file = file_name;
        dll_function = function_name;
    }

};

#endif