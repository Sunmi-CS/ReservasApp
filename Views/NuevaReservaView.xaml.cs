using System;
using System.Windows;
using ReservasApp.Models;
using ReservasApp.ViewModels;

namespace ReservasApp.Views
{
    public partial class NuevaReservaView : Window
    {
        public NuevaReservaView(Usuario usuario)
        {
            InitializeComponent();

            DataContext =
                new NuevaReservaViewModel(usuario);
        }

        private void SubirHora_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is NuevaReservaViewModel vm)
            {
                TimeSpan hora = vm.Hora;

                hora = hora.Add(TimeSpan.FromMinutes(30));

                if (hora >= TimeSpan.FromDays(1))
                {
                    hora = TimeSpan.Zero;
                }

                vm.Hora = hora;
            }
        }

        private void BajarHora_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is NuevaReservaViewModel vm)
            {
                TimeSpan hora = vm.Hora;

                hora = hora.Subtract(TimeSpan.FromMinutes(30));

                if (hora < TimeSpan.Zero)
                {
                    hora = new TimeSpan(23, 30, 0);
                }

                vm.Hora = hora;
            }
        }
    }
}