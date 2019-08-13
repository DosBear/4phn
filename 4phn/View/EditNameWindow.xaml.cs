using _4phn.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditNameWindow.xaml
    /// </summary>
    public partial class EditNameWindow : Window
    {
        public EditNameWindow()
        {
            InitializeComponent();
            SecondaryModelView vm = new SecondaryModelView();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }
    }
}
