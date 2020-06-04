using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Linq;

namespace guiApp
{

    public class dllFunction
    {
        public String functionName { get; set; }
        public String returnType { get; set; }
        public String parameters { get; set; }
    }

    public class dllInfo
    {
        public String dllName { get; set; }
        public String dllLocation { get; set; }
        public List<dllFunction> functionList { get; set; }

        public dllInfo()
        {
            dllName = "";
            dllLocation = "";
            functionList = new List<dllFunction>();
        }
    }

    class JSONParser
    {
        private String fileName { get; set; }
        private String filePath { get; set; }
        private JObject jsonData { get; set; }
        private List<dllInfo> allDLLData { get; set; }
        private List<dllFunction> tempFunctionData { get; set; }

        public JSONParser()
        {
            fileName = "";
            filePath = "";
            jsonData = new JObject();
            allDLLData = new List<dllInfo>();
            tempFunctionData = new List<dllFunction>();
        }

        public JSONParser(String name, String path)
        {
            fileName = name;
            filePath = path;
            jsonData = new JObject();
            allDLLData = new List<dllInfo>();
            tempFunctionData = new List<dllFunction>();
        }


        public async void readInJSON(StorageFile storageFile)
        {
            fileName = storageFile.DisplayName;
            filePath = storageFile.Path;
            using (StreamReader file = new StreamReader(await storageFile.OpenStreamForReadAsync()))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonData = (JObject)JToken.ReadFrom(reader);
            }
            parseJSON();
        }

        public void parseJSON()
        {
            Debug.WriteLine(jsonData.ToString());

            JArray jArray = (JArray)jsonData["dlls"];
            dynamic dllData = jArray;

            Debug.WriteLine("Testing Dynamics");
            foreach (dynamic item in dllData)
            {
                dllInfo newDll = new dllInfo();
                newDll.dllName = item.Name.ToString();
                newDll.dllLocation = item.Location.ToString();


                foreach (dynamic functionT in item.Functions)
                {

                    dllFunction newFunction = new dllFunction();
                    newFunction.functionName = functionT.FuncName;
                    newFunction.parameters = functionT.PassedIn;
                    newFunction.returnType = functionT.ReturnType;
                    newDll.functionList.Add(newFunction);

                }
                allDLLData.Add(newDll);
            }

            //IList<dllInfo> dllData = jArray.Select(dll => new dllInfo
            //{
            //    dllName = (string)dll["Name"],
            //    dllLocation = (string)dll["Location"],
            //    functionList = jArray.Select(functions => new dllFunction
            //    {

            //    }).ToList()
            //}).ToList();

            Debug.WriteLine("Testing Listing Function");
            foreach (dllInfo item in allDLLData)
            {
                Debug.WriteLine("Name: " + item.dllName);
                foreach (dllFunction function in item.functionList)
                {
                    Debug.WriteLine(function.functionName);
                }
                Debug.WriteLine("---------------------");
            }
        }

    }
}
