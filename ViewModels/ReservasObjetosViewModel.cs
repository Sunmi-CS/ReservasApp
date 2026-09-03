using System;
using System.Collections.Generic;
using System.Text;

using ReservasApp.Data;
using ReservasApp.Models;
using ReservasApp.MVVM;

namespace ReservasApp.ViewModels
{
    public class ReservasObjetosViewModel : ViewModelBase
    {
        private List<Reserva> reservas;

        private DateTime? fechaBusqueda;

        public List<Reserva> Reservas
        {
            get => reservas;
            set
            {
                reservas = value;
                OnPropertyChanged();
            }
        }

        public DateTime? FechaBusqueda
        {
            get => fechaBusqueda;
            set
            {
                fechaBusqueda = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand BuscarCommand { get; }

        private readonly ReservaData reservaData;

        public ReservasObjetosViewModel()
        {
            reservaData = new ReservaData();

            BuscarCommand =
                new RelayCommand(_ => Buscar());

            CargarReservas();
        }

        private void CargarReservas()
        {
            Reservas = reservaData.ListarReservasObjetos();
        }

        private void Buscar()
        {
            if (FechaBusqueda == null)
            {
                CargarReservas();
                return;
            }

            Reservas =
                reservaData.BuscarReservasPorFecha(
                    FechaBusqueda.Value);
        }
    }
}
