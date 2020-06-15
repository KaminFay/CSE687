#include <string>           
#include <iostream>         
#include <time.h>           //needed for getting time
#include <thread>           //needed in order to run threads

#include "logger_def.h"     //Contains struct for outbound results queue
#include "dll_control.h"    //Contains method for loading and unloading dlls
#include "blockingQueue.h"  //contains the thread blocking queues needed for the program
#include "HttpConnector.h"  //contains code for comms with api


////////////////////////////////////////////////////////////////////////////////////////////////////
//Class:        Test_Harness
//Description:  The Test Harness is the main control for the project
//              The test harness interface allows the user to start the harness with a certain 
//              number of threads, and a reference to an input and output queue.
//              The Harness will privately start up the threads and handle running the tests.
//              It will save the results of tests to the outbound queue after running.
////////////////////////////////////////////////////////////////////////////////////////////////////
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
        //create the number of threads requested and let them free run
        for (int x = 0; x < num_of_threads; x++)
        {
            std::thread worker_thread(Test_Harness::Tester_Thread_Proc, std::ref(dll_queue), std::ref(log_queue), x);
            worker_thread.detach();
        }

        return true;
    }//end Spinup_Threads

private:

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Tester_Thread_Proc
    //Description:  The process that the spawned worker threads will be executing
    //              waits for the next available object in the incoming blocking queue
    //              once it receives an object it will send it to the test executor to run
    //              The thread will take the result from the test executor and attempt to enqueue it
    //              to the outbound blocking queue.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    static bool Tester_Thread_Proc(BlockingQueue<dll_info>& dll_queue, BlockingQueue<result_log>& log_queue, int thread_id)
    {
        FNPTR       dll_function = NULL;
        result_log  results;
        dll_info    dll_info_inst;

        //threads will run indefinetly in this loop for entirety of program
        while (1)
        {

            //wait for available data from the input blocking queue
            //This is a blocking call
            dll_info_inst = dll_queue.deQ();

            //Only run the executor if we correctly load the dll. If we fail to load the dll, we will
            //set the logger exception to the returned exception and send that over
            try
            {
                dll_function = dll_control::load_dll(dll_info_inst.get_dll_name(), dll_info_inst.get_function_name());
                results = Test_Harness::Executor(dll_function);
            }
            catch (const char* error_msg)
            {
                results.exception = error_msg;
            }

            //set the rest of the logging results not related to actual test result
            results.thread_id = thread_id;
            results.function = dll_info_inst.dll_function;
            results.file = dll_info_inst.dll_file;
            results.databaseID = dll_info_inst.databaseID;

            //Add the result to the outgoing blocking queue
            log_queue.enQ(results);

            //clear the log before we start a new test
            results.clear_log();
        }
    }//end Test_Thread_Proc

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method:       Executor
    //Description:  Takes in a function pointer and tests the function pointed to in 
    //              a try, catch block. returns a results obj contained timestamped info,
    //              pass/fail status, and exception info
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    template<typename Function>
    static result_log Executor(Function& Test)
    {

        //variables
        result_log test_results;        //result log 
        char sbuff[20];                 //'string' for start time
        char ebuff[20];                 //'string' for end time
        time_t time_ptr= time(NULL);    //pointer needed for timegrab
        std::tm bt{};                   //struct that saves local time after conversion

        //start time code
        time(&time_ptr);                                //get time of test start
        localtime_s(&bt, &time_ptr);                    //convert epoch time to localtime format
        strftime(sbuff, 20, "%Y-%m-%d %H:%M:%S", &bt);  //convert localtime struct to string
        test_results.start_time = sbuff;                //save string to test results

        //run the dll test within a try catch so we can save exception if it fails
        try
        {
            test_results.pass       = Test();
        }
        catch (const char* error_msg)
        {
            test_results.exception  = error_msg;
        }

        //Completion time code
        time(&time_ptr);                                //get time test completed
        localtime_s(&bt, &time_ptr);                    //convert epoch time to localtime format
        strftime(ebuff, 20, "%Y-%m-%d %H:%M:%S", &bt);  //convert localtime struct to string
        test_results.completion_time = ebuff;           //save time string to test results

        return test_results;
    }//end Executor

};//end class Test_Harness



//Constants
const int C_NUM_OF_THREADS = 5; //Number of threads used for running tests


////////////////////////////////////////////////////////////////////////////////////////////////////
//Function:     main
//Description:  Starting point for the Test_Harness project. controls blocking queue setup, comm setup
//              Thread setup.
////////////////////////////////////////////////////////////////////////////////////////////////////
int main()
{
    //Create the input and output blocking queues
    BlockingQueue<dll_info>     input_queue;
    BlockingQueue<result_log>   output_queue;

    //Comms Setup
    HttpConnector connector (std::ref(input_queue), std::ref(output_queue));    //create connector class and initialize with the comms queues
    connector.getTestableFunctions();                                           //setup the RX thread to recieve dll info from the API
    connector.sendResults();                                                    //setup the TX thread to send logging results to API

    //Spinup the threads that will run the dlls
    Test_Harness::Spinup_Threads(C_NUM_OF_THREADS, std::ref(input_queue), std::ref(output_queue));

    //Sit in this loop for the rest of the program.
    //Nothing more to be done here, threads will handle all operations
    while (1)
    {
        //We are adding a sleep here so the loop doesn't hog resources
        Sleep(100);
    }
}