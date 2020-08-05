using System;
using System.Windows;
using System.Windows.Input;
using WpfAudioPlayer.Model;
using WpfAudioPlayer.Views;

namespace WpfAudioPlayer.ViewModels
{
    class AddSongViewModel : ViewModelBase
    {
        AddSong addSong;
        Service service = new Service();

        #region Constructors

        public AddSongViewModel(AddSong addSongOpen)
        {
            song = new tblSong();
            addSong = addSongOpen;
        }

        public AddSongViewModel(AddSong addSongOpen, tblUser user)
        {
            userToView = user;
            song = new tblSong();
            addSong = addSongOpen;
        }

        #endregion

        #region Properties 

        private tblSong song;

        public tblSong Song
        {
            get { return song; }
            set
            {
                song = value;
                OnPropertyChanged("Song");
            }
        }

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


        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }

                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                service.AddSong(Song, userToView.UserID);
                MessageBox.Show("Song added.");
                addSong.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (Song.SongName != null && Song.Author != null && Song.DurationInSeconds > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addSong.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
