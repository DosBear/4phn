using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
            try
            {
                LogWriter.Instance.WriteToLog("Try connect to ATS");
                manager = new ManagerConnection(
                    Properties.Settings.Default.Server,
                    Properties.Settings.Default.Port,
                    Properties.Settings.Default.User,
                    Properties.Settings.Default.Password,
                    Encoding.UTF8
                    );
                manager.Login();
                LogWriter.Instance.WriteToLog("ATS version " + manager.AsteriskVersion);
            } catch(Exception err)
            {
                MessageBox.Show("Ошибка подключения с серверу" + err.Message, "Ошиюка", MessageBoxButton.OK, MessageBoxImage.Error);
                LogWriter.Instance.WriteToLog("ATS init error " + err.Message);
            }
        }

        public static void CallToPhone(string phone, string name)
        {
            try
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
            catch (Exception err)
            {
                LogWriter.Instance.WriteToLog("CallToPhone error " + err.Message);
            }
        }

    }
}
