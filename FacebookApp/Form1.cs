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

        public Form1()
        {
            InitializeComponent();
            enableLoginButton();
            Console.WriteLine(FacebookService.s_CollectionLimit);
            FacebookService.s_CollectionLimit = 1000;
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            initAndLogin();
        }

        private void initAndLogin()
        {
            LoginResult result = FacebookService.Login("419917864886078", "user_about_me", "user_friends", "user_posts");
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

            foreach(InboxThread inboxThread in m_LoggedInUser.InboxThreads) {
                Console.WriteLine("Message from " + inboxThread.From);
            }

            //foreach (Post post in m_LoggedInUser.Posts)
            //{
            //    string content = post.Message != null ? post.Message : post.Description;
            //    if (!string.IsNullOrEmpty(content))
            //    {
            //        posts_listBox.Items.Add(content);
            //    }
            //}
            enableLoginButton(false);
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
    }
}
