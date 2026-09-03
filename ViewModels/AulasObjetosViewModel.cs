using System;
using System.Collections.Generic;
using System.Text;

using ReservasApp.Data;
using ReservasApp.Models;
using ReservasApp.MVVM;

namespace ReservasApp.ViewModels
{
    public class AulasObjetosViewModel : ViewModelBase
    {
        private List<Aula> aulas;

        private string nombreBusqueda;

        public List<Aula> Aulas
        {
            get => aulas;
            set
            {
                aulas = value;
                OnPropertyChanged();
            }
        }

        public string NombreBusqueda
        {
            get => nombreBusqueda;
            set
            {
                nombreBusqueda = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand BuscarCommand { get; }

        private readonly AulaData aulaData;

        public AulasObjetosViewModel()
        {
            aulaData = new AulaData();

            BuscarCommand =
                new RelayCommand(_ => Buscar());

            CargarAulas();
        }

        private void CargarAulas()
        {
            Aulas = aulaData.ListarAulasObjetos();
        }

        private void Buscar()
        {
            if (string.IsNullOrWhiteSpace(NombreBusqueda))
            {
                CargarAulas();
                return;
            }

            Aulas = aulaData.BuscarAulas(NombreBusqueda);
        }
    }
}