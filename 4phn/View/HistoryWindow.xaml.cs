using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }

        private void GridHistoryeBook_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataRowView item = e.Row.Item as DataRowView;
            if (item != null)
            {
                DataRow row = item.Row;
                BrushConverter bc = new BrushConverter();
                if (row["src"].ToString() == Properties.Settings.Default.Phone)
                {
                    e.Row.Background = (Brush)bc.ConvertFrom("#AFFFB9");
                } else
                {
                    e.Row.Background = (Brush)bc.ConvertFrom("#FFF6A8");
                }
                
            }
        }

        private void GridHistoryBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridHistoryBook.SelectedItem == null) return;
            DataRowView drv = (DataRowView)gridHistoryBook.SelectedItem;
            string name = drv.Row.ItemArray[1].ToString();
            if (name.IndexOf('"') >= 0) name = name.Split('"')[1];
            string phone = drv.Row.ItemArray[2].ToString();
            if(phone == Properties.Settings.Default.Phone) phone = drv.Row.ItemArray[3].ToString();

            LastCallWindow lstCallWindow = new LastCallWindow();
            lstCallWindow.phone = phone;
            lstCallWindow.name = name;
            lstCallWindow.ShowDialog();
        }
    }


    public sealed class SrcimageToImageConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (value.ToString() == Properties.Settings.Default.Phone)
                {
                    return "/Resources/user.png";
                }
                else
                {
                    return "/Resources/org.png";
                }
            }
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
