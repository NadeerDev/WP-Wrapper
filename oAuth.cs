using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_Wrapper
{
    public class oAuth
    {
        /// <summary>
        /// Return true of no error occurred.
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Error message if any exists
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// String of the authorization cookie.
        /// </summary>
        public string cookie { get; set; }
        /// <summary>
        /// String of the authorization cookie name.
        /// </summary>
        public string cookie_name { get; set; }
        /// <summary>
        /// The user info
        /// </summary>
        public User user { get; set; }
    }
}
