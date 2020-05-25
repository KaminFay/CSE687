#include <string>
#include <iostream>
#include <time.h>
#include <WinSock2.h>
#include <Windows.h>
#include <thread>


#include "logger_def.h"
#include "dll_control.h"
#include "blockingQueue.h"
#include "ThreadPool.h"
#include "Sockets.h"

using namespace Sockets;



class Test_Harness
{
public:

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Spinup_Threads
    //Description:  We will spin up the required number of threads here 
    //              For base functionality we do not need to mangage/destroy threads so this process
    //              will be relatively simple. Linkning In and Out queue as params.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    static bool Spinup_Threads(int num_of_threads, BlockingQueue<dll_info>& dll_queue, BlockingQueue<result_log>& log_queue)
    {
        for (int x = 0; x < num_of_threads; x++)
        {
            std::thread worker_thread(Test_Harness::Tester_Thread_Proc, std::ref(dll_queue), std::ref(log_queue), x);
            worker_thread.detach();
        }

        return true;
    }//end Spinup_Threads



    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Tester_Thread_Proc
    //Description:  The process that the spawned worker threads will be executing
    //              waits for the next available object in the incoming blocking queue
    //              once it receives an object it will send it to the test executor to run
    //              The thread will take the result from the test executor and attempt to enqueue it
    //              to the outbound blocking queue.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //static bool Tester_Thread_Proc(dll_info dll_info_obj)
    static bool Tester_Thread_Proc(BlockingQueue<dll_info> &dll_queue, BlockingQueue<result_log>& log_queue, int thread_id)
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

            //TODO: have this stay set and not cleared so we don't have to reset every time.
            results.thread_id = thread_id;

            //Add the result to the outgoing blocking queue
            log_queue.enQ(results);

            //clear the log before we start a new test
            results.clear_log();
        }
    }//end Test_Thread_Proc

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
            test_results.pass = Test();
        }
        catch (const char* error_msg)
        {
            test_results.exception = error_msg;
        }

        time(&time_ptr);
        test_results.completion_time = time_ptr;

        return test_results;
    }//end Executor

};//end class Test_Harness



//Constants
const int C_NUM_OF_THREADS = 5;


////////////////////////////////////////////////////////////////////////////////////////////////////
//Function:     main
//Description:  Starting point for the Test_Harness project. controls blocking queue setup, comm setup
//              Thread setup.
////////////////////////////////////////////////////////////////////////////////////////////////////
int main()
{
    BlockingQueue<dll_info>     input_queue;
    BlockingQueue< result_log>  output_queue;

    /*TEMP DEUG*/
    std::string dll_file_path = "C:\\cygwin64\\home\\Austin\\grad_school\\github\\CS687\\CSE687\\Testing\\dll_files\\dll_long_delay.dll";
    std::string dll_function_name = "Run_Test";
    dll_info dll_info_inst(dll_file_path, dll_function_name);
    for (int i = 0; i < 20; ++i)
    {
        input_queue.enQ(dll_info_inst);
    }
    /*TEMP DEBUG*/

    //TODO: SET UP COMMS
    /*INSERT COMM SETUP CODE HERE*/

    SocketSystem ss;


    /*INSERT COMM SETUP CODE HERE*/


    //Start up the threads that will stay running for the entirety of the program
    //Pass them a reference to our input and output queues so they can operate without any guidance
    Test_Harness::Spinup_Threads(C_NUM_OF_THREADS, std::ref(input_queue), std::ref(output_queue));


    //Sit in this loop for the rest of the program.

    /*TEMP DEBUG*/
    result_log test_results;
    while (1)
    {
        test_results = output_queue.deQ();

        std::cout << "Function run by thread: " << test_results.thread_id << std::endl;
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
    /*TEMP DEBUG*/


}