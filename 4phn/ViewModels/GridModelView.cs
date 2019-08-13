using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace _4phn.ViewModels
{
    public  class GridModelView: BaseVM
    {

        private DataView _PhoneBook;
        public DataView PhoneBook
        {
            get {
                _PhoneBook = Database.getData(Const.SQL.SELECT_PHONEBOOK);
                return _PhoneBook;
            }
            set
            { }
        }



        private DataView _HistoryBook;
        public DataView HistoryBook
        {
            get
            {
                _HistoryBook = Database.getData(string.Format(Const.SQL.SELECT_HISTORY, Properties.Settings.Default.Phone));
                return _HistoryBook;
            }
            set
            { }
        }



        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;
                OnPropertyChanged(nameof(Filter));
                _PhoneBook.RowFilter = string.Format("name LIKE '%{0}%' or number LIKE '%{0}%'", _Filter);
            }
        }
    }
}
