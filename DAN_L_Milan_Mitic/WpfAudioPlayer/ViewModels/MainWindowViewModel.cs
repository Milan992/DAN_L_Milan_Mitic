using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WpfAudioPlayer.Model;
using WpfAudioPlayer.Views;

namespace WpfAudioPlayer.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {

        Service service = new Service();
        MainWindow main;

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #endregion

        #region Properties

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => LogInExecute(), param => CanLogInExecute());
                }

                return logIn;
            }
        }

        private void LogInExecute()
        {
            try
            {
                if (service.IsUser(UserName, Password))
                {
                    User user = new User(UserName);
                    user.ShowDialog();
                }
                else
                {
                    tblUser newUser = service.AddUser(UserName, Password);
                    User user = new User(UserName);
                    user.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Username already exists or wrong format.");
            }
        }

        private bool CanLogInExecute()
        {
            return true;
        }

        #endregion
    }
}
