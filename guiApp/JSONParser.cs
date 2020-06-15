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
    public class completedTestFunction
    {
        public String DllName { get; set; }
        public String DllPath { get; set; }
        public String Exception { get; set; }
        public bool Result { get; set; }
        public String FuncName { get; set; }
        public int ID { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }

    }

    public class testFunctionJSON
    {
        public String FuncName { get; set; }
        public String DllName { get; set; }
        public String DllPath { get; set; }
    }

    public class dllBindingClass
    {
        public String dllName { get; set; }
        public String dllFullPath { get; set; }

        public dllBindingClass(String path, String name)
        {
            dllName = name;
            dllFullPath = path;
        }
    }

    public class dllFunction
    {
        public String FuncName { get; set; }
        public String DllName { get; set; }
        public String DllPath { get; set; }
    }

    public class dllInfo
    {
        public String dllName { get; set; }
        public String dllLocation { get; set; }
        public String jsonSourceName { get; set; }
        public List<dllFunction> functionList { get; set; }

        public dllInfo()
        {
            dllName = "";
            dllLocation = "";
            jsonSourceName = "";
            functionList = new List<dllFunction>();
        }
    }

    public class DLLViewModel
    {
        private dllInfo defaultDLL = new dllInfo();
        public dllInfo DefaultDLL { get { return this.defaultDLL; } }
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

        public async Task<List<dllInfo>> readInJSON(StorageFile storageFile)
        {
            fileName = storageFile.DisplayName;
            filePath = storageFile.Path;
            using (StreamReader file = new StreamReader(await storageFile.OpenStreamForReadAsync()))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonData = (JObject)JToken.ReadFrom(reader);
            }
            parseJSON(storageFile);
            return allDLLData;
        }

        public List<dllInfo> getDLLObject()
        {
            return allDLLData;
        }

        public void parseJSON(StorageFile storageFile)
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
        }

        public static JObject dllFunctionToJSON(dllFunction func)
        {

            JObject jObject = (JObject)JToken.FromObject(func);
            Debug.WriteLine(jObject);
            return jObject;
        }

        public static completedTestFunction jsonToCompleted(string jsonString)
        {
            completedTestFunction completedFunction = JsonConvert.DeserializeObject<completedTestFunction>(jsonString);
            return completedFunction;
        }

    }
}
