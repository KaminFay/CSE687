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
        private string loggingDisplay;
        private readonly TextBlock loggerText;
        private readonly ScrollViewer sv;

        public GuiLogger()
        {
            loggingDisplay = "";
        }

        public GuiLogger(string log, ref Windows.UI.Xaml.Controls.TextBlock logger, ref ScrollViewer sv)
        {
            loggerText = logger;
            this.sv = sv;
            loggingDisplay = log;
            loggerText.Text = loggingDisplay;
        }

        public static void SetLogLevel(int level)
        {
            currentLevel = level;
        }

        public void AddLogMessage(string log, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            loggingDisplay = loggingDisplay + "\n" + log;
            loggerText.Text = loggingDisplay;
            ReloadSV();
        }

        public void AddOpenFileMessage(string log, string path, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Opening File: " + log;
            loggingDisplay = loggingDisplay + "\nFile Path: " + path;
            loggerText.Text = loggingDisplay;
            AddSeparators();
            ReloadSV();
        }

        public void AddFunctionSendMessage(dllFunction dllFunc)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Sending Function: \n";
            loggingDisplay = loggingDisplay + "Dll Name: " + dllFunc.DllName + "\n";
            loggingDisplay = loggingDisplay + "Function Name: " + dllFunc.FuncName;
            AddSeparators();
            ReloadSV();
        }

        public void PostedTestFunctionLog(JObject jsonObject, int ID)
        {
            AddSeparators();
            loggingDisplay = loggingDisplay + "Sent JSON to API: \n";
            loggingDisplay = loggingDisplay + jsonObject.ToString() + "\n";
            loggingDisplay = loggingDisplay + "ID of entry = " + ID;
            AddSeparators();
            ReloadSV();
        }

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

        public void AddSeparators()
        {
            loggingDisplay += "\n--------------------------------------------------------------------------------\n";
            loggerText.Text = loggingDisplay;
        }

        // Which Each new addition we need to reload the scrollview and change it's view to the bottom.
        public void ReloadSV()
        {
            sv.UpdateLayout();
            sv.ChangeView(null, double.MaxValue, null);
        }
    }
}
