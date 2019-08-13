using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace _4phn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {

            LogWriter.logDir = Const.App.PATH;
            LogWriter.Instance.WriteToLog("Start app");
        }
    }
}
