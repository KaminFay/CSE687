/*
 * GuiLogger.cs - Logger class, binds to the textbox at the bottom of the GUI
 * functions within this class will allow writing too and scrolling of the logger box
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
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace guiApp
{
    public class GuiLogger
    {
        private static int currentLevel;
        private static string loggingDisplay;
        private static TextBlock loggerText;
        private static ScrollViewer sv;

        public GuiLogger()
        {
            loggingDisplay = "";
        }

        public GuiLogger(string log, ref Windows.UI.Xaml.Controls.TextBlock logger, ref ScrollViewer svP)
        {
            loggerText = logger;
            sv = svP;
            loggingDisplay = log;
            loggerText.Text = loggingDisplay;
        }

        /*
         * ----< Function > SetLogLevel
         * ----< Description > 
         * Setter for the current log level for display in GUI
         * ----< Description >
         * @Param int level -- Current level
         * @Return None
         */
        public static void SetLogLevel(int level)
        {
            currentLevel = level;
        }

        /*
         * ----< Function > AddLogMessage
         * ----< Description > 
         * Add any string into the log
         * ----< Description >
         * @Param string log -- String to be added
         * @Return None
         */
        public void AddLogMessage(string log)
        {
            loggingDisplay = loggingDisplay + "\n" + log;
            loggerText.Text = loggingDisplay;
            ReloadSV();
        }

        /*
         * ----< Function > AddOpenFileMessage
         * ----< Description > 
         * Will add a log displaying what file was opened
         * ----< Description >
         * @Param string log -- String to be added 
         * @Param string path -- location of file being opened
         * @Return None
         */
        public void AddOpenFileMessage(string log, string path)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Opening File: " + log;
            loggingDisplay = loggingDisplay + "\nFile Path: " + path;
            loggerText.Text = loggingDisplay;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > AddFunctionSendMessage
         * ----< Description > 
         * Add a log message describing the function being sent
         * ----< Description >
         * @Param dllFunction dllFunc -- Function being sent
         * @Return None
         */
        public void AddFunctionSendMessage(dllFunction dllFunc)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Sending Function: \n";
            loggingDisplay = loggingDisplay + "Dll Name: " + dllFunc.DllName + "\n";
            loggingDisplay = loggingDisplay + "Function Name: " + dllFunc.FuncName;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > PostedTestFunctionLog
         * ----< Description > 
         * Add a log message describing the function being sent to the API
         * ----< Description >
         * @Param JObject jsonObject -- JSON object that is being sent to the API
         * @Param int ID -- ID that was returned for the IP indicating it's location in the database
         * @Return None
         */
        public void PostedTestFunctionLog(JObject jsonObject, int ID)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Sent JSON to API: \n";
            loggingDisplay = loggingDisplay + jsonObject.ToString() + "\n";
            loggingDisplay = loggingDisplay + "ID of entry = " + ID;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > TestCompleteLog
         * ----< Description > 
         * Switch case for which logging level to use on the current completedTestFunction
         * ----< Description >
         * @Param completedTestFunction complete -- C# object that describes the completed test
         * @Return None
         */
        public void TestCompleteLog(completedTestFunction complete)
        {
            switch (currentLevel)
            {
                case 1:
                    TestCompleteLogLevelOne(complete);
                    break;
                case 2:
                    TestCompleteLogLevelTwo(complete);
                    break;
                case 3:
                    TestCompleteLogLevelThree(complete);
                    break;
                default:
                    Debug.WriteLine("Not A valid log Level.");
                    break;
            }
        }

        /*
         * ----< Function > TestCompleteLogLevelOne
         * ----< Description > 
         * Display DLL path, function tested, and if it passed / failed.
         * ----< Description >
         * @Param completedTestFunction complete -- C# object that describes the completed test
         * @Return None
         */
        private void TestCompleteLogLevelOne(completedTestFunction complete)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > TestCompleteLogLevelTwo
         * ----< Description > 
         * Display DLL path, function tested, if it passed/failed, and the exception.
         * ----< Description >
         * @Param completedTestFunction complete -- C# object that describes the completed test
         * @Return None
         */
        private void TestCompleteLogLevelTwo(completedTestFunction complete)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result + "\n";
            loggingDisplay = loggingDisplay + "Exception: " + complete.Exception;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > TestCompleteLogLevelThree
         * ----< Description > 
         * Display DLL path, function tested, if it passed/failed, exception, start time and end time of test.
         * ----< Description >
         * @Param completedTestFunction complete -- C# object that describes the completed test
         * @Return None
         */
        private void TestCompleteLogLevelThree(completedTestFunction complete)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result + "\n";
            loggingDisplay = loggingDisplay + "Exception: " + complete.Exception + "\n";
            loggingDisplay = loggingDisplay + "Start Time: " + complete.StartTime + "\n";
            loggingDisplay = loggingDisplay + "End Time: " + complete.EndTime;
            AddSeparators();
            ReloadSV();
        }

        public static void logException(string message, string exception)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + message + "\n";
            loggingDisplay = loggingDisplay + "Exception: " + exception;
            AddSeparators();
            ReloadSV();
        }

        /*
         * ----< Function > AddSeparators
         * ----< Description > 
         * Simply adds --- separators between items in the logger.
         * ----< Description >
         * @Return None
         */
        public static void AddSeparators()
        {
            loggingDisplay += "\n--------------------------------------------------------------------------------\n";
            loggerText.Text = loggingDisplay;
        }

        /*
         * ----< Function > ReloadSV
         * ----< Description > 
         * Each time an item is added into the logger the layout needs to be reloaded
         * which will allow for auto scroll to take over.
         * ----< Description >
         * @Return None
         */
        public static void ReloadSV()
        {
            sv.UpdateLayout();
            sv.ChangeView(null, double.MaxValue, null);
        }
    }
}
