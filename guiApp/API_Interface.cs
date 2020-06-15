/*
 * API_Interface.cs - Group of static functions that allow interaction with the
 * http API that acts as a middle man between the C# GUI and c++ test harness
 * 
 * Language:    C#, VS 2019
 * Platform:    Windows 10 (UWP)
 * Application: CSE687 Project
 * Author:      Kamin Fay       -- kfay02@syr.edu
 *              Brandon Hancock -- behancoc@syr.edu
 *              Austin Cassidy  -- aucassid@syr.edu
 *              Ralph Walker    -- rwalkeri@syr.edu
 */
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Web.Http;


namespace guiApp
{
    internal class API_Interface
    {
        public static ObservableCollection<int> functionsIDsSent = new ObservableCollection<int>();

        /*
         * ----< Function > TruncateTestFunctions
         * ----< Description > 
         * This function will truncate any items in the test function table of the api database
         * ----< Description >
         * @Param None
         * @Return None
         */
        public static async void TruncateTestFunctions()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            Windows.Web.Http.Headers.HttpRequestHeaderCollection headers = httpClient.DefaultRequestHeaders;

            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            Uri requestUri = new Uri("http://www.kaminfay.com/");

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
        }

        /*
         * ----< Function > PostTestFunctionAsync
         * ----< Description > 
         * Taking in a JSON object, the object is then sent to the API Endpoint /cse687/sendFunctions
         * ----< Description >
         * @Param JObject jsonObject -- Contains json data that describes a testable function
         * @Return Task<int> -- Contains the row ID of the newly inserted test function.
         */
        public static async Task<int> PostTestFunctionAsync(JObject jsonObject)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri uri = new Uri("http://www.kaminfay.com/cse687/sendFunctions");
                HttpStringContent content = new HttpStringContent(jsonObject.ToString());
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uri, content);

                httpResponseMessage.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine("What we got back: " + httpResponseBody);
                functionsIDsSent.Add(int.Parse(httpResponseBody));
                return int.Parse(httpResponseBody);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        /*
         * ----< Function > GetResultsAsync
         * ----< Description > 
         * For all test function ID's that have been sent this function will asynchronously 
         * reach out to the endpoint /cse687/results ; sending an ID ; and receiving back a 
         * JSON object containing the test results for that function.
         * ----< Description >
         * @Param GuiLogger logger -- Logger to write the data to in the GUI
         * @Return void
         */
        public static async void GetResultsAsync(GuiLogger logger)
        {
            foreach (int ID in functionsIDsSent)
            {

                JObject jsonOfIDs = new JObject
                {
                    { "ID", ID }
                };
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                try
                {
                    while (true)
                    {
                        HttpClient httpClient = new HttpClient();
                        Uri uri = new Uri("http://www.kaminfay.com/cse687/results");
                        HttpStringContent content = new HttpStringContent(jsonOfIDs.ToString());
                        httpResponseMessage = await httpClient.PostAsync(uri, content);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            string httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                            if (httpResponseBody != "nil")
                            {
                                completedTestFunction complete = JSONParser.jsonToCompleted(httpResponseBody);
                                Debug.WriteLine(httpResponseBody);
                                logger.TestCompleteLog(complete);
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return;
                }
            }

            functionsIDsSent.Clear();
        }
    }
}
