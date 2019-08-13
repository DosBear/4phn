using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsterNET.Manager;
using AsterNET.Manager.Action;

namespace _4phn
{
    public class ATS
    {
        public static ManagerConnection manager;
        public static string LastCallNum;
        public static string LastCallName;
  
        public static void Init()
        {
            LogWriter.Instance.WriteToLog("Try connect to ATS");
            manager = new ManagerConnection(
                Properties.Settings.Default.Server,
                Properties.Settings.Default.Port,
                Properties.Settings.Default.User,
                Properties.Settings.Default.Password,
                Encoding.UTF8
                );
            LogWriter.Instance.WriteToLog("ATS version" + manager.AsteriskVersion);
            manager.Login();
        }

        public static void CallToPhone(string phone, string name)
        {
            OriginateAction action = new OriginateAction();
            action.Channel = "SIP/" + Properties.Settings.Default.Phone;
            action.CallerId = "Набор номера: " + name;
            action.Exten = phone;
            action.Context = "call-out";
            action.Priority = "0";
            action.Async = true;
            manager.SendAction(action);
        }

    }
}
