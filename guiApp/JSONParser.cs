/*
 * JSONParser.cs - JSON Parser is a class that allows for parsing of JSON files
 * or JSON strings that are provided via http API calls.
 * 
 * Language:    C#, VS 2019
 * Platform:    Windows 10 (UWP)
 * Application: CSE687 Project
 * Author:      Kamin Fay       -- kfay02@syr.edu
 *              Brandon Hancock -- behancoc@syr.edu
 *              Austin Cassidy  -- aucassid@syr.edu
 *              Ralph Walker    -- rwalkeri@syr.edu
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace guiApp

{
    class JSONParser
    {
        private String fileName { get; set; }
        private String filePath { get; set; }
        private JObject jsonData { get; set; }
        private List<dllInfo> allDLLData { get; set; }
        private List<dllFunction> tempFunctionData { get; set; }

        public JSONParser()
        {
            this.fileName = "";
            this.filePath = "";
            this.jsonData = new JObject();
            this.allDLLData = new List<dllInfo>();
            this.tempFunctionData = new List<dllFunction>();
        }

        public JSONParser(String name, String path)
        {
            this.fileName = name;
            this.filePath = path;
            this.jsonData = new JObject();
            this.allDLLData = new List<dllInfo>();
            this.tempFunctionData = new List<dllFunction>();
        }

        public void setFileName(String fileName)
        {
            this.fileName = fileName;
        }

        public void setFilePath(String path)
        {
            this.filePath = path;
        }

        /*
         * ----< Function > readInJSON
         * ----< Description > 
         * Given a JSON file this function will read in the JSON data and call parseJSON
         * to pull out the required information.
         * ----< Description >
         * @Param StorageFile storageFile -- File object that contains the path / name of the JSON file
         * @Return allDLLData -- A list of all dll's within the JSON file and their corresponding functions.
         */
        public async Task<List<dllInfo>> readInJSON(StorageFile storageFile)
        {
            fileName = storageFile.DisplayName;
            filePath = storageFile.Path;
            try
            {
                using (StreamReader file = new StreamReader(await storageFile.OpenStreamForReadAsync()))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    jsonData = (JObject)JToken.ReadFrom(reader);
                }
                parseJSON(storageFile);
            }catch (Exception ex)
            {
                GuiLogger.logException("The JSON Passed in is invalid syntax.", ex.Message);
            }
            return allDLLData;
        }

        /*
         * ----< Function > getDLLObject
         * ----< Description > 
         * Get already parsed data of all dll's
         * ----< Description >
         * @Return allDLLData -- A list of all dll's within the JSON file and their corresponding functions.
         */
        public List<dllInfo> getDLLObject()
        {
            return allDLLData;
        }

        /*
         * ----< Function > parseJSON
         * ----< Description > 
         * Given a JSON file this function will read in the JSON data and parse the data
         * to pull out all relevent information about the dll's and functions within and store
         * the data in a list within the class.
         * ----< Description >
         * @Param StorageFile storageFile -- File object that contains the path / name of the JSON file
         */
        public void parseJSON(StorageFile storageFile)
        {
            Debug.WriteLine(jsonData.ToString());

            JArray jArray = (JArray)jsonData["dlls"];
            dynamic dllData = jArray;


            Debug.WriteLine("Testing Dynamics");
            if(dllData != null)
            {
                try
                {
                    foreach (dynamic item in dllData)
                    {
                        dllInfo newDll = new dllInfo();
                        newDll.dllName = item.Name.ToString();
                        newDll.dllLocation = item.Location.ToString();
                        newDll.jsonSourceName = storageFile.DisplayName;


                        foreach (dynamic functionT in item.Functions)
                        {

                            dllFunction newFunction = new dllFunction();
                            newFunction.FuncName = functionT.FuncName;
                            newDll.functionList.Add(newFunction);

                        }
                        allDLLData.Add(newDll);
                    }

                    Debug.WriteLine("Testing Listing Function");
                    foreach (dllInfo item in allDLLData)
                    {
                        Debug.WriteLine("Name: " + item.dllName);
                        foreach (dllFunction function in item.functionList)
                        {
                            Debug.WriteLine(function.FuncName);
                        }
                        Debug.WriteLine("---------------------");
                    }
                }catch(Exception ex)
                {
                    GuiLogger.logException("Invalid Source JSON", ex.Message);
                    allDLLData.Clear();
                    return;
                }
            }
            else
            {
                GuiLogger.logException("Invalid Source JSON", "Please Correct");
            }

        }

        /*
         * ----< Function > dllFunctionToJSON
         * ----< Description > 
         * Static function that will convert from a dllFunction object to a JSON object
         * ----< Description >
         * @Param dllFunction func -- Object containing function data
         * @Return JOBject -- JSON object containig all of the info from the dllFunction object.
         */
        public static JObject dllFunctionToJSON(dllFunction func)
        {

            JObject jObject = (JObject)JToken.FromObject(func);
            Debug.WriteLine(jObject);
            return jObject;
        }

        /*
         * ----< Function > jsonToCompleted
         * ----< Description > 
         * Static function that will convert a JSON string to a completedTestFunction object
         * ----< Description >
         * @Param string jsonString -- string containing JSON data.
         * @return completedTestFunction -- C# object containing the data of a completed test.
         */
        public static completedTestFunction jsonToCompleted(string jsonString)
        {
            completedTestFunction completedFunction = JsonConvert.DeserializeObject<completedTestFunction>(jsonString);
            return completedFunction;
        }

    }
}
