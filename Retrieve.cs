using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_Wrapper
{
    /// <summary>
    /// This object is used for any object which only has a status and a message
    /// </summary>
    public class Retrieve
    {
        /// <summary>
        /// Returned status in JSON object. ok means everything went fine
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Message received from request
        /// </summary>
        public string msg { get; set; }
    }
}
