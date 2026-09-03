using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using ReservasApp.Data;
using ReservasApp.MVVM;

namespace ReservasApp.ViewModels
{
    public class ReservasDataTableViewModel : ViewModelBase
    {
        private DataTable reservas;

        public DataTable Reservas
        {
            get => reservas;
            set
            {
                reservas = value;
                OnPropertyChanged();
            }
        }

        public ReservasDataTableViewModel()
        {
            CargarReservas();
        }

        private void CargarReservas()
        {
            ReservaData data = new ReservaData();

            Reservas = data.ListarReservasDataTable();
        }
    }
}