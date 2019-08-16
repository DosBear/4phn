using _4phn.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

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
                _HistoryBook = Database.getData(string.Format(Const.SQL.SELECT_HISTORY, 
                    Properties.Settings.Default.Phone,
                    Properties.Settings.Default.MaxHistoryRow
                    ));
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


        public ICommand DeleteRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    if (MessageBox.Show("Удалить запись " + name, "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string phone = drv.Row.ItemArray[0].ToString();
                        Database.execute(string.Format(Const.SQL.DELETE_PHONEBOOK, phone));
                        _PhoneBook = Database.getData(Const.SQL.SELECT_PHONEBOOK);
                    }
                    
                });
            }

        }

        public ICommand CopyRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    string phone = drv.Row.ItemArray[0].ToString();
                    Clipboard.SetDataObject(phone + " " + name);

                });
            }
        }

        public ICommand CallRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    string phone = drv.Row.ItemArray[0].ToString();
                    if (MessageBox.Show("Набрать контакт " + name, "Звонок", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ATS.CallToPhone(phone, name);
                    }

                });
            }
        }

        public ICommand EditRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    string phone = drv.Row.ItemArray[0].ToString();

                    EditNameWindow editNameWindow = new EditNameWindow();
                    editNameWindow.tbStateButton.Text = "Сохранить";
                    editNameWindow.Title = "Редактирование контакта";
                    editNameWindow.lblPhone.Content = phone;
                    editNameWindow.txtName.Text = name;
                    editNameWindow.ShowDialog();


                });
            }
        }



        public ICommand CopyHistoryRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    if (name.IndexOf('"') >= 0) name = name.Split('"')[1];
                    string phone = drv.Row.ItemArray[2].ToString();
                    if (phone == Properties.Settings.Default.Phone) phone = drv.Row.ItemArray[3].ToString();

                    Clipboard.SetDataObject(phone + " " + name);

                });
            }
        }

        public ICommand PlayRecFileRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string file = drv.Row.ItemArray[7].ToString();

                    file = Path.Combine(Properties.Settings.Default.SoundPath, file.Split('-')[0], file.Split('-')[1], file.Split('-')[2], file);
                    if(!File.Exists(file))
                    {
                        MessageBox.Show("Нет доступа к файлу " + file, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Question);
                        return;
                    }
                    PlayerWindow playerWindow = new PlayerWindow();
                    playerWindow.mp3filename = file;
                    playerWindow.ShowDialog();


                }, (obj) => Properties.Settings.Default.SoundPath.Length>0 );
            }
        }


        public ICommand CallHistoryRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    if (name.IndexOf('"') >= 0) name = name.Split('"')[1];
                    string phone = drv.Row.ItemArray[2].ToString();
                    if (phone == Properties.Settings.Default.Phone) phone = drv.Row.ItemArray[3].ToString();

                    if (MessageBox.Show("Набрать контакт " + name, "Звонок", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ATS.CallToPhone(phone, name);
                    }

                });
            }
        }

        public ICommand EditHistoryRow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Collections.IList items = (System.Collections.IList)obj;
                    DataRowView drv = (DataRowView)items[0];
                    string name = drv.Row.ItemArray[1].ToString();
                    EditNameWindow editNameWindow = new EditNameWindow();

                    if (name.IndexOf("\"\"") >= 0)
                    {
                        editNameWindow.tbStateButton.Text = "Добавить";
                        editNameWindow.txtName.Text = "";
                    } else
                    {
                        editNameWindow.tbStateButton.Text = "Сохранить";
                        if (name.IndexOf('"') >= 0) name = name.Split('"')[1];
                        editNameWindow.txtName.Text = name;

                    }


                    string phone = drv.Row.ItemArray[2].ToString();
                    if (phone == Properties.Settings.Default.Phone) phone = drv.Row.ItemArray[3].ToString();

                    
                    
                    editNameWindow.Title = "Добавление контакта";
                    editNameWindow.lblPhone.Content = phone;
                    
                    editNameWindow.ShowDialog();



                });
            }
        }

    }
}
