using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAudioPlayer.Views;

namespace WpfAudioPlayer.ViewModels
{
    class UserViewModel
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
            songsList = service.GetUserSongs(userName);
        }

        #endregion

        #region Properties



        #endregion
    }
}
