///////////////////////////////////////////////////////////////
// BlockingQueue.cpp / Test.cpp                              //
// Thread-safe Blocking Queue                                //
// Ralph Walker II                                           //
//                                                           //
// Reference Jim Fawcett                                     //
///////////////////////////////////////////////////////////////

#include <condition_variable>
#include <mutex>
#include <thread>
#include <queue>
#include <string>
#include <iostream>
#include <sstream>
#include <functional>
#include "BlockingQueue.h"
#include "Utilities.h"
#include "ThreadPool.h"
#include "Task.h"
#include "Logger.h"

using Util = Utilities::StringHelper;
using namespace Async;

std::mutex ioLock;

// test deQing string BlockingQ thread until quit
void test(BlockingQueue<std::string>* pQ)
{
    std::string msg;
    do
    {
        msg = pQ->deQ();
        {
            std::lock_guard<std::mutex> l(ioLock);
            std::cout << "\n  thread deQed " << msg.c_str();
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(10));
    } while (msg != "quit");
}




int main()
{
    Util::Title("Demonstrating Blocking Queue", '=');
    
    BlockingQueue<std::string> q;
    std::thread t(test, &q);

    for (int i = 0; i < 15; ++i)
    {
        std::ostringstream temp;
        temp << i;
        std::string msg = std::string("msg#") + temp.str();
        {
            std::lock_guard<std::mutex> l(ioLock);
            std::cout << "\n   main enQing " << msg.c_str();
        }
        q.enQ(msg);
        std::this_thread::sleep_for(std::chrono::milliseconds(3));
    }
    q.enQ("quit");
    t.join();          // blocks until thread t completes; Exits its thread function.

    std::cout << "\n";
    std::cout << "\n  Making move copy of BlockingQueue";
    std::cout << "\n -----------------------------------";

    std::string msg = "test";
    q.enQ(msg);
    std::cout << "\n  before move:";
    std::cout << "\n    q.size() = " << q.size();
    std::cout << "\n    q.front() = " << q.front();
    BlockingQueue<std::string> q2 = std::move(q);  // move assignment
    std::cout << "\n  after move:";
    std::cout << "\n    q2.size() = " << q2.size();
    std::cout << "\n    q.size() = " << q.size();
    std::cout << "\n    q2 element = " << q2.deQ() << "\n";

    std::cout << "\n  Move assigning state of BlockingQueue";
    std::cout << "\n ---------------------------------------";
    BlockingQueue<std::string> q3;
    q.enQ("test");
    q.enQ("test2");
    std::cout << "\n  before move:";
    std::cout << "\n    q.size() = " << q.size();
    std::cout << "\n    q.front() = " << q.front();
    q3 = std::move(q);
    std::cout << "\n  after move:";
    std::cout << "\n    q.size() = " << q.size();
    std::cout << "\n    q3.size() = " << q3.size();
    std::cout << "\n    q3 element = " << q3.deQ() << "\n";

    std::cout << "\n\n";

    system("Pause");

    Show::attach(&std::cout);
    Show::start();
    DebugLog::attach(&std::cout);
    DebugLog::start();

    Util::Title("Testing ThreadPool");

    ThreadPool<5> trpl;

    ThreadPool<5>::CallObj co = [&trpl]() ->bool {
        Show::write(
            "\n  hello from thread " +
            Utilities::Converter<std::thread::id>::toString(std::this_thread::get_id())
        );
        return true;
    };

    for (size_t i = 0; i < 20; ++i)
        trpl.workItem(co);

    ThreadPool<5>::CallObj exit = []() ->bool { return false; };
    trpl.workItem(exit);
    trpl.wait();

    std::cout << "\n\n";

    system("Pause");
    
    Util::Title("Testing Logger Class");
    Logger log;
    log.attach(&std::cout);
    log.write("\n  won't get logged - not started yet");
    log.start();
    log.title("Testing Logger Class", '=');
    log.write("\n  one");
    log.write("\n  two");
    log.write("\n  final");
    log.stop();
    log.write("\n  won't get logged - stopped");
    log.start();
    log.write("\n  starting again");
    log.write("\n  and stopping again");
    log.stop("\n  terminating now");

    StaticLogger<1>::attach(&std::cout);
    StaticLogger<1>::start();
    StaticLogger<1>::write("\n");
    StaticLogger<1>::title("Testing StaticLogger class");
    StaticLogger<1>::write("\n  static logger at work");
    Logger& logger = StaticLogger<1>::instance();
    logger.write("\n  static logger still at work");
    logger.stop("\n  stopping static logger");

}
