/////////////////////////////////////////////////////////////////////
// Logger.cpp - log text messages to std::ostream                  //
// ver 1.0                                                         //
//-----------------------------------------------------------------//
// Jim Fawcett (c) copyright 2015                                  //
// All rights granted provided this copyright notice is retained   //
//-----------------------------------------------------------------//
// Language:    C++, Visual Studio 2015                            //
// Application: Several Projects, CSE687 - Object Oriented Design  //
// Author:      Jim Fawcett, Syracuse University, CST 4-187        //
//              jfawcett@twcny.rr.com                              //
/////////////////////////////////////////////////////////////////////

#include <functional>
#include "Logger.h"
#include "Utilities.h"

//----< initialized privated data with constructor >---------------
Logger::Logger()
{
    std::thread* _pThr = NULL;
    std::ostream* _pOut = NULL;
}

//----< send text message to std::ostream >--------------------------
void Logger::write(const std::string& msg)
{
    if (_ThreadRunning)
        _queue.enQ(msg);
}
void Logger::title(const std::string& msg, char underline)
{
    std::string temp = "\n  " + msg + "\n " + std::string(msg.size() + 2, underline);
    write(temp);
}

//----< attach logger to existing std::ostream >---------------------
void Logger::attach(std::ostream* pOut)
{
    _pOut = pOut;
}

//----< start logging >----------------------------------------------
void Logger::start()
{
    if (_ThreadRunning)
        return;
    _ThreadRunning = true;
    std::function<void()> tp = [=]() {
        while (true)
        {
            std::string msg = _queue.deQ();
            if (msg == "quit")
            {
                _ThreadRunning = false;
                break;
            }
            *_pOut << msg;
        }
    };
    std::thread thr(tp);
    thr.detach();
}

//----< stop logging >-----------------------------------------------
void Logger::stop(const std::string& msg)
{
    if (_ThreadRunning)
    {
        if (msg != "")
            write(msg);
        write("quit");  // request thread to stop
        while (_ThreadRunning)
            /* wait for thread to stop*/
            ;
    }
}

//----< stop logging thread >----------------------------------------
Logger::~Logger()
{
    stop();
}

struct Cosmetic
{
    ~Cosmetic() { std::cout << "\n\n"; }
} cosmetic;

using Util = Utilities::StringHelper;



