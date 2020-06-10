﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace guiApp
{
    public class Logger
    {
        private String loggingDisplay;
        private Windows.UI.Xaml.Controls.TextBlock loggerText;
        private ScrollViewer sv;

        public Logger()
        {
            loggingDisplay = "";
        }

        public Logger(String log, ref Windows.UI.Xaml.Controls.TextBlock logger, ref ScrollViewer sv)
        {
            this.loggerText = logger;
            this.sv = sv;
            this.loggingDisplay = log;
            this.loggerText.Text = loggingDisplay;
        }

        public void addLogMessage(String log, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            loggingDisplay = loggingDisplay + "\n" + log;
            loggerText.Text = loggingDisplay;
            reloadSV();
        }

        public void addOpenFileMessage(String log, string path, ref Windows.UI.Xaml.Controls.TextBlock logger)
        {
            addSeparators(ref logger);
            loggingDisplay = loggingDisplay + "Opening File: " + log;
            loggingDisplay = loggingDisplay + "\nFile Path: " + path;
            loggerText.Text = loggingDisplay;
            addSeparators(ref logger);
            reloadSV();
        }

        public void addSeparators(ref Windows.UI.Xaml.Controls.TextBlock logger)
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