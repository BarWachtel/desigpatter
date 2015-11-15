using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookApp
{
    public partial class Form1 : Form
    {
        User m_LoggedInUser;
        List<string> m_SelectedAlbumIds;
        public Form1()
        {
            InitializeComponent();
            enableLoginButton();
            enableSaveAlbumButton(false);
            Console.WriteLine(FacebookService.s_CollectionLimit);
            FacebookService.s_CollectionLimit = 1000;
            m_SelectedAlbumIds = new List<string>();
        }

        private void enableSaveAlbumButton(bool i_Value = true)
        {
            saveAlbums_Button.Enabled = i_Value;
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            initAndLogin();
        }

        private void initAndLogin()
        {
            LoginResult result = FacebookService.Login("419917864886078", "user_about_me", "user_friends", "user_posts", "user_photos");
            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                m_LoggedInUser = result.LoggedInUser;
                displayInitialLoggedInUserScreen();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void displayInitialLoggedInUserScreen()
        {
            profilePicture_smallPictureBox.LoadAsync(m_LoggedInUser.PictureLargeURL);
            profilePicture_smallPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            usersName_label.Text = getUserFullName();

            foreach (Album album in m_LoggedInUser.Albums)
            {
                AlbumCover albumCover = new AlbumCover(album.Name, album.PictureAlbumURL, album.Id);
                albumPhotos_flowLayout.Controls.Add(albumCover);
                albumCover.LoadImage();
                albumCover.Click += new EventHandler(albumCoverSelected);               
            }

            enableLoginButton(false);
        }

        private void albumCoverSelected(object sender, EventArgs e) {
            AlbumCover albumCover = sender as AlbumCover;
           
            if (albumCover.IsSelected()) 
            {
                m_SelectedAlbumIds.Add(albumCover.Id); 
            } 
            else 
            {
                m_SelectedAlbumIds.Remove(albumCover.Id);
            }

            if (m_SelectedAlbumIds.Count > 0)
            {
                enableSaveAlbumButton();
            }
            else
            {
                enableSaveAlbumButton(false);
            }
        }

        private void displayInitialLoggedOutUserScreen()
        {
            profilePicture_smallPictureBox.Image = null;
            usersName_label.Text = "";
            enableLoginButton();
        }

        private string getUserFullName()
        {
            return string.Format("{0} {1}", m_LoggedInUser.FirstName, m_LoggedInUser.LastName);
        }

        private void enableLoginButton(bool enable = true)
        {
            login_button.Enabled = enable;
            login_button.Visible = enable;
            logout_button.Enabled = !enable;
            logout_button.Visible = !enable;
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            performUserLogout();
        }

        private void performUserLogout()
        {
            FacebookService.Logout(logoutCallback);
        }

        private void logoutCallback()
        {
            m_LoggedInUser = null;
            displayInitialLoggedOutUserScreen();
        }

        private void saveAlbums_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            string fileDirectoryPath = folderBrowser.SelectedPath;
            if (!string.IsNullOrEmpty(fileDirectoryPath))
            {
                Console.WriteLine("Folder " + fileDirectoryPath + " selected");
                foreach (string id in m_SelectedAlbumIds)
                {
                    Console.WriteLine("Album ID: " + id);
                }
            }
        }

        private void makeAlbumPrivateButton_click(object sender, EventArgs e)
        {
            string everyone = "everyone";
            string friendOfFriend = "friends-of-friends";

            foreach (string id in m_SelectedAlbumIds)
            {
                foreach (Album album in m_LoggedInUser.Albums)
                {
                    if (album.Id == id)
                    {
                        Console.WriteLine("Album ID: " + id + " has privacy settings:");
                        Console.WriteLine("Original privacy setting: " + album.PrivcaySettings);
                        Console.WriteLine("Changing privacy setting to " + friendOfFriend);
                        album.PrivcaySettings = friendOfFriend;
                        Console.WriteLine("New privacy setting: " + album.PrivcaySettings);
                    }
                }
            }
        }
    }
}
