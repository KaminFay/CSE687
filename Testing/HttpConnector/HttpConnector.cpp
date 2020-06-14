#include "HttpConnector.h"

HttpConnector::HttpConnector() {

}

HttpConnector::HttpConnector(BlockingQueue<dll_info>& input_queue, BlockingQueue<result_log>& output_queue)
{
	client_queue = &input_queue;
	result_queue = &output_queue;
}

void HttpConnector::sendResults() {
	std::thread sendResultThread(
		[&]() {
			std::cout << "Sending Thread: " << std::this_thread::get_id() << std::endl;
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

void HttpConnector::getTestableFunctions()
{
	std::thread getFunctionsThread(
		[&]()
		{
			std::cout << "Recieving Thread: " << std::this_thread::get_id() << std::endl;
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
