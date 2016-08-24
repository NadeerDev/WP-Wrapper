using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WP_Wrapper
{
    class Nonce
    {
        /// <summary>
        /// ok if no error occurred. 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Controller for the nonce.
        /// </summary>
        /// <example>Respond, Widgets, User</example>
        public string controller { get; set; }
        /// <summary>
        /// Method used for the request
        /// </summary>
        /// <example>Register</example>
        public string method { get; set; }
        /// <summary>
        /// Nonce code returned by the request
        /// </summary>
        public string nonce { get; set; }


        
        
    }
}
