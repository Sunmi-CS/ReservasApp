using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows;
using ReservasApp.Models;
using ReservasApp.ViewModels;

namespace ReservasApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            DataContext = new MainViewModel(usuario);
        }
    }
}
