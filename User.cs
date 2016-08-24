using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_Wrapper
{
    /// <summary>
    /// This is a class that hold user information
    /// </summary>
    /// <remarks>
    /// This is a class where it holds all user information. Three types of contrucors can be used. A full constructor where all variables are used. A less descrpitve contructor where only id, username, displayname and avatar url are used.
    /// And a defualt value constructor where all values are set to null
    /// </remarks>
    public class User
    {

        
        /// <summary>
        /// Returns the id of the user in the site
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// A string that contains the username (login username) of this user
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// A string that contains a nicer looking name for the user.
        /// </summary>
        public string nicename { get; set; }
        /// <summary>
        /// A string that contains the email of the user
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// A string that contains the registration date of the user
        /// </summary>
        public string registered { get; set; }
        /// <summary>
        /// A string that contains the display name of the user
        /// </summary>
        public string displayname { get; set; }
        /// <summary>
        /// A string that contains the first name of the user
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// A string that contains the last name of the user
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// A string that contains the display name of the user
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// A string that contains a custom description that the user sits
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// URL to the user avatar
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// A full descriptive user. With all parameters required
        /// </summary>
        /// <param name="ID">ID of the user</param>
        /// <param name="Username">User loging username</param>
        /// <param name="Nicename">Nicename of the user</param>
        /// <param name="URL"></param>
        /// <param name="Registered">Registration date</param>
        /// <param name="DisplayName">User display name</param>
        /// <param name="FirstName">User first name</param>
        /// <param name="LastName">User last name</param>
        /// <param name="NickName">User nick name</param>
        /// <param name="Description">User custom description</param>
        /// <param name="AvatarUrl">User display photo url</param>
        public User(int ID,
            string Username,
            string Nicename,
            string URL,
            string Registered,
            string DisplayName,
            string FirstName,
            string LastName,
            string NickName,
            string Description,
            string AvatarUrl)
        {
            this.username = Username;
            this.nicename = Nicename;
            this.url = URL;
            this.registered = Registered;
            this.displayname = DisplayName;
            this.firstname = FirstName;
            this.lastname = LastName;
            this.nickname = Nicename;
            this.description = Description;
            this.avatar = AvatarUrl;
        }
        /// <summary>
        /// Defualt contructor
        /// </summary>
        public User() { }
        /// <summary>
        /// A less descriptive user
        /// </summary>
        /// <param name="ID">ID of the user</param>
        /// <param name="Username">User loging username</param>
        /// <param name="DisplayName">User display name</param>
        /// <param name="AvatarUrl">User display photo url</param>
        public User(int ID,
            string Username,
            string DisplayName,
            string AvatarUrl)
        {
            this.username = Username;
            this.displayname = DisplayName;
            this.avatar = AvatarUrl;
        }
        /// <summary>
        /// Fix a known issue with gravatar url
        /// </summary>
        /// <param name="size">Wanted size</param>
        public void FixGravatar(int size = 512)
        {
            if (!this.avatar.Contains("gravatar")) return;
            this.avatar = this.getGravatar(size);
        }
        /// <summary>
        /// Get a fixed url of gravatar.
        /// </summary>
        /// <param name="size">Wanted size</param>
        /// <returns></returns>
        public string getGravatar(int size = 512)
        {
            if (!this.avatar.Contains("gravatar")) return null;
            string[] url = this.avatar.Split('?');
            if (url[0].Contains("http:"))
                return url[0] + "?s=" + size + "&r=g&d=monsterid";
            else
                return "http:" + url[0] + "?s=" + size + "&r=g&d=monsterid";
        }
    }
}
