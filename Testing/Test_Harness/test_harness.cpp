#include <string>
#include <iostream>
#include <time.h>
#include <Windows.h>


class result_obj
{
public:
    bool            pass;
    std::string     exception;
    time_t          start_time;
    time_t          completion_time;

    result_obj()
    {
        pass            = false;
        exception.clear();
        start_time      = 0;
        completion_time = 0;
    }
};

class Test_Greater_Than
{
private:
    int x, y;

public:
    Test_Greater_Than(int temp_x, int temp_y)
    {
        x = temp_x;
        y = temp_y;
    }

    bool operator()()
    {
        if (x > y)
        {
            return true;
        }
        else if (x == y)
        {
            throw "Cannot use the same value for this test";
        }
        else
        {
            return false;
        }
    }
};

class Test_Harness
{
public:

    template<typename Function>
    result_obj Executor(Function& Test)
    {
        result_obj test_results;

        time_t time_ptr;
        time(&time_ptr);
        test_results.start_time = time_ptr;

        try
        {
            test_results.pass = Test();
        }
        catch (const char* error_msg)
        {
            test_results.exception = error_msg;
        }

        time(&time_ptr);
        test_results.completion_time = time_ptr;

        return test_results;
    }

private:

};


int main()
{
    //Variable Declarations
    Test_Greater_Than   test_inst(5,2);
    Test_Greater_Than   exception_test_inst(8,8);
    result_obj          test_results;
    result_obj          test_results2;
    Test_Harness        test_harness_obj;

    test_results    = test_harness_obj.Executor(test_inst);

    Sleep(2000);

    test_results2   = test_harness_obj.Executor(exception_test_inst);

    ///print out the results
    std::cout << "TEST 1:" << std::endl;
    std::cout << "The result of the test was: " << std::boolalpha << test_results.pass << std::endl;
    if(test_results.exception != "")
    {
        std::cout << test_results.exception << std::endl;
    }
    else
    {
        std::cout << "There was no exception thrown" << std::endl;
    }
    std::cout << "The test was started at: " << test_results.start_time << std::endl;
    std::cout << "The test was finsihed at: " << test_results.completion_time << std::endl;

    std::cout <<"\n\n";

    ///print out the results
    std::cout << "TEST 2:" << std::endl;
    std::cout << "The result of the test was: " << std::boolalpha << test_results2.pass << std::endl;
    if (test_results2.exception != "")
    {
        std::cout << "Exception: " << test_results2.exception << std::endl;
    }
    else
    {
        std::cout << "There was no exception thrown" << std::endl;
    }
    std::cout << "The test was started at: " << test_results2.start_time << std::endl;
    std::cout << "The test was finsihed at: " << test_results2.completion_time << std::endl;


}