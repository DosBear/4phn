using _4phn.ViewModels;
using System;
using System.Windows;

namespace _4phn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            MainViewModel vm = new MainViewModel();
            this.DataContext = vm;
            if (vm.ShowPopup == null)
                vm.ShowPopup = new Action<string, string>((title, text) =>
                    taskbarIcon.ShowBalloonTip(title, text, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info)
                );

            Hide();

 
        }
    }
}
