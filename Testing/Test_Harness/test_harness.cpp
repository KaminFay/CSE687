#include <string>
#include <iostream>
#include <time.h>
#include <Windows.h>
#include <thread>

#include "logger_def.h"
#include "dll_control.h"
#include "blockingQueue.h"
#include "ThreadPool.h"



class Test_Harness
{
public:

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Tester_Thread_Proc
    //Description:  The process that the spawned worker threads will be executing
    //              waits for the next available object in the incoming blocking queue
    //              once it receives an object it will send it to the test executor to run
    //              The thread will take the result from the test executor and attempt to enqueue it
    //              to the outbound blocking queue.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //static bool Tester_Thread_Proc(dll_info dll_info_obj)
    static bool Tester_Thread_Proc(BlockingQueue<dll_info> &dll_queue, BlockingQueue<result_log>& log_queue)
    {
        FNPTR       dll_function    = NULL;
        result_log  results;
        dll_info    dll_info_inst;

        //We could add a terminating concdition in here. Not needed though. Maybe during final polish
        //Maybe add in a pointer arguement that points to terminating condition
        while (1)
        {

            //wait for available data from the input blocking queue
            dll_info_inst = dll_queue.deQ();

            //Only run the executor if we correctly load the dll. If we fail to load the dll, we will
            //set the logger exception to the returned exception and send that over
            try
            {
                dll_function    = dll_control::load_dll(dll_info_inst.dll_file, dll_info_inst.dll_function);
                results         = Test_Harness::Executor(dll_function);
            }
            catch (const char* error_msg)
            {
                results.exception = error_msg;
            }

            /******ADD CODE***************/
            //wait to place log data on outbound blocking queue
            /******ADD CODE***************/

            /******TEMP CODE***************/
            //Temp sleep while we have no blocking queue waits in place
            Sleep(500);

            log_queue.enQ(results);

            ///////print out the results
            //std::cout << "TEST 1:" << std::endl;
            //std::cout << "The result of the test was: " << std::boolalpha << results.pass << std::endl;
            //if(results.exception != "")
            //{
            //    std::cout << results.exception << std::endl;
            //}
            //else
            //{
            //    std::cout << "There was no exception thrown" << std::endl;
            //}
            //std::cout << "The test was started at: " << results.start_time << std::endl;
            //std::cout << "The test was finsihed at: " << results.completion_time << std::endl;

            //std::cout <<"\n\n";
            /******TEMP CODE***************/

            //clear the log before we start a new test
            results.clear_log();

            
        }

    }

private:

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Executor
    //Description:  Takes in a function pointer and tests the function pointed to in 
    //              a try, catch block. returns a results obj contained timestamped info,
    //              pass/fail status, and exception info
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    template<typename Function>
    static result_log Executor(Function& Test)
    {
        result_log test_results;

        time_t time_ptr;
        time(&time_ptr);
        test_results.start_time = time_ptr;

        try
        {
            test_results.pass = Test;
        }
        catch (const char* error_msg)
        {
            test_results.exception = error_msg;
        }

        time(&time_ptr);
        test_results.completion_time = time_ptr;

        return test_results;
    }
};


int main()
{
    ////Variable Declarations
    //Test_Greater_Than   test_inst(5,2);
    //Test_Greater_Than   exception_test_inst(8,8);
    //result_log          test_results;
    //result_log          test_results2;
    //Test_Harness        test_harness_obj;

    //Sleep(2000);

    /////print out the results
    //std::cout << "TEST 1:" << std::endl;
    //std::cout << "The result of the test was: " << std::boolalpha << test_results.pass << std::endl;
    //if(test_results.exception != "")
    //{
    //    std::cout << test_results.exception << std::endl;
    //}
    //else
    //{
    //    std::cout << "There was no exception thrown" << std::endl;
    //}
    //std::cout << "The test was started at: " << test_results.start_time << std::endl;
    //std::cout << "The test was finsihed at: " << test_results.completion_time << std::endl;

    //std::cout <<"\n\n";

    BlockingQueue<dll_info>     input_queue;
    BlockingQueue< result_log>  output_queue;

    std::string dll_file_path = "C:\\cygwin64\\home\\Austin\\grad_school\\github\\CS687\\CSE687\\Testing\\dll_files\\dll_instant_pass.dll";
    std::string dll_function_name = "Instant_Pass";

    dll_info dll_info_inst(dll_file_path, dll_function_name);

    for (int i = 0; i < 10; ++i)
    {
        input_queue.enQ(dll_info_inst);
    }
    
    std::thread worker_thread(Test_Harness::Tester_Thread_Proc, std::ref(input_queue), std::ref(output_queue));

    //bool return_val = Test_Harness::Tester_Thread_Proc(input_queue, output_queue);

    result_log test_results;
    while (1)
    {
        test_results = output_queue.deQ();

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

        Sleep(100);
    }

    worker_thread.join();


}