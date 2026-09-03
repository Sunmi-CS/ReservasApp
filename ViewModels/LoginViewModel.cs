using ReservasApp.Data;
using ReservasApp.MVVM;
using ReservasApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ReservasApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private string password;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; }

        private readonly UsuarioRepository usuarioRepository;

        public LoginViewModel()
        {
            usuarioRepository = new UsuarioRepository();

            LoginCommand = new RelayCommand(
                EjecutarLogin);
        }

        private void EjecutarLogin(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;

            string password = passwordBox?.Password;

            var usuario = usuarioRepository.ValidarLogin(
                Username,
                password);

            if (usuario != null)
            {
                MainWindow ventana = new MainWindow(usuario);
                ventana.Show();

                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)
                    ?.Close();
            }
            else
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}