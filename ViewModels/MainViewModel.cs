using System;
using System.Collections.Generic;
using System.Text;

using System.Windows;
using ReservasApp.Models;
using ReservasApp.MVVM;
using ReservasApp.Views;

namespace ReservasApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Usuario usuario;

        public string Bienvenida =>
            $"Bienvenido, {usuario.NombreCompleto}";

        public RelayCommand AulasDataTableCommand { get; }
        public RelayCommand AulasObjetosCommand { get; }
        public RelayCommand ReservasDataTableCommand { get; }
        public RelayCommand ReservasObjetosCommand { get; }
        public RelayCommand NuevaReservaCommand { get; }

        public MainViewModel(Usuario usuario)
        {
            this.usuario = usuario;

            AulasDataTableCommand =
                new RelayCommand(_ => AbrirAulasDataTable());

            AulasObjetosCommand =
                new RelayCommand(_ => AbrirAulasObjetos());

            ReservasDataTableCommand =
                new RelayCommand(_ => AbrirReservasDataTable());

            ReservasObjetosCommand =
                new RelayCommand(_ => AbrirReservasObjetos());

            NuevaReservaCommand =
                new RelayCommand(_ => AbrirNuevaReserva());
        }

        private void AbrirAulasDataTable()
        {
            new AulasDataTableView().ShowDialog();
        }

        private void AbrirAulasObjetos()
        {
            new AulasObjetosView().ShowDialog();
        }

        private void AbrirReservasDataTable()
        {
            new ReservasDataTableView().ShowDialog();
        }

        private void AbrirReservasObjetos()
        {
            new ReservasObjetosView().ShowDialog();
        }

        private void AbrirNuevaReserva()
        {
            new NuevaReservaView(usuario).ShowDialog();
        }
    }
}