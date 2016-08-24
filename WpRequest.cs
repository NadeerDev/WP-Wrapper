using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WP_Wrapper
{
    class WpRequest : WebClient
    {
        private int _timeout;
        public WpRequest(int TimeOutmiliseconds)
        {
            _timeout = TimeOutmiliseconds;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            
            WebRequest request = base.GetWebRequest(address);
            request.Timeout = _timeout;
            return request;
        }
        
    }
}
