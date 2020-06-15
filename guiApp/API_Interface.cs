using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using System.Collections.ObjectModel;


namespace guiApp
{
    class API_Interface
    {
        public static ObservableCollection<Int32> functionsIDsSent = new ObservableCollection<int>();
        public static async void getHompage()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            var headers = httpClient.DefaultRequestHeaders;

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

        public static async Task<int> postTestFunctionAsync(JObject jsonObject)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri uri = new Uri("http://www.kaminfay.com/cse687/sendFunctions");
                HttpStringContent content = new HttpStringContent(jsonObject.ToString());
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uri, content);

                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine("What we got back: " + httpResponseBody);
                functionsIDsSent.Add(Int32.Parse(httpResponseBody));
                return Int32.Parse(httpResponseBody);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        public static async void getResultsAsync(GuiLogger logger)
        {
            foreach(Int32 ID in functionsIDsSent)
            {

                JObject jsonOfIDs = new JObject();
                jsonOfIDs.Add("ID", ID);
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
                            var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                            if (httpResponseBody != "nil")
                            {
                                completedTestFunction complete = JSONParser.jsonToCompleted(httpResponseBody);
                                Debug.WriteLine(httpResponseBody);
                                logger.testCompleteLog(complete);
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
