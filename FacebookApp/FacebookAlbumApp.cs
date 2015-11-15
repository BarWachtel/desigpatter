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
    public partial class FacebookAlbumApp : Form
    {
        User m_LoggedInUser;
        AlbumCover m_SelectedAlbumCover;
        public FacebookAlbumApp()
        {
            InitializeComponent();
            enableLoginButton();
            enableSaveAlbumButton(false);
            resetAlbumInfoValues();
            FacebookService.s_CollectionLimit = 1000;
        }

        private void enableSaveAlbumButton(bool i_Value = true)
        {
            button_saveAlbums.Enabled = i_Value;
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
            pictureBox_profilePicture.LoadAsync(m_LoggedInUser.PictureLargeURL);
            pictureBox_profilePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            label_userName.Text = getUserFullName();

            foreach (Album album in m_LoggedInUser.Albums)
            {
                AlbumCover albumCover = new AlbumCover(album.Name, album.PictureAlbumURL, album.Id);
                flowLayout_albumPhotos.Controls.Add(albumCover);
                albumCover.LoadImage();
                albumCover.Click += new EventHandler(albumCoverSelected);               
            }

            enableLoginButton(false);
        }

        private void albumCoverSelected(object sender, EventArgs e) {
            AlbumCover albumCover = sender as AlbumCover;

            if (m_SelectedAlbumCover != null) 
            {
                if (m_SelectedAlbumCover != albumCover)
                {
                    m_SelectedAlbumCover.Deselect();
                }
            }
            m_SelectedAlbumCover = albumCover;
            if (m_SelectedAlbumCover.IsSelected())
            {
                albumIsSelected(true);
            }
            else
            {
                albumIsSelected(false);
            }
        }

        private void albumIsSelected(bool i_AlbumIsSelected)
        {
            if (i_AlbumIsSelected)
            {
                setAlbumInfoValues();
                enableSaveAlbumButton();
            }
            else
            {
                resetAlbumInfoValues();
                enableSaveAlbumButton(false);
            }
        }

        private void setAlbumInfoValues()
        {
            Album album = getAlbumById(m_SelectedAlbumCover.Id);
            if (album != null)
            {
                label_albumName.Text = album.Name;
                label_description.Text = album.Description;
                label_lastUpdated.Text = album.UpdateTime.ToString();
                label_numPhotos.Text = album.Count.ToString();
                label_privacySettings.Text = album.PrivcaySettings;
                pictureBox_albumCoverPhoto.LoadAsync(album.PictureAlbumURL);
                pictureBox_albumCoverPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void displayInitialLoggedOutUserScreen()
        {
            pictureBox_profilePicture.Image = null;
            label_userName.Text = "";
            enableLoginButton();
        }

        private string getUserFullName()
        {
            return string.Format("{0} {1}", m_LoggedInUser.FirstName, m_LoggedInUser.LastName);
        }

        private void enableLoginButton(bool enable = true)
        {
            button_login.Enabled = enable;
            button_login.Visible = enable;
            button_logout.Enabled = !enable;
            button_logout.Visible = !enable;
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
            Album selectedAlbum = null;
            if (!string.IsNullOrEmpty(fileDirectoryPath))
            {
                selectedAlbum = getAlbumById(m_SelectedAlbumCover.Id);

                if (selectedAlbum != null)
                {
                    int imgName = 0;
                    foreach (Photo photo in selectedAlbum.Photos)
	                {
                        System.Drawing.Imaging.ImageFormat imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        string photoName =
                            String.Format(@"{0}{1}{2}_{3}.{4}", fileDirectoryPath, System.IO.Path.DirectorySeparatorChar, selectedAlbum.Name, imgName.ToString(), imageFormat.ToString());
                        Console.WriteLine("Saving image " + photoName);
                        photo.ImageNormal.Save(photoName, imageFormat);
                        imgName++;
	                } 
                }
            }
        }

        private void resetAlbumInfoValues()
        {
            label_albumName.Text = "";
            label_description.Text = "";
            label_lastUpdated.Text = "";
            label_privacySettings.Text = "";
            label_numPhotos.Text = "";
            pictureBox_albumCoverPhoto.Image = null;
        }

        private Album getAlbumById(string i_Id)
        {
            Album theAlbum = null;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                if (album.Id == i_Id)
                {
                    theAlbum = album;
                    break;
                }
            }

            return theAlbum;
        }
    }
}
