using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace _4phn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Mutex InstanceCheckMutex;
        void App_Startup(object sender, StartupEventArgs e)
        {

            LogWriter.logDir = Const.App.PATH;
            LogWriter.Instance.WriteToLog("Start app");
            if (!InstanceCheck())
            {
                MessageBox.Show("Программа уже запущена");
            }
        }

        
        static bool InstanceCheck()
        {
            bool isNew;
            InstanceCheckMutex = new Mutex(true, "4phn", out isNew);
            return isNew;
        }
    }
}
