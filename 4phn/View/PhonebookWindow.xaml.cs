using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _4phn.View
{
    /// <summary>
    /// Interaction logic for PhonebookWindow.xaml
    /// </summary>
    public partial class PhonebookWindow : Window
    {
        public PhonebookWindow()
        {
            InitializeComponent();
        }

        private void GridPhoneBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridPhoneBook.SelectedItem == null) return;
            DataRowView drv = (DataRowView)gridPhoneBook.SelectedItem;
            string name = drv.Row.ItemArray[1].ToString();
            string phone = drv.Row.ItemArray[0].ToString();
            LastCallWindow lstCallWindow = new LastCallWindow();
            lstCallWindow.phone = phone;
            lstCallWindow.name = name;
            lstCallWindow.ShowDialog();
        }
    }
}
