using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace _4phn.ViewModels
{
    public class SecondaryModelView : BaseVM
    {
        public Action CloseAction { get; set; }

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _OrgName;
        public string OrgName
        {
            get { return _OrgName; }
            set
            {
                _OrgName = value;
                OnPropertyChanged(nameof(OrgName));
            }
        }


        public ICommand UserCall
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ATS.CallToPhone(_Phone, "4phn" + _Phone);
                }, (obj) => Phone?.Length>0);
            }
        }


        public ICommand Save
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Database.execute(string.Format(Const.SQL.INSERT_PHONEBOOK, _OrgName, _Phone));
                    MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();


                }, (obj) => OrgName?.Length > 0);
            }
        }
    }
}
