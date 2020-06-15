/*
 * GlobalDataTypes.cs - Set of classes / datatypes that are needed throughout
 * the C# Application, separated to this file to avoid cluttering the solution.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guiApp
{
    /*
     * ----< Class > completedTestFunction
     * ----< Description > 
     * All of the data that describes a completed test
     * ----< Description >
     */
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

    /*
     * ----< Class > dllBindingClass
     * ----< Description > 
     * Data used to bind a function to it's dll.
     * ----< Description >
     */
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

    /*
     * ----< Class > dllFunction
     * ----< Description > 
     * Data used to describe a testable function.
     * ----< Description >
     */
    public class dllFunction
    {
        public String FuncName { get; set; }
        public String DllName { get; set; }
        public String DllPath { get; set; }
    }

    /*
     * ----< Class > dllInfo
     * ----< Description > 
     * Overarching datatype that contains a single dll's info including 
     * a list of it's functions.
     * ----< Description >
     */
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
}
