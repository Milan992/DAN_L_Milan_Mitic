using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfAudioPlayer.Model;
using WpfAudioPlayer.Views;

namespace WpfAudioPlayer
{
    class Service
    {
        AutoResetEvent are = new AutoResetEvent(false);
        /// <summary>
        /// Adds an user to the data base
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        internal tblUser AddUser(string userName, string password)
        {
            using (AudioPlayerEntities context = new AudioPlayerEntities())
            {
                tblUser user = new tblUser();
                user.UserName = userName;
                user.Pass = password;
                context.tblUsers.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        /// <summary>
        /// Returns am user with UserName as parameter.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal tblUser GetUser(string userName)
        {
            tblUser user = new tblUser();
            using (AudioPlayerEntities context = new AudioPlayerEntities())
            {
                user = (from u in context.tblUsers where u.UserName == userName select u).First();
            }
            return user;
        }

        /// <summary>
        /// Adds a song to the database.
        /// </summary>
        /// <param name="song"></param>
        /// <param name="user"></param>
        internal void AddSong(tblSong song, int userID)
        {
            using (AudioPlayerEntities context = new AudioPlayerEntities())
            {
                tblSong newSong = new tblSong();
                newSong.SongName = song.SongName;
                newSong.Author = song.Author;
                newSong.DurationInSeconds = song.DurationInSeconds;
                newSong.UserID = userID;
                context.tblSongs.Add(newSong);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Checks if user with userName and password exists in the database.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsUser(string userName, string password)
        {
            try
            {
                using (AudioPlayerEntities context = new AudioPlayerEntities())
                {
                    tblUser user = (from u in context.tblUsers where u.UserName == userName && u.Pass == password select u).First();
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the song from tblSong in the database
        /// </summary>
        /// <param name="song"></param>
        internal void DeleteSong(tblSong song)
        {
            using (AudioPlayerEntities context = new AudioPlayerEntities())
            {
                tblSong songToDelete = (from s in context.tblSongs where s.SongID == song.SongID select s).First();
                context.tblSongs.Remove(songToDelete);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns list of songs with user's ID.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal List<tblSong> GetUserSongs(string userName)
        {
            List<tblSong> list = new List<tblSong>();
            using (AudioPlayerEntities context = new AudioPlayerEntities())
            {
                tblUser user = (from u in context.tblUsers where u.UserName == userName select u).First();
                list = (from s in context.tblSongs where s.UserID == user.UserID select s).ToList();
            }
            return list;
        }

        internal void PlaySong(tblSong song, tblUser user)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\MyMusic"+ user.UserName +".txt", true))
                {
                    sw.WriteLine("playing -" + song.Author + " -" + song.SongName + " -" + DateTime.Now.ToString());
                }
                Thread.Sleep(song.DurationInSeconds * 1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            are.Set();
        }

        internal void FinishSong(tblSong song, tblUser user)
        {
            are.WaitOne();
            try
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\MyMusic" + user.UserName + ".txt", true))
                {
                    sw.WriteLine("finished playing -" + song.Author + " -" + song.SongName + " -" + DateTime.Now.ToString());
                }
                MessageBox.Show(song.SongName + " finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
