#ifndef LOGGER_DEF_H_
#define LOGGER_DEF_H_

#include <string>
#include <time.h>

class result_log
{
public:
    int             databaseID;
    bool            pass;
    std::string     exception;
    std::string     start_time;
    std::string     completion_time;
    std::string     file;
    std::string     function;
    int             thread_id;

    result_log()
    {
        this->clear_log();
    }

    void clear_log()
    {
        databaseID = 0;
        pass = false;
        exception.clear();
        file.clear();
        function.clear();
        start_time.clear();
        completion_time.clear();
    }

};

class dll_info
{
public:
    int             databaseID;
    std::string     dll_file;
    std::string     dll_function;

    dll_info()
    {
        databaseID = 0;
        dll_file.clear();
        dll_function.clear();
    }

    dll_info(std::string file_name, std::string function_name)
    {
        dll_file = file_name;
        dll_function = function_name;
    }

    std::string get_dll_name() {
        return dll_file;
    }

    std::string get_function_name() {
        return dll_function;
    }

};

#endif