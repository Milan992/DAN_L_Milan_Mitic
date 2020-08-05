using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAudioPlayer.Model;
using WpfAudioPlayer.Views;

namespace WpfAudioPlayer.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        User user;
        Service service = new Service();

        #region Constructors

        public UserViewModel(User userOpen)
        {
            user = userOpen;
        }

        public UserViewModel(User userOpen, string userName)
        {
            user = userOpen;
            userToView = service.GetUser(userName);
            songsList = service.GetUserSongs(userName);
        }

        #endregion

        #region Properties

        private tblUser userToView;

        public tblUser UserToView
        {
            get { return userToView; }
            set
            {
                userToView = value;
                OnPropertyChanged("UserToView");
            }
        }

        private List<tblSong> songsList;

        public List<tblSong> SongsList
        {
            get { return songsList; }
            set
            {
                songsList = value;
                OnPropertyChanged("SongsList");
            }
        }

        #endregion

        #region Commands 

        private ICommand addsong;

        public ICommand AddSong
        {
            get
            {
                if (addsong == null)
                {
                    addsong = new RelayCommand(param => AddSongExecute(), param => CanAddSongExecute());
                }

                return addsong;
            }
        }

        private void AddSongExecute()
        {
            try
            {
                AddSong addsong = new AddSong(UserToView);
                addsong.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddSongExecute()
        {
            return true;
        }

        #endregion
    }
}
