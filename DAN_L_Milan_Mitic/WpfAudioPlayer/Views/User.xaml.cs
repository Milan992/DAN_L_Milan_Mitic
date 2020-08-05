using System.Windows;
using WpfAudioPlayer.ViewModels;

namespace WpfAudioPlayer.Views
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this);
        }

        public User(string userName)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, userName);
        }
    }
}
