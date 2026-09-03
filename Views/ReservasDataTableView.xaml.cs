using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using ReservasApp.ViewModels;

namespace ReservasApp.Views
{
    public partial class ReservasDataTableView : Window
    {
        public ReservasDataTableView()
        {
            InitializeComponent();

            DataContext =
                new ReservasDataTableViewModel();
        }

        private void DataGrid_AutoGeneratingColumn(
            object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Fecha")
            {
                if (e.Column is DataGridTextColumn columna)
                {
                    columna.Binding = new Binding("Fecha")
                    {
                        StringFormat = "dd/MM/yyyy"
                    };
                }
            }
        }
    }
}