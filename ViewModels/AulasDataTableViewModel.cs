using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using ReservasApp.Data;
using ReservasApp.MVVM;

namespace ReservasApp.ViewModels
{
    public class AulasDataTableViewModel : ViewModelBase
    {
        private DataTable aulas;

        public DataTable Aulas
        {
            get => aulas;
            set
            {
                aulas = value;
                OnPropertyChanged();
            }
        }

        public AulasDataTableViewModel()
        {
            CargarAulas();
        }

        private void CargarAulas()
        {
            AulaData data = new AulaData();

            Aulas = data.ListarAulasDataTable();
        }
    }
}