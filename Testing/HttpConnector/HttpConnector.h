#pragma once

#include <iostream>
#include "httplib.h"
#include <Windows.h>      // Windnows API

#include "logger_def.h"
#include "blockingQueue.h"
#include "ThreadPool.h"
#include "utilities.h"
#include "../Kamin_Parsing_Test/Parsers/Parsers.h"
class HttpConnector
{
public:

	BlockingQueue<dll_info>* client_queue;
	BlockingQueue<result_log>* result_queue;

	HttpConnector(BlockingQueue<dll_info>& input_queue, BlockingQueue<result_log>& output_queue);

	void sendResults();

	void getTestableFunctions();
};

