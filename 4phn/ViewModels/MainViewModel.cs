using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using _4phn.View;
using System.Windows;
using System.Reflection;

namespace _4phn.ViewModels
{
    public class MainViewModel : BaseVM
    {
        public MainViewModel()
        {
            ATS.Init();
            ATS.manager.DialBegin += Manager_DialBegin;
            ATS.manager.DeviceStateChanged += Manager_DeviceStateChanged;


        }

        private void Manager_DeviceStateChanged(object sender, AsterNET.Manager.Event.DeviceStateChangeEvent e)
        {
            if (!Properties.Settings.Default.ShowPopup) return;
            if(e.Attributes["device"] == String.Format(Const.Phone.DND, Properties.Settings.Default.Phone))
            {
                LogWriter.Instance.WriteToLog("DND " + e.Attributes["device"] + " " + e.Attributes["state"]);
                ShowPopup("DND", "Состояние " + (e.Attributes["state"] == Const.Phone.DND_STATE_BUSY ? "ЗАНЯТ" : "ДОСТУПЕН"));
            }
                
        }

        private void Manager_DialBegin(object sender, AsterNET.Manager.Event.DialBeginEvent e)
        {
            if (Properties.Settings.Default.Phone == "") return;
            if (e.DialString.IndexOf(Properties.Settings.Default.Phone) < 0 && 
                e.Attributes["destcalleridnum"]!= Properties.Settings.Default.Phone) return;

            ATS.LastCallNum = e.CallerIdNum;
            ATS.LastCallName = e.CallerIdName;
            LogWriter.Instance.WriteToLog("Call " + ATS.LastCallNum + " " + ATS.LastCallName);

            if (Properties.Settings.Default.ShowPopup)
            {
                string title = "Звонок";
                if (e.DialString.IndexOf(Properties.Settings.Default.Phone) < 0) title = "Перехват от " + e.DialString;
                ShowPopup(title, string.Format("Телефон {0} \r\n{1}", ATS.LastCallNum, ATS.LastCallName));
            }
            if(e.CallerIdName == string.Empty || e.CallerIdName == Const.Phone.UNKNOWN)
            {
                LogWriter.Instance.WriteToLog("Add contact ");
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    EditNameWindow editNameWindow = new EditNameWindow();
                    editNameWindow.tbStateButton.Text = "Добавить";
                    editNameWindow.Title = "Добавление нового контакта";
                    editNameWindow.lblPhone.Content = ATS.LastCallNum;
                    editNameWindow.ShowDialog();
                    
                });
            }
                
        }

        public Action <string, string> ShowPopup { get; set; }

        public ICommand CloseApp
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Windows.Application.Current.Shutdown();
                });
            }

        }

        public ICommand ShowSettings
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var window = new SettingsWindow();
                    if (window.ShowDialog() == true)
                    {
                        ATS.Init();
                    }
                });
            }
        }

        public ICommand Phonebook
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    PhonebookWindow phonebookWindow = new PhonebookWindow();
                    phonebookWindow.ShowDialog();
                });
            }
        }


        public ICommand UserCall
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    UserCallWindow userCallWindow = new UserCallWindow();
                    userCallWindow.ShowDialog();
                });
            }
        }

        public ICommand HistoryPhone
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    HistoryWindow historyWindow = new HistoryWindow();
                    historyWindow.ShowDialog();
                });
            }
        }

        public ICommand LastCall
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    LastCallWindow lstCallWindow = new LastCallWindow();
                    lstCallWindow.phone = ATS.LastCallNum;
                    lstCallWindow.name = ATS.LastCallName;
                    lstCallWindow.ShowDialog();
                });
            }
        }

        public ICommand About
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    AboutWindow aboutWindow = new AboutWindow();
                    aboutWindow.tbName.Text = String.Format("4phn v.{0}", version);
                    aboutWindow.ShowDialog();

                });
            }
        }

    }
}
