using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BlockSystemUI
{
    /// <summary>
    /// Interaction logic for WindowDataBind.xaml
    /// </summary>
    public partial class WindowDataBindStrecke : Window
    {
       

        public WindowDataBindStrecke()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TestViewModel();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TestViewModel2();
        }
    }

 

  
    
}
