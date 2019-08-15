using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LastCallWindow.xaml
    /// </summary>
    public partial class LastCallWindow : Window
    {
        public string phone, name;

        public LastCallWindow()
        {
            InitializeComponent();
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            string txt = txtPhone.Text;
            Clipboard.SetDataObject(txt);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtPhone.Text = phone + "\r\n" + name;
            btnCall.IsEnabled =  btnCopy.IsEnabled = (phone != null) ;
        }

        private void BtnCall_Click(object sender, RoutedEventArgs e)
        {
            ATS.CallToPhone(phone.Replace(" ", ""), name);
            this.Close();
        }
    }
}
