using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace _4phn.ViewModels
{
    public class SettingsModelView : BaseVM
    {

        public Action CloseAction { get; set; }

        public string PropPhone
        {
            get { return Properties.Settings.Default.Phone; }
            set
            {
                Properties.Settings.Default.Phone = value;
                OnPropertyChanged(nameof(PropPhone));
            }
        }


        public int PropMaxHistoryRow
        {
            get { return Properties.Settings.Default.MaxHistoryRow; }
            set
            {
                Properties.Settings.Default.MaxHistoryRow = value;
                OnPropertyChanged(nameof(PropMaxHistoryRow));
            }
        }



        public bool PropShowPopup
        {
            get { return Properties.Settings.Default.ShowPopup; }
            set
            {
                Properties.Settings.Default.ShowPopup = value;
                OnPropertyChanged(nameof(PropShowPopup));
            }
        }

        public string PropServer
        {
            get { return Properties.Settings.Default.Server; }
            set
            {
                Properties.Settings.Default.Server = value;
                OnPropertyChanged(nameof(PropServer));
            }
        }

        public string PropUser
        {
            get { return Properties.Settings.Default.User; }
            set
            {
                Properties.Settings.Default.User = value;
                OnPropertyChanged(nameof(PropUser));
            }
        }

        public string PropPassword
        {
            get { return Properties.Settings.Default.Password; }
            set
            {
                Properties.Settings.Default.Password = value;
                OnPropertyChanged(nameof(PropPassword));
            }
        }

        public int PropPort
        {
            get { return Properties.Settings.Default.Port; }
            set
            {
                Properties.Settings.Default.Port = value;
                OnPropertyChanged(nameof(PropPort));
            }
        }

        public ICommand Save
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Properties.Settings.Default.Save();
                    CloseAction();
                });
            }
        }

        public string PropMySQLServer
        {
            get { return Properties.Settings.Default.MYSQLServer; }
            set
            {
                Properties.Settings.Default.MYSQLServer = value;
                OnPropertyChanged(nameof(PropMySQLServer));
            }
        }

        public string PropMySQLUser
        {
            get { return Properties.Settings.Default.MYSQLUser; }
            set
            {
                Properties.Settings.Default.MYSQLUser = value;
                OnPropertyChanged(nameof(PropMySQLUser));
            }
        }

        public string PropMySQLPassword
        {
            get { return Properties.Settings.Default.MYSQLPassword; }
            set
            {
                Properties.Settings.Default.MYSQLPassword = value;
                OnPropertyChanged(nameof(PropMySQLPassword));
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Properties.Settings.Default.Reload();
                    CloseAction();
                });
            }
        }


    }
}
