﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace guiApp
{
    public class GuiLogger
    {
        private static int currentLevel;
        private String loggingDisplay;
        private Windows.UI.Xaml.Controls.TextBlock loggerText;
        private ScrollViewer sv;

        public GuiLogger()
        {
            loggingDisplay = "";
        }

        public GuiLogger(String log, ref Windows.UI.Xaml.Controls.TextBlock logger, ref ScrollViewer sv)
        {
            this.loggerText = logger;
            this.sv = sv;
            this.loggingDisplay = log;
            this.loggerText.Text = loggingDisplay;
        }

        public static void setLogLevel(int level)
        {
            currentLevel = level;
        }

        public void addLogMessage(String log, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            loggingDisplay = loggingDisplay + "\n" + log;
            loggerText.Text = loggingDisplay;
            reloadSV();
        }

        public void addOpenFileMessage(String log, string path, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Opening File: " + log;
            loggingDisplay = loggingDisplay + "\nFile Path: " + path;
            loggerText.Text = loggingDisplay;
            addSeparators();
            reloadSV();
        }

        public void addFunctionSendMessage(dllFunction dllFunc)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Sending Function: \n";
            loggingDisplay = loggingDisplay + "Dll Name: " + dllFunc.DllName + "\n";
            loggingDisplay = loggingDisplay + "Function Name: " + dllFunc.FuncName;
            addSeparators();
            reloadSV();
        }

        public void postedTestFunctionLog(JObject jsonObject, int ID)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Sent JSON to API: \n";
            loggingDisplay = loggingDisplay + jsonObject.ToString() + "\n";
            loggingDisplay = loggingDisplay + "ID of entry = " + ID;
            addSeparators();
            reloadSV();
        }

        public void testCompleteLog(completedTestFunction complete)
        {
            switch (currentLevel)
            {
                case 1:
                    testCompleteLogLevelOne(complete);
                    break;
                case 2:
                    testCompleteLogLevelTwo(complete);
                    break;
                case 3:
                    testCompleteLogLevelThree(complete);
                    break;
                default:
                    Debug.WriteLine("Not A valid log Level.");
                    break;
            }
        }

        private void testCompleteLogLevelOne(completedTestFunction complete)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result;
            addSeparators();
            reloadSV();
        }

        private void testCompleteLogLevelTwo(completedTestFunction complete)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result + "\n";
            loggingDisplay = loggingDisplay + "Exception: " + complete.Exception;
            addSeparators();
            reloadSV();
        }

        private void testCompleteLogLevelThree(completedTestFunction complete)
        {
            addSeparators();
            loggingDisplay = loggingDisplay + "Completed Test: \n";
            loggingDisplay = loggingDisplay + "Path: " + complete.DllPath + "\n";
            loggingDisplay = loggingDisplay + "Function: " + complete.FuncName + "\n";
            loggingDisplay = loggingDisplay + "Pass: " + complete.Result + "\n";
            loggingDisplay = loggingDisplay + "Exception: " + complete.Exception + "\n";
            loggingDisplay = loggingDisplay + "Start Time: " + complete.StartTime + "\n";
            loggingDisplay = loggingDisplay + "End Time: " + complete.EndTime;
            addSeparators();
            reloadSV();
        }

        public void addSeparators()
        {
            loggingDisplay += "\n--------------------------------------------------------------------------------\n";
            loggerText.Text = loggingDisplay;
        }

        // Which Each new addition we need to reload the scrollview and change it's view to the bottom.
        public void reloadSV()
        {
            sv.UpdateLayout();
            sv.ChangeView(null, double.MaxValue, null);
        }
    }
}