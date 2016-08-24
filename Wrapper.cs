using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
/// <summary>
/// This is an API Wrapper for Wordpress JSON API
/// </summary>
/// <remarks>
/// You can use this API to 
/// 1- Login with a user.
/// 2- Get all user info.
/// 3- Register user.
/// 
/// Please make sure all required plugins are installed on wordpress. <see cref="https://github.com/EntropyDevelopments/WP-Wrapper"/>
/// </remarks>
/// 
namespace WP_Wrapper
{
    public class Wrapper
    {
        /// <summary>
        /// Authorization class. This will contain the status and the cookies
        /// </summary>
        private oAuth oauth;
        /// <summary>
        /// The user object if signup
        /// <seealso cref="User"/>
        /// </summary>
        private User user_data;
        /// <summary>
        /// Determin whether or not should the Wrapper call insecure.
        /// </summary>
        private bool SSL = true;
        /// <summary>
        /// The main URL of the api request page
        /// </summary>
        private string base_Url;
        /// <summary>
        /// The timeout for the web request
        /// </summary>
        private int timeout = 1*60*1000;
        /// <summary>
        /// A privetly used nounce for some authentications
        /// <seealso cref="Nonce"/>
        /// </summary>
        private Nonce nounce;
        /// <summary>
        /// If this is set to true the wrapper will print all JSON objects and errors
        /// </summary>
        private bool allowPrint = false;
        /// <summary>
        /// Start a wrapper
        /// </summary>
        /// <param name="url">The URL to API request</param>
        /// <param name="useSSL">Should the program use SSL? Set to false if no SSL certifcate exits on the website</param>
        public Wrapper(string url, bool useSSL = true)
        {
            this.SSL = useSSL;
            if (url[url.Length - 1] != '/')
                url += '/';
            base_Url = url;
        }
        /// <summary>
        /// Contructor with URL and timeout set
        /// </summary>
        /// <param name="url">The URL to API request</param>
        /// <param name="timeout">A privetly used nounce for some authentications</param>
        /// <param name="useSSL">Should the program use SSL? Set to false if no SSL certifcate exits on the website</param>
        public Wrapper(string url,int timeout, bool useSSL = true)
        {
            this.SSL = useSSL;
            this.timeout = timeout;
            if (url[url.Length - 1] != '/')
                url += '/';
            base_Url = url;
        }
        /// <summary>
        /// Construct and login
        /// </summary>
        /// <param name="url">The URL to API request</param>
        /// <param name="UserName">Username for login</param>
        /// <param name="Password">User password</param>
        /// <param name="useSSL">Should the program use SSL? Set to false if no SSL certifcate exits on the website</param>
        public Wrapper(string url, string UserName, string Password, bool useSSL = true)
        {
            this.SSL = useSSL;
            if (url[url.Length - 1] != '/')
                url += '/';
            base_Url = url;
            LoginAsUser(UserName, Password);
        }
        /// <summary>
        /// Login a user using their username and password.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>Return false if any errors shows up</returns>
        public bool LoginAsUser(string UserName, string Password)
        {
            try
            {
                string request;
                if (SSL)
                    request = base_Url + "user/generate_auth_cookie?username=" + UserName + "&password=" + Password;
                else
                    request = base_Url + "user/generate_auth_cookie?insecure=cool&username=" + UserName + "&password=" + Password;
                string jObj = GetResponse(request);
                oAuth auth = JsonConvert.DeserializeObject<oAuth>(jObj);
                if (auth.status.ToLower() != "ok" || auth.status == null || auth.status == "") return false;
                this.user_data = auth.user;
                this.user_data.avatar = "http:" + this.user_data.avatar;
                this.user_data.FixGravatar();
                this.oauth = auth;
                return true;
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Retrive a user password using their username
        /// </summary>
        /// <param name="username">Login username</param>
        /// <returns><see cref="Retrieve"/></returns>
        public Retrieve RetrievePassword(string username)
        {
            try
            {
                string request;
                if (SSL)
                    request = base_Url + "user/retrieve_password?user_login=" + username;
                else
                    request = base_Url + "user/retrieve_password?insecure=cool&user_login=" + username;
                string jObj = GetResponse(request);
                return JsonConvert.DeserializeObject<Retrieve>(jObj);
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Authrize a cookie. First will check if it is valid
        /// </summary>
        /// <param name="cookie">Cookie ID</param>
        /// <returns>Will return true if everything goes fine</returns>
        public bool AuthCookie(string cookie)
        {
            try
            {
                if (!ValidateCookie(cookie)) return false;
                string request;
                if (SSL)
                    request = base_Url + "user/get_currentuserinfo/?cookie=" + cookie;
                else
                    request = base_Url + "user/get_currentuserinfo/?insecure=cool&cookie=" + cookie;
                string jObj = GetResponse(request);
                oAuth auth = JsonConvert.DeserializeObject<oAuth>(jObj);
                if (auth.status.ToLower() != "ok" || auth.status == null || auth.status == "" || string.IsNullOrEmpty(auth.status)) return false;
                this.user_data = auth.user;
                this.user_data.avatar = "http:" + this.user_data.avatar;
                this.user_data.FixGravatar();
                this.oauth = auth;
                return true;
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Validate a cooike.
        /// </summary>
        /// <param name="cookie">Cookie ID</param>
        /// <returns>Return true if cookie is valid</returns>
        public bool ValidateCookie(string cookie)
        {
            try
            {
                string request;
                if (SSL)
                    request = base_Url + "user/validate_auth_cookie/?cookie=" + cookie;
                else
                    request = base_Url + "user/validate_auth_cookie/?insecure=cool&cookie=" + cookie;
                string jObj = GetResponse(request);
                Cookie c = JsonConvert.DeserializeObject<Cookie>(jObj);
                if (c == null)
                    return false;
                return c.valid;
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Register a user and authorize
        /// </summary>
        /// <param name="username">A username requested by client</param>
        /// <param name="email">User email</param>
        /// <param name="displayname">A display name to be set on wordpress</param>
        /// <returns>Return false if any errors shows up</returns>
        public bool RegisterUser(string username, string email, string displayname)
        {
            try
            {
                this.nounce = setRegistration();
                if (isNounceSet())
                {
                    string request;
                    if (SSL)
                        request = base_Url + "user/register?username=" + username + "&email=" + email + "&nonce=" + getNounce() + "&display_name=" + displayname + "&notify=both";
                    else
                        request = base_Url + "user/register?insecure=cool&username=" + username + "&email=" + email + "&nonce=" + getNounce() + "&display_name=" + displayname + "&notify=both";
                    string jObj = GetResponse(request);
                    Register regObj = JsonConvert.DeserializeObject<Register>(jObj);
                    if (regObj.cookie == null || regObj.cookie == "" || regObj.status.ToLower() != "ok")
                        return false;
                    if (SSL)
                        request = base_Url + "user/get_currentuserinfo&cookie=" + regObj.cookie;
                    else
                        request = base_Url + "user/get_currentuserinfo?insecure=cool&cookie=" + regObj.cookie;
                    jObj = GetResponse(request);
                    oAuth auth = JsonConvert.DeserializeObject<oAuth>(jObj);
                    if (auth.status.ToLower() != "ok" || auth.status == null || auth.status == "") return false;
                    this.user_data = auth.user;
                    this.user_data.avatar = "http:" + this.user_data.avatar;
                    this.user_data.FixGravatar();
                    this.oauth = auth;
                    return true;
                }
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
            return false;
        }
        /// <summary>
        /// Check if there is a nounce set
        /// </summary>
        /// <returns>True if all is well</returns>
        private bool isNounceSet()
        {
            if (nounce == null || nounce.status == "" || nounce.status == null)
                return false;
            else if (nounce.status.ToLower() == "error")
                return false;
            else
                return true;
        }
        /// <summary>
        /// Get the nounce code
        /// </summary>
        /// <returns><see cref="Nonce"/></returns>
        private string getNounce()
        {
            if (isNounceSet())
                return nounce.nonce;
            else
                return null;
        }
        /// <summary>
        /// Set a nounce for a registration.
        /// </summary>
        /// <returns>A nounce with a register method <see cref="Nonce"/></returns>
        private Nonce setRegistration()
        {
            try
            {
                string request;
                if (SSL)
                    request = base_Url + "get_nonce?controller=user&method=register";
                else
                    request = base_Url + "get_nonce?insecure=cool&controller=user&method=register";
                string jObj = GetResponse(request);
                Nonce serializer = JsonConvert.DeserializeObject<Nonce>(jObj);
                return serializer;
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Get JSON object from URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetResponse(string url)
        {
            try
            {
                using (WpRequest client = new WpRequest(timeout))
                {
                    
                    string s = client.DownloadString(url);
                    if (allowPrint)
                        Console.Write("\nJSON Respone{\n" + s + "\n}\n");
                    return s;
                }
            }
            catch (Exception e)
            {
                if (allowPrint)
                    Console.Write(e.Message);
                throw;
            }
        }
        /// <summary>
        /// If this is set to true the wrapper will print all JSON objects and errors. By default this is off
        /// </summary>
        /// <remarks>This will use Console.Write() to print any JSON objects retirved or errors</remarks>
        /// <param name="allow">If set to true all will print</param>
        public void AllowConsolePrinting(bool allow)
        {
            this.allowPrint = allow;
        }
        /// <summary>
        /// The main URL of the api request page
        /// </summary>
        public string URL
        {
            get { return this.base_Url; }
        }
        /// <summary>
        /// /// The user object if signup
        /// <seealso cref="User"/>
        /// </summary>
        public User user
        {
            get { return this.user_data; }
        }
        /// <summary>
        /// Authrization info. <see cref="oAuth"/>
        /// </summary>
        /// <remarks>This will contain info about the authrization such as the cookie ID and the authrized user</remarks>
        public oAuth Auth
        {
            get { return this.oauth; }
        }
        /// <summary>
        /// The timeout for the web request
        /// </summary>
        public int Timeout {
            get { return this.timeout; }
            set { this.timeout = value; }
        }
    }
}
