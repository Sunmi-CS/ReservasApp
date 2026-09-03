using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.Windows;
using ReservasApp.Data;
using ReservasApp.Models;
using ReservasApp.MVVM;

namespace ReservasApp.ViewModels
{
    public class NuevaReservaViewModel : ViewModelBase
    {
        private readonly ReservaData reservaData;
        private readonly AulaData aulaData;
        private readonly Usuario usuario;

        private Aula aulaSeleccionada;
        private DateTime fecha = DateTime.Today;
        private TimeSpan hora = new TimeSpan(8, 0, 0);
        private string motivo;

        public ObservableCollection<Aula> Aulas { get; }

        public Aula AulaSeleccionada
        {
            get => aulaSeleccionada;
            set
            {
                aulaSeleccionada = value;
                OnPropertyChanged();
            }
        }

        public DateTime Fecha
        {
            get => fecha;
            set
            {
                fecha = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Hora
        {
            get => hora;
            set
            {
                hora = value;
                OnPropertyChanged();
            }
        }

        public string Motivo
        {
            get => motivo;
            set
            {
                motivo = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand GuardarCommand { get; }

        public NuevaReservaViewModel(Usuario usuario)
        {
            this.usuario = usuario;

            reservaData = new ReservaData();
            aulaData = new AulaData();

            Aulas = new ObservableCollection<Aula>(
                aulaData.ListarAulasObjetos());

            GuardarCommand =
                new RelayCommand(_ => Guardar());
        }

        private void Guardar()
        {
            if (AulaSeleccionada == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un aula.");

                return;
            }

            if (string.IsNullOrWhiteSpace(Motivo))
            {
                MessageBox.Show(
                    "Debe ingresar el motivo.");

                return;
            }

            bool existe = reservaData.ExisteReserva(
                AulaSeleccionada.AulaId,
                Fecha,
                Hora);

            if (existe)
            {
                MessageBox.Show(
                    "Ya existe una reserva para esa aula, fecha y hora.",
                    "Reserva duplicada",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            bool insertado = reservaData.InsertarReserva(
                AulaSeleccionada.AulaId,
                usuario.UsuarioId,
                Fecha,
                Hora,
                Motivo);

            if (insertado)
            {
                MessageBox.Show(
                    "Reserva registrada correctamente.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                Motivo = "";
            }
        }
    }
}
