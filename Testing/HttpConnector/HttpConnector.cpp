/*
 * HttpConnector.cpp - Class that is used to interface with the 
 * golang API that is hosted at http://www.kaminfay.com
 * Repo for API Source: https://github.com/KaminFay/CSE687_API
 *
 * Language:    C#, VS 2019
 * Platform:    Windows 10 (UWP)
 * Application: CSE687 Project
 * Author:      Kamin Fay       -- kfay02@syr.edu
 *              Brandon Hancock -- behancoc@syr.edu
 *              Austin Cassidy  -- aucassid@syr.edu
 *              Ralph Walker    -- rwalkeri@syr.edu
 */
#include "HttpConnector.h"

/*
 * ----< Function > HttpConnector
 * ----< Description >
 * Initialize the connector with the input / outputing blocking queues
 * ----< Description >
 * @Param BlockingQueue<dll_info>& input_queue -- Reference to the input queue where new tests are placed.
 * @Param BlockingQueue<result_log>& output_queue -- Reference to the output queue where completed results are placed.
 * @Return None
 */
HttpConnector::HttpConnector(BlockingQueue<dll_info>& input_queue, BlockingQueue<result_log>& output_queue)
{
	client_queue = &input_queue;
	result_queue = &output_queue;
}

/*
 * ----< Function > sendResults
 * ----< Description >
 * On it's own thread sendResults will wait for any results being placed into the outbound queue and send
 * them to the API to be received in the GUI.
 * ----< Description >
 * @Param None
 * @Return None
 */
void HttpConnector::sendResults() {
	std::thread sendResultThread(
		[&]() {
			while (true) {
				Sleep(5);
				// If there are results we'll wanna push them up
				try {
					httplib::Client cli("www.kaminfay.com");
					result_log result = result_queue->deQ();
					std::string resultToSend = Parsers::JSONParser::resultObjectToJSONString(result);
					std::cout << "Attempting to post: " << std::endl;
					std::cout << resultToSend << std::endl;
					auto res = cli.Post("/cse687/sendResults", resultToSend, "application/json");
				}
				catch (const std::exception& e){
					std::cout << "There was an issue connecting to the server: " << std::endl;
					std::cout << e.what() << std::endl;
				}
			}
		}
		);
	sendResultThread.detach();

}

/*
 * ----< Function > getTestableFunctions
 * ----< Description >
 * On it's own thread getTestableFunctions continually reach out to see if the API has any
 * new testable functions from the C# GUI. If there are this function will add them into the input
 * queue.
 * ----< Description >
 * @Param None
 * @Return None
 */
void HttpConnector::getTestableFunctions()
{
	std::thread getFunctionsThread(
		[&]()
		{
			while (true) {
				Sleep(5);
				std::string body;

				try {
					httplib::Client cli("www.kaminfay.com");
					httplib::Headers headers = {
							{ "Accept-Encoding", "gzip, deflate," },
							{"Content-Type", "application/json"}
					};
					auto res = cli.Get("/cse687/recieveFunctions", headers,
						[&](const char* data, size_t data_length) {
							body.append(data, data_length);
							return true;
						});
				}
				catch (const std::exception& e) {
					std::cout << "There was an issue connecting to the server: " << std::endl;
					std::cout << e.what() << std::endl;
				}
				std::vector<dll_info> functions = Parsers::JSONParser::jsonStringToFunctionObject(body);

				if (!functions.empty()) {
					for (auto& func : functions) {
						client_queue->enQ(func);
					}
				}
			}
		}
	);
	getFunctionsThread.detach();
}
