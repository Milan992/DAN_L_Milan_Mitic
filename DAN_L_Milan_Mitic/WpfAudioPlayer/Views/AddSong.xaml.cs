using System.Windows;
using WpfAudioPlayer.Model;
using WpfAudioPlayer.ViewModels;

namespace WpfAudioPlayer.Views
{
    /// <summary>
    /// Interaction logic for AddSong.xaml
    /// </summary>
    public partial class AddSong : Window
    {
        public AddSong()
        {
            InitializeComponent();
            this.DataContext = new AddSongViewModel(this);
        }

        public AddSong(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new AddSongViewModel(this, user);
        }
    }
}
